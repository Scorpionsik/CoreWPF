using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CoreWPF.Utitltes.Event
{
    public static class EventMaker
    {
        private static Dictionary<int, EventUnit> events;
        private static EventWaitHandle locker;

        static EventMaker()
        {
            EventMaker.events = new Dictionary<int, EventUnit>();
            EventMaker.locker = new AutoResetEvent(false);
        }

        public static int Start(Task job, Action end_action = null)
        {
            int curr_id = 0;
            while (EventMaker.events.ContainsKey(curr_id)) curr_id++;

            EventUnit tmp = new EventUnit(curr_id, job, EventMaker.Delete, end_action);
            EventMaker.events.Add(curr_id, tmp);
            EventMaker.events[curr_id].Start();
            return curr_id;
        }

        private static void Delete(EventUnit unit)
        {
            unit.Event_appFinish?.Invoke();
            EventMaker.events.Remove(unit.Id);
            //if (EventMaker.events.Count == 0) EventMaker.locker.Set();
        }

        public static void WaitForEvent(int id)
        {
            if (EventMaker.events.ContainsKey(id)) EventMaker.events[id].WaitForEvent();
        }
    }
}
