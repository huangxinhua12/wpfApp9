using System.Collections.Generic;

namespace WpfApp9.BaseData
{
    public class RowModel
    {
        public string Label1 { get; set; }  
        public string Label2 { get; set; }  
        public string ColumnName1 { get; set; }
        public string ColumnName2 { get; set; }
        public List<string> DropDownOptions1 { get; set; }  
        public List<string> DropDownOptions2 { get; set; }  
        // 可以添加选中的值属性等  
        public RowModel(string label1, string label2, List<string> dropDownOptions1, List<string> dropDownOptions2)
        {
            Label1 = label1;
            Label2 = label2;
            DropDownOptions1 = dropDownOptions1;
            DropDownOptions2 = dropDownOptions2;
        }

        public RowModel()
        {
        }

        public RowModel(string label1, string label2, string columnName1, string columnName2, List<string> dropDownOptions1, List<string> dropDownOptions2)
        {
            Label1 = label1;
            Label2 = label2;
            ColumnName1 = columnName1;
            ColumnName2 = columnName2;
            DropDownOptions1 = dropDownOptions1;
            DropDownOptions2 = dropDownOptions2;
        }
    }
}