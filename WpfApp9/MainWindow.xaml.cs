using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Newtonsoft.Json;
using WpfApp9;
using WpfApp9.BaseData;
using WpfApp9.DataBase;
using WpfApp9.EntityData;
using Path = System.IO.Path;

namespace WpfApp9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private YourViewModel _viewModel;

        //外部事件
        private ExternalEvent _externalEvent = null;
        private CreateWall _createWall = null;

        public MainWindow()
        {
            //打开窗口初始化操作
            InitializeComponent();
            this.Loaded += MainWindow_Loaded; // 订阅窗口的Loaded事件 

            _viewModel = new YourViewModel();
            _createWall = new CreateWall();
            //定义外部事件
            _externalEvent = ExternalEvent.Create(_createWall);
            this.DataContext = _viewModel;
            btnMin.Click += (s, e) => { this.WindowState = WindowState.Minimized; };
            btnMax.Click += (s, e) =>
            {
                if (this.WindowState == WindowState.Maximized)
                    this.WindowState = WindowState.Normal;
                else
                    this.WindowState = WindowState.Maximized;
            };
            btnClose.Click += async (s, e) => { this.Close(); };
            btnClose2.Click += async (s, e) => { this.Close(); };
            ColorZone.MouseMove += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    this.DragMove();
            };
            ColorZone.MouseDoubleClick += (s, e) =>
            {
                if (this.WindowState == WindowState.Normal)
                    this.WindowState = WindowState.Maximized;
                else
                    this.WindowState = WindowState.Normal;
            };
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (ListBoxYourList.Items.Count > 0)
            {
                ListBoxYourList.SelectedIndex = 0; // 设置选中第一个元素  
                // 或者使用 ListBoxYourList.SelectedItem = ListBoxYourList.Items[0];  
            }
        }

        private async void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "DXF文件 (*.dxf)|*.dxf";
            openFileDialog.Filter = "DWG 文件 (*.dwg)|*.dwg";
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                double buildingHeight = 0; // 从UI获取或硬编码  
                double floorHeight = 0; // 从UI获取或硬编码  
                string uuid = GuidGenerator.GenerateUuid().ToString();

                try
                {
                    using (var client = new HttpClient())
                    {
                        // 设置请求的目标URL  
                        var url = "http://120.24.46.215:8097/designer22/downLoad/importNew"; // 替换为你的实际URL  
                        // 创建multipart/form-data内容  
                        var multiForm = new MultipartFormDataContent();
                        // 添加文件内容  
                        var fileContent = new StreamContent(File.OpenRead(selectedFilePath));
                        fileContent.Headers.ContentType =
                            new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                        multiForm.Add(fileContent, "DxfData", Path.GetFileName(selectedFilePath));

                        // 添加其他表单数据  
                        multiForm.Add(new StringContent(buildingHeight.ToString(CultureInfo.CurrentCulture)),
                            "BuildingHeight");
                        multiForm.Add(new StringContent(floorHeight.ToString(CultureInfo.CurrentCulture)),
                            "floorHeight");

                        // 发送POST请求  
                        var response = await client.PostAsync(url, multiForm);

                        // ... 省略前面的代码 ...  

                        if (response.IsSuccessStatusCode)
                        {
                            // 读取响应内容作为字符串  
                            string responseBody = await response.Content.ReadAsStringAsync();

                            try
                            {
                                // 解析响应体为 AjaxResult 对象  
                                AjaxResult result = JsonConvert.DeserializeObject<AjaxResult>(responseBody);

                                // 使用 result 对象中的属性  
                                if (result.Code == 200) // 假设 200 表示成功  
                                {
                                    // MySqlUtil mySql = new MySqlUtil();
                                    //string res = mySql.Resolver(result.Data.ToString(), uuid);
                                    XmlHandler xmlHandler = new XmlHandler();
                                    string res = xmlHandler.Resolve(result.Data.ToString());
                                    _viewModel.YourList.Add(res);
                                    MessageBox.Show($"操作成功: {res}");
                                    // 处理 Data 属性，可能需要进一步解析或转换为具体类型  
                                }
                                else
                                {
                                    MessageBox.Show($"操作失败: {result.Message}");
                                }
                            }
                            catch (JsonReaderException ex)
                            {
                                // 处理 JSON 解析错误  
                                MessageBox.Show($"无法解析服务器响应: {ex.Message}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // 处理异常，例如显示错误消息  
                    MessageBox.Show($"发生异常: {ex.Message}");
                }
            }
        }

        private void Bt1_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxYourList.SelectedIndex >= 0) // 确保有选中的项  
            {
                // 获取选中的项  
                var selectedItem = ListBoxYourList.SelectedItem;
                // 假设你的数据项是字符串类型  
                if (selectedItem is string item)
                {
                    // 显示确认删除的窗口  
                    if (!item.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                    {
                        item += ".xml";
                    }

                    MessageBoxResult result = MessageBox.Show($"您确定要删除图纸: {item} 吗？", "确认删除", MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        // 删除数据库中的记录  
                        // MySqlUtil mySql = new MySqlUtil();
                        XmlHandler xmlHandler = new XmlHandler();
                        // 后面改成逻辑删除  
                        int tags = xmlHandler.DeleteFile(item);
                        if (tags > 0)
                        {
                            // 从视图模型中删除该项  
                            _viewModel.YourList.RemoveAt(ListBoxYourList.SelectedIndex);
                            // 同步更新ListBox的选中状态（如果需要）  
                            ListBoxYourList.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        // 用户取消删除，不执行任何操作  
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择图纸");
            }
        }

        private void Bt3_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxYourList.SelectedIndex > 0) // 确保不是第一个元素  
            {
                int index = ListBoxYourList.SelectedIndex;
                string selectedItem = _viewModel.YourList[index];
                _viewModel.YourList.RemoveAt(index); // 删除当前项  
                _viewModel.YourList.Insert(index - 1, selectedItem); // 在上一位置插入  
                ListBoxYourList.SelectedIndex = index - 1; // 更新选中项  
            }
        }

        private void Bt4_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxYourList.SelectedIndex >= 0 &&
                ListBoxYourList.SelectedIndex < _viewModel.YourList.Count - 1) // 确保不是最后一个元素  
            {
                int index = ListBoxYourList.SelectedIndex;
                string selectedItem = _viewModel.YourList[index];
                _viewModel.YourList.RemoveAt(index); // 删除当前项  
                _viewModel.YourList.Insert(index + 1, selectedItem); // 在下一位置插入  
                ListBoxYourList.SelectedIndex = index + 1; // 更新选中项  
            }
        }

        private void excuteWall(object sender, RoutedEventArgs e)
        {
            if (ListBoxYourList.SelectedIndex >= 0) // 确保有选中的项  
            {
                // 获取选中的项  
                var selectedItem = ListBoxYourList.SelectedItem;
                // 假设你的数据项是某种类型，比如叫做 YourListItemType  
                if (selectedItem is string item)
                {
                    // 提取 name 属性  
                    _createWall._fileName = item + ".xml"; // 假设 YourListItemType 有一个名为 name 的属性 
                    // 显示确认删除的窗口  
                    MessageBoxResult result = MessageBox.Show($"您确定要生成: {item} 吗？", "确认生成", MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        _externalEvent.Raise();
                    }
                    else
                    {
                        // 用户取消删除，不执行任何操作  
                    }
                }
            }
            else
            {
                MessageBox.Show("未选中图纸");
            }
        }

        private void Bt2_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("编辑按钮");
        }

        private void _DeleteBtn(object sender, RoutedEventArgs e)
        {
            if (myComboBox.SelectedIndex >= 0) // 确保有选中的项  
            {
                // 获取选中的项  
                var selectedItem = myComboBox.SelectedItem;
                // 假设你的数据项是字符串类型  
                if (selectedItem is string item)
                {
                    // 显示确认删除的窗口  
                    MessageBoxResult result = MessageBox.Show($"您确定要删除方案: {item} 吗？", "确认删除", MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        // 删除数据库中的记录  
                        MessageBox.Show(" 删除数据库中的记录  ");
                    }
                    else
                    {
                        MessageBox.Show("用户取消删除，不执行任何操作  ");
                        // 用户取消删除，不执行任何操作  
                    }
                }
            }
            else
            {
                MessageBox.Show("没有选中项  ");
            }
        }

        private void _ImportBtn(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("导入事件");
        }

        private void _SaveBtn(object sender, RoutedEventArgs e)
        {
            var selectedItem = myComboBox.SelectedItem;
            var storeyHeight = StoreyHeight.Text;
            foreach (var row in _viewModel.Rows) // 假设 RowsCollection 是你的数据源集合  
            {
                string selectedOption1 = row.SelectedOption1; // 获取第一个 ComboBox 的选中项  
                string
                    defaultOption2 =
                        row.SelectedOption2; // 获取第二个 ComboBox 的选中项（注意这里可能应该是 SelectedOption2，根据你的代码和需求来定）  
                string label1 = row.Label1;
                string label2 = row.Label2;
                MessageBox.Show("保存事件:" + label1 + "," + label2 + "," + selectedOption1 + "," + defaultOption2);
                // 处理这些选中项...  
            }
        }

        private void Bt5_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("预览窗口");
        }

        private void ListBoxYourList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // 获取双击的 ListBoxItem  
            var listBox = sender as ListBox;
            if (listBox == null) return;
            var selectedItem = listBox.SelectedItem;
            if (selectedItem == null) return;
            this.excuteWall(sender, e);
        }
    }
}