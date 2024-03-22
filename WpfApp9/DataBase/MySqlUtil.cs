using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using System.IO;
using MySql.Data.MySqlClient;
using WpfApp9.BaseData;

namespace WpfApp9.DataBase
{
    public class MySqlUtil
    {
        public static string ConnectionString = "server=120.24.46.215;userid=admin;password=admin;database=revit2020";
        public static double feet = 0.3048;

        public string Resolver2(string uuid)
        {
            string xmlFilePath = @"C:\Users\lx\Desktop\1.xml"; // XML文件路径  
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath); // 加载XML文件  
            XmlNodeList wallDataNodes = xmlDoc.SelectNodes("//WallData");
            XmlNode nameNodes = xmlDoc.SelectSingleNode("//FileName");
            string fileName = nameNodes?.Attributes?["value"].Value;
            return executeData(wallDataNodes, fileName, uuid);
            // ... 其他处理逻辑 ...  
        }

        public string Resolver(string xmlData)
        {
            XmlHandler xmlHandler = new XmlHandler();
            return xmlHandler.Resolve(xmlData);
        }


        public static string executeData(XmlNodeList wallDataNodes, string fileName, string uuid)
        {
            string status = "1"; // 根据您的表结构，status 的默认值是 '1' 
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
                            @"INSERT INTO revit2020.wall (start_point_x, start_point_y, end_point_x, end_point_y,wall.width,spare_field1,uuid /*, 其他列名 */) 
                            VALUES (@StartX, @StartY, @EndX, @EndY,@width,@fileName,@uuid/*, 其他参数 */)";
                        command.Parameters.AddWithValue("@StartX", startX);
                        command.Parameters.AddWithValue("@StartY", startY);
                        command.Parameters.AddWithValue("@EndX", endX);
                        command.Parameters.AddWithValue("@EndY", endY);
                        command.Parameters.AddWithValue("@width", width);
                        command.Parameters.AddWithValue("@fileName", fileName);
                        command.Parameters.AddWithValue("@uuid", uuid);

                        command.ExecuteNonQuery();
                        // 清除参数以便下次迭代使用相同的命令对象  
                        command.Parameters.Clear();
                    }

                    // 插入到 drawing_information 表中的代码  
                    command.CommandText =
                        @"INSERT INTO drawing_information (name, uuid, status, created_at, updated_at)    VALUES (@Name, @Uuid, @Status, NOW(), NOW())"; // 使用 NOW() 函数来设置时间戳  
                    command.Parameters.AddWithValue("@Name", fileName);
                    command.Parameters.AddWithValue("@Uuid", uuid);
                    command.Parameters.AddWithValue("@Status", status);
                    // 注意：由于 created_at 和 updated_at 字段设置了默认值 CURRENT_TIMESTAMP，所以在这里不需要显式地插入它们。  
                    // 如果您需要插入 spare_field1, spare_field2, spare_field3，请确保您从节点中获取了这些值，并像上面那样添加它们。
                    command.ExecuteNonQuery();
                    // 清除参数以便下次迭代使用相同的命令对象  
                    command.Parameters.Clear();
                }
            }

            Console.WriteLine("数据导入完成");
            return fileName;
        }

        public int DeleteRecordByName(string name)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM drawing_information WHERE name = @name";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", name);
                        return command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception as per your requirements.  
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

            return 0;
        }

        public List<string> GetNamesFromDrawingInformation()
        {
            List<string> names = new List<string>();

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT `name` FROM `drawing_information`";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                names.Add(reader.GetString("name")); // 或者使用列的索引：reader.GetString(0)  
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // 处理异常，比如记录日志或者通知用户  
                    Console.WriteLine(ex.Message);
                }
            }

            return names;
        }

    

        public static void Main()
        {
            string uuid = Guid.NewGuid().ToString();
            string xmlFilePath = @"C:\Users\lx\Desktop\1.xml"; // XML文件路径  
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath); // 加载XML文件  
            XmlNodeList wallDataNodes = xmlDoc.SelectNodes("//WallData");
            XmlNode nameNodes = xmlDoc.SelectSingleNode("//FileName");
            string fileName = nameNodes?.Attributes?["value"].Value;
            executeData(wallDataNodes, fileName, uuid);
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

        public List<WallData> GetWallDataList(string fileName)
        {
            string query = "SELECT * FROM revit2020.wall WHERE spare_field1 = @param";
            MySqlParameter parameter = new MySqlParameter("@param", MySqlDbType.VarChar)
                { Value = fileName };
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