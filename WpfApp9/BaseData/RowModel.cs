// 引入必要的命名空间  

using System.Collections.Generic;
using System.Collections.ObjectModel; // 用于List<T>等泛型集合  
using System.ComponentModel;
using System.Linq; // 用于INotifyPropertyChanged接口  
using System.Runtime.CompilerServices; // 用于CallerMemberName特性  

// 声明WpfApp9.BaseData命名空间，其中WpfApp9可能是应用程序的名称，BaseData表示这是基础数据模型  
namespace WpfApp9.BaseData
{
    // RowModel类表示一行数据模型，实现了INotifyPropertyChanged接口，用于通知属性变更  
    public class RowModel : INotifyPropertyChanged
    {
        // DropDownOptions1和DropDownOptions2分别存储两个ComboBox下拉选项的列表  
        public ObservableCollection<string> DropDownOptions1 { get; set; }
        public ObservableCollection<string> DropDownOptions2 { get; set; }

        // _selectedOption1和_selectedOption2是私有字段，用于存储ComboBox的当前选中项  
        private string _selectedOption1;
        private string _selectedOption2;

        // _label1和_label2是私有字段，用于存储标签的文本  
        private string _label1;
        public string Label2 { get; set; } // 注意：这里Label2没有实现INotifyPropertyChanged  

        // Label1属性封装了_label1字段，并提供了属性变更通知  
        public string Label1
        {
            get => _label1; // 获取_label1的值  
            set // 设置_label1的值，并检查是否发生了变化  
            {
                if (_label1 != value) // 如果新值与旧值不同  
                {
                    _label1 = value; // 更新_label1的值  
                    OnPropertyChanged(nameof(Label1)); // 触发属性变更通知，指明Label1属性已经变更  
                }
            }
        }

        // 构造函数，带有四个参数，用于初始化RowModel实例的状态  
        public RowModel(string label1, string label2, ObservableCollection<string> dropDownOptions1,
            ObservableCollection<string> dropDownOptions2)
        {
            Label1 = label1; // 设置Label1属性的值  
            Label2 = label2; // 设置Label2属性的值（注意这里没有属性变更通知）  
            DropDownOptions1 = dropDownOptions1; // 设置下拉选项列表1  
            DropDownOptions2 = dropDownOptions2; // 设置下拉选项列表2  
        }

        // 无参数构造函数，提供一个默认的RowModel实例（所有属性将被初始化为它们的默认值）  
        public RowModel()
        {
            // 假设已经有一些选项填充到了DropDownOptions1和DropDownOptions2中  
            DropDownOptions1 = new ObservableCollection<string>();
            for (int i = 1; i <= 20; i++)
            {
                DropDownOptions1.Add("预设图层-" + i);
            }

            DropDownOptions2 = new ObservableCollection<string>();
            for (int i = 1; i <= 20; i++)
            {
                DropDownOptions2.Add("饰面工程-" + i);
            }

            // 设置默认选中为第一个选项  
            SelectedOption1 = DropDownOptions1.FirstOrDefault();
            SelectedOption2 = DropDownOptions2.FirstOrDefault();
        }

        public RowModel(string label1, string label2, string selectedOption1, string selectedOption2,
            ObservableCollection<string> dropDownOptions1, ObservableCollection<string> dropDownOptions2)
        {
            this.Label1 = label1;
            this.Label2 = label2;
            DropDownOptions1 = new ObservableCollection<string>();
            for (int i = 1; i <= 20; i++)
            {
                DropDownOptions1.Add("预设图层-" + i);
            }

            DropDownOptions2 = new ObservableCollection<string>();
            for (int i = 1; i <= 20; i++)
            {
                DropDownOptions2.Add("饰面工程-" + i);
            }
            // 设置默认选中为第一个选项  
            SelectedOption1 = selectedOption1;
            SelectedOption2 = selectedOption2;
        }

        public RowModel(string label1, string label2)
        {
            this.Label1 = label1;
            this.Label2 = label2;
            // 假设已经有一些选项填充到了DropDownOptions1和DropDownOptions2中  
            DropDownOptions1 = new ObservableCollection<string>();
            for (int i = 1; i <= 20; i++)
            {
                DropDownOptions1.Add("预设图层-" + i);
            }

            DropDownOptions2 = new ObservableCollection<string>();
            for (int i = 1; i <= 20; i++)
            {
                DropDownOptions2.Add("饰面工程-" + i);
            }
            // 设置默认选中为第一个选项  
            SelectedOption1 = DropDownOptions1.FirstOrDefault();
            SelectedOption2 = DropDownOptions2.FirstOrDefault();
        }

        // SelectedOption1属性封装了_selectedOption1字段，并提供了属性变更通知  
        public string SelectedOption1
        {
            get => _selectedOption1; // 获取_selectedOption1的值  
            set // 设置_selectedOption1的值，并检查是否发生了变化  
            {
                if (_selectedOption1 != value) // 如果新值与旧值不同  
                {
                    _selectedOption1 = value; // 更新_selectedOption1的值  
                    OnPropertyChanged(nameof(SelectedOption1)); // 触发属性变更通知，指明SelectedOption1属性已经变更  
                }
            }
        }

        // SelectedOption2属性封装了_selectedOption2字段，类似地提供了属性变更通知  
        public string SelectedOption2
        {
            get => _selectedOption2; // 获取_selectedOption2的值  
            set // 设置_selectedOption2的值，并进行相应的检查和通知  
            {
                if (_selectedOption2 != value) // 如果值有变化  
                {
                    _selectedOption2 = value; // 更新值  
                    OnPropertyChanged(nameof(SelectedOption2)); // 触发属性变更通知  
                }
            }
        }

        // INotifyPropertyChanged接口要求的事件，当属性发生变更时触发  
        public event PropertyChangedEventHandler PropertyChanged;

        // 受保护的方法，用于触发PropertyChanged事件，通知监听者某个属性已经变更  
        // 使用[CallerMemberName]特性自动获取调用此方法的属性的名称，避免了硬编码属性名称的字符串  
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName)); // 如果PropertyChanged事件有订阅者，则调用它们  
        }
    }
}