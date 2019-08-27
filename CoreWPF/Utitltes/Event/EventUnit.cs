using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoreWPF.Utitltes.Event
{
    class EventUnit
    {
        public int Id { get; private set; }
        //private EventWaitHandle locker;

        private event Action event_appFinish;
        private event Action<EventUnit> event_finish;
        private readonly Task job;

        public Action Event_appFinish
        {
            get { return this.event_appFinish; }
        }

        public EventUnit(int id, Task job, Action<EventUnit> manager_action, Action end_action)
        {
            this.Id = id;
            this.job = job;
            this.event_finish += manager_action;
            this.event_appFinish += end_action;
            //this.locker = new AutoResetEvent(false);
        }

        public async void Start()
        {
            await Task.Run(()=> { this.job.Start(); });
            await Task.Run(() => { this.event_finish?.Invoke(this); });
            //await Task.Run(() => { this.locker.Set(); });
        }

        public void WaitForEvent()
        {
            //this.locker.WaitOne();
        }
    }
}
