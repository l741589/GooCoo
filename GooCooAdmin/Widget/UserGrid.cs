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
        private PasswordBox pb_pw = new PasswordBox();
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
            pb_pw.PasswordChanged += pb_pw_PasswordChanged;
            cb_authority.Items.Add("USER");
            cb_authority.Items.Add("ADMIN");
            if (authory != UserEx.EAuthority.SUPERADMIN) cb_authority.IsEnabled = false;
            cb_authority.SelectionChanged += cb_authority_SelectionChanged;
            lb_status.HorizontalContentAlignment = HorizontalAlignment.Center;

            Children.Add(bn_revert);
            Children.Add(bn_delete);
            Children.Add(tb_id);
            Children.Add(tb_name);
            Children.Add(pb_pw);
            Children.Add(cb_authority);
            Children.Add(lb_status);

            SetColumn(bn_revert, 0);
            SetColumn(bn_delete, 1);
            SetColumn(tb_id, 2);
            SetColumn(tb_name, 3);
            SetColumn(pb_pw, 4);
            SetColumn(cb_authority, 5);
            SetColumn(lb_status, 6);
            update();
        }

        

        public UserGrid(UserEx entity, UserEx.EAuthority? authory = null)
            : base(entity)
        {
            AddCol(36, GridUnitType.Pixel);
            AddCol(36, GridUnitType.Pixel);
            AddCol(1, GridUnitType.Star);
            AddCol(2, GridUnitType.Star);
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
            pb_pw.PasswordChanged += pb_pw_PasswordChanged;
            cb_authority.Items.Add("USER");
            cb_authority.Items.Add("ADMIN");
            if (authory != UserEx.EAuthority.SUPERADMIN) cb_authority.IsEnabled = false;
            cb_authority.SelectionChanged += cb_authority_SelectionChanged;
            lb_status.HorizontalContentAlignment = HorizontalAlignment.Center;

            Children.Add(bn_revert);
            Children.Add(bn_delete);
            Children.Add(tb_id);
            Children.Add(tb_name);
            Children.Add(pb_pw);
            Children.Add(cb_authority);
            Children.Add(lb_status);

            SetColumn(bn_revert, 0);
            SetColumn(bn_delete, 1);
            SetColumn(tb_id, 2);
            SetColumn(tb_name, 3);
            SetColumn(pb_pw, 4);
            SetColumn(cb_authority, 5);
            SetColumn(lb_status, 6);
            update();
        }

        async void pb_pw_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Status == EGridStatus.新建 || Status == EGridStatus.新建并删除 || pb_pw.Password==null || pb_pw.Password=="")
            {
                update(false);
            }else
            if (await (App.Current.MainWindow as MainWindow).Grant(Entity)) update(false);
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
                if (Entity.Authority == UserEx.EAuthority.SUPERADMIN)
                {
                    if (cb_authority.Items.Count<3) 
                        cb_authority.Items.Add("SUPER");
                    cb_authority.SelectedIndex = 2;
                    cb_authority.IsEnabled = false;
                }
                else
                {
                    cb_authority.SelectedIndex = (int)Entity.Authority;
                    cb_authority.IsEnabled = true;
                }
                if (Deleted) bn_delete.Content = "恢复";
                else bn_delete.Content = "删除";
                if (Entity.Password != null) pb_pw.Password = Entity.Password;
                UpdateLabel(lb_status);
                bn_revert.IsEnabled = Status != EGridStatus.无变化 && RealEntity!=null;
                if (Holder!=null) Holder.Filter(((MainWindow)App.Current.MainWindow).ftb_user.Status);
            }
            else
            {
                Entity.Id = tb_id.Text;
                Entity.Name = tb_name.Text;
                if (cb_authority.SelectedIndex == 2)
                {
                    Entity.Authority = UserEx.EAuthority.SUPERADMIN;                    
                }
                else
                {
                    Entity.Authority = (UserEx.EAuthority)cb_authority.SelectedIndex;
                }
                if (pb_pw.Password != null && pb_pw.Password != "")
                {
                    if (Status == EGridStatus.新建 || Status == EGridStatus.新建并删除 || (App.Current.MainWindow as MainWindow).Granted(Entity))
                    {
                        Entity.Password = pb_pw.Password;
                    }
                }
                else
                {
                    Entity.Password = null;
                }
                UpdateLabel(lb_status);
                bn_revert.IsEnabled = Status != EGridStatus.无变化 && RealEntity != null;
                if (Holder != null) Holder.Filter(((MainWindow)App.Current.MainWindow).ftb_user.Status);
            }
            lb_status.ToolTip = Entity.ToString();
            updating = false;
        }

        public override bool RealEqual()
        {
            return RealEntity.Id == Entity.Id && RealEntity.Name == Entity.Name && RealEntity.Authority == Entity.Authority && RealEntity.Password==Entity.Password;
        }

        public override void UpdateSuccess()
        {
            Entity.Password = null;
            if (Status == EGridStatus.新建)
            {
                tb_id.IsEnabled = false;
            }
            base.UpdateSuccess();
        }
    }
}
