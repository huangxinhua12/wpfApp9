using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfApp9.BaseData;
using WpfApp9.DataBase;

namespace WpfApp9
{
    public class YourViewModel : INotifyPropertyChanged
    {
        // 定义一个可观察集合来存储字符串列表  
        // 定义一个公共属性来访问 YourList，并确保在 setter 中触发 PropertyChanged 事件  
        public ObservableCollection<string> YourList
        {
            get { return _yourList; }
            set
            {
                if (_yourList != value)
                {
                    _yourList = value;
                    OnPropertyChanged(nameof(YourList));
                }
            }
        }

        public ObservableCollection<string> _yourList { get; set; }

        public ObservableCollection<string> YourList2 { get; private set; }

        public ObservableCollection<string> MyOptions { get; private set; }

        public ObservableCollection<RowModel> Rows { get; set; }

        // INotifyPropertyChanged 接口的实现  
        public event PropertyChangedEventHandler PropertyChanged;

        // ViewModel 的构造函数  
        // ViewModel 的构造函数  
        public YourViewModel()
        {
            Rows = new ObservableCollection<RowModel>
            {
                new RowModel("1", "label1_2", new List<string>() { "2", "2" }, new List<string>() { "2", "2" }),
                new RowModel("2", "label1_2", new List<string>() { "2", "2" }, new List<string>() { "2", "2" }),
                new RowModel("3", "label1_2", new List<string>() { "2", "2" }, new List<string>() { "2", "2" }),
                new RowModel("4", "label1_2", new List<string>() { "2", "2" }, new List<string>() { "2", "2" }),
                new RowModel("5", "label1_2", new List<string>() { "2", "2" }, new List<string>() { "2", "2" }),
                new RowModel("6", "label1_2", new List<string>() { "2", "2" }, new List<string>() { "2", "2" }),
                new RowModel("7", "label1_2", new List<string>() { "2", "2" }, new List<string>() { "2", "2" }),
                new RowModel("8", "label1_2", new List<string>() { "2", "2" }, new List<string>() { "2", "2" }),
                new RowModel("9", "label1_2", new List<string>() { "2", "2" }, new List<string>() { "2", "2" }),
                new RowModel("10", "label1_2", new List<string>() { "2", "2" }, new List<string>() { "2", "2" }),
                new RowModel("11", "label1_2", new List<string>() { "2", "2" }, new List<string>() { "2", "2" }),
                new RowModel("12", "label1_2", new List<string>() { "2", "2" }, new List<string>() { "2", "2" }),
                new RowModel("13", "label1_2", new List<string>() { "2", "2" }, new List<string>() { "2", "2" }),
                new RowModel("14", "label1_2", new List<string>() { "2", "2" }, new List<string>() { "2", "2" }),
                new RowModel("15", "label1_2", new List<string>() { "2", "2" }, new List<string>() { "2", "2" }),
                new RowModel("16", "label1_2", new List<string>() { "2", "2" }, new List<string>() { "2", "2" }),
                new RowModel("17", "label1_2", new List<string>() { "2", "2" }, new List<string>() { "2", "2" }),
                new RowModel("18", "label1_2", new List<string>() { "2", "2" }, new List<string>() { "2", "2" }),
                new RowModel("19", "label1_2", new List<string>() { "2", "2" }, new List<string>() { "2", "2" }),
                new RowModel("20", "label1_2", new List<string>() { "2", "2" }, new List<string>() { "2", "2" }),


                // 添加行数据...  
            };
            // 初始化选项集合  
            MyOptions = new ObservableCollection<string>
            {
                "自定义",
                "选项1",
                "选项2",
                "选项3",
                "自定义",
                "选项1",
                "选项2",
                "选项3",
                "自定义",
                "选项1",
                "选项2",
                "选项3",
                "自定义",
                "选项1",
                "选项2",
                "选项3",
                "自定义",
                "选项1",
                "选项2",
                "选项3"
                // ...更多选项...  
            };
            YourList2 = new ObservableCollection<string> { };
            // 初始化 YourList 并添加一些示例数据  
            List<string> names = new MySqlUtil().GetNamesFromDrawingInformation();
            _yourList = new ObservableCollection<string>(names);
            YourList = _yourList;
        }

        // 调用此方法以通知属性更改  

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // 调用此方法以添加新项到YourList并通知UI更新  
        public void AddItem(string item)
        {
            YourList.Add(item);
        }
    }
}