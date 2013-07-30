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
        private bool updating = false;
        
        public BookGrid()
            : base()
        {
            AddCol(36, GridUnitType.Pixel);
            AddCol(36, GridUnitType.Pixel);
            AddCol(1, GridUnitType.Star);
            AddCol(2, GridUnitType.Star);
            AddCol(96, GridUnitType.Pixel);
            AddCol(72, GridUnitType.Pixel);

            bn_delete.Content = "删除";
            bn_delete.Click += bn_delete_Click;
            bn_revert.Content = "重置";
            bn_revert.Click += bn_revert_Click;
            bn_revert.IsEnabled = false;
            tb_isbn.TextChanged += tb_name_TextChanged;
            tb_name.TextChanged += tb_name_TextChanged;
            se_count.Value = Entity.Count;
            se_count.Margin = new Thickness(1, 0, 1, 0);
            se_count.ValueChanged += se_count_ValueChanged;
            lb_status.HorizontalContentAlignment = HorizontalAlignment.Center;

            Children.Add(bn_revert);
            Children.Add(bn_delete);
            Children.Add(tb_isbn);
            Children.Add(tb_name);
            Children.Add(se_count);
            Children.Add(lb_status);

            SetColumn(bn_revert, 0);
            SetColumn(bn_delete, 1);
            SetColumn(tb_isbn, 2);
            SetColumn(tb_name, 3);
            SetColumn(se_count, 4);
            SetColumn(lb_status, 5);
            update();
        }

        public BookGrid(BookEx entity)
            : base(entity)
        {
            AddCol(36, GridUnitType.Pixel);
            AddCol(36, GridUnitType.Pixel);
            AddCol(1, GridUnitType.Star);
            AddCol(2, GridUnitType.Star);
            AddCol(96, GridUnitType.Pixel);
            AddCol(72, GridUnitType.Pixel);

            bn_delete.Content = "删除";
            bn_delete.Click += bn_delete_Click;
            bn_revert.Content = "重置";
            bn_revert.Click += bn_revert_Click;
            tb_isbn.IsEnabled = false;
            tb_isbn.TextChanged += tb_name_TextChanged;
            tb_name.TextChanged += tb_name_TextChanged;
            se_count.Value = Entity.Count; 
            se_count.Margin = new Thickness(1, 0, 1, 0);
            se_count.ValueChanged += se_count_ValueChanged;
            lb_status.HorizontalContentAlignment = HorizontalAlignment.Center;

            Children.Add(bn_revert);
            Children.Add(bn_delete);
            Children.Add(tb_isbn);
            Children.Add(tb_name);
            Children.Add(se_count);
            Children.Add(lb_status);

            SetColumn(bn_revert, 0);
            SetColumn(bn_delete, 1);
            SetColumn(tb_isbn, 2);
            SetColumn(tb_name, 3);
            SetColumn(se_count, 4);
            SetColumn(lb_status, 5);

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
                se_count.Value = Entity.Count;
                if (Deleted) bn_delete.Content = "恢复";
                else bn_delete.Content = "删除";
                UpdateLabel(lb_status);
                bn_revert.IsEnabled = Status != EGridStatus.无变化 && RealEntity != null;
                if (Holder != null) Holder.Filter(((MainWindow)App.Current.MainWindow).ftb_book.Status);
            }
            else
            {
                Entity.Isbn = tb_isbn.Text;
                Entity.Name = tb_name.Text;
                Entity.SetCount(se_count.Value);
                UpdateLabel(lb_status);
                bn_revert.IsEnabled = Status != EGridStatus.无变化 && RealEntity != null;
                if (Holder != null) Holder.Filter(((MainWindow)App.Current.MainWindow).ftb_book.Status);
            }
            if (Entity != null) lb_status.ToolTip = Entity.ToString();
            updating = false;
        }

        public override bool RealEqual()
        {
            return RealEntity.Isbn == Entity.Isbn && RealEntity.Name == Entity.Name && RealEntity.RealCount == Entity.Count;
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
