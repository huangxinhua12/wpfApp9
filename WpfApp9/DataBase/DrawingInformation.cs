using System;
namespace WpfApp9.DataBase
{
    public class DrawingInformation : IDatabaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Uuid { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string SpareField1 { get; set; }
        public string SpareField2 { get; set; }
        public string SpareField3 { get; set; }

        // 构造函数，用于初始化对象时设置默认值  
        public DrawingInformation()
        {
            Name = "";
            Uuid = "";
            Status = "1";
            SpareField1 = null;
            SpareField2 = null;
            SpareField3 = null;
        }
    }
}