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
using System.Windows.Shapes;

namespace GooCooAdmin
{
    /// <summary>
    /// LoginDialog.xaml 的交互逻辑
    /// </summary>
    public partial class LoginDialog : Window
    {
        public LoginDialog()
        {
            InitializeComponent();
            this.Owner = this.Parent as Window;
            tb_id.Focus();
            bn_login.Click += bn_login_Click;
            bn_cancel.Click += bn_cancel_Click;
        }

        void bn_cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        void bn_login_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
