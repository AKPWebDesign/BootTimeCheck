using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootTimeCheck
{
    class StartupEvent : IEvent
    {
        public StartupEvent(DateTime bootDateTime, int bootTime, int mainPath, int postBoot, int priority)
        {
            this.bootDateTime = bootDateTime;
            this.bootTime = bootTime;
            this.mainPath = mainPath;
            this.postBoot = postBoot;
            this.priority = priority;
        }

        DateTime IEvent.DateTime()
        {
            return this.bootDateTime;
        }

        int IEvent.BootTime()
        {
            return this.bootTime;
        }


        int IEvent.Priority()
        {
            return this.priority;
        }

        int IEvent.MainPath()
        {
            return this.mainPath;
        }

        int IEvent.PostBoot()
        {
            return this.postBoot;
        }

        public DateTime bootDateTime { get; set; }

        public int priority { get; set; }

        public int bootTime { get; set; }

        public int mainPath { get; set; }

        public int postBoot { get; set; }
    }
}
