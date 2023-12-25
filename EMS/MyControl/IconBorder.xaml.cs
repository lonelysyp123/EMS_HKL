using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// IconBorder.xaml 的交互逻辑
    /// </summary>
    public partial class IconBorder : UserControl
    {
        public IconBorder()
        {
            InitializeComponent();
        }
        //字体图标
        public string FIcon_Text
        {
            get { return (string)GetValue(FIcon_TextProperty); }
            set { SetValue(FIcon_TextProperty, value); }
        }
        public static readonly DependencyProperty FIcon_TextProperty =
            DependencyProperty.Register("FIcon_Text", typeof(string), typeof(IconBorder), new PropertyMetadata(""));

        //标题文字
        public string FIconTextBlock_Text
        {
            get { return (string)GetValue(FIconTextBlock_TextProperty); }
            set { SetValue(FIconTextBlock_TextProperty, value); }
        }
        public static readonly DependencyProperty FIconTextBlock_TextProperty =
            DependencyProperty.Register("FIconTextBlock_Text", typeof(string), typeof(IconBorder), new PropertyMetadata(""));

        //字体图标颜色
        public string FIcon_Foreground
        {
            get { return (string)GetValue(FIcon_ForegroundProperty); }
            set { SetValue(FIcon_ForegroundProperty, value); }
        }
        public static readonly DependencyProperty FIcon_ForegroundProperty =
            DependencyProperty.Register("FIcon_Foreground", typeof(string), typeof(IconBorder), new PropertyMetadata("Colors.Green"));

        //字体图标背景
        public string FIconBorder_Background
        {
            get { return (string)GetValue(FIconBorder_BackgroundProperty); }
            set { SetValue(FIconBorder_BackgroundProperty, value); }
        }
        public static readonly DependencyProperty FIconBorder_BackgroundProperty =
            DependencyProperty.Register("FIconBorder_Background", typeof(string), typeof(IconBorder), new PropertyMetadata("Colors.Orange"));


    }
}
