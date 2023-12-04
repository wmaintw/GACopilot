using YourFlightInstructor.Service;
using System;
using System.Speech.Synthesis;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace YourFlightInstructor.controller
{
    internal class Controller
    {
        private MainUI mainForm;
        private bool continueToRun = false;
        private String lastMessage = "";

        public Controller(MainUI form)
        {
            mainForm = form;
            continueToRun = false;
        }

        internal void start()
        {
            SimDataReader reader = new SimDataReader();
            SimAnalyzer analyzer = new SimAnalyzer();
            continueToRun = true;
            while (continueToRun) 
            {
                reader.TriggerReadSimData();
                List<SimData> simDataList = reader.GetRecentSimData();

                if (simDataList.Count() == 0)
                {
                    sleep(1000);
                    continue;
                }
                SimData latestSimData = simDataList.Last<SimData>();
                updateMainUI(latestSimData);

                String message = analyzer.DetermineScriptsBaseOnAircraftStatus(simDataList);
                if (!message.Equals(lastMessage))
                {
                    callout(message);
                    lastMessage = message;
                }
                sleep(500);
            }

            if (continueToRun == false) 
            {
                reader.stop();
            }
        }

        private void callout(String message)
        {
            using (SpeechSynthesizer synth = new SpeechSynthesizer())
            {
                synth.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Senior);
                synth.Rate = 2;
                synth.Speak(message);
            }
        }

        private void updateMainUI(SimData simData) 
        {
            mainForm.UpdateData(simData);
        }

        internal void stop()
        {
            continueToRun = false;
        }

        private void sleep(int seconds)
        {
            Thread.Sleep(seconds);
        }
    }
}
