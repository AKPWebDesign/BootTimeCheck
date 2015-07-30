using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BootTimeCheck
{
    public class EventLogInfoReader
    {
        public EventLogInfoReader()
        {

        }

        public List<IEvent> ReadEventLogData() {
            List<IEvent> coll = new List<IEvent>();
            string queryString = 
                "<QueryList>" +
                "  <Query Id='0' Path='Microsoft-Windows-Diagnostics-Performance/Operational'>" +
                "    <Select Path='Microsoft-Windows-Diagnostics-Performance/Operational'>*[System[(EventID=100)]]</Select>" +
                "  </Query>" +
                "</QueryList>"; 
            try
            {
                EventLogQuery eventsQuery = new EventLogQuery("System", PathType.LogName, queryString);
                EventLogReader logReader = new EventLogReader(eventsQuery);

                //create an array of strings for xpath enum.
                String[] xPathRefs = new String[4];
                xPathRefs[0] = "Event/EventData/Data[@Name='BootEndTime']";
                xPathRefs[1] = "Event/EventData/Data[@Name='BootTime']";
                xPathRefs[2] = "Event/EventData/Data[@Name='MainPathBootTime']";
                xPathRefs[3] = "Event/EventData/Data[@Name='BootPostBootTime']";
                IEnumerable<String> xPathEnum = xPathRefs;
                EventLogPropertySelector logPropertyContext = new EventLogPropertySelector(xPathEnum);

                for (EventRecord eventDetail = logReader.ReadEvent(); eventDetail != null; eventDetail = logReader.ReadEvent())
                {
                    IList<object> logEventProps;
                    // Cast the EventRecord into an EventLogRecord to retrieve property values.
                    // This will fetch the event properties we requested through the
                    // context created by the EventLogPropertySelector
                    logEventProps = ((EventLogRecord)eventDetail).GetPropertyValues(logPropertyContext);
                    if (eventDetail.Id == 100)
                    {
                        int boot = Convert.ToInt32(logEventProps[1]);
                        int mainPath = Convert.ToInt32(logEventProps[2]);
                        int postBoot = Convert.ToInt32(logEventProps[3]);
                        DateTime date = (DateTime)logEventProps[0];
                        coll.Add(new StartupEvent(date, boot, mainPath, postBoot, (int)eventDetail.Level));
                    }
                }
            }
            catch (EventLogNotFoundException e)
            {
                Console.WriteLine("Error while reading the event logs");
            }
            return coll;
        }
    }
}
