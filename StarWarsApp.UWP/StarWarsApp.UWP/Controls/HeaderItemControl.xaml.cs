using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace StarWarsApp.UWP.Controls
{
    public sealed partial class HeaderItemControl : UserControl
    {
        public static readonly DependencyProperty DataProperty = DependencyProperty.Register(
            "Data", typeof (object), typeof (HeaderItemControl), new PropertyMetadata(default(object)));

        public object Data
        {
            get { return (object) GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty ImagePathProperty = DependencyProperty.Register(
            "ImagePath", typeof (string), typeof (HeaderItemControl), new PropertyMetadata(default(string)));

        public string ImagePath
        {
            get { return (string) GetValue(ImagePathProperty); }
            set { SetValue(ImagePathProperty, value); }
        }

        public static readonly DependencyProperty HeaderTextProperty = DependencyProperty.Register(
            "HeaderText", typeof (string), typeof (HeaderItemControl), new PropertyMetadata(default(string)));

        public string HeaderText
        {
            get { return (string) GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }
        public HeaderItemControl()
        {
            this.InitializeComponent();
        }
    }
}
