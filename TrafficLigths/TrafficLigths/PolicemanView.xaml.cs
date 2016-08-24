using System;
using System.Windows;
using System.Windows.Controls;

namespace TrafficLigths
{
    /// <summary>
    ///     Interaction logic for PolicemanView.xaml
    /// </summary>
    public partial class PolicemanView : UserControl
    {
        private int angle;

        public PolicemanView()
        {
            InitializeComponent();


            Visibility = Visibility.Hidden;
        }


        private void OnChangeRotation(int angle)
        {
            this.angle = angle;
        }


        public event EventHandler HorizontalGreenLightSet;
        public event EventHandler VerticalGreenLightSet;

        public event EventHandler TurnLeftFromDown;
        public event EventHandler TurnLeftFromLeft;
        public event EventHandler TurnLeftFromUp;
        public event EventHandler TurnLeftFromRight;
        public event EventHandler WarningSet;
        public event EventHandler PolicemanGetOutFromCrossroads;
        public event EventHandler PolicemanEnterTheCrossroads;

        protected void OnPolicemanGetOutFromCrossroads()
        {
            var tempHandler = PolicemanGetOutFromCrossroads;
            if (tempHandler != null)
            {
                tempHandler(this, EventArgs.Empty);
            }
        }

        protected void OnPolicemanEnterTheCrossroads()
        {
            var tempHandler = PolicemanEnterTheCrossroads;
            if (tempHandler != null)
            {
                tempHandler(this, EventArgs.Empty);
            }
        }

        protected void OnTurnLeftFromDown()
        {
            var tempHandler = TurnLeftFromDown;
            if (tempHandler != null)
            {
                tempHandler(this, EventArgs.Empty);
            }
        }

        protected void OnTurnLeftFromLeft()
        {
            var tempHandler = TurnLeftFromLeft;
            if (tempHandler != null)
            {
                tempHandler(this, EventArgs.Empty);
            }
        }

        protected void OnTurnLeftFromUp()
        {
            var tempHandler = TurnLeftFromUp;
            if (tempHandler != null)
            {
                tempHandler(this, EventArgs.Empty);
            }
        }

        protected void OnTurnLeftFromRight()
        {
            var tempHandler = TurnLeftFromRight;
            if (tempHandler != null)
            {
                tempHandler(this, EventArgs.Empty);
            }
        }

        protected void OnWarningSet()
        {
            var tempHandler = WarningSet;
            if (tempHandler != null)
            {
                tempHandler(this, EventArgs.Empty);
            }
        }

        protected void OnHorizontalGreenLightSet()
        {
            var tempHandler = HorizontalGreenLightSet;
            if (tempHandler != null)
            {
                tempHandler(this, EventArgs.Empty);
            }
        }

        protected void OnVerticalGreenLightSet()
        {
            var tempHandler = VerticalGreenLightSet;
            if (tempHandler != null)
            {
                tempHandler(this, EventArgs.Empty);
            }
        }

        public void closeKinect()
        {
            //  policeman.Dispose();
        }
    }
}