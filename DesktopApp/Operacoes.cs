using Common.Models;
using Common.Support;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopApp
{
    public partial class Operacoes : Form
    {
        private readonly string WEBAPI_URL = ConfigurationManager.AppSettings["WebApiUrl"].ToString();
        private GroupByEnum GroupBy = GroupByEnum.Active;

        public Operacoes()
        {
            InitializeComponent();
        }

        #region Eventos

        private void Operacoes_Load(object sender, EventArgs e)
        {
            GetAllOperations();
        }

        private void cbGroupBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedGroup = (string)cbGroupBy.SelectedItem;

            btnExportExcel.Enabled = !string.IsNullOrEmpty(selectedGroup);
            btnExportCSV.Enabled = !string.IsNullOrEmpty(selectedGroup);

            if (string.IsNullOrEmpty(selectedGroup))
                GetAllOperations();
            else
                GetOperationByGroup(selectedGroup);
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            GetFile(btnExportExcel);
        }

        private void btnExportCSV_Click(object sender, EventArgs e)
        {
            GetFile(btnExportCSV);
        }

        #endregion

        private void SetFormatting()
        {
            dgvOperacoes.Columns["Price"].DefaultCellStyle.Format = "c";
            dgvOperacoes.Columns["Quantity"].DefaultCellStyle.Format = "g";

            dgvOperacoes.Columns["OperationType"].HeaderText = "Operação";
            dgvOperacoes.Columns["Active"].HeaderText = "Ativo";
            dgvOperacoes.Columns["Quantity"].HeaderText = "Quantidade";
            dgvOperacoes.Columns["Price"].HeaderText = "Preço";
            dgvOperacoes.Columns["AccountNumber"].HeaderText = "Conta";

            dgvOperacoes.Columns["Active"].Visible = true;
            dgvOperacoes.Columns["OperationType"].Visible = true;
            dgvOperacoes.Columns["AccountNumber"].Visible = true;
        }

        private void SetFormattingGroup(GroupByEnum groupName)
        {
            dgvOperacoes.Columns["AveragePrice"].DefaultCellStyle.Format = "c";
            dgvOperacoes.Columns["AveragePrice"].HeaderText = "Preço Médio";

            dgvOperacoes.Columns["Active"].Visible = false;
            dgvOperacoes.Columns["OperationType"].Visible = false;
            dgvOperacoes.Columns["AccountNumber"].Visible = false;
            dgvOperacoes.Columns[Enum.GetName(typeof(GroupByEnum), groupName)].Visible = true;
        }

        private void GetAllOperations()
        {
            try
            {
                var operacoes = Helper.Get<List<Operation>>($"{ WEBAPI_URL }/operations");
                dgvOperacoes.DataSource = operacoes;
                SetFormatting();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro " + ex.Message);
            }

        }

        private void GetOperationByGroup(string groupBy)
        {
            try
            {
                switch (groupBy)
                {
                    case "Ativo":
                        GroupBy = GroupByEnum.Active; break;
                    case "Tipo de operação":
                        GroupBy = GroupByEnum.OperationType; break;
                    case "Conta":
                        GroupBy = GroupByEnum.AccountNumber; break;
                }

                var operacoes = Helper.Get<List<GroupOperation>>($"{ WEBAPI_URL }/operations?groupBy={ GroupBy }");
                dgvOperacoes.DataSource = operacoes;
                SetFormattingGroup(GroupBy);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro " + ex.Message);
            }
        }

        private void GetFile(Button button)
        {
            if (dgvOperacoes.Rows.Count > 0)
            {
                try
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.FilterIndex = 1;
                    saveFileDialog.RestoreDirectory = false;

                    if (button == btnExportExcel)
                        saveFileDialog.Filter = "xls files (*.xls)|*.xls";
                    else
                        saveFileDialog.Filter = "csv files (*.csv)|*.csv";


                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        btnExportExcel.Enabled = false;
                        btnExportCSV.Enabled = false;
                        button.Text = "Processando...";
                        Task.Run(() => DownloadFile(button, saveFileDialog.FileName));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro : " + ex.Message);
                }
            }
        }

        private void DownloadFile(System.Windows.Forms.Button button, string fileName)
        {
            try
            {
                var parameters = new Dictionary<string, string>();
                parameters.Add("groupBy", GroupBy.ToString());
                parameters.Add("fileType", ((button == btnExportExcel) ? "xls" : "csv"));

                var byteArray = Helper.DownloadAsync($"{ WEBAPI_URL }/operations/export", parameters);
                File.WriteAllBytes(fileName, byteArray);
                MessageBox.Show("Download realizado com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
            finally
            {
                button.Invoke(new System.Action(() => button.Text = $"Download {((button == btnExportExcel) ? "Excel" : "CSV")}"));
                btnExportExcel.Invoke(new System.Action(() => btnExportExcel.Enabled = true));
                btnExportCSV.Invoke(new System.Action(() => btnExportCSV.Enabled = true));
            }
        }
    }
}
