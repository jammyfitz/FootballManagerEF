using System.Collections.Generic;

namespace FootballManagerEF.Events
{
    public static class EventManager<T>
    {
        private static Dictionary<string, GenericEventHandler<T>> events =
                new Dictionary<string, GenericEventHandler<T>>();

        public static void AddEvent(string name, GenericEventHandler<T> handler)
        {
            if (!events.ContainsKey(name))
                events.Add(name, handler);
        }
        public static void RaiseEvent(string name, object sender, GenericEventArgs<T> args)
        {
            if (events.ContainsKey(name) && events[name] != null)
                events[name](sender, args);
        }
        public static void RegisterEvent(string name, GenericEventHandler<T> handler)
        {
            if (events.ContainsKey(name))
                events[name] += handler;
        }
    }
}
