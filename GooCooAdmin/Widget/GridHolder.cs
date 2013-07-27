using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using GooCooServer.Entity.Ex;

namespace GooCooAdmin.Widget
{

    public interface IHolder
    {
        void Remove(IInfoGrid grid);
        void Revert(bool includenew = false);
        void Filter(HashSet<EGridStatus> status, IInfoGrid grid = null);
    }

    public class GridHolder<T> : StackPanel,IHolder
    {
        public GridHolder()
        {
            Tag = "Holder";
        }

        public UserEx.EAuthority? Admin { get; set; }

        public InfoGrid<T> this[int i]
        {
            get
            {
                return Children[i] as InfoGrid<T>;
            }
            set
            {
                Children[i] = value;
            }
        }

        public InfoGrid<T> this[T i]
        {
            get
            {
                foreach (var e in Children)
                {
                    if ((e as InfoGrid<T>).Entity.Equals(i)) return e as InfoGrid<T>;
                }
                return null;
            }
        }

        public InfoGrid<T> Add(T entity)
        {
            if (entity is UserEx)
            {
                int index = Children.Add(new UserGrid(entity as UserEx,Admin));
                return this[index];
            }
            else if (entity is BookEx)
            {
                int index = Children.Add(new BookGrid(entity as BookEx));
                return this[index];
            }
            return null;
        }

        public InfoGrid<T> Add(Type type)
        {
            if (type.Equals(typeof(UserEx)))
            {
                int index = Children.Add(new UserGrid(Admin));
                return this[index];
            }
            else if (type.Equals(typeof(BookEx)))
            {
                int index = Children.Add(new BookGrid());
                return this[index];
            }
            return null;
        }

        public void Remove(IInfoGrid grid)
        {
            Children.Remove(grid as UIElement);
        }

        public void Clear()
        {
            Children.Clear();
        }

        public void Revert(bool includenew = false)
        {
            InfoGrid<T>[] tobedel = new InfoGrid<T>[Children.Count];
            Children.CopyTo(tobedel,0);
            foreach (var e in tobedel)
            {
                ((InfoGrid<T>)e).Revert(includenew);
            }
        }

        public void Filter(HashSet<EGridStatus> status, IInfoGrid grid = null)
        {
            if (grid == null)
            {
                foreach (var e in Children)
                {
                    InfoGrid<T> g = e as InfoGrid<T>;
                    if (status.Contains(g.Status)) g.Visibility = System.Windows.Visibility.Visible;
                    else g.Visibility = System.Windows.Visibility.Collapsed;
                }
                InvalidateArrange();
            }
            else
            {
                InfoGrid<T> g = grid as InfoGrid<T>;
                if (status.Contains(g.Status)) g.Visibility = System.Windows.Visibility.Visible;
                else g.Visibility = System.Windows.Visibility.Collapsed;
                InvalidateArrange();
            }
        }

        public void RemoveAllMarked()
        {
            InfoGrid<T>[] tobedel = new InfoGrid<T>[Children.Count];
            Children.CopyTo(tobedel, 0);
            foreach (var e in tobedel)
            {
                if (((InfoGrid<T>)e).MarkToRemove)
                {
                    Children.Remove(e);
                }
            }
        }
    }
}
