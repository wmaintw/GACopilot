using System;
using Microsoft.FlightSimulator.SimConnect;
using System.Runtime.InteropServices;
using YourFlightInstructor.Service;
using System.Collections.Generic;
using System.Linq;

namespace YourFlightInstructor.controller
{
    public enum DEFINITION
    { 
        STRING_VARS = 0,
        NUMBER_VARS = 1,
        SUPER = 2,
    }
    public enum REQUEST
    { 
        DUMMY = 0,
        DUMMY2 = 1,
        SUPER = 2,
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct SimDataStringStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public String title;
    };

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct SimDataNumberStruct
    {
        public int isUserSim;
        public int batteryMaster;
        public int avionicsMaster;
        public int alternatorSwitch;
        public double radioHeight;
        public double verticalSpeed;
        public int onAnyRunway;
        public double airspeedTrue;
        public double airspeedIndicated;
        public double touchDownVelocity;
        public double HeadingDegreesGyro;
        public int rpm;
        public double groundSpeed;
        public int engineRunning;
        public int aircraftOnGround;
        public int userInputEnabled;
    }
    public struct SuperStruct
    {
        public SimDataStringStruct stringValues;
        public SimDataNumberStruct numberValues;
    }

    public class SimDataReader
    {
        private SimConnect simConnect;
        private const int WM_USER_SIMCONNECT = 0x0402;
        private static readonly int SIM_DATA_MAX_CAPACITY = 10;
        private List<SimData> recentSimData = new List<SimData>(SIM_DATA_MAX_CAPACITY);
        
        public SimDataReader()
        {
            try
            {
                Console.WriteLine("Create connection");
                simConnect = new SimConnect("YourFlightInstructor", IntPtr.Zero, WM_USER_SIMCONNECT, null, 0);
                simConnect.OnRecvOpen += onRecvOpen;
                simConnect.OnRecvQuit += onRecvQuit;
                simConnect.OnRecvSimobjectData += onRecvSimobjectData;

                recentSimData.Clear();
                recentSimData.Add(new SimData());
            }
            catch (COMException ex) 
            {
                Console.WriteLine("Error! " + ex.Message);
            }
        }

        private void onRecvSimobjectData(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA data)
        {
            Console.WriteLine("data.dwRequestID: " + data.dwRequestID);

            if (data.dwRequestID == (uint)REQUEST.SUPER) 
            {
                SuperStruct parsedSimData = (SuperStruct)data.dwData[0];
                SimData latestSimData = new SimData();
                latestSimData.AircraftTitle = parsedSimData.stringValues.title;
                latestSimData.IsUserSim = parsedSimData.numberValues.isUserSim == 1;
                latestSimData.ElectricalMasterBattery = parsedSimData.numberValues.batteryMaster == 1;
                latestSimData.AvionicsMasterSwitch = parsedSimData.numberValues.avionicsMaster == 1;
                latestSimData.AlternatorSwitch = parsedSimData.numberValues.alternatorSwitch == 1;
                latestSimData.RadioAltitude = parsedSimData.numberValues.radioHeight;
                latestSimData.VerticalSpeed = parsedSimData.numberValues.verticalSpeed;
                latestSimData.OnAnyRunway = parsedSimData.numberValues.onAnyRunway == 1;
                latestSimData.AirspeedTrue = parsedSimData.numberValues.airspeedTrue;
                latestSimData.AirspeedIndicated = parsedSimData.numberValues.airspeedIndicated;
                latestSimData.TouchDownVelocity = parsedSimData.numberValues.touchDownVelocity;
                latestSimData.HeadingDegreesGyro = parsedSimData.numberValues.HeadingDegreesGyro;
                latestSimData.RPM = parsedSimData.numberValues.rpm;
                latestSimData.GroundSpeed = parsedSimData.numberValues.groundSpeed;
                latestSimData.EngineRunning = parsedSimData.numberValues.engineRunning == 1;
                latestSimData.AircraftOnGround = parsedSimData.numberValues.aircraftOnGround == 1;
                latestSimData.UserInputEnabled = parsedSimData.numberValues.userInputEnabled == 1;

                saveToSimDataStorage(latestSimData);
                Console.WriteLine(latestSimData.ToValueString());
            }
        }

        private void saveToSimDataStorage(SimData latestSimData)
        {
            if (recentSimData.Count() == SIM_DATA_MAX_CAPACITY)
            {
                recentSimData.RemoveAt(0);
            }
            recentSimData.Add(latestSimData);
        }

        private void onRecvQuit(SimConnect sender, SIMCONNECT_RECV data)
        {
            Console.WriteLine("Close connection");
            if (simConnect != null)
            {
                simConnect.Dispose();
                simConnect = null;
                Console.WriteLine("Closed");
            }
        }

        private void onRecvOpen(SimConnect sender, SIMCONNECT_RECV_OPEN data)
        {
            Console.WriteLine("Connected to MSFS 2020");

            simConnect.AddToDataDefinition(DEFINITION.SUPER, "TITLE", null, SIMCONNECT_DATATYPE.STRING256, 0, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITION.SUPER, "IS USER SIM", "Bool", SIMCONNECT_DATATYPE.INT32, 0, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITION.SUPER, "ELECTRICAL MASTER BATTERY", "Bool", SIMCONNECT_DATATYPE.INT32, 0, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITION.SUPER, "AVIONICS MASTER SWITCH:1", null, SIMCONNECT_DATATYPE.INT32, 0, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITION.SUPER, "GENERAL ENG MASTER ALTERNATOR", null, SIMCONNECT_DATATYPE.INT32, 0, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITION.SUPER, "RADIO HEIGHT", "feet", SIMCONNECT_DATATYPE.FLOAT64, 0, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITION.SUPER, "VERTICAL SPEED", "feet per second", SIMCONNECT_DATATYPE.FLOAT64, 0, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITION.SUPER, "ON ANY RUNWAY", "Bool", SIMCONNECT_DATATYPE.INT32, 0, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITION.SUPER, "AIRSPEED TRUE", "Knots", SIMCONNECT_DATATYPE.FLOAT64, 0, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITION.SUPER, "AIRSPEED INDICATED", "Knots", SIMCONNECT_DATATYPE.FLOAT64, 0, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITION.SUPER, "PLANE TOUCHDOWN NORMAL VELOCITY", "feet per second", SIMCONNECT_DATATYPE.FLOAT64, 0, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITION.SUPER, "PLANE HEADING DEGREES GYRO", "Degrees", SIMCONNECT_DATATYPE.FLOAT64, 0, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITION.SUPER, "PROP RPM:1", "RPM", SIMCONNECT_DATATYPE.INT32, 0, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITION.SUPER, "GROUND VELOCITY", "Knots", SIMCONNECT_DATATYPE.FLOAT64, 0, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITION.SUPER, "ENG COMBUSTION:1", "Bool", SIMCONNECT_DATATYPE.INT32, 0, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITION.SUPER, "SIM ON GROUND", "Bool", SIMCONNECT_DATATYPE.INT32, 0, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITION.SUPER, "USER INPUT ENABLED", "Bool", SIMCONNECT_DATATYPE.INT32, 0, SimConnect.SIMCONNECT_UNUSED);

            simConnect.RegisterDataDefineStruct<SuperStruct>(DEFINITION.SUPER);
            simConnect.RequestDataOnSimObject(REQUEST.SUPER, DEFINITION.SUPER, SimConnect.SIMCONNECT_OBJECT_ID_USER, SIMCONNECT_PERIOD.SECOND, SIMCONNECT_DATA_REQUEST_FLAG.DEFAULT, 0, 0, 0);
        }

        internal string TriggerReadSimData()
        {
            if (simConnect != null)
            {
                Console.WriteLine("Trigger RequestId to SIM");
                try
                {
                    simConnect.ReceiveMessage();
                    Console.WriteLine("Triggered");
                } catch (Exception e) 
                {
                    Console.WriteLine(e.ToString());
                }
                return "RequestId sent to Sim";
            }
            else
            {
                return "SimConnection not created";
            }
        }

        internal void stop()
        {
            Console.WriteLine("Determine if need to stop connection");
            if (simConnect != null)
            { 
                Console.WriteLine("Try to close connection");
                simConnect.Dispose();
            }
        }

        internal SimData GetLatestSimData()
        {
            return recentSimData.Last<SimData>();
        }

        internal List<SimData> GetRecentSimData()
        {
            return recentSimData.ToList<SimData>();
        }
    }
}