namespace Bzway.Data.Core
{
    public class PagedList<T>
    {
        private int count;
        private System.Collections.Generic.List<T> list;
        private int take;
        private int v;

        public PagedList(System.Collections.Generic.List<T> list, int v, int take, int count)
        {
            this.list = list;
            this.v = v;
            this.take = take;
            this.count = count;
        }
    }
}