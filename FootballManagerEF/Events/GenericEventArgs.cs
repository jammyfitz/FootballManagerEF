using System;

namespace FootballManagerEF.Events
{
    public delegate void GenericEventHandler<T>(object sender, GenericEventArgs<T> args);

    public class GenericEventArgs<T> : EventArgs
    {
        public T Item { get; set; }

        public GenericEventArgs(T item)
        {
            Item = item;
        }
    }
}
