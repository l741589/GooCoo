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
    public interface IInfoGrid
    {
        bool RealEqual();
        void update(bool moduleToView = true);
        void Revert(bool includeNew = false);
        //IHolder Holder { get; }
        int Index { get; }
        EGridStatus Status { get; }
    }

    public abstract class InfoGrid<T> : Grid,IInfoGrid
    {
        private T realEntity;
        public T RealEntity { get { return realEntity; } }
        public T Entity { get; set; }
        public bool Deleted { get; set; }
        public bool MarkToRemove { get; set; }

        public InfoGrid()
        {
            RowDefinitions.Add(new RowDefinition());
            Entity = (T)typeof(T).GetConstructor(new Type[0]).Invoke(new object[0]);
            realEntity = default(T);
            Margin = new Thickness(0.5);
            MarkToRemove = false;
        }

        public InfoGrid(T entity)
        {
            RowDefinitions.Add(new RowDefinition());
            Entity = Util.CloneEntity<T>(entity);
            realEntity = Util.CloneEntity<T>(entity);
            Margin = new Thickness(0.5);
            MarkToRemove = false;
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
        public abstract void update(bool moduleToView = true);

        public void Revert(bool includeNew = false){
            if (includeNew && (Status == EGridStatus.新建 || Status == EGridStatus.新建并删除)) Holder.Remove(this);
            else if (RealEntity!=null) Entity = Util.CloneEntity(RealEntity);
            if (Deleted) Deleted = false;
            update();
        }

        public EGridStatus Status
        {
            get
            {
                if (Deleted && RealEntity == null) return EGridStatus.新建并删除;
                if (RealEntity == null) return EGridStatus.新建;
                if (Deleted) return EGridStatus.已删除;
                if (RealEqual()) return EGridStatus.无变化;
                return EGridStatus.已修改;
            }
        }

        public void UpdateLabel(Label lb){
            lb.Content = Status;
            switch (Status)
            {
                case EGridStatus.无变化: 
                    lb.Background = Util.GetBrush(Properties.Settings.Default.CL_NOCHANGE);
                    lb.Foreground = Util.GetBrush(Properties.Settings.Default.CL_BLACK);
            	    break;
                case EGridStatus.已删除:
                    lb.Background = Util.GetBrush(Properties.Settings.Default.CL_DELETED);
                    lb.Foreground = Util.GetBrush(Properties.Settings.Default.CL_WHITE);
                    break;
                case EGridStatus.新建:
                    lb.Background = Util.GetBrush(Properties.Settings.Default.CL_NEW);
                    lb.Foreground = Util.GetBrush(Properties.Settings.Default.CL_BLACK);
                    break;
                case EGridStatus.已修改:
                    lb.Background = Util.GetBrush(Properties.Settings.Default.CL_CHANGED);
                    lb.Foreground = Util.GetBrush(Properties.Settings.Default.CL_WHITE);
                    break;
                case EGridStatus.新建并删除:
                    lb.Background = Util.GetBrush(Properties.Settings.Default.CL_DELNEW);
                    lb.Foreground = Util.GetBrush(Properties.Settings.Default.CL_BLACK);
                    break;
            }
        }

        virtual public void UpdateSuccess()
        {
            switch (Status)
            {
                case EGridStatus.无变化:break;
                case EGridStatus.已删除:
                case EGridStatus.新建并删除: MarkToRemove = true; break;
                case EGridStatus.新建:
                case EGridStatus.已修改: realEntity = Util.CloneEntity(Entity); break;
            }
            update();
        }
    }
}
