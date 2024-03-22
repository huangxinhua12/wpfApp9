using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using System;
using System.Reflection;
using System.Windows.Media.Imaging;
using WpfApp9.DataBase;

namespace WpfApp9
{
    [Transaction(TransactionMode.Manual)]
    public class PlugIn : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
        public Result OnStartup(UIControlledApplication application)
        {
            //1.第一步创建一个RibbonTab
            application.CreateRibbonTab("新增插件");
            //RibbonTab中创建UIPanel
            RibbonPanel rp = application.CreateRibbonPanel("新增插件", "一键翻模");
            //指定程序集的名称以及使用的类名
            //string assemblyPath = @"C:\Users\lx\source\repos\ClassLibrary5\bin\Debug\ClassLibrary5.dll";
            string assemblyPath = Assembly.GetExecutingAssembly().Location;
            string classNameHelloRevitDemo = "wpfApp9.HelloController";
            //创建PushButton
            PushButtonData pbd = new PushButtonData("InnerNameRevit","HelloController", assemblyPath,classNameHelloRevitDemo);
            //将pushButton添加到面板中
            PushButton pushButton = rp.AddItem(pbd) as PushButton;
            //设置按钮图片
            string imgPath = @"http://cdn.zlili.cn/root/icon-revit.png";
            if (pushButton != null)
            {
                pushButton.LargeImage = new BitmapImage(new Uri(imgPath, UriKind.Absolute));
                pushButton.ToolTip = "自定义提示框";
            }

            return Result.Succeeded;


        }
    }
}