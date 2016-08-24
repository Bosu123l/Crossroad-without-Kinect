using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace TrafficLigths
{
    public class CarAnimation
    {
        public Grid grid;
        public BeginStoryboard beginStoryboard;

        public event EventHandler AnimationEnd;

        private bool _keeper;


        protected void OnAnimationEnd()
        {
            var tempHandler = AnimationEnd;
            if (tempHandler != null)
            {
                tempHandler(this, EventArgs.Empty);
            }
        }


        public CarAnimation(Grid grid, BeginStoryboard beginStoryboard)
        {
            this.grid = grid;
            this.beginStoryboard = beginStoryboard;

        }

        public void KeeperEnter()
        {
            _keeper = true;
        }

        public void KeeperLeave()
        {
            _keeper = false;
        }
        public void StartAnimation()
        {
            beginStoryboard.Storyboard.Stop();
            grid.BeginStoryboard(beginStoryboard.Storyboard);
            beginStoryboard.Storyboard.Seek(TimeSpan.Zero, TimeSeekOrigin.BeginTime);
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            if (_keeper)
            {
                StartAnimation();
            }
            else
            {
                OnAnimationEnd();
            }


        }

        public void AllowAnimation()
        {
            beginStoryboard.Storyboard.Completed -= Storyboard_Completed;
            beginStoryboard.Storyboard.Completed += Storyboard_Completed;
        }
        public void StopAnimation()
        {
            beginStoryboard.Storyboard.Completed -= Storyboard_Completed;
        }
    }
}
