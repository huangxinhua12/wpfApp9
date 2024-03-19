using System.Linq;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using WpfApp9.Extension;
namespace WpfApp9
{
    //创建屋顶
    [Transaction(TransactionMode.Manual)]  
    public class CreateRoof : IExternalCommand  
    {  
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)  
        {  
            Document doc = commandData.Application.ActiveUIDocument.Document;  
          
            // 避免魔法数字，增加可读性  
            const double mmToFeet = 1 / 304.8;  
            double roofLength = 5000 * mmToFeet;  
          
            // 使用 LINQ 查询获取标高和屋顶类型  
            var level = doc.OfClass<Level>().FirstOrDefault(l => l.Name == "标高 1");  
            var roofType = doc.OfClass<RoofType>().FirstOrDefault(rt => rt.Name == "常规 - 400mm");  
          
            // 检查是否找到了标高和屋顶类型  
            if (level == null || roofType == null)  
            {  
                message = "找不到指定的标高或屋顶类型。";  
                return Result.Failed;  
            }  
          
            // 创建屋顶轮廓的线条  
            // 创建屋顶轮廓的线条  
            CurveArray curveArray = new CurveArray();  
            curveArray.Append(Line.CreateBound(new XYZ(0, 0, 0), new XYZ(roofLength, 0, 0)));  
            curveArray.Append(Line.CreateBound(new XYZ(roofLength, 0, 0), new XYZ(roofLength, roofLength, 0)));  
            curveArray.Append(Line.CreateBound(new XYZ(roofLength, roofLength, 0), new XYZ(0, roofLength, 0)));  
            curveArray.Append(Line.CreateBound(new XYZ(0, roofLength, 0), new XYZ(0, 0, 0))); 
          
            // 使用 Transaction 创建屋顶  
            using (var transaction = new Transaction(doc, "创建屋顶"))  
            {  
                transaction.Start();  
                ModelCurveArray modelCurveArray = new ModelCurveArray();  
                doc.Create.NewFootPrintRoof(curveArray, level, roofType, out modelCurveArray);  
                transaction.Commit();  
            }  
  
            return Result.Succeeded;  
        }  
    }
}