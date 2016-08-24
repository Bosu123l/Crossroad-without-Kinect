namespace TrafficLights.Domain
{
    public class AutomaticLight : Light
    {
        public AutomaticLight(LightState.LightParameter StartState) : base(StartState)
        {
        }

        public void ChangeLight()
        {
            if (IsRed())
            {
                changeToRedAndYellow();
                return;
            }
            else if (IsRedAndYellow())
            {
                changeToGreen();
                return;
            }
            else if (IsGrren())
            {
                changeToYellow();
                return;
            }
            else if (IsYellow())
            {
                changeToRed();
                return;
            }
        }
    }
}