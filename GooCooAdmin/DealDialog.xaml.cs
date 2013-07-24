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
using GooCooServer.Entity;
using GooCooServer.Entity.Ex;

namespace GooCooAdmin
{
    /// <summary>
    /// DealDialog.xaml 的交互逻辑
    /// </summary>
    public partial class DealDialog : Window
    {
        public BookEx book = null;
        public UserEx user = null;
        public DealDialog()
        {
            InitializeComponent();
            this.Owner = this.Parent as Window;
        }

        public DealDialog(BookEx book, UserEx user)
            : this()
        {
            this.book = book;
            this.user = user;
            if (user != null)
            {
                tb_user.Content = this.user.ToString(new String[] { "Id", "Name" });
            }
            if (book != null)
            {
                tb_book.Content = this.book.ToString(new String[] { "Name", "Isbn" });
            }

            if (book != null && user != null)
            {
                if (book.Owner_id == user.Id) cb_relation.SelectedIndex = 1;
                else
                    if (book.Orderer_id != null && book.Orderer_id != user.Id) cb_relation.SelectedIndex = 2;
                    else cb_relation.SelectedIndex = 0;
            }
            else if (user != null)
            {
                cb_relation.SelectedIndex = 2;
            }
            else if (book != null)
            {
                cb_relation.SelectedIndex = 0;
            }
        }
    }
}
