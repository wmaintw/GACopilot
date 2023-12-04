using System;

namespace YourFlightInstructor.Service
{
    public class SimData
    {
        double groundSpeed = 0;
        int rpm = 0;
        string aircraftTitle = "";
        bool isUserSim = false;
        bool onAnyRunway = false;
        double airspeedTrue = -1;
        double airspeedIndicated = -1;
        bool avionicsMasterSwitch = false;
        bool electricalMasterBattery = false;
        bool alternatorSwitch = false;
        double radioAltitude = -1;
        double verticalSpeed = 0;
        double touchDownVelocity = 0;
        double headingDegreesGyro;
        bool engineRunning = false;
        bool aircraftOnGround = false;
        bool userInputEnabled = false;

        public double VerticalSpeed { get => verticalSpeed; set => verticalSpeed = value; }
        public double RadioAltitude { get => radioAltitude; set => radioAltitude = value; }
        public bool ElectricalMasterBattery { get => electricalMasterBattery; set => electricalMasterBattery = value; }
        public bool AvionicsMasterSwitch { get => avionicsMasterSwitch; set => avionicsMasterSwitch = value; }
        public double AirspeedTrue { get => airspeedTrue; set => airspeedTrue = value; }
        public double AirspeedIndicated { get => airspeedIndicated; set => airspeedIndicated = value; }
        public bool OnAnyRunway { get => onAnyRunway; set => onAnyRunway = value; }
        public string AircraftTitle { get => aircraftTitle; set => aircraftTitle = value; }
        public bool IsUserSim { get => isUserSim; set => isUserSim = value; }
        public bool AlternatorSwitch { get => alternatorSwitch; internal set => alternatorSwitch = value; }
        public double TouchDownVelocity { get => touchDownVelocity; internal set => touchDownVelocity = value; }
        public double HeadingDegreesGyro { get => headingDegreesGyro; internal set => headingDegreesGyro = value; }
        public bool EngineRunning { get => engineRunning; set => engineRunning = value; }
        public int RPM { get => rpm; set => rpm = value; }
        public double GroundSpeed { get => groundSpeed; set => groundSpeed = value; }
        public bool AircraftOnGround { get => aircraftOnGround; set => aircraftOnGround = value; }
        public bool UserInputEnabled { get => userInputEnabled; internal set => userInputEnabled = value; }
        

        public string ToValueString()
        {
            return
                "isUserSim: " + isUserSim +
                "\r\nuserInputEnabled: " + userInputEnabled +
                "\r\naircraftTitle: " + AircraftTitle +
                "\r\naircraftOnGround: " + AircraftOnGround +
                "\r\nonAnyRunway: " + onAnyRunway +
                "\r\navionicsMasterSwitch: " + avionicsMasterSwitch +
                "\r\nelectricalMasterBattery: " + electricalMasterBattery +
                "\r\nalternatorSwitch: " + alternatorSwitch +
                "\r\nRPM: " + rpm +
                "\r\nengineRunning: " + engineRunning +
                "\r\nairspeedTrue: " + airspeedTrue +
                "\r\nairspeedIndicated: " + airspeedIndicated +
                "\r\ngroundSpeed: " + groundSpeed +
                "\r\nradioAltitude: " + radioAltitude +
                "\r\nverticalSpeed: " + verticalSpeed +
                "\r\ntouchDownVelocity: " + touchDownVelocity +
                "\r\nheadingDegreesGyro: " + headingDegreesGyro;
        }
    }
}
