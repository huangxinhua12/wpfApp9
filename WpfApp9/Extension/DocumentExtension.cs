using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

namespace WpfApp9.Extension
{
    public static class DocumentExtension
    {
        public static List<T> OfClass<T>(this Document doc) where T : Element
        {
            return new FilteredElementCollector(doc).OfClass(typeof(T)).ToElements().Cast<T>().ToList();
        }
        
        public static T GetElementOfType<T>(this Document doc, Document document, string name) where T : Element
        {
            return new FilteredElementCollector(doc)
                .OfClass(typeof(T))
                .FirstOrDefault(x => x.Name.Equals(name)) as T;
        }

        public static T FindElementByName<T>(this Document doc, Document document, BuiltInCategory category, string elementName) where T : Element  
        {  
            FilteredElementCollector collector = new FilteredElementCollector(doc);  
          
            // 确保T是FamilySymbol或其派生类，因为我们使用OfClass和OfCategory特定于FamilySymbol  
            if (typeof(T).IsAssignableFrom(typeof(FamilySymbol)))  
            {  
                collector = collector.OfClass(typeof(T)) // 使用泛型类型T  
                    .OfCategory(category); // 使用提供的类别  
            }  
            else  
            {  
                collector = collector.OfClass(typeof(T)); // 仅使用泛型类型T，因为不是所有类型都有类别  
            }  
            return collector.Cast<T>() // 将结果转换为泛型类型T  
                .FirstOrDefault(x => x.Name.Equals(elementName)); // 查找匹配名称的第一个元素  
        }
       
     
    }
}