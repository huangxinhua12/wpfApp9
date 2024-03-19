using System;

namespace WpfApp9.DataBase
{
    public class WallData : IDatabaseEntity
    {
        public int Id { get; set; }
        public float StartPointX { get; set; }
        public float StartPointY { get; set; }
        public float EndPointX { get; set; }
        public float EndPointY { get; set; }
        public string WallType { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public string Status { get; set; } = "DefaultStatus"; // 可以在这里设置默认值，或者通过构造函数传入  
        public DateTime CreatedAt { get; set; } // 设置默认创建时间为UTC当前时间  
        public DateTime UpdatedAt { get; set; } // 设置默认更新时间为UTC当前时间，可在保存前更新  
        public string SpareField1 { get; set; }
        public string SpareField2 { get; set; }
        public string SpareField3 { get; set; }

        // 无参构造函数  
        public WallData()
        {
            // 这里可以设置其他属性的默认值，如果需要的话  
        }

        // 带参数的构造函数，用于初始化墙的起始点和结束点坐标  
        public WallData(float startX, float startY, float endX, float endY,float width)
        {
            StartPointX = startX;
            StartPointY = startY;
            EndPointX = endX;
            EndPointY = endY;
            Width = width;
            // 可以设置其他属性的默认值或执行其他初始化逻辑  
        }

        // 根据需要，你可以添加验证逻辑、方法、事件等  
    }
}