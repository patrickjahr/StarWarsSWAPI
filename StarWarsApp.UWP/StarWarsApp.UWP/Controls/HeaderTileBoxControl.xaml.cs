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
    public sealed partial class HeaderTileBoxControl : UserControl
    {
        public static readonly DependencyProperty HeaderContentProperty = DependencyProperty.Register(
            "HeaderContent", typeof (object), typeof (HeaderTileBoxControl), new PropertyMetadata(default(object)));

        public object HeaderContent
        {
            get { return (object) GetValue(HeaderContentProperty); }
            set { SetValue(HeaderContentProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof (string), typeof (HeaderTileBoxControl), new PropertyMetadata(default(string)));

        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public HeaderTileBoxControl()
        {
            this.InitializeComponent();
        }
    }
}
