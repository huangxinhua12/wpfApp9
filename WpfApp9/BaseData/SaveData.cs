using System.Collections.Generic;
using System.Xml.Serialization;

namespace WpfApp9.BaseData
{
    // 定义要序列化的数据结构  
    [XmlRoot("SaveData")]
    public class SaveData
    {
        [XmlElement("FileName")] public string SelectedItem { get; set; }
        [XmlElement("BuildingHeightLimit")] public string StoreyHeight { get; set; }
        
        [XmlArray("Rows")]
        [XmlArrayItem("Row")]
        public List<RowData> Rows { get; set; }

        public class RowData
        {
            [XmlElement("Label1")] public string Label1 { get; set; }
            [XmlElement("Label2")] public string Label2 { get; set; }
            [XmlElement("SelectedOption1")] public string SelectedOption1 { get; set; }
            [XmlElement("SelectedOption2")] public string SelectedOption2 { get; set; }
        }
    }
}