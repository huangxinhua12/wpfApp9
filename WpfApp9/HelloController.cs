using System.Linq;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using MySql.Data.MySqlClient;
using WpfApp9;
using WpfApp9.Extension;

namespace wpfApp9
{
    [Transaction(TransactionMode.Manual)]
    public class HelloController : Autodesk.Revit.UI.IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            // 如果一切正常，返回成功结果  
            return Result.Succeeded;
        }
    }
}