﻿using System;

namespace YourFlightInstructor.Service
{
    public enum SIM_STATUS
    {
        SIM_MAIN,
        AIRCRAFT_LOADING,
        AIRCRAFT_LOADED,
        AIRCRAFT_COLD_AND_DARK,
        AIRCRAFT_READY_TO_TAKEOFF,
        AIRCRAFT_ON_RUNWAY,
        AIRCRAFT_TAKEOFF_SPEED_UP,
        AIRCRAFT_TAKEOFF_AIRBORNE,
        AIRCRAFT_UPWIND,
        AIRCRAFT_UPWIND_TO_CROSSWIND_LEG,
        AIRCRAFT_DOWNWIND_LEG,
        AIRCRAFT_BASE_LEG,
        AIRCRAFT_FINAL_LEG,
        AIRCRAFT_FINAL_APPROACH,
        AIRCRAFT_TOUCH_DOWN,
        AIRCRAFT_1000_HEIGHT,
        AIRCRAFT_500_HEIGHT,
        AIRCRAFT_200_HEIGHT,
        AIRCRAFT_100_HEIGHT,
        AIRCRAFT_50_HEIGHT,
        AIRCRAFT_40_HEIGHT,
        AIRCRAFT_30_HEIGHT,
        AIRCRAFT_20_HEIGHT,
        AIRCRAFT_10_HEIGHT,
    }
}
