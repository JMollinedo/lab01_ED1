using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListasArtesanales
{
    class LinkList<T> : LinkedList<T> where T : IComparable
    {
        private void SortAsArray(T[] array, int floor, int top)
        {

            int start = floor;
            int end = top;
            int mid = (floor + top) / 2;

            T Aux = default(T);
            T Pivot = array[mid];
            do
            {
                while (array[start].CompareTo(Pivot) < 0) start++;
                while (array[end].CompareTo(Pivot) > 0) end--;
                if (start <= end)
                {
                    Aux = array[start];
                    array[start] = array[end];
                    array[end] = Aux;
                    start++;
                    end--;
                }
            } while (start <= end);
            if (floor < end)
            {
                SortAsArray(array, floor, end);
            }
            if (start < top)
            {
                SortAsArray(array, start, top);
            }
        }

        public void Sort()
        {
            if (Count > 0)
            {
                T[] array = this.ToArray();
                SortAsArray(array, 0, Count - 1);
                Clear();
                foreach (T data in array)
                {
                    AddLast(data);
                }
            }
        }

        public void RemoveAll(T Value)
        {
            while (Contains(Value))
            {
                Remove(Value);
            }
        }

        public LinkList<T> Search(T Value)
        {
            LinkList<T> list = new LinkList<T>();
            foreach (T item in this)
            {
                if (item.Equals(Value))
                {
                    list.AddLast(item);
                }
            }
            return list;
        }
    }
}
