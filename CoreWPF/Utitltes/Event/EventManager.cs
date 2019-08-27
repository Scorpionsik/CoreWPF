using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CoreWPF.Utitltes.Event
{
    public static class EventManager
    {
        private static Dictionary<int, EventUnit> events;
        public static Mutex IsWork { get; private set; }

        static EventManager()
        {
            EventManager.events = new Dictionary<int, EventUnit>();
            EventManager.IsWork = new Mutex();
        }

        public static int Start(Task job)
        {
            int curr_id = 0;
            while (EventManager.events.ContainsKey(curr_id)) curr_id++;
            if (EventManager.events.Count == 0) EventManager.IsWork.WaitOne();

            EventUnit tmp = new EventUnit(curr_id, job);
            tmp.Finish += new System.Action<EventUnit>(EventManager.Delete);
            EventManager.events.Add(curr_id, tmp);
            EventManager.events[curr_id].Start();
            return curr_id;
        }

        private static void Delete(EventUnit unit)
        {
            EventManager.events.Remove(unit.Id);
            if (EventManager.events.Count == 0) EventManager.IsWork.ReleaseMutex();
        }
    }
}
