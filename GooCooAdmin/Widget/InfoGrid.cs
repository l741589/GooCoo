using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GooCooAdmin.Utility;

namespace GooCooAdmin.Widget
{
    abstract class InfoGrid<T> : Grid
    {
        public enum EStatus { 无变化, 修改, 删除, 新建, 新建并删除 }

        private readonly T realEntity;
        public T RealEntity { get { return realEntity; } }
        public T Entity { get; set; }
        public bool Deleted { get; set; }

        public InfoGrid()
        {
            RowDefinitions.Add(new RowDefinition());
            Entity = (T)typeof(T).GetConstructor(new Type[0]).Invoke(new object[0]);
            realEntity = default(T);
            Margin = new Thickness(0.5);
        }

        public InfoGrid(T entity)
        {
            RowDefinitions.Add(new RowDefinition());
            Entity = Util.CloneEntity<T>(entity);
            realEntity = Util.CloneEntity<T>(entity);
            Margin = new Thickness(0.5);
        }

        public GridHolder<T> Holder
        {
            get { return Parent as GridHolder<T>; }
        }

        public int Index
        {
            get
            {
                return Holder.Children.IndexOf(this);
            }
        }

        protected void AddCol(double value, GridUnitType type)
        {
            ColumnDefinition cd;
            cd = new ColumnDefinition();
            cd.Width = new GridLength(value, type);
            ColumnDefinitions.Add(cd);
        }

        public abstract bool RealEqual();

        public EStatus Status
        {
            get
            {
                if (Deleted && RealEntity == null) return EStatus.新建并删除;
                if (RealEntity == null) return EStatus.新建;
                if (Deleted) return EStatus.删除;
                if (RealEqual()) return EStatus.无变化;
                return EStatus.修改;
            }
        }

        public void UpdateLabel(Label lb){
            lb.Content = Status;
            switch (Status)
            {
                case EStatus.无变化: 
                    lb.Background = Util.GetBrush(Properties.Settings.Default.CL_NOCHANGE);
                    lb.Foreground = Util.GetBrush(Properties.Settings.Default.CL_BLACK);
            	    break;
                case EStatus.删除:
                    lb.Background = Util.GetBrush(Properties.Settings.Default.CL_DELETED);
                    lb.Foreground = Util.GetBrush(Properties.Settings.Default.CL_WHITE);
                    break;
                case EStatus.新建:
                    lb.Background = Util.GetBrush(Properties.Settings.Default.CL_NEW);
                    lb.Foreground = Util.GetBrush(Properties.Settings.Default.CL_BLACK);
                    break;
                case EStatus.修改:
                    lb.Background = Util.GetBrush(Properties.Settings.Default.CL_CHANGED);
                    lb.Foreground = Util.GetBrush(Properties.Settings.Default.CL_WHITE);
                    break;
                case EStatus.新建并删除:
                    lb.Background = Util.GetBrush(Properties.Settings.Default.CL_DELNEW);
                    lb.Foreground = Util.GetBrush(Properties.Settings.Default.CL_BLACK);
                    break;
            }
        }
    }
}
