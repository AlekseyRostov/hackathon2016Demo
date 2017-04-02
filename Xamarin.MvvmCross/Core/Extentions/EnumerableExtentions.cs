using System.Collections;

namespace Feedback.Core.Extentions
{
    public static class EnumerableExtentions
    {
        public static int Count(this IEnumerable enumerable)
        {
            if(enumerable == null) return 0;

            var itemsList = enumerable as ICollection;
            if(itemsList != null)
            {
                return itemsList.Count;
            }

            var enumerator = enumerable.GetEnumerator();
            var count = 0;
            while(enumerator.MoveNext())
            {
                count++;
            }

            return count;
        }

        public static int GetPosition(this IEnumerable items, object item)
        {
            if(items == null) return -1;

            var itemsList = items as IList;
            if(itemsList != null) return itemsList.IndexOf(item);

            var enumerator = items.GetEnumerator();
            for(var i = 0;; i++)
            {
                if(!enumerator.MoveNext()) return -1;

                if(enumerator.Current == item) return i;
            }
        }

        public static System.Object ElementAt(this IEnumerable items, int position)
        {
            if(items == null) return null;

            var itemsList = items as IList;
            if(itemsList != null) return itemsList[position];

            var enumerator = items.GetEnumerator();
            for(var i = 0; i <= position; i++)
            {
                enumerator.MoveNext();
            }

            return enumerator.Current;
        }

        public static int IndexOf(this IEnumerable items, object obj)
        {
            var itemsList = items as IList;
            if(itemsList != null)
            {
                return itemsList.IndexOf(obj);
            }

            int i = 0;
            foreach(var item in items)
            {
                if(item == obj)
                    return i;
                i++;
            }
            return -1;
        }
    }
}