using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootTimeCheck
{
    public interface IEvent
    {
        DateTime DateTime();
        int BootTime();
        int MainPath();
        int PostBoot();
        int Priority();
    }
}
