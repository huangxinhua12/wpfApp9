using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using MySql.Data.MySqlClient;
using WpfApp9;
using WpfApp9.DataBase;
using WpfApp9.Extension;

namespace wpfApp9
{
    [Transaction(TransactionMode.Manual)]
    public class HelloController : Autodesk.Revit.UI.IExternalCommand
    {
        // 静态变量用于跟踪MainWindow实例和它的打开状态  
        private static MainWindow _mainWindow;
        private static bool _isMainWindowOpen = false;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // 检查_mainWindow是否已经被创建并且正在打开状态  
            if (!_isMainWindowOpen)
            {
                // 如果_mainWindow不存在或未打开，则创建新的MainWindow实例并显示它  
                _mainWindow = new MainWindow();
                _mainWindow.Closed += (sender, e) =>
                {
                    // 当窗口关闭时，更新打开状态并清除对窗口的引用（可选）  
                    _isMainWindowOpen = false;
                    _mainWindow = null; // 清除引用以便垃圾回收（如果需要）  
                };
                _mainWindow.Show();
                _isMainWindowOpen = true;
            }
            // 否则，如果_mainWindow已经存在并且正在打开状态，则不执行任何操作（即不创建新的MainWindow实例）  

            return Result.Succeeded;
        }
    }
}