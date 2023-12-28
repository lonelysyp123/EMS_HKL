using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PCSTester.MyControl
{
    /// <summary>
    /// DateInput.xaml 的交互逻辑
    /// </summary>
    public partial class DateInput : UserControl
    {
        public DateInput()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty DateTextProperty =
            DependencyProperty.Register(
                "DateText",
                typeof(string),
                typeof(DateInput),
                new FrameworkPropertyMetadata(
                    string.Empty,
                    OnDateTextPropertyChanged));

        private static void OnDateTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dateInput = d as DateInput;
            if (e.NewValue is string text && !dateInput._isChangingDate)
            {
                dateInput.PasteTextIPTextBox(text);
            }
        }

        private bool _isChangingDate = false;

        void PasteTextIPTextBox(string text)
        {
            P1.TextChanged -= P1_TextChanged;
            P2.TextChanged -= P2_TextChanged;
            P3.TextChanged -= P3_TextChanged;
            if (string.IsNullOrWhiteSpace(text))
            {
                P1.Text = string.Empty;
                P2.Text = string.Empty;
                P3.Text = string.Empty;
            }
            else
            {
                var strs = text.Split(':');
                var _textboxBoxes = new TextBox[] { P1, P2, P3 };
                for (short i = 0; i < _textboxBoxes.Length; i++)
                {
                    var str = i < strs.Length ? strs[i] : string.Empty;
                    _textboxBoxes[i].Text = str;
                }
            }
            P1.TextChanged += P1_TextChanged;
            P2.TextChanged += P2_TextChanged;
            P3.TextChanged += P3_TextChanged;
        }

        public string DateText
        {
            get
            {
                return (string)GetValue(DateTextProperty);
            }
            set
            {
                SetValue(DateTextProperty, value);
            }
        }

        private void Date_TextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            if (!regex.IsMatch(e.Text))
            {
                if ((sender as TextBox).Text.Length >= 2)
                {
                    e.Handled = true;
                }
                else
                {
                    _isChangingDate = true;
                }
            }
            else
            {
                _isChangingDate = false;
                e.Handled = true;
            }
        }

        private void P1_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDateText();
        }

        private void P2_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDateText();
        }

        private void P3_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDateText();
        }

        private void UpdateDateText()
        {
            var segments = new string[3]
            {
                P1.Text.ToString(),
                P2.Text.ToString(),
                P3.Text.ToString()
            };
            var allEmpty = segments.All(string.IsNullOrEmpty);
            if (allEmpty)
            {
                SetValue(DateTextProperty, string.Empty);
                return;
            }
            for (int i = 0; i < segments.Length; i++)
            {
                if (string.IsNullOrEmpty(segments[i]))
                {
                    segments[i] = "00";
                }
            }
            var date = string.Join(":", segments);
            if(date != DateText)
            {
                SetValue (DateTextProperty, date);
            }
        }
    }
}
