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
using GooCooAdmin.Utility;
using GooCooServer.Entity.Ex;
using System.Web.Configuration;
using System.ComponentModel;
using GooCooServer.Entity;
using System.Diagnostics;
using System.Threading;
using GooCooAdmin.Widget;

namespace GooCooAdmin
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ListHolder<UserEx> user_list;
        private ListHolder<BookEx> book_list;
        private readonly String[] user_field = new String[] { "Id", "Name", "Authority" };
        private readonly String[] book_field = new String[] { "Isbn", "Name", "Timestamp" };
        private BookEx sel_book = null;
        private UserEx sel_user = null;
        private UserEx authorisedUser = null;
        private UserEx adminUser = null;
        private GridHolder<UserEx> gh_user = new GridHolder<UserEx>();
        private GridHolder<BookEx> gh_book = new GridHolder<BookEx>();

        #region 公用

        public MainWindow()
        {
            InitializeComponent();
            sv_user.Content = gh_user;

            tb_user.TextChanged += tb_user_TextChanged;
            tb_book.TextChanged += tb_book_TextChanged;
            bn_login.Click += bn_login_Click;
            mi_login.Click += bn_login_Click;
            mi_logout.Click += mi_logout_Click;
            mi_exit.Click += mi_exit_Click;
            lb_user.SelectionChanged += lb_user_SelectionChanged;
            lb_book.SelectionChanged += lb_book_SelectionChanged;
            lb_book.MouseDoubleClick += lb_book_MouseDoubleClick;
            lb_user.MouseDoubleClick += lb_book_MouseDoubleClick;
            bn_syncUser.Click += bn_sync_Click;
            bn_addUser.Click += bn_addUser_Click;

            Title = Properties.Resources.Title;
            user_list = new ListHolder<UserEx>();
            book_list = new ListHolder<BookEx>();
        }

        public IHolder GetHolder(String tag)
        {
            switch (tag)
            {
                case "user": return gh_user;
                case "book": return gh_book;
            }
            return null;
        }

        private async void AuthorisedLifeCount()
        {
            while (authorisedUser != null && adminUser != null)
            {
                Title = adminUser.Name + "(" + adminUser.Authority + ") | " +
                    authorisedUser.Name + "[" + authorisedUser.LifeTime + "](" + authorisedUser.Authority + ")";
                await Task.Run(() => { Thread.Sleep(1000); });
                if (adminUser == null || authorisedUser == null) break;
                --authorisedUser.LifeTime;
                if (authorisedUser.LifeTime <= 0)
                    authorisedUser = null;
            }
            if (adminUser != null) Title = adminUser.Name + "(" + adminUser.Authority + ")";
            else Title = Properties.Resources.Title;
            authorisedUser = null;
        }

        private UserEx AuthorisedUser
        {
            get
            {
                if (authorisedUser != null) authorisedUser.LifeTime = int.Parse(Properties.Resources.INT_AUTHORYLIFETIME);
                return authorisedUser;
            }
            set
            {
                authorisedUser = value;
                if (authorisedUser != null)
                {
                    authorisedUser.LifeTime = int.Parse(Properties.Resources.INT_AUTHORYLIFETIME);
                    AuthorisedLifeCount();
                }
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
            gh_user.Admin = gh_book.Admin = null;
            adminUser = null;
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
                    MessageBox.Show(this, "登录失败");
                }
                else
                {
                    UserEx user = Util.DecodeJson<UserEx>(s);
                    adminUser = user;
                    if (user.Authority == UserEx.EAuthority.ADMIN || user.Authority == UserEx.EAuthority.SUPERADMIN)
                    {
                        tc_main.IsEnabled = true;
                        Title = user.Name + "(" + user.Authority + ")";
                        bn_login.Visibility = Visibility.Collapsed;
                        gh_user.Admin = gh_book.Admin = user.Authority;
                    }
                    else
                    {
                        tc_main.IsEnabled = false;
                        Title = user.Name + "(" + user.Authority + ")";
                        bn_login.Visibility = Visibility.Visible;
                        gh_user.Admin = gh_book.Admin = user.Authority;
                        MessageBox.Show(this, "该用户没有权限使用GooCoo图书管理系统");
                    }
                }
            }
        }
        #endregion

        #region 借阅逻辑部分

        private void Update_User_List()
        {
            lb_user.Items.Clear();
            foreach (UserEx e in user_list.ToList())
            {
                int index = lb_user.Items.Add(e);
            }
            lb_user.SelectionChanged -= lb_user_SelectionChanged;
            if (lb_user.Items.Contains(sel_user)) lb_user.SelectedItem = sel_user; else sel_user = null;
            lb_user.SelectionChanged += lb_user_SelectionChanged;
        }

        private void Update_Book_List()
        {
            lb_book.Items.Clear();
            foreach (BookEx e in book_list.ToList())
            {
                lb_book.Items.Add(e);
            }
            lb_book.SelectionChanged -= lb_book_SelectionChanged;
            if (lb_book.Items.Contains(sel_book)) lb_book.SelectedItem = sel_book; else sel_book = null;
            lb_book.SelectionChanged += lb_book_SelectionChanged;
        }



        async void tb_book_TextChanged(object sender, TextChangedEventArgs e)
        {
            Dictionary<String, String> cv = new Dictionary<String, String>();
            cv.Add("keyword", (sender as TextBox).Text);
            String s = await HttpHelper.Post(Properties.Resources.URL_FINDBOOK, cv);
            book_list["search"] = Util.DecodeJson(s, typeof(List<BookEx>)) as List<BookEx>;
            book_list.reset();
            book_list.Priorities["search"] = 1;
            Update_Book_List();
        }

        async void tb_user_TextChanged(object sender, TextChangedEventArgs e)
        {
            Dictionary<String, String> cv = new Dictionary<String, String>();
            cv.Add("keyword", (sender as TextBox).Text);
            String s = await HttpHelper.Post(Properties.Resources.URL_FINDUSER, cv);
            user_list["search"] = Util.DecodeJson(s, typeof(List<UserEx>)) as List<UserEx>;
            user_list.reset();
            user_list.Priorities["search"] = 1;
            Update_User_List();
        }

        async Task<bool> Grant()
        {
            if (sel_user == AuthorisedUser) return true;
            LoginDialog dlg = new LoginDialog();
            dlg.tb_id.Text = sel_user.Id.ToString();
            dlg.tb_id.IsEnabled = false;
            dlg.pb_pw.Focus();
            if (dlg.ShowDialog() == true)
            {
                Dictionary<String, String> cv = new Dictionary<string, string>();
                cv.Add("id", dlg.tb_id.Text);
                cv.Add("pw", dlg.pb_pw.Password);
                String s = await HttpHelper.Post(Properties.Resources.URL_LOGIN, cv);
                if (s == null || s == "")
                {
                    MessageBox.Show(this, "登录失败");
                    return false;
                }
                else
                {
                    UserEx user = Util.DecodeJson<UserEx>(s);
                    Debug.Assert(user == sel_user, "登录用户不是目标用户");
                    AuthorisedUser = user;
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        async void lb_book_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sel_book != null && !sel_book.Filled) return;
            DealDialog dlg = new DealDialog(sel_book, sel_user);
            if (dlg.ShowDialog() == true)
            {
                String ret = null;
                String url = null;
                Dictionary<String, String> cv = new Dictionary<string, string>();
                switch (dlg.cb_relation.SelectedIndex)
                {
                    case 0: if (sel_user == null || !await Grant()) { MessageBox.Show(this, "操作失败"); return; }
                        url = Properties.Resources.URL_BORROW;
                        cv.Add("user", sel_user.Id);
                        cv.Add("book", (dlg.cb_bookid.SelectedValue as Label).Content.ToString());
                        break;
                    case 1:
                        url = Properties.Resources.URL_RETURN;
                        cv.Add("book", (dlg.cb_bookid.SelectedValue as Label).Content.ToString());
                        break;
                    case 2:
                        url = Properties.Resources.URL_DONATE;
                        cv.Add("book", dlg.tb_isbn.Text);
                        cv.Add("num", dlg.tb_num.Text);
                        if (sel_user != null) cv.Add("user", sel_user.Id);
                        break;
                }
                ret = await HttpHelper.Post(url, cv);
                MessageBox.Show(this, ret);
            }
        }

        async void lb_book_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox).SelectedItem == null) return;
            Dictionary<String, String> cv = new Dictionary<string, string>();
            sel_book = (sender as ListBox).SelectedValue as BookEx;

            cv.Add("isbn", sel_book.Isbn);
            String s = await HttpHelper.Post(Properties.Resources.URL_GETUSERBYBORROW, cv);
            object[] objs = Util.DecodeJson(s, typeof(List<UserEx>), typeof(BookEx));
            user_list["borrow"] = objs[0] as List<UserEx>;
            Util.Merge(sel_book, objs[1] as BookEx);
            user_list.Priorities["borrow"] = 2;

            s = await HttpHelper.Post(Properties.Resources.URL_GETUSERBYORDER, cv);
            objs = Util.DecodeJson(s, typeof(List<UserEx>), typeof(BookEx));
            user_list["order"] = objs[0] as List<UserEx>;
            if ((objs[1] as BookEx).Orderer_id != null) sel_book.Orderer_id = null;
            Util.Merge(sel_book, objs[1] as BookEx);
            user_list.Priorities["order"] = 2;

            s = await HttpHelper.Post(Properties.Resources.URL_GETUSERBYFAVOR, cv);
            user_list["favor"] = Util.DecodeJson<List<UserEx>>(s);
            user_list.Priorities["favor"] = 1;

            lb_book.UpdateLayout();
            Update_User_List();
        }

        async void lb_user_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox).SelectedItem == null) return;
            Dictionary<String, String> cv = new Dictionary<string, string>();
            sel_user = (sender as ListBox).SelectedValue as UserEx;
            cv.Add("user", sel_user.Id);
            String s = await HttpHelper.Post(Properties.Resources.URL_GETBOOKBYBORROW, cv);
            book_list["borrow"] = Util.DecodeJson(s, typeof(List<BookEx>)) as List<BookEx>;
            book_list.Priorities["borrow"] = 2;

            s = await HttpHelper.Post(Properties.Resources.URL_GETBOOKBYORDER, cv);
            book_list["order"] = Util.DecodeJson(s, typeof(List<BookEx>)) as List<BookEx>;
            book_list.Priorities["order"] = 2;

            s = await HttpHelper.Post(Properties.Resources.URL_GETBOOKBYFAVOR, cv);
            book_list["favor"] = Util.DecodeJson(s, typeof(List<BookEx>)) as List<BookEx>;
            book_list.Priorities["favor"] = 1;

            Update_Book_List();
        }
        #endregion

        #region 用户管理

        private void tool_bar_Loaded(object sender, RoutedEventArgs e)
        {
            ToolBar toolBar = sender as ToolBar;
            var overflowGrid = toolBar.Template.FindName("OverflowGrid", toolBar) as FrameworkElement;
            if (overflowGrid != null)
            {
                overflowGrid.Visibility = Visibility.Collapsed;
            }
            var mainPanelBorder = toolBar.Template.FindName("MainPanelBorder", toolBar) as FrameworkElement;
            if (mainPanelBorder != null)
            {
                mainPanelBorder.Margin = new Thickness(0);
            }
        }

        async void bn_sync_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).IsEnabled = false;
            gh_user.Clear();
            String s = await HttpHelper.Post(Properties.Resources.URL_FINDUSER);
            List<User> ret = Util.DecodeJson<List<User>>(s);
            foreach (var elem in ret)
            {
                gh_user.Add(Util.CloneEntity<UserEx>(elem));
            }
            (sender as Button).IsEnabled = true;
        }

        void bn_addUser_Click(object sender, RoutedEventArgs e)
        {
            gh_user.Add(typeof(UserEx));
        }

        private void bn_revertUser_Click(object sender, RoutedEventArgs e)
        {
            switch (MessageBox.Show(this, "是否撤销新建用户", "撤销方式", MessageBoxButton.YesNoCancel))
            {
                case MessageBoxResult.Yes: gh_user.Revert(true); break;
                case MessageBoxResult.No: gh_user.Revert(); break;
            }

        }

        private async void bn_commitUser_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).IsEnabled = false;
            int count = 0;
            int successcount = 0;
            foreach (var elem in gh_user.Children)
            {
                UserGrid g = (UserGrid)elem;
                if (g.Entity.Id == null || g.Entity.Id == "" || g.Entity.Name == null || g.Entity.Name == "") continue;
                String url = null;
                Dictionary<String, String> cv = new Dictionary<String, String>();
                User user = Util.CloneEntity<User>(g.Entity);                
                switch (g.Status)
                {                    
                    case EGridStatus.新建: 
                        url = Properties.Resources.URL_ADDUSER;
                        cv.Add("user", Util.EncodeJson(user));
                        break;
                    case EGridStatus.已删除:
                        url = Properties.Resources.URL_DELUSER;
                        cv.Add("user", user.Id);
                        break;
                    case EGridStatus.已修改:
                        url = Properties.Resources.URL_UPDATEUSER;
                        cv.Add("user", Util.EncodeJson(user));
                        break;
                    case EGridStatus.新建并删除:
                        g.UpdateSuccess();
                        continue;
                    default:continue;
                }
                String ret = await HttpHelper.Post(url, cv);
                ++count;
                if (ret.Contains("成功"))
                {
                    ++successcount;
                    g.UpdateSuccess();
                }
            }
            gh_user.RemoveAllMarked();
            MessageBox.Show("提交完成，共提交" + count + "个项目，成功" + successcount + "个项目");
            (sender as Button).IsEnabled = true;
        }

        #endregion

        #region 书籍管理
        #endregion

        #region 日志查询
        #endregion
    }
}
