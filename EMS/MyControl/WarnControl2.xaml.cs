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
    /// WarnControl2.xaml 的交互逻辑
    /// </summary>
    public partial class WarnControl2 : UserControl
    {
        public WarnControl2()
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
            DependencyProperty.Register("Text", typeof(string), typeof(WarnControl2), new PropertyMetadata(""));

        // 自定义属性：IsTrue
        public bool IsTrue
        {
            get { return (bool)GetValue(IsTrueProperty); }
            set { SetValue(IsTrueProperty, value); }
        }
        public static readonly DependencyProperty IsTrueProperty =
            DependencyProperty.Register("IsTrue", typeof(bool), typeof(WarnControl2), new PropertyMetadata(false, OnIsTrueChanged));

        private static void OnIsTrueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((WarnControl2)d).OnIsTrueChanged((bool)e.NewValue);
        }

        protected virtual void OnIsTrueChanged(bool newValue)
        {
            UpdateColors(newValue);
        }

        // 自定义属性：BorderColor
        public Brush BorderColor
        {
            get { return (Brush)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }
        public static readonly DependencyProperty BorderColorProperty =
            DependencyProperty.Register("BorderColor", typeof(Brush), typeof(WarnControl2), new PropertyMetadata(Brushes.Transparent));

        // 自定义属性：BorderBackground
        public Brush BorderBackground
        {
            get { return (Brush)GetValue(BorderBackgroundProperty); }
            set { SetValue(BorderBackgroundProperty, value); }
        }
        public static readonly DependencyProperty BorderBackgroundProperty =
            DependencyProperty.Register("BorderBackground", typeof(Brush), typeof(WarnControl2), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F4F4F4"))));

        // 自定义属性：TextColor
        public Brush TextColor
        {
            get { return (Brush)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }
        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.Register("TextColor", typeof(Brush), typeof(WarnControl2), new PropertyMetadata(Brushes.Black));

        private void UpdateColors(bool isTrue)
        {
            if (isTrue)
            {
                BorderColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFA12F"));
                BorderBackground= Brushes.Transparent;
                TextColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFA12F"));
            }
            else
            {
                BorderColor = Brushes.Transparent; 
                BorderBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F4F4F4"));
                TextColor = Brushes.Black;
            }
        }
    }
}
