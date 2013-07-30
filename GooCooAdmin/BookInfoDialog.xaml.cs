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
    /// BookInfoDialog.xaml 的交互逻辑
    /// </summary>
    public partial class BookInfoDialog : Window
    {
        public BookInfoDialog(BookEx book, List<UserEx> favors, List<UserEx> orders, List<UserEx> borrows)
        {
            InitializeComponent();
            lb_isbn.Content = book.Isbn;
            lb_name.Content = book.Name;
            lb_author.Content = book.Author;
            lb_publisher.Content = book.Publisher;
            tb_summary.Text = book.Summary;
            foreach (var e in favors) lb_favors.Items.Add(e);
            foreach (var e in orders) lb_orders.Items.Add(e);
            foreach (var e in borrows) lb_holds.Items.Add(e);
        }
    }
}
