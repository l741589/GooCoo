using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using GooCooServer.Entity.Ex;

namespace GooCooAdmin
{
    /// <summary>
    /// DealDialog.xaml 的交互逻辑
    /// </summary>
    public partial class DealDialog : Window
    {
        public readonly BookEx book = null;
        public readonly UserEx user = null;
        private int lastsel = 0;

        public DealDialog()
        {
            InitializeComponent();
            Debug.Assert(book==null||book.Filled, "书籍信息不完整。");
            this.Owner = this.Parent as Window;
            cb_relation.SelectionChanged += cb_relation_SelectionChanged;
            bn_cancel.Click += bn_cancel_Click;
            bn_ok.Click += bn_ok_Click;
            tb_num.TextChanged += tb_num_TextChanged;
        }

        public DealDialog(BookEx book, UserEx user)
            : this()
        {
            this.book = book;
            this.user = user;
            if (user != null)
            {
                lb_user.Content = this.user.ToString(new String[] { "Id", "Name" });
                lb_user.ToolTip = this.user.ToString();
            }
            if (book != null)
            {
                tb_book.Content = this.book.ToString(new String[] { "Name", "Isbn" });
                tb_book.ToolTip = this.book.ToString();
            }
            ManageSelect();
        }

        void tb_num_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            TextChange[] change = new TextChange[e.Changes.Count];
            e.Changes.CopyTo(change, 0);

            int offset = change[0].Offset;
            if (change[0].AddedLength > 0)
            {
                uint num = 0;
                if (!uint.TryParse(textBox.Text, out num))
                {
                    textBox.Text = textBox.Text.Remove(offset, change[0].AddedLength);
                    textBox.Select(offset, 0);
                }
            }
        }

        void bn_ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        void bn_cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void HideAll()
        {
            dp_user.Visibility = Visibility.Collapsed;
            dp_book.Visibility = Visibility.Collapsed;
            dp_isbn.Visibility = Visibility.Collapsed;
            dp_num.Visibility = Visibility.Collapsed;
            dp_bookid.Visibility = Visibility.Collapsed;
            dp_password.Visibility = Visibility.Collapsed;
            dp_user_editable.Visibility = Visibility.Collapsed;
        }

        void cb_relation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!((sender as ComboBox).SelectedItem as Label).IsEnabled)
            {
                if (lastsel!=(sender as ComboBox).SelectedIndex)
                    (sender as ComboBox).SelectedIndex = lastsel;
            }
            lastsel = (sender as ComboBox).SelectedIndex;
            HideAll();
            if (lastsel == 0)
            {
                if (user != null) dp_user.Visibility = Visibility.Visible;
                else dp_user_editable.Visibility = Visibility.Visible;
                dp_bookid.Visibility = Visibility.Visible;
                dp_password.Visibility = Visibility.Visible;
                cb_bookid.Items.Clear();
                foreach (BookEx.Book b in book.Books)
                {
                    if (b.Owner == null)
                    {
                        Label label = new Label();
                        label.Content = b.Id.ToString();
                        cb_bookid.Items.Add(label);
                    }
                }
                if (cb_bookid.Items.Count > 0)
                {
                    cb_bookid.SelectedIndex = 0;
                }
                else ManageSelect();
            }

            if (lastsel == 1)
            {
                dp_book.Visibility = Visibility.Visible;
                dp_bookid.Visibility = Visibility.Visible;
                cb_bookid.Items.Clear();
                Label item = null;
                foreach (BookEx.Book b in book.Books)
                {
                    if (b.Owner != null)
                    {
                        Label label = new Label();
                        label.Content = b.Id.ToString();
                        cb_bookid.Items.Add(label);
                        if (user != null && b.Owner == user.Id) item = label;
                    }
                }
                if (cb_bookid.Items.Count > 0 && item != null)
                {
                    cb_bookid.SelectedItem = item;
                }
                else if (item == null)
                {
                    cb_bookid.SelectedIndex = 0;
                }
                else ManageSelect();
            }

            if (lastsel == 2)
            {
                dp_num.Visibility = Visibility.Visible;
                dp_isbn.Visibility = Visibility.Visible;
                if (user != null) dp_user.Visibility = Visibility.Visible;
                tb_num.Text = "1";
                if (book != null) tb_isbn.Text = book.Isbn;
            }
        }

        private void ManageSelect()
        {
            
            //处理Enable
            (cb_relation.Items[0] as Label).IsEnabled = (book != null) &&
                ((book.Orderers.Count + book.BorrowedBook < book.Count) ||
                (book.BorrowedBook < book.Count && user != null && book.Orderer_id == user.Id));

            (cb_relation.Items[1] as Label).IsEnabled = (book != null) && (book.BorrowedBook > 0);
            (cb_relation.Items[2] as Label).IsEnabled = true;

            //处理默认选项
            if (book != null && user != null)
            {
                if (book[user.Id]!=null)//如果user是book的持有者之一，该还书
                {
                    cb_relation.SelectedIndex = 1;
                }
                else
                {//如果书籍没有多余的书                    
                    if (book.Count <= book.BorrowedBook || book.Count <= book.Orderers.Count + book.BorrowedBook)
                    {//如果是该用户预定，那么为借书，否则为捐书                        
                        if (book.Count > book.BorrowedBook && book.Orderer_id == user.Id) cb_relation.SelectedIndex = 0;
                        else cb_relation.SelectedIndex = 2;
                    }//如果有多余的书借书                  
                    else cb_relation.SelectedIndex = 0;
                }
            }
            else if (user != null)
            {                
                cb_relation.SelectedIndex = 2;
            }
            else if (book != null)
            {
                if (book.Filled && book.BorrowedBook > 0)
                    cb_relation.SelectedIndex = 1;
                else
                    cb_relation.SelectedIndex = 2;
            }
            else
            {
                cb_relation.SelectedIndex = 2;
            }
            lastsel = cb_relation.SelectedIndex;

            Debug.Assert((cb_relation.SelectedItem as Label).IsEnabled, "该选项无效");
        }
    }
}
