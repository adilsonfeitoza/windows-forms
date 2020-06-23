using Common.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.IO;

namespace DataAccess
{
    public class ApiDbContext : DbContext
    {

        public ApiDbContext() : base("ApiDbContext")
        {

            Database.SetInitializer(new ApiDbContextInitializer());
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Operation> Operations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Operation>().HasKey(x => x.Id);

            base.OnModelCreating(modelBuilder);
        }

        public class ApiDbContextInitializer : DropCreateDatabaseIfModelChanges<ApiDbContext>
        {
            protected override void Seed(ApiDbContext context)
            {
                string[] ativos = { "AZUL4", "GOLL4", "ELET3", "USIM5", "ELET6", "GOAU4", "EMBR3", "COGN3", "PETR4", "PETR3", "GGBR4", "BRML3", "BBAS3", "CMIG4", "CVCB3", "CSNA3", "BRAP4", "SMLS3", "ENBR3", "MRVE3" };

                var orderIds = 0;
                var fakeOperacoes = new Bogus.Faker<Operation>()
                                        .StrictMode(true)
                                        .RuleFor(x => x.Id, f => orderIds++)
                                        .RuleFor(x => x.DateTime, f => f.Date.Between(DateTime.Now.AddDays(-1), DateTime.Now))
                                        .RuleFor(x => x.OperationType, f => f.PickRandom("C", "V"))
                                        .RuleFor(x => x.Active, f => f.PickRandom(ativos))
                                        .RuleFor(x => x.Quantity, f => f.Random.Int(10, 2000))
                                        .RuleFor(x => x.Price, f => f.Random.Decimal(10, 100))
                                        .RuleFor(x => x.AccountNumber, f => f.Random.Int(1111, 999999));

                context.Operations.AddRange(fakeOperacoes.Generate(20000).ToList());
                context.SaveChanges();

                foreach (var file in Directory.GetFiles(Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory?.Replace("WebApi", "DataAccess"), "StoredProcedures")), "*.sql"))
                {
                    context.Database.ExecuteSqlCommand(File.ReadAllText(file), new object[0]);
                }

                base.Seed(context);
            }
        }
    }
}
