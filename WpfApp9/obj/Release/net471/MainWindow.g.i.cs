﻿#pragma checksum "..\..\..\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "44331EE60199DCCF61F565DB4BAE547E85F2382B"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using Prism.DryIoc;
using Prism.Interactivity;
using Prism.Interactivity.InteractionRequest;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Regions.Behaviors;
using Prism.Services.Dialogs;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using WpfApp9;


namespace WpfApp9 {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 50 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.ColorZone ColorZone;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMin;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMax;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClose;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ListBoxYourList;
        
        #line default
        #line hidden
        
        
        #line 126 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt1;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt2;
        
        #line default
        #line hidden
        
        
        #line 134 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt3;
        
        #line default
        #line hidden
        
        
        #line 138 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt4;
        
        #line default
        #line hidden
        
        
        #line 162 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox myComboBox;
        
        #line default
        #line hidden
        
        
        #line 294 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClose2;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.17.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfApp9;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.17.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ColorZone = ((MaterialDesignThemes.Wpf.ColorZone)(target));
            return;
            case 2:
            this.btnMin = ((System.Windows.Controls.Button)(target));
            return;
            case 3:
            this.btnMax = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.btnClose = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            
            #line 105 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ImportButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ListBoxYourList = ((System.Windows.Controls.ListBox)(target));
            return;
            case 7:
            this.bt1 = ((System.Windows.Controls.Button)(target));
            
            #line 126 "..\..\..\MainWindow.xaml"
            this.bt1.Click += new System.Windows.RoutedEventHandler(this.Bt1_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.bt2 = ((System.Windows.Controls.Button)(target));
            
            #line 130 "..\..\..\MainWindow.xaml"
            this.bt2.Click += new System.Windows.RoutedEventHandler(this.Bt2_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.bt3 = ((System.Windows.Controls.Button)(target));
            
            #line 134 "..\..\..\MainWindow.xaml"
            this.bt3.Click += new System.Windows.RoutedEventHandler(this.Bt3_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.bt4 = ((System.Windows.Controls.Button)(target));
            
            #line 138 "..\..\..\MainWindow.xaml"
            this.bt4.Click += new System.Windows.RoutedEventHandler(this.Bt4_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 148 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Bt5_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.myComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 13:
            
            #line 167 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this._DeleteBtn);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 175 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this._ImportBtn);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 183 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this._SaveBtn);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 293 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.excuteWall);
            
            #line default
            #line hidden
            return;
            case 17:
            this.btnClose2 = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

