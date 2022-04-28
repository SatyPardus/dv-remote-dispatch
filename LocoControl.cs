using DV.Logic.Job;
using System.Linq;

namespace DvMod.RemoteDispatch
{
    public static class LocoControl
    {
        public const float CouplerRange = 0.5f;

        public static bool CanBeControlled(TrainCarType carType)
        {
            return carType == TrainCarType.LocoShunter;
        }

        public static LocoControllerBase? GetLocoController(string id)
        {
            return SingletonBehaviour<IdGenerator>.Instance.logicCarToTrainCar.Values
                .FirstOrDefault(c => c.ID == id && CanBeControlled(c.carType))
                ?.GetComponent<LocoControllerBase>();
        }

        public static void SetCoast(LocoControllerBase controller)
        {
            controller.SetThrottle(0f);
            controller.SetBrake(0f);
            controller.SetIndependentBrake(0f);
        }

        public static void SetForward(LocoControllerBase controller)
        {
            controller.SetThrottle(0f);
            controller.SetBrake(0f);
            controller.SetIndependentBrake(0f);
            controller.SetReverser(1f);
            controller.SetThrottle(0.4f);
        }

        public static void SetReverse(LocoControllerBase controller)
        {
            controller.SetThrottle(0f);
            controller.SetBrake(0f);
            controller.SetIndependentBrake(0f);
            controller.SetReverser(-1f);
            controller.SetThrottle(0.4f);
        }

        public static void SetStop(LocoControllerBase controller)
        {
            controller.SetThrottle(0f);
            controller.SetBrake(1f);
            controller.SetIndependentBrake(1f);
            controller.SetReverser(0f);
        }

        public static bool IsSupportedCommand(string command)
        {
            return command == "coast" || command == "forward" || command == "reverse" || command == "stop";
        }

        public static void RunCommand(LocoControllerBase controller, string command)
        {
            switch (command)
            {
                case "coast":
                    SetCoast(controller);
                    break;
                case "forward":
                    SetForward(controller);
                    break;
                case "reverse":
                    SetReverse(controller);
                    break;
                case "stop":
                    SetStop(controller);
                    break;
            }
        }
    }
}
