using static TrafficLights.Domain.LightState;

namespace TrafficLights.Domain
{
    public interface ILight
    {
        LightParameter CurrentState
        {
            get;
        }

        void changeToRed();

        void changeToGreen();

        void changeToYellow();

        void changeToRedAndYellow();

        bool IsRed();

        bool IsGrren();

        bool IsRedAndYellow();

        bool IsYellow();
    }
}