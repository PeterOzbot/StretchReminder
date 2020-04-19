using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StretchReminder.Core.Events
{
    /// <summary>
    /// Concrete implementation of <see cref="IEventAggregator"/>
    /// </summary>
    public class EventAggregator : IEventAggregator
    {
        private Dictionary<Type, List<Action>> _registeredEvents = new Dictionary<Type, List<Action>>();

        public void Raise<T>() where T : BaseEvent
        {
            if (_registeredEvents.ContainsKey(typeof(T)))
            {
                var registeredActions = _registeredEvents[typeof(T)];
                if (registeredActions != null)
                {
                    foreach (var action in registeredActions)
                    {
                        action();
                    }
                }
            }
        }

        public void Register<T>(Action action) where T : BaseEvent
        {
            if (!_registeredEvents.ContainsKey(typeof(T)))
            {
                _registeredEvents.Add(typeof(T), new List<Action>());
            }
            var registeredActions = _registeredEvents[typeof(T)];
            registeredActions?.Add(action);
        }
    }
}
