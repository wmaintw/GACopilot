using YourFlightInstructor.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace YourFlightInstructor.controller
{
    internal class SimAnalyzer
    {
        String message = "";
        internal String DetermineScriptsBaseOnAircraftStatus(List<SimData> simDataList)
        {
            SimData latestSimData = simDataList.First<SimData>();
            message = "";

            if (ColdAndDark(simDataList))
            {
                return message = "你好，我是你的飞行教员，今天我们进行起落航线练习。" +
                    "整个飞行都由你来操纵飞机，不过别担心，我会给你提示。" +
                    "第一步，先启动飞机。";
                //设置系留刹车，打开油箱，油门往前推一点点，设置全富油，打开主电源，打开信标灯和航行灯，接下来给发动机预注油，两次就差不多了。最后确认螺旋桨区域没有障碍物，磁力机启动。
            }

            if (AircraftJustStarted(simDataList))
            {
                return message = "本次练习使用跑道13，做好滑出前的准备之后，就滑行进入跑道吧。";
            }

            if (IsTaxingToRunway(simDataList))
            {
                return message = "检查滑行灯是否打开，可以先放一节襟翼";
            }

            if (AircraftJustEnterRunway(simDataList))
            {
                return message = "进入跑道，先做好起飞前准备，完成之后就全油门起飞";
                //打开着陆灯和频闪灯，确认襟翼位置10度，油箱打开，全富油，配平起飞位
            }

            if (IsTakeOffStage(simDataList))
            {
                if (IsAirspeedAlive(simDataList))
                {
                    return message = "空速40节, 控制好滑跑方向";
                }
                if (IsTakeoffSpeed(simDataList))
                {
                    return message = "55节，抬轮";
                }
                if (IsJustAirborne(simDataList))
                {
                    return message = "离地，调整俯仰姿态，以75节空速爬升";
                }
            }
            else if (IsUpWindLeg(simDataList))
            {
                if (IsInitialClimb(simDataList))
                {
                    return message = "高度300，收襟翼，保持好爬升空速";
                }
                if (IsFirstTurn(simDataList))
                {
                    return message = "高度500，一转弯，继续爬升";
                }
            }
            else if (IsCrossWindLeg(simDataList))
            {
                if (IsTrafficPatternHeight(simDataList))
                {
                    return message = "高度1000，改平飞，保持起落航线高度飞行，油门可以收到2400转";
                }
                if (IsSecondTurn(simDataList))
                {
                    return message = "二转弯，进三边";
                }
            }
            else if (IsDownWindLeg(simDataList))
            {
                if (IsJustEnterDownWindLeg(simDataList))
                {
                    return message = "调整配平，让飞机稳定在起落航线上飞行";
                }
                if (IsCrossOverRunwayEntrance(simDataList))
                {
                    return message = "正切跑道头，计时30秒";
                }
                if (IsSlowDownForApproach(simDataList))
                {
                    return message = "油门收到2000转，往后带点杆，让飞机保持平飞，等待空速下降";
                }
                if (IsDropFlapsOne(simDataList))
                {
                    return message = "放第一节襟翼";
                }
                if (IsThirdTurn(simDataList))
                {
                    return message = "三转弯";
                }
            }
            else if (IsBaseLeg(simDataList))
            {
                if (IsBaseLegApproachConfigured(simDataList))
                {
                    return message = "空速低于85节，放第二节襟翼";
                }
                if (IsFourthTurn(simDataList))
                {
                    return message = "四转弯";
                }
            }
            else if (IsFinalLeg(simDataList))
            {
                if (IsPromptLandingConfiguration(simDataList))
                {
                    return message = "襟翼全放，把空速保持在65节。";
                }
                if (IsJustEnteringFinalLeg(simDataList))
                {
                    return message = "进五边，瞄准跑道入口，判断高距比，按需进行修正";
                }
                if (IsFinalApproachStable(simDataList))
                {
                    return message = "继续进近，循环检查瞄准点，跑道中线，空速";
                }
                if (IsFiveHundredHeight(simDataList))
                {
                    return message = "高度500";
                }
                if (IsTwoHundredHeight(simDataList))
                {
                    return message = "高度200";
                }
                if (IsOneHundredHeight(simDataList))
                {
                    return message = "高度100";
                }
                if (IsFiftyHeight(simDataList))
                {
                    return message = "高度50";
                }
                if (IsThirtyHeight(simDataList))
                {
                    return message = "高度30，柔和收光油门，往后带杆拉平";
                }
                if (IsTouchDown(simDataList))
                {
                    return message = "接地，这次落地速率" + (int)Math.Floor(latestSimData.TouchDownVelocity * 60);
                }
            }

            if (ExitRunway(simDataList))
            {
                return message = "现在减速，从前方滑行道脱离跑道，滑回停机位。";
            }


            if (latestSimData.AircraftOnGround == false && latestSimData.RadioAltitude > 1200)
            {
                message = "注意高度，下降到起落航线高度";
            }

            if (latestSimData.AircraftOnGround == false && (latestSimData.VerticalSpeed * 60) < -1200)
            {
                message = "注意下降率";
            }

            if (latestSimData.AircraftOnGround == false && (latestSimData.AirspeedTrue > 40 && latestSimData.AirspeedTrue < 50))
            {
                message = "注意空速";
            }

            return this.message;
        }

        private bool IsUserAircraftLoaded(SimData latestSimData)
        {
            return latestSimData.IsUserSim
                && latestSimData.AirspeedTrue > 0;
        }

        private bool ExitRunway(List<SimData> simDataList)
        {
            return false;
        }

        private bool IsTouchDown(List<SimData> simDataList)
        {
            SimData latestSimData = simDataList.Last();
            SimData oldestSimData = simDataList.First();
            return latestSimData.AircraftOnGround == true
                && oldestSimData.AircraftOnGround == false
                && latestSimData.OnAnyRunway == true;
        }

        private bool IsThirtyHeight(List<SimData> simDataList)
        {
            SimData latestSimData = simDataList.Last();
            return latestSimData.RadioAltitude < 35
                && latestSimData.RadioAltitude > 25;
        }

        private bool IsFiftyHeight(List<SimData> simDataList)
        {
            SimData latestSimData = simDataList.Last();
            return latestSimData.RadioAltitude < 55
                && latestSimData.RadioAltitude > 45;
        }

        private bool IsOneHundredHeight(List<SimData> simDataList)
        {
            SimData latestSimData = simDataList.Last();
            return latestSimData.RadioAltitude < 110
                && latestSimData.RadioAltitude > 90;
        }

        private bool IsTwoHundredHeight(List<SimData> simDataList)
        {
            SimData latestSimData = simDataList.Last();
            return latestSimData.RadioAltitude < 210
                && latestSimData.RadioAltitude > 190;
        }

        private bool IsFiveHundredHeight(List<SimData> simDataList)
        {
            SimData latestSimData = simDataList.Last();
            return latestSimData.RadioAltitude < 510
                && latestSimData.RadioAltitude > 480;
        }

        private bool IsFinalApproachStable(List<SimData> simDataList)
        {
            SimData latestSimData = simDataList.Last();
            SimData oldestSimData = simDataList.First();
            return latestSimData.RadioAltitude < 500
                && latestSimData.RadioAltitude > 200
                && latestSimData.AirspeedIndicated > 55
                && latestSimData.AirspeedIndicated < 75
                && latestSimData.VerticalSpeed < -200
                && latestSimData.VerticalSpeed > -600;
        }

        private bool IsJustEnteringFinalLeg(List<SimData> simDataList)
        {
            SimData latestSimData = simDataList.Last();
            SimData oldestSimData = simDataList.First();
            return latestSimData.HeadingDegreesGyro > 110
                && latestSimData.HeadingDegreesGyro < 150
                && oldestSimData.HeadingDegreesGyro > 180;
        }

        private bool IsPromptLandingConfiguration(List<SimData> simDataList)
        {
            return false;
        }

        private bool IsFinalLeg(List<SimData> simDataList)
        {
            SimData latestSimData = simDataList.Last();
            return latestSimData.HeadingDegreesGyro > 110
                && latestSimData.HeadingDegreesGyro < 150
                && latestSimData.RadioAltitude < 1000;
        }

        private bool IsFourthTurn(List<SimData> simDataList)
        {
            return false;
        }

        private bool IsBaseLegApproachConfigured(List<SimData> simDataList)
        {
            SimData latestSimData = simDataList.Last();
            return latestSimData.AirspeedIndicated <= 85;
        }

        private bool IsBaseLeg(List<SimData> simDataList)
        {
            SimData latestSimData = simDataList.Last();
            return latestSimData.HeadingDegreesGyro > 205
                && latestSimData.HeadingDegreesGyro < 235
                && latestSimData.AircraftOnGround == false
                && latestSimData.AirspeedIndicated > 40
                && latestSimData.RadioAltitude > 500
                && latestSimData.RadioAltitude < 1000;
        }

        private bool IsThirdTurn(List<SimData> simDataList)
        {
            return false;
        }
        private bool IsDropFlapsOne(List<SimData> simDataList)
        {
            return false;
        }

        private bool IsSlowDownForApproach(List<SimData> simDataList)
        {
            return false;
        }

        private bool IsCrossOverRunwayEntrance(List<SimData> simDataList)
        {
            return false;
        }

        private bool IsJustEnterDownWindLeg(List<SimData> simDataList)
        {
            return false;
        }

        private bool IsDownWindLeg(List<SimData> simDataList)
        {
            SimData latestSimData = simDataList.Last();
            return latestSimData.HeadingDegreesGyro > 295
                && latestSimData.HeadingDegreesGyro < 330
                && latestSimData.AircraftOnGround == false
                && latestSimData.AirspeedIndicated > 40
                && latestSimData.RadioAltitude > 700
                && latestSimData.RadioAltitude < 1200;
        }

        private bool IsSecondTurn(List<SimData> simDataList)
        {
            return false;
        }

        private bool IsTrafficPatternHeight(List<SimData> simDataList)
        {
            SimData latestSimData = simDataList.Last();
            return latestSimData.RadioAltitude > 950
                && latestSimData.RadioAltitude < 1200;
        }

        private bool IsCrossWindLeg(List<SimData> simDataList)
        {
            SimData latestSimData = simDataList.Last();
            return latestSimData.HeadingDegreesGyro > 25
                && latestSimData.HeadingDegreesGyro < 55
                && latestSimData.AircraftOnGround == false
                && latestSimData.AirspeedIndicated > 40
                && latestSimData.RadioAltitude > 500;
        }
        private bool IsFirstTurn(List<SimData> simDataList)
        {
            SimData latestSimData = simDataList.Last();
            return latestSimData.RadioAltitude > 500
                && latestSimData.RadioAltitude < 550
                && IsUpWindLeg(simDataList);
        }

        private bool IsInitialClimb(List<SimData> simDataList)
        {
            SimData latestSimData = simDataList.Last();
            return latestSimData.RadioAltitude > 300
                && latestSimData.RadioAltitude < 350;
        }

        private bool IsUpWindLeg(List<SimData> simDataList)
        {
            SimData latestSimData = simDataList.Last();
            return latestSimData.AircraftOnGround == false
                && latestSimData.RadioAltitude < 1000
                && latestSimData.VerticalSpeed > 1
                && latestSimData.HeadingDegreesGyro > 110
                && latestSimData.HeadingDegreesGyro < 150;
        }

        private bool IsJustAirborne(List<SimData> simDataList)
        {
            SimData latestSimData = simDataList.LastOrDefault();
            SimData oldestSimData = simDataList.FirstOrDefault();
            return latestSimData.AirspeedIndicated > 54
                && oldestSimData.AircraftOnGround == true
                && latestSimData.AircraftOnGround == false
                && latestSimData.RadioAltitude > 7;
        }

        private bool IsTakeoffSpeed(List<SimData> simDataList)
        {
            SimData latestSimData = simDataList.LastOrDefault();
            return latestSimData.AirspeedIndicated > 54
                && latestSimData.AirspeedIndicated < 65
                && latestSimData.AircraftOnGround;
        }

        private bool IsAirspeedAlive(List<SimData> simDataList)
        {
            SimData latestSimData = simDataList.LastOrDefault();
            return latestSimData.AirspeedIndicated > 40
                && latestSimData.AirspeedIndicated < 45
                && latestSimData.AircraftOnGround;
        }

        private bool IsTakeOffStage(List<SimData> simDataList)
        {
            SimData latestSimData = simDataList.LastOrDefault();
            return latestSimData.RPM >= 2400
                && latestSimData.GroundSpeed > 0 && latestSimData.GroundSpeed < 70;
        }

        private bool AircraftJustEnterRunway(List<SimData> simDataList)
        {
            SimData oldestSimData = simDataList.FirstOrDefault();
            SimData latestSimData = simDataList.LastOrDefault();
            return oldestSimData.OnAnyRunway == false
                && latestSimData.OnAnyRunway == true
                && oldestSimData.AircraftOnGround == true
                && latestSimData.AircraftOnGround == true;
        }

        private bool IsTaxingToRunway(List<SimData> simDataList)
        {
            SimData latestSimData = simDataList.LastOrDefault();
            SimData oldestSimData = simDataList.FirstOrDefault();

            return latestSimData.EngineRunning
                && latestSimData.RPM > 500 && latestSimData.RPM < 1500
                && latestSimData.GroundSpeed > 1 && latestSimData.GroundSpeed <= 20
                && oldestSimData.OnAnyRunway == false
                && latestSimData.OnAnyRunway == false;
        }

        private bool AircraftJustStarted(List<SimData> simDataList)
        {
            SimData data = simDataList.LastOrDefault();
            return simDataList.LastOrDefault().EngineRunning == true
                && simDataList.FirstOrDefault().EngineRunning == false
                && data.ElectricalMasterBattery == true 
                && data.AlternatorSwitch == true 
                && data.OnAnyRunway == false;
        }

        private bool ColdAndDark(List<SimData> simDataList)
        {
            SimData data = simDataList.LastOrDefault();
            return data.RadioAltitude > 0
                && data.RPM == 0
                && data.GroundSpeed < 1
                && data.ElectricalMasterBattery == false
                && data.AlternatorSwitch == false
                && data.OnAnyRunway == false;
        }
    }
}