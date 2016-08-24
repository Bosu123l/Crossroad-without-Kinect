using System;
using System.ComponentModel;
using System.Windows.Controls;
using TrafficLights.Domain;

namespace TrafficLigths
{
    public partial class LightView : UserControl, INotifyPropertyChanged
    {
        public LightState.LightParameter StartState
        {
            get
            {
                return _startState;
            }
            set
            {
                if (value != _startState)
                {
                    _startState = value;
                    SetLight(_startState);

                    _automaticLight.CurrentState = _startState;//LG: DLA TESTOW!
                }
            }
        }


        public event EventHandler GreenLightSet;

        protected void OnGreenLightSet()
        {
            var tempHandler = GreenLightSet;
            if (tempHandler != null)
            {
                tempHandler(this, EventArgs.Empty);
            }
        }

        public event EventHandler<LightState.LightParameter> ActualState;

        protected void OnActualStateeChange()
        {
            var tempHandler = ActualState;
            if (tempHandler != null)
            {
                tempHandler(this, CurrentState);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string RedLight
        {
            get
            {
                return _redLight;
            }
            set
            {
                if (value != _redLight)
                {
                    _redLight = value;
                    OnPropertyChanged("RedLight");
                }
            }
        }

        public string YellowLight
        {
            get
            {
                return _yellowLight;
            }
            set
            {
                if (value != _yellowLight)
                {
                    _yellowLight = value;
                    OnPropertyChanged("YellowLight");
                }
            }
        }

        public string GreenLight
        {
            get
            {
                return _greenLight;
            }
            set
            {
                if (value != _greenLight)
                {
                    _greenLight = value;
                    OnPropertyChanged("GreenLight");
                }
            }
        }

        protected void OnPropertyChanged(Func<string> p)
        {
            var tempHandler = PropertyChanged;
            if (tempHandler != null)
            {
                tempHandler(this, new PropertyChangedEventArgs(p()));
            }
        }

        protected void OnPropertyChanged(string property)
        {
            var tempHandler = PropertyChanged;
            if (tempHandler != null)
            {
                tempHandler(this, new PropertyChangedEventArgs(property));
            }
        }

        private AutomaticLight _automaticLight;
        private string _redLight;
        private string _yellowLight;
        private string _greenLight;
        private LightState.LightParameter _startState;
        public LightState.LightParameter CurrentState;

        public LightView()
        {
            _automaticLight = new AutomaticLight(StartState);
            _automaticLight.ActualState += _automaticLight_ActualState;

            _automaticLight.GreenLight += _automaticLight_GreenLight;

            SetLight(_automaticLight.CurrentState);
            InitializeComponent();
        }

        private void _automaticLight_GreenLight(object sender, EventArgs e)
        {
            OnGreenLightSet();
        }

        private void _automaticLight_ActualState(object sender, LightState.LightParameter e)
        {
            CurrentState = e;
            OnActualStateeChange();
        }

        public void ChangeLight()
        {
            _automaticLight.ChangeLight();
            SetLight(_automaticLight.CurrentState);
        }

        public void SetLightGreen()
        {
            GreenLight = "Green";
            RedLight = YellowLight = "Black";
        }

        public void SetLightRed()
        {
            RedLight = "Red";
            YellowLight = GreenLight = "Black";
        }

        public void SetLightYellow()
        {
            YellowLight = "Yellow";
            RedLight = GreenLight = "Black";
        }

        public void SetLightYellowAndRed()
        {
            RedLight = "Red";
            YellowLight = "Yellow";
            GreenLight = "Black";
        }

        private void SetLight(LightState.LightParameter state)
        {
            switch (state)
            {
                case LightState.LightParameter.Red:
                    {
                        SetLightRed();
                    }
                    break;

                case LightState.LightParameter.RedAndYellow:
                    {
                        SetLightYellowAndRed();
                    }
                    break;

                case LightState.LightParameter.Yellow:
                    {
                        SetLightYellow();
                    }
                    break;

                case LightState.LightParameter.Green:
                    {
                        SetLightGreen();
                    }
                    break;

                default:
                    {
                        throw new ArgumentOutOfRangeException(_automaticLight.CurrentState.ToString());
                    }
            }
        }
    }
}