using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace WpfApp9.DataBase
{
    public class UniqueObservableCollection<T> : ObservableCollection<T>
    {
        // 由于ObservableCollection<T>不是设计为可继承的，我们使用封装而不是继承  
        private readonly ObservableCollection<T> _internalCollection = new ObservableCollection<T>();

        // 将内部集合的更改事件通知到外部  
        public UniqueObservableCollection()
        {
            _internalCollection.CollectionChanged += (sender, e) =>
            {
                // 使用调用者线程的信息引发事件（如果需要的话）  
                // Note: 如果您的应用程序是多线程的，您需要确保线程安全  
                OnCollectionChanged(e);
            };
        }

        // 重写事件以便我们可以从外部订阅它  
        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        // 自定义的Insert方法，用于插入不重复的元素  
        public void InsertUnique(int index, T item)
        {
            // 检查项是否已存在于集合中  
            if (!_internalCollection.Contains(item))
            {
                _internalCollection.Insert(index, item);
            }
            else
            {
                // 可选：如果项已存在，可以抛出异常或执行其他操作  
                throw new Exception($"Item {item} already exists in the collection.");
            }
        }

        // 封装ObservableCollection的其他方法，如Add, Remove等，以保持接口的一致性  
        // ... (实现Add, Remove等其他需要的方法)  

        // 例如，实现Add方法  
        public new void Add(T item)
        {
            InsertUnique(_internalCollection.Count, item);
        }

        // 隐藏不需要的方法或属性，或者使用显式接口实现来避免它们被公开调用（如果需要）  
        // ... (隐藏不需要的成员)  
    }

    //使用UniqueObservableCollection  
    public class ExampleUsage
    {
        public void AddUniqueFileName(string fileName)
        {
            var fileNames = new UniqueObservableCollection<string>();
            // 订阅事件以便在集合更改时收到通知  
            fileNames.CollectionChanged += FileNames_CollectionChanged;

            string fileNameWithoutExtension = GetFileNameWithoutExtension(fileName); // 假设这是您的方法  
            fileNames.InsertUnique(0, fileNameWithoutExtension); // 尝试插入不重复的文件名  

            // ... (其他逻辑)  
        }

        private void FileNames_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // 处理集合更改事件...  
        }

        // 这个方法应该是XmlHandler的一部分或者是其他处理文件名的方法  
        private string GetFileNameWithoutExtension(string fileName)
        {
            // 实现提取不带扩展名的文件名...  
            return fileName; // 假设返回的是没有扩展名的文件名（这里应该是一个简化的示例）  
        }
    }
}