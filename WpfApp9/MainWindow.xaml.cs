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
using MaterialDesignThemes.Wpf;
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
            InitializeComponent();
            _viewModel = new YourViewModel();
            _createWall = new CreateWall();
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
                                    MySqlUtil mySql = new MySqlUtil();
                                    string res = mySql.Resolver(result.Data.ToString(), uuid);
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

        private void Bt2_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxYourList.SelectedIndex >= 0) // 确保有选中的项  
            {
                // 获取选中的项  
                var selectedItem = ListBoxYourList.SelectedItem;
                // 假设你的数据项是某种类型，比如叫做 YourListItemType  
                if (selectedItem is string item)
                {
                    // 提取 name 属性  
                    string name = item; // 假设 YourListItemType 有一个名为 name 的属性  
                    MessageBox.Show($"删除图纸: {name}");
                    // 删除数据库中的记录  
                    MySqlUtil mySql = new MySqlUtil();
                    //后面改成逻辑删除
                    int tags = mySql.DeleteRecordByName(name);
                    if (tags > 0)
                    {
                        // 从视图模型中删除该项  
                        _viewModel.YourList.RemoveAt(ListBoxYourList.SelectedIndex);
                    }
                }
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
                    _createWall._fileName = item; // 假设 YourListItemType 有一个名为 name 的属性  
                }
            }
            MessageBox.Show($"生成图纸: {_createWall._fileName}");
            _externalEvent.Raise();
        }
    }
}