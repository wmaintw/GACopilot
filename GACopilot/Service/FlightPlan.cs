using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourFlightInstructor.Service
{
    internal class FlightPlan
    {
        private int amountOfLowPass = 1;
        private int amountOfTouchAndGo = 1;
        private List<FlightStatus> status = new List<FlightStatus>();
        public FlightPlan() 
        {
            status.Add(FlightStatus.COLD_AND_DARK);
            status.Add(FlightStatus.READY_TO_TAXI);
            status.Add(FlightStatus.TAXING_TO_RUNWAY);
            status.Add(FlightStatus.HOLD_SHORT_OF_RUNWAY);
            status.Add(FlightStatus.ENTERING_RUNWAY);
            status.Add(FlightStatus.TAKEOFF_RUN);
            status.Add(FlightStatus.LEG1);
            status.Add(FlightStatus.LEG2);
            status.Add(FlightStatus.LEG3);
            status.Add(FlightStatus.LEG4);
            status.Add(FlightStatus.LEG5);
            status.Add(FlightStatus.LOW_PASS);
            status.Add(FlightStatus.LEG1);
            status.Add(FlightStatus.LEG2);
            status.Add(FlightStatus.LEG3);
            status.Add(FlightStatus.LEG4);
            status.Add(FlightStatus.LEG5);
            status.Add(FlightStatus.TOUCH_AND_GO);
            status.Add(FlightStatus.LEG1);
            status.Add(FlightStatus.LEG2);
            status.Add(FlightStatus.LEG3);
            status.Add(FlightStatus.LEG4);
            status.Add(FlightStatus.LEG5);
            status.Add(FlightStatus.EXIT_RUNWAY);
            status.Add(FlightStatus.TAXING_TO_PARKING_LOT);
            status.Add(FlightStatus.SHUTDOWN);
        }

        public FlightPlan(int amountOfLowPass, int amountOfTouchAndGo)
        {
            this.amountOfLowPass = amountOfLowPass;
            this.amountOfTouchAndGo = amountOfTouchAndGo;
        }


    }
}
