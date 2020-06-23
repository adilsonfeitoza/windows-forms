using Common.Models;
using Common.Support;
using DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public class OperationBusiness
    {
        private OperationDataAccess operationDataAccess;

        public OperationBusiness()
        {
            operationDataAccess = new OperationDataAccess();
        }

        public IEnumerable<Operation> GetAll()
        {
            return operationDataAccess.GetAll();
        }
        
        public IEnumerable<GroupOperation> GetByGroup(GroupByEnum groupBy)
        {
            return operationDataAccess.GetByGroup(groupBy);
        }

        public byte[] ExportXls(List<GroupOperation> list, GroupByEnum groupBy)
        {
            NPOI.SS.UserModel.IWorkbook workbook = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("Plan 1");

            int rowNumer = 0;
            NPOI.SS.UserModel.IRow row = sheet.CreateRow(rowNumer);
            NPOI.SS.UserModel.ICell cell;
            NPOI.SS.UserModel.IFont hFont = workbook.CreateFont();

            hFont.FontHeightInPoints = 12;
            hFont.FontName = "Arial";

            NPOI.SS.UserModel.ICellStyle styleHeader = workbook.CreateCellStyle();
            styleHeader.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
            styleHeader.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;
            styleHeader.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            styleHeader.SetFont(hFont);

            NPOI.SS.UserModel.ICellStyle styleDisabled = workbook.CreateCellStyle();
            styleDisabled.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
            styleDisabled.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;
            styleDisabled.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;

            var listColumns = new List<ExportColumnInfo>();
            listColumns.Add(new ExportColumnInfo() { Name = groupBy.GetDescription() });
            listColumns.Add(new ExportColumnInfo() { Name = "Quantidade" });
            listColumns.Add(new ExportColumnInfo() { Name = "Preço Médio" });

            for (int i = 0; i < listColumns.Count; i++)
            {
                cell = row.CreateCell(i);
                cell.SetCellValue(listColumns[i].Name);
                cell.CellStyle = styleHeader;
            }

            //---- row
            foreach (var item in list)
            {
                rowNumer++;

                row = sheet.CreateRow(rowNumer);
                row.CreateCell(0).SetCellValue(item.AccountNumber ?? item.Active ?? item.OperationType);
                row.CreateCell(1).SetCellValue(item.Quantity);
                row.CreateCell(2).SetCellValue((double)item.AveragePrice);
            }
           
            for (int i = 0; i < listColumns.Count; i++)
                sheet.AutoSizeColumn(i);

            byte[] byteArray;
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                workbook.Write(stream);
                byteArray = stream.ToArray();
            }

            return byteArray;
        }

        public byte[] ExportCSV(List<GroupOperation> list, GroupByEnum groupBy)
        {
            const string DELIMITER = ";";
            var csv = new StringBuilder();

            csv.Append(groupBy.GetDescription()); csv.Append(DELIMITER);
            csv.Append("Quantidade"); csv.Append(DELIMITER);
            csv.Append("Preço Médio"); csv.Append(DELIMITER);
            csv.AppendLine();

            foreach (GroupOperation currentItem in list)
            {
                csv.AppendLine(this.ExportItemCSV(currentItem));
            }

            var bytes = Encoding.UTF8.GetBytes(csv.ToString());
            return Encoding.UTF8.GetPreamble().Concat(bytes).ToArray();
        }

        private string ExportItemCSV(GroupOperation item)
        {
            const string DELIMITER = ";";
            var csv = new StringBuilder();

            csv.AppendFormat("\"{0}\"", item.AccountNumber ?? item.Active ?? item.OperationType); csv.Append(DELIMITER);
            csv.AppendFormat("\"{0}\"", item.Quantity); csv.Append(DELIMITER);
            csv.AppendFormat("{0}", item.AveragePrice.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)); csv.Append(DELIMITER);
      
            return csv.ToString();
        }
    }
}
