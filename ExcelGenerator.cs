using Excel = Microsoft.Office.Interop.Excel;

namespace WBParcer
{
    public class ExcelGenetator
    {
        public Dictionary<string, string> propertyNameByColumnName = new Dictionary<string, string>  {
            {"Title", "name"},
            {"Brand", "brand"},
            {"ID", "id"},
            {"Feedbacks", "feedbacks"},
            {"Price", "priceU"}
        };
        public void Generate(Dictionary<string, Data> productsDataByKeys)
        {
            Excel.ApplicationClass excelApp = new Excel.ApplicationClass();
            excelApp.Visible = false;
            Excel.Workbook workbook;
            Excel.Worksheet sheet;
            try
            {
                workbook = excelApp.Workbooks.Open(Environment.CurrentDirectory + @"\Solution.xlsx");
                workbook.Close(true);
                File.Delete(Environment.CurrentDirectory + @"\Solution.xlsx");
                workbook = excelApp.Workbooks.Add();
            }
            catch (Exception e)
            {
                workbook = excelApp.Workbooks.Add();
                Console.WriteLine(e);
            }
            foreach (var item in productsDataByKeys)
            {
                if (item.Key != productsDataByKeys.First().Key)
                {
                    sheet = (Excel.Worksheet)workbook.Sheets.Add(Type.Missing, workbook.Sheets[workbook.Sheets.Count]);
                }
                else
                {
                    sheet = (Excel.Worksheet)workbook.Sheets[1];
                }
                int i = 1;
                sheet.Name = item.Key;
                foreach (var columnName in propertyNameByColumnName)
                {
                    sheet.Cells[1, i] = columnName.Key;
                    i++;
                }
                int j = 2;
                foreach (var product in item.Value.products)
                {
                    int k = 1;
                    foreach (var property in propertyNameByColumnName)
                    {
                        var propertyValue = product.GetType().GetProperty(property.Value)?.GetValue(product);
                        if (propertyValue != null && property.Value.ToLower().Contains("price"))
                        {
                            sheet.Cells[j, k] = (int)propertyValue / 100;
                        }
                        else if (propertyValue == null)
                        {
                            Console.WriteLine("Property name is invalid");
                            sheet.Cells[j, k] = "No data";
                        }
                        else
                        {
                            sheet.Cells[j, k] = propertyValue;
                        }
                        k++;
                    }
                    j++;
                }
            }
            workbook.SaveAs(Environment.CurrentDirectory + @"\Solution.xlsx");
            excelApp.Quit();
        }

    }
}
