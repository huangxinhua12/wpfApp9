using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml;

namespace WpfApp9.BaseData
{
    public class XmlHandler
    {
        // 解析 XML 数据并保存到文件，然后执行相关操作  
        public string Resolve(string xmlData)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlData); // 加载 XML 数据字符串  
                XmlNode fileNameNode = xmlDoc.SelectSingleNode("//FileName");
                string fileName = GetAttributeValue(fileNameNode, "value");
                if (string.IsNullOrEmpty(fileName))
                {
                    throw new InvalidOperationException("FileName attribute not found or empty.");
                }

                string relativePath = $"import/{fileName}.xml"; // 使用插值字符串简化路径组合  
                string filePath = GetAbsoluteFilePath(relativePath);
                SaveXmlDataToFile(xmlData, filePath); // 保存 XML 数据到文件  
                return ExecuteData(fileName); // 假设 ExecuteData 是已定义的方法  
            }
            catch (Exception ex)
            {
                // 这里可以记录异常信息或返回适当的错误消息  
                throw new Exception("An error occurred while processing the XML data.", ex);
            }
        }

        public List<string> GetNamesFromDrawingInformationByFile(string folderPath)
        {
            List<string> names = new List<string>();
            // 指定要搜索的文件夹路径  
            // string folderPath = @"C:\YourFolderPath"; // 替换为你的文件夹路径  
            try
            {
                // 获取文件夹中所有文件的文件名，并过滤出.xml文件  
                string[] xmlFiles = Directory.GetFiles(folderPath, "*.xml", SearchOption.AllDirectories);

                // 遍历数组并打印每个.xml文件的文件名  
                foreach (string file in xmlFiles)
                {
                    // 如果你只需要文件名，而不是完整路径，可以使用Path.GetFileName方法  
                    //Console.WriteLine(Path.GetFileName(file));
                    //names.Add(file);
                    //names.Add(Path.GetFileName(file));
                    names.Add(GetFileNameWithoutExtension(Path.GetFileName(file)));
                }
            }
            catch (Exception ex)
            {
                // 处理任何可能出现的异常，例如路径不存在或没有访问权限等  
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return names;
        }

        public static string GetFileNameWithoutExtension(string path)
        {
            // 查找最后一个目录分隔符的位置  
            int directorySeparatorIndex = path.LastIndexOfAny(new char[] { '/', '\\' });
            if (directorySeparatorIndex >= 0)
            {
                // 截取文件名（包含扩展名）  
                path = path.Substring(directorySeparatorIndex + 1);
            }

            // 查找最后一个点的位置（扩展名分隔符）  
            int extensionSeparatorIndex = path.LastIndexOf('.');
            if (extensionSeparatorIndex >= 0)
            {
                // 截取文件名（不含扩展名）  
                return path.Substring(0, extensionSeparatorIndex);
            }

            // 如果没有找到扩展名分隔符，则返回原始字符串（它可能就是一个没有扩展名的文件名）  
            return path;
        }

        // 获取 XML 节点的属性值，如果节点或属性不存在则返回 null  
        private string GetAttributeValue(XmlNode node, string attributeName)
        {
            return node?.Attributes?.GetNamedItem(attributeName)?.Value;
        }

        // 获取绝对文件路径，确保目录存在  
        private string GetAbsoluteFilePath(string relativePath)
        {
            string directory = Path.Combine(Directory.GetCurrentDirectory(), Path.GetDirectoryName(relativePath));
            string filePath = Path.Combine(directory, Path.GetFileName(relativePath));

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return filePath;
        }

        // 将 XML 数据保存到文件  
        private void SaveXmlDataToFile(string xmlData, string filePath)
        {
            File.WriteAllText(filePath, xmlData);
        }

        // 假设这是处理 WallData 节点和文件名的逻辑（需要您自己实现）  
        private string ExecuteData(string fileName)
        {
            // TODO: 实现处理逻辑并返回结果  
            return fileName;
        }

        // 读取指定文件名的 XML 数据并返回 XmlDocument 对象  
        public XmlDocument ReadXmlFile(string fileName)
        {
            try
            {
                if (!fileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    fileName += ".xml";
                }

                // 构造文件的完整路径（这里假设文件位于 "import" 文件夹下）  
                string catalogue = "import";
                string relative = fileName;
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), catalogue, relative);
                //MessageBox.Show("当前路径:" + fullPath);
                // 确保文件存在  
                if (!File.Exists(fullPath))
                {
                    throw new FileNotFoundException($"The file {fullPath} was not found.");
                }

                // 创建 XmlDocument 对象并加载 XML 文件  
                XmlDocument xmlDoc = new XmlDocument();
                //如果是文件，则使用 xmlDoc.Load("filePath");  
                xmlDoc.Load(fullPath); // 使用 Load 方法直接加载文件  
                return xmlDoc;
            }
            catch (Exception ex)
            {
                // 这里可以记录异常信息或返回适当的错误消息  
                throw new Exception($"An error occurred while reading the XML file {fileName}.", ex);
            }
        }

        public int DeleteFile(string fileName)
        {
            // 指定要删除的文件路径  
            string catalogue = "import";
            string relative = fileName;
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), catalogue, relative);
            MessageBox.Show("当前路径:" + fullPath);
            // 检查文件是否存在  
            if (File.Exists(fullPath))
            {
                // 删除文件  
                File.Delete(fullPath);
                MessageBox.Show("文件已成功删除");
                return 1;
            }
            else
            {
                MessageBox.Show("指定的文件不存在");
                return 0;
            }
        }
    }
}