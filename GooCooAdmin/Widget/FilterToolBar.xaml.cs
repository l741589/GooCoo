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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GooCooAdmin.Widget
{
    /// <summary>
    /// FilterToolBar.xaml 的交互逻辑
    /// </summary>
    public partial class FilterToolBar : ToolBar
    {
        public FilterToolBar()
        {
            InitializeComponent();
            cb_changed.Checked += checkBox_Changed;
            cb_deleted.Checked += checkBox_Changed;
            cb_nochange.Checked += checkBox_Changed;
            cb_delnew.Checked += checkBox_Changed;
            cb_new.Checked += checkBox_Changed;

            cb_changed.Unchecked += checkBox_Changed;
            cb_deleted.Unchecked += checkBox_Changed;
            cb_nochange.Unchecked += checkBox_Changed;
            cb_delnew.Unchecked += checkBox_Changed;
            cb_new.Unchecked += checkBox_Changed;
            
        }



        private void checkBox_Changed(object sender, RoutedEventArgs e)
        {
           
            Holder.Filter(Status);
        }

        IHolder Holder
        {
            get
            {
                return ((App.Current.MainWindow as MainWindow).GetHolder(Tag as String));
            }
        }

       public  HashSet<EGridStatus> Status
        {
            get
            {
                var status = new HashSet<EGridStatus>();
                if (cb_changed.IsChecked == true) status.Add(EGridStatus.已修改);
                if (cb_nochange.IsChecked == true) status.Add(EGridStatus.无变化);
                if (cb_new.IsChecked == true) status.Add(EGridStatus.新建);
                if (cb_deleted.IsChecked == true) status.Add(EGridStatus.已删除);
                if (cb_delnew.IsChecked == true) status.Add(EGridStatus.新建并删除);
                return status;
            }
        }
    }
}

