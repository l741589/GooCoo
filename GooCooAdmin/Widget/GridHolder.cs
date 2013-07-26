using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using GooCooServer.Entity.Ex;

namespace GooCooAdmin.Widget
{
    class GridHolder<T> : StackPanel
    {
        public GridHolder()
        {
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
                int index = Children.Add(new UserGrid(entity as UserEx));
                return this[index];
            }
            else if (entity is BookEx)
            {
                //int index = Children.Add(new BookdGrid(entity as BookEx));
                //dreturn this[index];
            }
            return null;
        }

        public InfoGrid<T> Add(Type type)
        {
            if (type.Equals(typeof(UserEx)))
            {
                int index = Children.Add(new UserGrid());
                return this[index];
            }
            else if (type.Equals(typeof(BookEx)))
            {
                //int index = Children.Add(new BookdGrid(entity as BookEx));
                //dreturn this[index];
            }
            return null;

        }
    }
}
