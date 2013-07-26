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
    class UserGrid : InfoGrid<UserEx>
    {
        private Button bn_delete = new Button();
        private TextBox tb_id = new TextBox();
        private TextBox tb_name = new TextBox();
        private ComboBox cb_authority = new ComboBox();
        private Label lb_status = new Label();
        private bool updating = false;

        public UserGrid()
            : base()
        {
            AddCol(72, GridUnitType.Pixel);
            AddCol(1, GridUnitType.Star);
            AddCol(2, GridUnitType.Star);
            AddCol(72, GridUnitType.Pixel);
            AddCol(72, GridUnitType.Pixel);

            bn_delete.Content = "删除";
            bn_delete.Click += bn_delete_Click;
            tb_id.IsEnabled = false;
            tb_name.TextChanged += tb_id_TextChanged;
            cb_authority.Items.Add("USER");
            cb_authority.Items.Add("ADMIN");
            cb_authority.SelectionChanged += cb_authority_SelectionChanged;
            lb_status.HorizontalContentAlignment = HorizontalAlignment.Center;

            Children.Add(bn_delete);
            Children.Add(tb_id);
            Children.Add(tb_name);
            Children.Add(cb_authority);
            Children.Add(lb_status);

            SetColumn(bn_delete, 0);
            SetColumn(tb_id, 1);
            SetColumn(tb_name, 2);
            SetColumn(cb_authority, 3);
            SetColumn(lb_status, 4);

            update();
        }

        public UserGrid(UserEx entity)
            : base(entity)
        {
            AddCol(72, GridUnitType.Pixel);
            AddCol(1, GridUnitType.Star);
            AddCol(2, GridUnitType.Star);
            AddCol(72, GridUnitType.Pixel);
            AddCol(72, GridUnitType.Pixel);

            bn_delete.Content = "删除";
            bn_delete.Click += bn_delete_Click;
            tb_id.IsEnabled = false;
            tb_name.TextChanged += tb_id_TextChanged;
            cb_authority.Items.Add("USER");
            cb_authority.Items.Add("ADMIN");
            cb_authority.SelectionChanged += cb_authority_SelectionChanged;
            lb_status.HorizontalContentAlignment = HorizontalAlignment.Center;

            Children.Add(bn_delete);
            Children.Add(tb_id);
            Children.Add(tb_name);
            Children.Add(cb_authority);
            Children.Add(lb_status);

            SetColumn(bn_delete, 0);
            SetColumn(tb_id, 1);
            SetColumn(tb_name, 2);
            SetColumn(cb_authority, 3);
            SetColumn(lb_status, 4);

            update();
        }

        void bn_delete_Click(object sender, RoutedEventArgs e)
        {
            Deleted = !Deleted;
            update();
        }

        void cb_authority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            update(false);
        }

        void tb_id_TextChanged(object sender, TextChangedEventArgs e)
        {
            update(false);
        }

        void update(bool moduleToView = true)
        {
            if (updating) return;
            updating = true;
            if (moduleToView)
            {
                
                tb_id.Text = Entity.Id;
                tb_name.Text = Entity.Name;
                cb_authority.SelectedIndex = (int)Entity.Authority;
                if (Deleted) bn_delete.Content = "恢复";
                else bn_delete.Content = "删除";            
                UpdateLabel(lb_status);
            }
            else
            {
                Entity.Id = tb_id.Text;
                Entity.Name = tb_name.Text;
                Entity.Authority = (UserEx.EAuthority)cb_authority.SelectedIndex;
                UpdateLabel(lb_status);
            }
            updating = false;
        }

        public override bool RealEqual()
        {
            return RealEntity.Id == Entity.Id && RealEntity.Name == Entity.Name && RealEntity.Authority == Entity.Authority;
        }
    }
}
