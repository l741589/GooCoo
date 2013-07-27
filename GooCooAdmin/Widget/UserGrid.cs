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
        private Button bn_revert = new Button();
        private TextBox tb_id = new TextBox();
        private TextBox tb_name = new TextBox();
        private ComboBox cb_authority = new ComboBox();
        private Label lb_status = new Label();
        private bool updating = false;

        public UserGrid(UserEx.EAuthority? authory = null)
            : base()
        {
            AddCol(36, GridUnitType.Pixel);
            AddCol(36, GridUnitType.Pixel);
            AddCol(1, GridUnitType.Star);
            AddCol(2, GridUnitType.Star);
            AddCol(72, GridUnitType.Pixel);
            AddCol(72, GridUnitType.Pixel);

            bn_delete.Content = "删除";
            bn_delete.Click += bn_delete_Click;
            bn_revert.Content = "重置";
            bn_revert.Click += bn_revert_Click;
            bn_revert.IsEnabled = false;
            tb_id.TextChanged += tb_name_TextChanged;
            tb_name.TextChanged += tb_name_TextChanged;
            cb_authority.Items.Add("USER");
            cb_authority.Items.Add("ADMIN");
            if (authory != UserEx.EAuthority.SUPERADMIN) cb_authority.IsEnabled = false;
            cb_authority.SelectionChanged += cb_authority_SelectionChanged;
            lb_status.HorizontalContentAlignment = HorizontalAlignment.Center;

            Children.Add(bn_revert);
            Children.Add(bn_delete);
            Children.Add(tb_id);
            Children.Add(tb_name);
            Children.Add(cb_authority);
            Children.Add(lb_status);

            SetColumn(bn_revert, 0);
            SetColumn(bn_delete, 1);
            SetColumn(tb_id, 2);
            SetColumn(tb_name, 3);
            SetColumn(cb_authority, 4);
            SetColumn(lb_status, 5);
            update();
        }

        public UserGrid(UserEx entity, UserEx.EAuthority? authory = null)
            : base(entity)
        {
            AddCol(36, GridUnitType.Pixel);
            AddCol(36, GridUnitType.Pixel);
            AddCol(1, GridUnitType.Star);
            AddCol(2, GridUnitType.Star);
            AddCol(72, GridUnitType.Pixel);
            AddCol(72, GridUnitType.Pixel);

            bn_delete.Content = "删除";
            bn_delete.Click += bn_delete_Click;
            bn_revert.Content = "重置";
            bn_revert.Click += bn_revert_Click;
            tb_id.IsEnabled = false;
            tb_id.TextChanged += tb_name_TextChanged;
            tb_name.TextChanged += tb_name_TextChanged;
            cb_authority.Items.Add("USER");
            cb_authority.Items.Add("ADMIN");
            if (authory != UserEx.EAuthority.SUPERADMIN) cb_authority.IsEnabled = false;
            cb_authority.SelectionChanged += cb_authority_SelectionChanged;
            lb_status.HorizontalContentAlignment = HorizontalAlignment.Center;

            Children.Add(bn_revert);
            Children.Add(bn_delete);
            Children.Add(tb_id);
            Children.Add(tb_name);
            Children.Add(cb_authority);
            Children.Add(lb_status);

            SetColumn(bn_revert, 0);
            SetColumn(bn_delete, 1);
            SetColumn(tb_id, 2);
            SetColumn(tb_name, 3);
            SetColumn(cb_authority, 4);
            SetColumn(lb_status, 5);

            update();
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

        void cb_authority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            update(false);
        }

        void tb_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            update(false);
        }

        public override void update(bool moduleToView = true)
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
                bn_revert.IsEnabled = Status != EGridStatus.无变化 && RealEntity!=null;
                if (Holder!=null) Holder.Filter(((MainWindow)App.Current.MainWindow).ftb_user.Status);
            }
            else
            {
                Entity.Id = tb_id.Text;
                Entity.Name = tb_name.Text;
                Entity.Authority = (UserEx.EAuthority)cb_authority.SelectedIndex;
                UpdateLabel(lb_status);
                bn_revert.IsEnabled = Status != EGridStatus.无变化 && RealEntity != null;
                if (Holder != null) Holder.Filter(((MainWindow)App.Current.MainWindow).ftb_user.Status);
            }
            updating = false;
        }

        public override bool RealEqual()
        {
            return RealEntity.Id == Entity.Id && RealEntity.Name == Entity.Name && RealEntity.Authority == Entity.Authority;
        }

        public override void UpdateSuccess()
        {
            if (Status == EGridStatus.新建)
            {
                tb_id.IsEnabled = false;
            }
            base.UpdateSuccess();
        }
    }
}
