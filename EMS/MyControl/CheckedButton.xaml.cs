using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EMS.MyControl
{
    /// <summary>
    /// CheckedButton.xaml 的交互逻辑
    /// </summary>
    public partial class CheckedButton : UserControl
    {
        public CheckedButton()
        {
            InitializeComponent();
        }
        // 自定义属性：Text
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(CheckedButton), new PropertyMetadata(""));

        // 自定义属性：BackgroundColor
        public Brush BackgroundColor
        {
            get { return (Brush)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }
        public static readonly DependencyProperty BackgroundColorProperty =
            DependencyProperty.Register("BackgroundColor", typeof(Brush), typeof(CheckedButton), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BBBBBB"))));

        // 自定义属性：ForegroundColor
        public Brush ForegroundColor
        {
            get { return (Brush)GetValue(ForegroundColorProperty); }
            set { SetValue(ForegroundColorProperty, value); }
        }
        public static readonly DependencyProperty ForegroundColorProperty =
            DependencyProperty.Register("ForegroundColor", typeof(Brush), typeof(CheckedButton), new PropertyMetadata(Brushes.White));

        // 自定义属性：CheckedBackgroundColor
        public Brush CheckedBackgroundColor
        {
            get { return (Brush)GetValue(CheckedBackgroundColorProperty); }
            set { SetValue(CheckedBackgroundColorProperty, value); }
        }
        public static readonly DependencyProperty CheckedBackgroundColorProperty =
            DependencyProperty.Register("CheckedBackgroundColor", typeof(Brush), typeof(CheckedButton), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1890FF"))));

        // 自定义属性：CheckedForegroundColor
        public Brush CheckedForegroundColor
        {
            get { return (Brush)GetValue(CheckedForegroundColorProperty); }
            set { SetValue(CheckedForegroundColorProperty, value); }
        }
        public static readonly DependencyProperty CheckedForegroundColorProperty =
            DependencyProperty.Register("CheckedForegroundColor", typeof(Brush), typeof(CheckedButton), new PropertyMetadata(Brushes.White));

        [CommonDependencyProperty]
        public static readonly DependencyProperty CommandProperty;

    }
}
