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
using GooCooServer.Entity.Ex;

namespace GooCooAdmin
{
    /// <summary>
    /// UserInfoDialog.xaml 的交互逻辑
    /// </summary>
    public partial class UserInfoDialog : Window
    {
        public UserInfoDialog(UserEx user, List<BookEx> favors, List<BookEx> orders, List<BookEx> holds)
        {
            InitializeComponent();
            lb_id.Content = user.Id;
            lb_name.Content = user.Name;
            lb_email.Content = user.Email;
            lb_repvalue.Content = user.Repvalue;
            lb_authory.Content = user.Authority.ToString();
            lb_phone.Content = user.Phonenumber;
            foreach (var e in favors) lb_favors.Items.Add(e);
            foreach (var e in orders) lb_orders.Items.Add(e);
            foreach (var e in holds) lb_holds.Items.Add(e);
        }
    }
}
