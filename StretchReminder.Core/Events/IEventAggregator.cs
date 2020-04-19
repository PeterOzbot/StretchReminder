using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StretchReminder.Core.Events
{
    /// <summary>
    /// Defines event aggregator design pattern.
    /// </summary>
    public interface IEventAggregator
    {
        /// <summary>
        /// Registers the event type and action which should be executed on that event type raise.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        void Register<T>(Action action) where T: BaseEvent;

        /// <summary>
        /// Notifies the event raise which executes the registered actions for this event type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void Raise<T>() where T : BaseEvent;
    }
}
