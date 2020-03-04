using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSWithEvents
{
    public interface IHeater
    {
        void TurnOn();
        void TurnOff();
    }
}
