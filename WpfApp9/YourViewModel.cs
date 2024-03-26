using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Xml;
using WpfApp9.BaseData;
using WpfApp9.DataBase;

namespace WpfApp9
{
    // 声明YourViewModel类，它实现了INotifyPropertyChanged接口，以便在属性变更时通知绑定的UI元素  
    public class YourViewModel : INotifyPropertyChanged
    {
        // 定义一个私有字段来存储字符串列表的ObservableCollection实例  
        private ObservableCollection<string> _yourList;

        // 定义一个公共属性YourList来访问_yourList字段，并确保在setter中触发PropertyChanged事件  
        public ObservableCollection<string> YourList
        {
            get { return _yourList; } // 获取_yourList的值  
            set // 设置_yourList的值，并在值改变时触发PropertyChanged事件  
            {
                if (_yourList != value) // 如果新值与旧值不同  
                {
                    _yourList = value; // 更新_yourList的值  
                    OnPropertyChanged(nameof(YourList)); // 触发PropertyChanged事件，指明YourList属性已经变更  
                }
            }
        }

        // 定义一个公共属性YourList2，它只有get访问器，因此只能在构造函数或类内部被设置  
        public ObservableCollection<string> YourList2 { get; private set; }

        // 定义一个公共属性MyOptions，它也只有get访问器，用于存储选项的字符串列表  
        public ObservableCollection<string> MyOptions { get; private set; }

        // 定义一个私有字段来存储RowModel对象的ObservableCollection实例  
        private ObservableCollection<RowModel> _row;

        // 定义一个公共属性Rows来访问_row字段，并在setter中触发PropertyChanged事件  
        public ObservableCollection<RowModel> Rows
        {
            get { return _row; } // 获取_row的值  
            set // 设置_row的值，并在值改变时触发PropertyChanged事件  
            {
                _row = value; // 更新_row的值  
                OnPropertyChanged(nameof(Rows)); // 触发PropertyChanged事件，指明Rows属性已经变更  
            }
        }

        private bool _isItemSelected;

        public bool IsItemSelected
        {
            get { return _isItemSelected; }
            set
            {
                _isItemSelected = value;
                OnPropertyChanged(nameof(IsItemSelected));
            }
        }

        private string _SelectedText;

        public string SelectedText
        {
            get => _SelectedText;
            set
            {
                if (_SelectedText != value)
                {
                    _SelectedText = value;
                    OnPropertyChanged(nameof(SelectedText));
                }
            }
        }


        // 定义一个私有字段来存储选中的项  
        private string _selectedItem;

        // 定义一个公共属性SelectedItem来获取和设置选中的项，并在setter中更新其他相关属性并触发PropertyChanged事件  
        public string SelectedItem
        {
            get { return _selectedItem; } // 获取_selectedItem的值  
            set // 设置_selectedItem的值，并更新Heights和Rows属性  
            {
                _selectedItem = value; // 更新_selectedItem的值
                SelectedText = value;
                //MessageBox.Show($"当前选中{value}");
                if (value.Contains("(方案)"))
                {
                    Rows = GetRowsForValueByScheme(value); // 设置Rows属性的值  
                }
                else
                {
                    Rows = GetRowsForValue(value); // 设置Rows属性的值     
                }

                // 假设GetHeightsForValue和GetRowsForValue是根据选中的项来获取相应值的方法  
                Heights = GetHeightsForValue(value); // 设置Heights属性的值  
                //MessageBox.Show($"当前高度{Heights}");

                //MessageBox.Show(Rows.Count.ToString());
                OnPropertyChanged(nameof(SelectedItem)); // 触发PropertyChanged事件，指明SelectedItem属性已经变更  
                IsItemSelected = value != null; // 假设null表示没有选中任何项 
                OnPropertyChanged(nameof(IsItemSelected));
            }
        }

        // 定义一个私有字段来存储层高  
        private string _heights;

        // 定义一个公共属性Heights来获取和设置层高，并在setter中触发PropertyChanged事件  
        public string Heights
        {
            get { return _heights; } // 获取_heights的值  
            set // 设置_heights的值，并触发PropertyChanged事件  
            {
                _heights = value; // 更新_heights的值  
                OnPropertyChanged(nameof(Heights)); // 触发PropertyChanged事件，指明Heights属性已经变更  
            }
        }

        // INotifyPropertyChanged接口要求的事件，当属性发生变更时触发  
        public event PropertyChangedEventHandler PropertyChanged;

        // YourViewModel类的构造函数，用于初始化实例  
        public YourViewModel()
        {
            // 初始化Rows属性，并添加一些RowModel对象（这里只是演示，实际添加逻辑可能不同）  
            Rows = new ObservableCollection<RowModel>
            {
                // ...添加行数据...  
            };

            // 初始化MyOptions属性，并添加一些选项字符串  
            MyOptions = new ObservableCollection<string>
            {
                "选项1",
                "选项2",
                "选项3",
                // ...更多选项...  
            };

            // 初始化YourList2属性为一个空的ObservableCollection<string>  
            YourList2 = new ObservableCollection<string> { };

            // 初始化YourList属性，这里通过XmlHandler类从XML文件中获取数据，并将其转换为ObservableCollection<string>  
            XmlHandler xmlHandler = new XmlHandler(); // 创建XmlHandler实例  
            string directory = Path.Combine(Directory.GetCurrentDirectory(), "import"); // 获取当前目录下的"import"文件夹路径  
            List<string> names = xmlHandler.GetNamesFromDrawingInformationByFile(directory); // 从XML文件中获取名称列表  
            _yourList =
                new ObservableCollection<string>(names); // 将名称列表转换为ObservableCollection<string>并赋值给_yourList字段  
            YourList = _yourList; // 通过属性设置YourList的值，以触发可能的绑定更新（尽管在这里是多余的，因为构造函数内部不会有监听者）  
        }

        // 受保护的方法，用于触发PropertyChanged事件，通知监听者某个属性已经变更  
        // 使用[CallerMemberName]特性自动获取调用此方法的属性的名称，避免了硬编码属性名称的字符串  
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName)); // 如果PropertyChanged事件有订阅者，则调用它们并传递属性名称作为参数  
        }

        private string GetHeightsForValue(string value)
        {
            string _hei = "";
            // 根据选中的项返回对应的层高，这里只是一个示例  
            XmlHandler xmlHandler = new XmlHandler();
            // 创建XmlDocument对象并加载XML内容  
            XmlDocument xmlFile = xmlHandler.ReadXmlFile(value);
            if (xmlFile == null)
            {
                MessageBox.Show("获取xmlFile为空");
            }
            else
            {
                // 获取元素的value属性值  
                try
                {
                    _hei = XmlHandler.GetBuildingHeightLimit(xmlFile, "BuildingHeightLimit");
                    //string s = buildingHeightLimitNode.InnerText; 
                    if (_hei != null)
                        _hei = (double.Parse(_hei) * 10).ToString(CultureInfo.CurrentCulture);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }

            return _hei;
        }

        private ObservableCollection<RowModel> GetRowsForValueByScheme(string value)
        {
            ObservableCollection<RowModel> res = new ObservableCollection<RowModel>();
            // 根据选中的项返回对应的层高，这里只是一个示例  
            XmlHandler xmlHandler = new XmlHandler();
            // 创建XmlDocument对象并加载XML内容  
            XmlDocument xmlFile = xmlHandler.ReadXmlFile(value);
            // 使用XPath查询获取Rows节点  
            XmlNode rowsNode = xmlFile.SelectSingleNode("//Rows");
            if (rowsNode != null)
            {
                // 使用XPath查询获取Rows节点下的所有Row节点  
                XmlNodeList rowNodes = rowsNode.SelectNodes("Row");
                if (rowNodes != null)
                {
                    foreach (XmlNode rowNode in rowNodes)
                    {
                        // 在这里处理每个Row节点，例如打印Label1和Label2的值  
                        XmlNode label1Node = rowNode.SelectSingleNode("Label1");
                        XmlNode label2Node = rowNode.SelectSingleNode("Label2");
                        XmlNode selectedOption1Node1 =
                            rowNode.SelectSingleNode("SelectedOption1"); // 添加这行代码来获取SelectedOption1节点  
                        XmlNode selectedOption1Node2 =
                            rowNode.SelectSingleNode("SelectedOption2"); // 添加这行代码来获取SelectedOption2节点  

                        if (label1Node != null && label2Node != null && selectedOption1Node1 != null &&
                            selectedOption1Node2 != null)
                        {
                            ObservableCollection<string> dropDownOptions1 = new ObservableCollection<string>();
                            
                            ObservableCollection<string> dropDownOptions2 = new ObservableCollection<string>();
                               
                            RowModel rowModel = new RowModel(label1Node.InnerText, label2Node.InnerText,
                                selectedOption1Node1.InnerText, selectedOption1Node2.InnerText,
                                dropDownOptions1, dropDownOptions2);
                            res.Add(rowModel);
                        }
                    }
                }
                else
                {
                }
            }
            else
            {
            }

            return res;
        }

        private ObservableCollection<RowModel> GetRowsForValue(string value)
        {
            ObservableCollection<RowModel> res = new ObservableCollection<RowModel>();
            // 根据选中的项返回对应的层高，这里只是一个示例  
            XmlHandler xmlHandler = new XmlHandler();
            // 创建XmlDocument对象并加载XML内容  
            XmlDocument xmlFile = xmlHandler.ReadXmlFile(value);
            if (xmlFile == null)
            {
                MessageBox.Show("获取xmlFile为空");
            }
            else
            {
                // 选择要获取的元素，这里使用XPath表达式定位到BuildingHeightLimit元素  子节点
                XmlNodeList wallDataNodes = xmlFile.SelectNodes("//layerNameData");
                // 检查是否找到了该元素  
                if (wallDataNodes != null)
                {
                    for (int i = 0; i < wallDataNodes.Count; i++)
                    {
                        XmlNode dataNode = wallDataNodes[i];
                        string name = dataNode.Attributes?["Name"].Value;
                        RowModel rowModel = new RowModel((i + 1).ToString(), name);
                        res.Add(rowModel);
                    }
                }
                else
                {
                    MessageBox.Show("获取layerNameData为空");
                }
            }

            //MessageBox.Show(res.Count.ToString());
            return res;
        }
    }
}