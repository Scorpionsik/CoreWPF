using System;
using System.Threading.Tasks;

namespace CoreWPF.Utitltes.Event
{
    class EventUnit
    {
        public int Id { get; private set; }

        private event Action<EventUnit> finish;
        public Action<EventUnit> Finish
        {
            get { return this.finish; }
            set
            {
                this.finish = new Action<EventUnit>(value);
            }
        }

        private readonly Task job;

        public EventUnit(int id, Task job)
        {
            this.Id = id;
            this.job = job;
        }

        public async void Start()
        {
            await Task.Run(()=> { this.job.Start(); });
            await Task.Run(() => { this.Finish?.Invoke(this); });
        }
    }
}
