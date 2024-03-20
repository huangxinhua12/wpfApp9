﻿using System;
using System.Collections.Generic;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using WpfApp9.DataBase;
using WpfApp9.Extension;

namespace WpfApp9.EntityData
{
    
    /// <summary>
    /// 外部事件
    /// </summary>
    public class CreateWall : IExternalEventHandler
    {
        private static double param = 0.3048;

        /// <summary>
        /// 属性传值
        /// </summary>
        public string _fileName { get; set; }

        public void Execute(UIApplication app)
        {
            try
            {
                Document doc = app.ActiveUIDocument.Document;
                Level level = doc.GetElementOfType<Level>(doc, "标高 1");
                // 指定门的坐标（这里假设是门的中心点）    
                using (Transaction transaction = new Transaction(doc, "创建墙01"))
                {
                    //开启事务
                    transaction.Start();
                    // 获取墙类型和标高  
                    WallType wallType = null;
                    string name = _fileName;
                    List<WallData> wallList = new MySqlUtil().GetWallDataList(name);
                    foreach (var data in wallList)
                    {
                        wallType = doc.GetElementOfType<WallType>(doc, "常规 - 200mm");
                        double dThickness = data.Width / 304.8;
                        CompoundStructure cs = wallType.GetCompoundStructure();
                        // 获取所有层  
                        IList<CompoundStructureLayer> lstLayers = cs.GetLayers();
                        foreach (CompoundStructureLayer item in lstLayers)
                        {
                            if (item.Function == MaterialFunctionAssignment.Structure)
                            {
                                // 这里只考虑有一个结构层，如果有多个就自己算算  
                                item.Width = dThickness;
                                break;
                            }
                        }

                        // 修改后要设置一遍  
                        cs.SetLayers(lstLayers);
                        wallType.SetCompoundStructure(cs);

                        // 修改坐标以使其在Revit视图中可见  
                        XYZ start = new XYZ(data.StartPointX / param / 100, data.StartPointY / param / 100,
                            0); // 修改为合适的起点  
                        XYZ end = new XYZ(data.EndPointX / param / 100, data.EndPointY / param / 100,
                            0); // 修改为合适的终点，这里假设10单位应该是英尺，所以转换为毫米  
                        // 使用有界的线来创建墙  
                        Line geomLine = Line.CreateBound(start, end);
                        double height = 3 / param; //这个是300厘米
                        double offset = 0; // 偏移量，根据需要调整  
                        Wall.Create(doc, geomLine, wallType.Id, level.Id, height, offset, false, false);
                    }

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public string GetName()
        {
            return "CreateWall";
        }
    }
}