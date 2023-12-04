using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourFlightInstructor.Service
{
    public enum FlightStatus
    {
        COLD_AND_DARK,
        READY_TO_TAXI,
        TAXING_TO_RUNWAY,
        HOLD_SHORT_OF_RUNWAY,
        ENTERING_RUNWAY,
        TAKEOFF_RUN,
        LEG1, LEG2, LEG3, LEG4, LEG5,
        LOW_PASS,
        TOUCH_AND_GO,
        EXIT_RUNWAY,
        TAXING_TO_PARKING_LOT,
        SHUTDOWN
    }
}
