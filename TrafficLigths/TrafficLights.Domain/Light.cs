using System;

namespace TrafficLights.Domain
{
    public class Light : ILight
    {

        public event EventHandler<LightState.LightParameter> ActualState;



        public event EventHandler GreenLight;




        protected void OnGreenLight()
        {
            var tempHandler = GreenLight;
            if (tempHandler != null)
            {
                tempHandler(this, EventArgs.Empty);
            }
        }

        protected void OnActualStateChange()
        {
            var tempHandler = ActualState;
            if (tempHandler != null)
            {
                tempHandler(this, CurrentState);
            }
        }

        public LightState.LightParameter CurrentState
        {
            get;
            set; //L:G: zmienic na PRIVATE SET po testach
        }

        public void changeToGreen()
        {
            CurrentState = LightState.LightParameter.Green;
            OnActualStateChange();

            OnGreenLight();

        }

        public void changeToRed()
        {
            CurrentState = LightState.LightParameter.Red;
            OnActualStateChange();
        }

        public void changeToRedAndYellow()
        {
            CurrentState = LightState.LightParameter.RedAndYellow;
            OnActualStateChange();
        }

        public void changeToYellow()
        {
            CurrentState = LightState.LightParameter.Yellow;
            OnActualStateChange();
        }

        public bool IsRed()
        {
            return CurrentState == LightState.LightParameter.Red ? true : false;
        }

        public bool IsGrren()
        {
            return CurrentState == LightState.LightParameter.Green ? true : false;
        }

        public bool IsRedAndYellow()
        {
            return CurrentState == LightState.LightParameter.RedAndYellow ? true : false;
        }

        public bool IsYellow()
        {
            return CurrentState == LightState.LightParameter.Yellow ? true : false;
        }

        public Light(LightState.LightParameter StartState)
        {
            if (StartState == LightState.LightParameter.Yellow || StartState == LightState.LightParameter.RedAndYellow)
            {
                throw new ArgumentException("Light cannot be yellow on start!");
            }
            CurrentState = StartState;
        }
    }
}