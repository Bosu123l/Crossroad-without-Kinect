using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TrafficLights.Domain;

namespace TrafficLigths
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CarAnimation _carFromDownToLeft;
        private readonly CarAnimation _carFromDownToRigth;


        private readonly CarAnimation _carFromDownToUp;
        private readonly CarAnimation _carFromLeftToDown;
        private readonly CarAnimation _carFromLeftToRigth;

        private readonly CarAnimation _carFromLeftToUp;
        private readonly CarAnimation _carFromRigthToDown;
        private readonly CarAnimation _carFromRigthToLeft;

        private readonly CarAnimation _carFromRigthToUp;

        private readonly CarAnimation _carFromUpToDown;
        private readonly CarAnimation _carFromUpToLeft;
        private readonly CarAnimation _carFromUpToRigth;


        private readonly DispatcherTimer _ligthTimer;
        private readonly DispatcherTimer _yellowDuration;


        private bool _toYellow;
        private bool _toLeftBlock;

        private Random random;


        public MainWindow()
        {
            InitializeComponent();

            _carFromDownToUp = new CarAnimation(CarFromDownToUp, CarFromDownToUp_BeginStoryboard);
            _carFromDownToUp.AnimationEnd += AnimationEnd;
            _carFromDownToRigth = new CarAnimation(CarFromDownToRigth, CarFromDownToRight_BeginStoryboard);
            _carFromDownToRigth.AnimationEnd += AnimationEnd;
            _carFromDownToLeft = new CarAnimation(CarFromDownToLeft, CarFromDownToLeft_BeginStoryboard);
            _carFromDownToLeft.AnimationEnd += AnimationEnd;

            _carFromUpToDown = new CarAnimation(CarFromUpToDown, CarFromUpToDown_BeginStoryboard);
            _carFromUpToDown.AnimationEnd += AnimationEnd;
            _carFromUpToRigth = new CarAnimation(CarFromUpToRigth, CarFromUpToRight_BeginStoryboard);
            _carFromUpToRigth.AnimationEnd += AnimationEnd;
            _carFromUpToLeft = new CarAnimation(CarFromUpToLeft, CarFromUpToLeft_BeginStoryboard);
            _carFromUpToLeft.AnimationEnd += AnimationEnd;

            _carFromLeftToUp = new CarAnimation(CarFromLeftToUp, CarFromLeftToUp_BeginStoryboard);
            _carFromLeftToUp.AnimationEnd += AnimationEnd;
            _carFromLeftToRigth = new CarAnimation(CarFromLeftToRigth, carFromLeftToRight_BeginStoryboard);
            _carFromLeftToRigth.AnimationEnd += AnimationEnd;
            _carFromLeftToDown = new CarAnimation(CarFromLeftToDown, CarFromLeftToDown_BeginStoryboard);
            _carFromLeftToDown.AnimationEnd += AnimationEnd;

            _carFromRigthToUp = new CarAnimation(CarFromRigthToUp, CarFromRigthToUp_BeginStoryboard);
            _carFromRigthToUp.AnimationEnd += AnimationEnd;
            _carFromRigthToLeft = new CarAnimation(CarFromRightToLeft, CarFromRIgthToLeft_BeginStoryboard);
            _carFromRigthToLeft.AnimationEnd += AnimationEnd;
            _carFromRigthToDown = new CarAnimation(CarFromRigthToDown, CarFromRightToDown_BeginStoryboard);
            _carFromRigthToDown.AnimationEnd += AnimationEnd;

            _ligthTimer = new DispatcherTimer();
            _ligthTimer.Interval = new TimeSpan(0, 0, 12);
            _ligthTimer.Tick += _ligthTimer_Tick;
            _ligthTimer.Start();

            _yellowDuration = new DispatcherTimer();

            _yellowDuration.Interval = new TimeSpan(0, 0, 3);
            _yellowDuration.Tick += _yellowDuration_Tick;


            LightViewHorizontal1.StartState = LightState.LightParameter.Red;
            LightViewHorizontal2.StartState = LightState.LightParameter.Red;
            LightViewVertical1.StartState = LightState.LightParameter.Green;
            LightViewVertical2.StartState = LightState.LightParameter.Green;


            policemanView.VerticalGreenLightSet += LightView2_GreenLightSet;
            policemanView.VerticalGreenLightSet += LightView2_GreenLightSet1;

            policemanView.HorizontalGreenLightSet += LightView_GreenLightSet;
            policemanView.HorizontalGreenLightSet += LightView_Copy_GreenLightSet;

            policemanView.WarningSet += (sender, args) => { StopAnimation(); };
            policemanView.TurnLeftFromUp += (sender, args) =>
            {
                _carFromUpToRigth.AllowAnimation();
                _carFromUpToRigth.StartAnimation();
            };
            policemanView.TurnLeftFromDown += (sender, args) =>
            {
                _carFromDownToLeft.AllowAnimation();
                _carFromDownToLeft.StartAnimation();
            };
            policemanView.TurnLeftFromRight += (sender, args) =>
            {
                _carFromRigthToDown.AllowAnimation();
                _carFromRigthToDown.StartAnimation();
            };
            policemanView.TurnLeftFromLeft += (sender, args) =>
            {
                _carFromLeftToUp.AllowAnimation();
                _carFromLeftToUp.StartAnimation();
            };

            policemanView.PolicemanGetOutFromCrossroads += (sender, args) =>
            {

                PolicemanLeave();
                KeeperLeave();
                StopAnimation();
                CarStraight();

                StartTimers();
            };
            policemanView.PolicemanEnterTheCrossroads += (sender, args) =>
            {

                PolicemanEnter();
                KeeperEnter();
                StopAnimation();

                StopTimers();
            };

            Closing += (sender, args) => { policemanView.closeKinect(); };
        }


        private void PolicemanEnter()
        {
            LightViewHorizontal1.GreenLightSet += LightView_GreenLightSet;

            LightViewHorizontal2.GreenLightSet += LightView_Copy_GreenLightSet;

            LightViewVertical1.GreenLightSet += LightView2_GreenLightSet;

            LightViewVertical2.GreenLightSet += LightView2_GreenLightSet1;
        }

        private void PolicemanLeave()
        {
            LightViewHorizontal1.GreenLightSet -= LightView_GreenLightSet;

            LightViewHorizontal2.GreenLightSet -= LightView_Copy_GreenLightSet;

            LightViewVertical1.GreenLightSet -= LightView2_GreenLightSet;

            LightViewVertical2.GreenLightSet -= LightView2_GreenLightSet1;
        }

        private void AnimationEnd(object sender, EventArgs e)
        {


            random = new Random();
            var resultOfRandom = random.Next(1, 3);


            switch (resultOfRandom)
            {
                case 1:
                    {
                        CarToRigth();
                    }
                    break;
                case 2:
                    {
                        CarStraight();
                    }
                    break;
                case 3:
                    {
                        CartoLeft();

                    }
                    break;
            }
        }

        private void StopTimers()
        {
            _ligthTimer.Stop();
            _yellowDuration.Stop();
        }

        private void StartTimers()
        {
            _ligthTimer.Start();
        }




        private void LightView2_GreenLightSet1(object sender, EventArgs e)
        {
            CarStraight();
            CarToRigth();
        }

        private void LightView2_GreenLightSet(object sender, EventArgs e)
        {
            CarStraight();
            CarToRigth();
        }

        private void LightView_Copy_GreenLightSet(object sender, EventArgs e)
        {
            CarStraight();
            CarToRigth();
        }

        private void LightView_GreenLightSet(object sender, EventArgs e)
        {
            CarStraight();
            CarToRigth();
        }
        private void _ligthTimer_Tick(object sender, EventArgs e)
        {
            _toYellow = true;
            _ligthTimer.Stop();
            button_Click(this, new AccessKeyPressedEventArgs());
        }
        private void _yellowDuration_Tick(object sender, EventArgs e)
        {
            _toLeftBlock = false;
            _toYellow = false;
            _yellowDuration.Stop();
            _ligthTimer.Start();

            button_Click(this, new AccessKeyPressedEventArgs());
            AnimationEnd(this, EventArgs.Empty);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (_toYellow)
            {
                StopAnimation();
                _yellowDuration.Start();
                _ligthTimer.Stop();
            }
            ChangeLigth();
        }

        private void ChangeLigth()
        {
            LightViewHorizontal1.ChangeLight();
            LightViewHorizontal2.ChangeLight();
            LightViewVertical1.ChangeLight();
            LightViewVertical2.ChangeLight();


            StopAnimation();
        }

        private void StopAnimation()
        {
            _carFromLeftToUp.StopAnimation();
            _carFromRigthToDown.StopAnimation();

            _carFromDownToLeft.StopAnimation();
            _carFromUpToRigth.StopAnimation();


            _carFromLeftToDown.StopAnimation();
            _carFromRigthToUp.StopAnimation();

            _carFromDownToRigth.StopAnimation();
            _carFromUpToLeft.StopAnimation();

            _carFromLeftToRigth.StopAnimation();
            _carFromRigthToLeft.StopAnimation();

            _carFromUpToDown.StopAnimation();
            _carFromDownToUp.StopAnimation();


        }

        private void KeeperEnter()
        {
            _carFromLeftToUp.KeeperEnter();
            _carFromRigthToDown.KeeperEnter();

            _carFromDownToLeft.KeeperEnter();
            _carFromUpToRigth.KeeperEnter();


            _carFromLeftToDown.KeeperEnter();
            _carFromRigthToUp.KeeperEnter();

            _carFromDownToRigth.KeeperEnter();
            _carFromUpToLeft.KeeperEnter();

            _carFromLeftToRigth.KeeperEnter();
            _carFromRigthToLeft.KeeperEnter();

            _carFromUpToDown.KeeperEnter();
            _carFromDownToUp.KeeperEnter();
        }
        private void KeeperLeave()
        {
            _carFromLeftToUp.KeeperLeave();
            _carFromRigthToDown.KeeperLeave();

            _carFromDownToLeft.KeeperLeave();
            _carFromUpToRigth.KeeperLeave();


            _carFromLeftToDown.KeeperLeave();
            _carFromRigthToUp.KeeperLeave();

            _carFromDownToRigth.KeeperLeave();
            _carFromUpToLeft.KeeperLeave();

            _carFromLeftToRigth.KeeperLeave();
            _carFromRigthToLeft.KeeperLeave();

            _carFromUpToDown.KeeperLeave();
            _carFromDownToUp.KeeperLeave();
        }
        private void AllowRigth_Click(object sender, RoutedEventArgs e)
        {
            CarToRigth();
        }


        private void AllowLeft_Click(object sender, RoutedEventArgs e)
        {
            CartoLeft();
        }

        private void straight_Click(object sender, RoutedEventArgs e)
        {
            CarStraight();
        }

        private void CartoLeft()
        {
            StopAnimation();

            if (IsGreenHorizontal())
            {
                CarFromLeftToUpAnimation();

                CarFromRigthToDownAnimation();
            }
            else
            {
                CarFromDownToLeftAnimation();

                CarFromUpToRigthAnimation();
            }
        }



        private void CarToRigth()
        {
            StopAnimation();

            if (IsGreenHorizontal())
            {
                CarFromLeftToDownAnimation();

                CarFromRigthToUpAnimation();
            }
            else
            {
                CarFromDownToRigthAnimation();

                CarFromUpToLeftAnimation();
            }
        }



        private void CarStraight()
        {
            StopAnimation();

            if (IsGreenHorizontal())
            {
                CarFromLeftToRigthAnimation();

                CarFromRigthToLeftAnimation();
            }
            else
            {
                CarFromUpToDownAnimation();

                CarFromDownToUpAnimation();
            }
        }

        private async void CarFromLeftToRigthAnimation()
        {
            await Task.Delay(1000);
            _carFromLeftToRigth.AllowAnimation();
            _carFromLeftToRigth.StartAnimation();
        }

        private void CarFromRigthToLeftAnimation()
        {

            _carFromRigthToLeft.AllowAnimation();
            _carFromRigthToLeft.StartAnimation();
        }

        private void CarFromDownToUpAnimation()
        {
            _carFromDownToUp.AllowAnimation();
            _carFromDownToUp.StartAnimation();
        }

        private void CarFromUpToDownAnimation()
        {
            _carFromUpToDown.AllowAnimation();
            _carFromUpToDown.StartAnimation();
        }
        private void CarFromLeftToDownAnimation()
        {
            _carFromLeftToDown.AllowAnimation();
            _carFromLeftToDown.StartAnimation();
        }

        private async void CarFromRigthToUpAnimation()
        {
            await Task.Delay(1000);
            _carFromRigthToUp.AllowAnimation();
            _carFromRigthToUp.StartAnimation();
        }

        private async void CarFromDownToRigthAnimation()
        {
            await Task.Delay(1000);
            _carFromDownToRigth.AllowAnimation();
            _carFromDownToRigth.StartAnimation();
        }

        private async void CarFromUpToLeftAnimation()
        {
            await Task.Delay(1000);
            _carFromUpToLeft.AllowAnimation();
            _carFromUpToLeft.StartAnimation();
        }

        private void CarFromLeftToUpAnimation()
        {
            _carFromLeftToUp.AllowAnimation();
            _carFromLeftToUp.StartAnimation();
        }

        private void CarFromRigthToDownAnimation()
        {
            _carFromRigthToDown.AllowAnimation();
            _carFromRigthToDown.StartAnimation();
        }

        private async void CarFromDownToLeftAnimation()
        {
            await Task.Delay(1000);
            _carFromDownToLeft.AllowAnimation();
            _carFromDownToLeft.StartAnimation();
        }

        private async void CarFromUpToRigthAnimation()
        {
            await Task.Delay(1000);
            _carFromUpToRigth.AllowAnimation();
            _carFromUpToRigth.StartAnimation();
        }
        private bool IsGreenHorizontal()
        {
            //if (policemanView.IsAtTheCrossroads)
            //    return policemanView.HorizontalLigth.CurrentState == LightState.LightParameter.Green;
            return LightViewHorizontal1.CurrentState == LightState.LightParameter.Green;
        }
    }
}