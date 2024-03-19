using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WpfApp9.BaseData;

namespace WpfApp9
{
    public class YourViewModel : INotifyPropertyChanged
    {
        // 定义一个可观察集合来存储字符串列表  
        public ObservableCollection<string> YourList { get; private set; }

        public ObservableCollection<string> YourList2 { get; private set; }

        public ObservableCollection<string> MyOptions { get; private set; }
        
        public ObservableCollection<RowModel> Rows { get; set; } 

        // INotifyPropertyChanged 接口的实现  
        public event PropertyChangedEventHandler PropertyChanged;

        // ViewModel 的构造函数  
        public YourViewModel()
        {
            Rows = new ObservableCollection<RowModel>  
            { 
                new RowModel("1","label1_2",new List<string>(){"2","2"},new List<string>(){"2","2"}),
                new RowModel("2","label1_2",new List<string>(){"2","2"},new List<string>(){"2","2"}),
                new RowModel("3","label1_2",new List<string>(){"2","2"},new List<string>(){"2","2"}),
                new RowModel("4","label1_2",new List<string>(){"2","2"},new List<string>(){"2","2"}),
                new RowModel("5","label1_2",new List<string>(){"2","2"},new List<string>(){"2","2"}),
                new RowModel("6","label1_2",new List<string>(){"2","2"},new List<string>(){"2","2"}),
                new RowModel("7","label1_2",new List<string>(){"2","2"},new List<string>(){"2","2"}),
                new RowModel("8","label1_2",new List<string>(){"2","2"},new List<string>(){"2","2"}),
                new RowModel("9","label1_2",new List<string>(){"2","2"},new List<string>(){"2","2"}),
                new RowModel("10","label1_2",new List<string>(){"2","2"},new List<string>(){"2","2"}),
                new RowModel("11","label1_2",new List<string>(){"2","2"},new List<string>(){"2","2"}),
                new RowModel("12","label1_2",new List<string>(){"2","2"},new List<string>(){"2","2"}),
                new RowModel("13","label1_2",new List<string>(){"2","2"},new List<string>(){"2","2"}),
                new RowModel("14","label1_2",new List<string>(){"2","2"},new List<string>(){"2","2"}),
                new RowModel("15","label1_2",new List<string>(){"2","2"},new List<string>(){"2","2"}),
                new RowModel("16","label1_2",new List<string>(){"2","2"},new List<string>(){"2","2"}),
                new RowModel("17","label1_2",new List<string>(){"2","2"},new List<string>(){"2","2"}),
                new RowModel("18","label1_2",new List<string>(){"2","2"},new List<string>(){"2","2"}),
                new RowModel("19","label1_2",new List<string>(){"2","2"},new List<string>(){"2","2"}),
                new RowModel("20","label1_2",new List<string>(){"2","2"},new List<string>(){"2","2"}),
                
                
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
            YourList = new ObservableCollection<string>
            {
                "第一行文本",
                "第二行文本",
                "第三行文本",
                "第一行文本",
                "第二行文本",
                "第三行文本",
                "第一行文本",
                "第二行文本",
                "第三行文本",
                "第一行文本",
                "第二行文本",
                "第三行文本",
                "第一行文本",
                "第二行文本",
                "第三行文本",
                "第一行文本",
                "第二行文本",
                "第三行文本",
                "第一行文本",
                "第二行文本",
                "第三行文本",
                // ... 添加更多行 ...  
            };
        }

        // 调用此方法以通知属性更改  
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}