using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseControl.Sunset
{
    interface ISunsetProvider
    {
        DateTime GetSunset(DateTime date);
    }
}
