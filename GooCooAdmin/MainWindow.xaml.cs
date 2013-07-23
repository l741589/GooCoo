using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
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
using System.Runtime.Serialization;
using System.Web;
using System.Web.Script.Serialization;
using GooCooAdmin.Properties;
using GooCooAdmin.Util;
using GooCooServer.Entity.Ex;

namespace GooCooAdmin
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ListHolder<UserEx> user_list;
        private ListHolder<BookEx> book_list;
        private readonly String[] user_field = null;
        private readonly String[] book_field = null;
        private BookEx sel_book = null;
        private UserEx sel_user = null;
        public MainWindow()
        {
            InitializeComponent();
            user_field = new String[] { "Id", "Name", "Authority" };
            book_field = new String[] { "Isbn", "Name", "Timestamp" };
            tb_user.TextChanged += tb_user_TextChanged;
            tb_book.TextChanged += tb_book_TextChanged;
            bn_login.Click += bn_login_Click;
            mi_login.Click += bn_login_Click;
            mi_logout.Click += mi_logout_Click;
            mi_exit.Click += mi_exit_Click;
            //mn_user.Opened += mn_user_Opened;
            lb_user.SelectionChanged += lb_user_SelectionChanged;
            lb_book.SelectionChanged += lb_book_SelectionChanged;
            lb_book.MouseDoubleClick += lb_book_MouseDoubleClick;
            lb_user.MouseDoubleClick += lb_book_MouseDoubleClick;
            Title = Properties.Resources.Title;
            user_list = new ListHolder<UserEx>();
            book_list = new ListHolder<BookEx>();
        }

        private void Update_User_List()
        {
            lb_user.Items.Clear();
            foreach (UserEx e in user_list.ToList())
            {
                int index = lb_user.Items.Add(e);
                //if (user_list["borrow"].Contains(e))lb_user.selection
            }
        }

        private void Update_Book_List()
        {
            lb_book.Items.Clear();
            foreach (BookEx e in book_list.ToList())
            {
                lb_book.Items.Add(e);
            }
        }

        void mi_exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        void mi_logout_Click(object sender, RoutedEventArgs e)
        {
            pn_main.IsEnabled = false;
            Title = Properties.Resources.Title;
            bn_login.Visibility = Visibility.Visible;
        }

        void mn_user_Opened(object sender, RoutedEventArgs e)
        {
            ContextMenu menu = sender as ContextMenu;
            if (lb_user.SelectedItem == null)
            {
                foreach (var item in menu.Items)
                {
                    (item as MenuItem).IsEnabled = false;
                }
            }
            else
            {
                foreach (var item in menu.Items)
                {
                    (item as MenuItem).IsEnabled = true;
                }
            }
        }

        async void bn_login_Click(object sender, RoutedEventArgs e)
        {
            LoginDialog dlg = new LoginDialog();
            if (dlg.ShowDialog() == true)
            {
                Dictionary<String, String> cv = new Dictionary<string, string>();
                cv.Add("id", dlg.tb_id.Text);
                cv.Add("pw", dlg.pb_pw.Password);
                String s = await HttpHelper.Post(Properties.Resources.URL_LOGIN, cv);
                if (s == null || s == "")
                {
                    pn_main.IsEnabled = false;
                    Title = Properties.Resources.Title;
                    bn_login.Visibility = Visibility.Visible;
                    MessageBox.Show("登录失败");
                }
                else
                {
                    UserEx user = new JavaScriptSerializer().Deserialize<UserEx>(s);
                    if (user.Authority == UserEx.EAuthority.ADMIN || user.Authority == UserEx.EAuthority.SUPERADMIN)
                    {
                        pn_main.IsEnabled = true;
                        Title = user.Name + "(" + user.Authority + ")";
                        bn_login.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        pn_main.IsEnabled = false;
                        Title = user.Name + "(" + user.Authority + ")";
                        bn_login.Visibility = Visibility.Visible;
                        MessageBox.Show("该用户没有权限使用GooCoo图书管理系统");
                    }
                }
            }
        }

        async void tb_book_TextChanged(object sender, TextChangedEventArgs e)
        {
            Dictionary<String, String> cv = new Dictionary<String, String>();
            cv.Add("keyword", (sender as TextBox).Text);
            String s = await HttpHelper.Post(Properties.Resources.URL_FINDBOOK,cv);
            book_list["search"] = new JavaScriptSerializer().Deserialize(s, typeof(List<BookEx>)) as List<BookEx>;
            book_list.reset();
            book_list.Priorities["search"] = 1;
            sel_book = null;
            Update_Book_List();
        }

        async void tb_user_TextChanged(object sender, TextChangedEventArgs e)
        {
            Dictionary<String, String> cv = new Dictionary<String, String>();
            cv.Add("keyword", (sender as TextBox).Text);
            String s = await HttpHelper.Post(Properties.Resources.URL_FINDUSER,cv);
            user_list["search"] = new JavaScriptSerializer().Deserialize(s, typeof(List<UserEx>)) as List<UserEx>;
            user_list.reset();
            user_list.Priorities["search"] = 1;
            sel_user = null;
            Update_User_List();
        }

        void lb_book_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DealDialog dlg = new DealDialog();
            bool? ret = null;
            if (sel_user != null)
            {
                dlg.user = sel_user;
                dlg.tb_user.Text = sel_user.ToString(); ;
            }
            if (sel_book != null)
            {
                dlg.bookInfo = sel_book;
                String s = sel_book.ToString();
                dlg.tb_book.Text = s;
            }
            ret = dlg.ShowDialog();
        }

        async void lb_book_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox).SelectedItem == null) return;
            Dictionary<String, String> cv = new Dictionary<string, string>();
            sel_book = (sender as ListBox).SelectedValue as BookEx;
            cv.Add("isbn", sel_book.Isbn);
            String s = await HttpHelper.Post(Properties.Resources.URL_GETUSERBYBORROW, cv);
            user_list["borrow"] = new JavaScriptSerializer().Deserialize(s, typeof(List<UserEx>)) as List<UserEx>;
            user_list.Priorities["borrow"] = 2;
            s = await HttpHelper.Post(Properties.Resources.URL_GETUSERBYORDER, cv);
            user_list["order"] = new JavaScriptSerializer().Deserialize(s, typeof(List<UserEx>)) as List<UserEx>;
            user_list.Priorities["order"] = 2;
            
            Update_User_List();
        }

        async void lb_user_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox).SelectedItem == null) return;
            Dictionary<String, String> cv = new Dictionary<string, string>();
            sel_user = (sender as ListBox).SelectedValue as UserEx;
            cv.Add("user", sel_user.Id);
            String s = await HttpHelper.Post(Properties.Resources.URL_GETBOOKBYBORROW, cv);
            book_list["borrow"] = new JavaScriptSerializer().Deserialize(s, typeof(List<BookEx>)) as List<BookEx>;
            book_list.Priorities["borrow"] = 2;
            s = await HttpHelper.Post(Properties.Resources.URL_GETBOOKBYORDER, cv);
            book_list["order"] = new JavaScriptSerializer().Deserialize(s, typeof(List<BookEx>)) as List<BookEx>;
            book_list.Priorities["order"] = 2;
            
            Update_Book_List();
        }

    }
}
