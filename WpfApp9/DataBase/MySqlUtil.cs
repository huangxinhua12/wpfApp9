using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using MySql.Data.MySqlClient;

namespace WpfApp9.DataBase
{
    public class MySqlUtil
    {
        public static string ConnectionString = "server=120.24.46.215;userid=admin;password=admin;database=revit2020";
        public static double feet = 0.3048;

        public string Resolver2()
        {
            string xmlFilePath = @"C:\Users\lx\Desktop\1.xml"; // XML文件路径  
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath); // 加载XML文件  
            XmlNodeList wallDataNodes = xmlDoc.SelectNodes("//WallData");
            XmlNode nameNodes = xmlDoc.SelectSingleNode("//FileName");
            string fileName = nameNodes?.Attributes?["value"].Value;
            return executeData(wallDataNodes, fileName);
            // ... 其他处理逻辑 ...  
        }

        public string Resolver(string xmlData)
        {
            //假设 xmlData 是包含完整 XML 数据的字符串  
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData); // 加载 XML 数据字符串  
            XmlNodeList wallDataNodes = xmlDoc.SelectNodes("//WallData");
            XmlNode nameNode = xmlDoc.SelectSingleNode("//FileName"); // 注意这里应该是单数 Node，因为您只选择了一个节点  
            string fileName = nameNode?.Attributes?["value"]?.Value; // 使用空值传播运算符（?.）来避免空引用异常 
            return executeData(wallDataNodes, fileName);
        }

        public static string executeData(XmlNodeList wallDataNodes, string fileName)
        {
            if (wallDataNodes == null) return "数据解析失败或数据为空";
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                using (MySqlCommand command = connection.CreateCommand())
                {
                    foreach (XmlNode node in wallDataNodes)
                    {
                        string startX = node.Attributes?["StartX"]?.Value;
                        string startY = node.Attributes?["StartY"]?.Value;
                        string endX = node.Attributes?["EndX"]?.Value;
                        string endY = node.Attributes?["EndY"]?.Value;
                        string width = node.Attributes?["Width"]?.Value;
                        // ... 获取其他属性  
                        command.CommandText =
                            @"INSERT INTO revit2020.wall (start_point_x, start_point_y, end_point_x, end_point_y,wall.width,spare_field1 /*, 其他列名 */) 
                            VALUES (@StartX, @StartY, @EndX, @EndY,@width,@fileName /*, 其他参数 */)";
                        command.Parameters.AddWithValue("@StartX", startX);
                        command.Parameters.AddWithValue("@StartY", startY);
                        command.Parameters.AddWithValue("@EndX", endX);
                        command.Parameters.AddWithValue("@EndY", endY);
                        command.Parameters.AddWithValue("@width", width);
                        command.Parameters.AddWithValue("@fileName", fileName);
                        // ... 添加其他参数  

                        command.ExecuteNonQuery();
                        command.Parameters.Clear(); // 清除参数以便下次迭代使用相同的命令对象  
                    }
                }
            }

            Console.WriteLine("数据导入完成");
            return "数据导入完成";
        }


        public static void Main()
        {
            string xmlFilePath = @"C:\Users\lx\Desktop\1.xml"; // XML文件路径  
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath); // 加载XML文件  
            XmlNodeList wallDataNodes = xmlDoc.SelectNodes("//WallData");
            XmlNode nameNodes = xmlDoc.SelectSingleNode("//FileName");
            string fileName = nameNodes?.Attributes?["value"].Value;
            executeData(wallDataNodes, fileName);
        }


        public List<T> GetDataListFromDatabase<T>(string query, MySqlParameter parameter,
            Func<DataRow, T> entityFactory) where T : IDatabaseEntity
        {
            List<T> entityList = new List<T>();
            DatabaseHelper dbHelper = new DatabaseHelper(ConnectionString);
            try
            {
                DataTable result = dbHelper.ExecuteQuery(query, CommandType.Text, parameter);
                if (result.Rows.Count > 0)
                {
                    foreach (DataRow row in result.Rows)
                    {
                        T entity = entityFactory(row); // 使用工厂方法创建实体对象  
                        entityList.Add(entity);

                        // 注意：这里的打印逻辑可能需要根据实际的T类型进行调整  
                        // 因为泛型类型T的属性是未知的，所以不能直接访问像id、startX等属性  
                        // 除非使用反射或确保所有实现IDatabaseEntity的类都有相同的属性名和类型  
                        // 但这通常不是泛型设计的初衷，因此打印逻辑应该被移除或重构为适用于泛型的方式  
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return entityList;
        }

        public List<WallData> GetWallDataList()
        {
            string query = "SELECT * FROM revit2020.wall WHERE spare_field1 = @param";
            MySqlParameter parameter = new MySqlParameter("@param", MySqlDbType.VarChar)
                { Value = "华润广州南沙交标全套97户型3" };
            Func<DataRow, WallData> wallDataFactory = CreateWallDataFromRow; // 工厂方法作为委托传递  
            return GetDataListFromDatabase<WallData>(query, parameter, wallDataFactory);
        }

        public WallData CreateWallDataFromRow(DataRow row)
        {
            int id = Convert.ToInt32(row["id"]); // 假设id列是整数类型    
            float startX = (float)row["start_point_x"];
            float startY = (float)row["start_point_y"];
            float endX = (float)row["end_point_x"];
            float endY = (float)row["end_point_y"];
            float width = (float)row["width"];
            return new WallData(startX, startY, endX, endY, width);
        }
    }
}