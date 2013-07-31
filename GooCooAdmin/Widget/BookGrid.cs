using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using GooCooServer.Entity.Ex;
using System.Diagnostics;
using GooCooAdmin.Utility;
using System.Windows.Media;

namespace GooCooAdmin.Widget
{
    class BookGrid : InfoGrid<BookEx>
    {
        private Button bn_delete = new Button();
        private Button bn_revert = new Button();
        private TextBox tb_isbn = new TextBox();
        private TextBox tb_name = new TextBox();
        private SpinEdit se_count = new SpinEdit();
        private Label lb_status = new Label();
        private TextBox tb_author = new TextBox();
        private TextBox tb_publisher = new TextBox();
        private Button bn_detail = new Button();
        private bool updating = false;
        
        public BookGrid()
            : base()
        {
            AddCol(36, GridUnitType.Pixel);
            AddCol(36, GridUnitType.Pixel);
            AddCol(36, GridUnitType.Pixel);
            AddCol(96, GridUnitType.Pixel);
            AddCol(2, GridUnitType.Star);
            AddCol(2, GridUnitType.Star);
            AddCol(2, GridUnitType.Star);
            AddCol(96, GridUnitType.Pixel);
            AddCol(72, GridUnitType.Pixel);

            bn_detail.Content = "详细";
            bn_detail.Click += bn_detail_Click;
            bn_delete.Content = "删除";
            bn_delete.Click += bn_delete_Click;
            bn_revert.Content = "重置";
            bn_revert.Click += bn_revert_Click;
            bn_revert.IsEnabled = false;
            tb_isbn.TextChanged += tb_name_TextChanged;
            tb_name.TextChanged += tb_name_TextChanged;
            tb_author.TextChanged += tb_name_TextChanged;
            tb_publisher.TextChanged += tb_name_TextChanged;
            se_count.Value = Entity.Count;
            se_count.Margin = new Thickness(1, 0, 1, 0);
            se_count.ValueChanged += se_count_ValueChanged;
            lb_status.HorizontalContentAlignment = HorizontalAlignment.Center;

            Children.Add(bn_detail);
            Children.Add(bn_revert);
            Children.Add(bn_delete);
            Children.Add(tb_isbn);
            Children.Add(tb_name);
            Children.Add(tb_author);
            Children.Add(tb_publisher);
            Children.Add(se_count);
            Children.Add(lb_status);

            SetColumn(bn_detail, 0);
            SetColumn(bn_revert, 1);
            SetColumn(bn_delete, 2);
            SetColumn(tb_isbn, 3);
            SetColumn(tb_name, 4);
            SetColumn(tb_author, 5);
            SetColumn(tb_publisher, 6);
            SetColumn(se_count, 7);
            SetColumn(lb_status, 8);
            update();
        }

        async void bn_detail_Click(object sender, RoutedEventArgs e)
        {
            BookEx user = new BookEx();
            var borrows = Util.DecodeJson(await Util.CreateContentValue().Add("isbn", Entity.Isbn).Post(Properties.Resources.URL_GETUSERBYBORROW), typeof(List<UserEx>), typeof(BookEx))[0] as List<UserEx>;
            var favors = Util.DecodeJson<List<UserEx>>(await Util.CreateContentValue().Add("isbn", Entity.Isbn).Post(Properties.Resources.URL_GETUSERBYFAVOR));
            var orders = Util.DecodeJson(await Util.CreateContentValue().Add("isbn", Entity.Isbn).Post(Properties.Resources.URL_GETUSERBYORDER), typeof(List<UserEx>), typeof(BookEx))[0] as List<UserEx>;
            new BookInfoDialog(Entity, favors, orders, borrows).Show();
        }

        public BookGrid(BookEx entity)
            : base(entity)
        {
            AddCol(36, GridUnitType.Pixel);
            AddCol(36, GridUnitType.Pixel);
            AddCol(36, GridUnitType.Pixel);
            AddCol(96, GridUnitType.Pixel);
            AddCol(2, GridUnitType.Star);
            AddCol(2, GridUnitType.Star);
            AddCol(2, GridUnitType.Star);
            AddCol(96, GridUnitType.Pixel);
            AddCol(72, GridUnitType.Pixel);

            bn_detail.Content = "详细";
            bn_detail.Click += bn_detail_Click;
            bn_delete.Content = "删除";
            bn_delete.Click += bn_delete_Click;
            bn_revert.Content = "重置";
            bn_revert.Click += bn_revert_Click;
            tb_isbn.IsEnabled = false;
            tb_isbn.TextChanged += tb_name_TextChanged;
            tb_name.TextChanged += tb_name_TextChanged;
            tb_author.TextChanged += tb_name_TextChanged;
            tb_publisher.TextChanged += tb_name_TextChanged;
            se_count.Value = Entity.Count; 
            se_count.Margin = new Thickness(1, 0, 1, 0);
            se_count.ValueChanged += se_count_ValueChanged;
            lb_status.HorizontalContentAlignment = HorizontalAlignment.Center;

            Children.Add(bn_detail);
            Children.Add(bn_revert);
            Children.Add(bn_delete);
            Children.Add(tb_isbn);
            Children.Add(tb_name);
            Children.Add(tb_author);
            Children.Add(tb_publisher);
            Children.Add(se_count);
            Children.Add(lb_status);

            SetColumn(bn_detail, 0);
            SetColumn(bn_revert, 1);
            SetColumn(bn_delete, 2);
            SetColumn(tb_isbn, 3);
            SetColumn(tb_name, 4);
            SetColumn(tb_author, 5);
            SetColumn(tb_publisher, 6);
            SetColumn(se_count, 7);
            SetColumn(lb_status, 8);

            update();
        }

        private void CloneBooks()
        {
            if (Entity == null || RealEntity == null) return;
            if (object.ReferenceEquals(Entity.Books, RealEntity.Books))
            {
                if (RealEntity.Books != null)
                {
                    Entity.Books = new List<BookEx.Book>();
                    foreach (var e in RealEntity.Books)
                    {
                        Entity.Books.Add(e);
                    }
                }
            }
        }

        void se_count_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            update(false);
        }

        void bn_revert_Click(object sender, RoutedEventArgs e)
        {
            Revert();
            update();
        }

        void bn_delete_Click(object sender, RoutedEventArgs e)
        {
            Deleted = !Deleted;
            update();
        }

        void tb_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            update(false);
        }

        public override void update(bool moduleToView = true)
        {
            if (updating) return;
            updating = true;
            CloneBooks();
            if (moduleToView)
            {
                tb_isbn.Text = Entity.Isbn;
                tb_name.Text = Entity.Name;
                tb_author.Text = Entity.Author;
                tb_publisher.Text = Entity.Publisher;
                se_count.Value = Entity.Count;
                if (Deleted) bn_delete.Content = "恢复";
                else bn_delete.Content = "删除";
                UpdateLabel(lb_status);
                bn_detail.IsEnabled = !(Status == EGridStatus.新建 || Status == EGridStatus.新建并删除);
                bn_revert.IsEnabled = Status != EGridStatus.无变化 && RealEntity != null;
                if (Holder != null) Holder.Filter(((MainWindow)App.Current.MainWindow).ftb_book.Status);
            }
            else
            {
                Entity.Isbn = tb_isbn.Text;
                Entity.Name = tb_name.Text;
                Entity.Author = tb_author.Text;
                Entity.Publisher = tb_publisher.Text;
                Entity.SetCount(se_count.Value);
                UpdateLabel(lb_status);
                bn_detail.IsEnabled = !(Status == EGridStatus.新建 || Status == EGridStatus.新建并删除);
                bn_revert.IsEnabled = Status != EGridStatus.无变化 && RealEntity != null;
                if (Holder != null) Holder.Filter(((MainWindow)App.Current.MainWindow).ftb_book.Status);
            }
            if (Entity != null) lb_status.ToolTip = Entity.ToString();
            updating = false;
        }

        public override bool RealEqual()
        {
            return RealEntity.Isbn == Entity.Isbn && RealEntity.Name == Entity.Name && RealEntity.RealCount == Entity.Count
                && RealEntity.Author == Entity.Author && RealEntity.Publisher == Entity.Publisher;
        }

        public void UpdateSuccess(String json)
        {
            try
            {
                Entity = Util.DecodeJson(json, typeof(BookEx), typeof(String))[0] as BookEx;
            }catch { }
            if (Status == EGridStatus.新建)
            {
                tb_isbn.IsEnabled = false;
            }
            base.UpdateSuccess();
        }
    }    
}
