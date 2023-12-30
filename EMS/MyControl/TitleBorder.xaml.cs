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
    /// TitleBorder.xaml 的交互逻辑
    /// </summary>
    public partial class TitleBorder : UserControl
    {
        public TitleBorder()
        {
            InitializeComponent();
        }

        //标题文字
             public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TitleBorder), new PropertyMetadata(""));

        //// 内容属性
        //public  object BorderContent
        //{
        //    get { return GetValue(BorderContentProperty); }
        //    set { SetValue(BorderContentProperty, value); }
        //}
        //public static readonly DependencyProperty BorderContentProperty =
        //    DependencyProperty.Register("Content", typeof(object), typeof(TitleBorder), new PropertyMetadata(""));
    }
}
