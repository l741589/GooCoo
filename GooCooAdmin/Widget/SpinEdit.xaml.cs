using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace GooCooAdmin.Widget
{
    /// <summary>
    /// SpinEdit.xaml 的交互逻辑
    /// </summary>
    public partial class SpinEdit : UserControl
    {
        [Category("Behavior")]
        public event RoutedPropertyChangedEventHandler<int> ValueChanged
        {
            add { this.AddHandler(ValueChangedEvent, value); }
            remove { this.RemoveHandler(ValueChangedEvent, value); }
        }
        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent("ValueChanged",
             RoutingStrategy.Direct, typeof(RoutedPropertyChangedEventHandler<int>), typeof(SpinEdit));

        protected virtual void OnValueChanged(int oldValue, int newValue)
        {
            RoutedPropertyChangedEventArgs<int> arg =
                new RoutedPropertyChangedEventArgs<int>(oldValue, newValue, ValueChangedEvent);
            this.RaiseEvent(arg);
        }

        public SpinEdit()
        {
            InitializeComponent();
            Value = 0;
        }

        public int Value
        {
            get
            {
                int num = 0;
                if (!int.TryParse(tb.Text, out num)) return 0;
                return num;
            }
            set
            {
                int num = 0;
                if (!int.TryParse(tb.Text, out num)) num = 0;
                int oldvalue = num;
                tb.Text = value.ToString();
                OnValueChanged(oldvalue, value);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            TextChange[] change = new TextChange[e.Changes.Count];
            e.Changes.CopyTo(change, 0);

            int offset = change[0].Offset;
            if (change[0].AddedLength > 0)
            {
                int n = 0;
                if (!int.TryParse(textBox.Text, out n))
                {
                    textBox.Text = textBox.Text.Remove(offset, change[0].AddedLength);
                    textBox.Select(offset, 0);
                }else
                if (n < 0)
                {
                    textBox.Text = textBox.Text.Remove(offset, change[0].AddedLength);
                    textBox.Select(offset, 0);
                }
            }
            int num = 0;
            if (!int.TryParse(tb.Text, out num)) num = 0;
            Value = num;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Value>0) --Value;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Value<0x7fffffff) ++Value;
        }
    }
}
