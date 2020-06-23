using Common.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccess
{
    public class OperationDataAccess
    {
        private ApiDbContext db;

        public OperationDataAccess()
        {
            db = new ApiDbContext();
        }

        public IEnumerable<Operation> GetAll()
        {
            return db.Operations.ToList();
        }
        
        public IEnumerable<GroupOperation> GetByGroup(GroupByEnum groupBy)
        {
            var groupName = Enum.GetName(typeof(GroupByEnum), groupBy);
            return db.Database.SqlQuery<GroupOperation>("GetOperationGroup @GroupName", new SqlParameter("GroupName", groupName));
        }
    }
}
