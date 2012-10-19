using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MaToMaTo
{
    /// <summary>
    /// Interaction logic for Window_FC_Pomodoro.xaml
    /// </summary>
    public partial class Pomodoro : Window
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        DispatcherTimer dispatcherTimerAn = new DispatcherTimer();
        TimeSpan _ts;
        Storyboard _st;

        public Pomodoro()
        {
            this.InitializeComponent();
        }

        #region window
        private void Window_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source.GetType().IsSubclassOf(typeof(Window)) || (e.Source.GetType().Equals(typeof(Image)) && ((System.Windows.FrameworkElement)(e.Source)).Name == "imgTomato")) DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            initializeTomato();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
        }
        #endregion

        #region Timer
        void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            workTomato();
        }

        void dispatcherTimerAn_Tick(object sender, EventArgs e)
        {
            animateTomato();
        }

        #endregion

        #region tomato

        private void initializeTomato()
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);

            dispatcherTimerAn.Tick += new EventHandler(dispatcherTimerAn_Tick);
            dispatcherTimerAn.Interval = new TimeSpan(0, 0, 2);
        }

        private void startTomato()
        {
            _st = (Storyboard)this.Resources["OkTomato"];
            _st.Stop();
            _ts = new TimeSpan(0, 25, 0);
            lblTime.Content = _ts.Minutes + "." + _ts.Seconds.ToString().PadLeft(2, '0');
            _st = (Storyboard)this.Resources["GridMove"];
            _st.Begin();
            dispatcherTimer.Start();
            dispatcherTimerAn.Start();
        }

        private void stopTomato()
        {
            dispatcherTimer.Stop();
            dispatcherTimerAn.Stop();
            _st = (Storyboard)this.Resources["GridBig"];
            _st.Stop();
            _st = (Storyboard)this.Resources["GridMove"];
            _st.Stop();
        }

        private void workTomato()
        {
            _ts = _ts.Subtract(new TimeSpan(0, 0, 1));
            lblTime.Content = _ts.Minutes + "." + _ts.Seconds.ToString().PadLeft(2, '0');
            if (_ts == new TimeSpan(0, 0, 0))
            {
                stopTomato();
                _st = (Storyboard)this.Resources["OkTomato"];
                _st.RepeatBehavior = new RepeatBehavior(new TimeSpan(0, 10, 0));
                _st.Begin();
            }
        }

        private void animateTomato()
        {
            if (_ts <= new TimeSpan(0, 5, 0))
                _st = (Storyboard)this.Resources["GridBig"];
            else
                _st = (Storyboard)this.Resources["GridMove"];

            _st.Begin();
        }

        #endregion

        #region img

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            this.Close();
        }

        private void imgExit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           _st = (Storyboard)this.Resources["KillGrid"];
            _st.Begin();
        }

        private void imgStop_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            stopTomato();
        }

        private void imgPlay_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startTomato();
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            _st = (Storyboard)this.Resources["Img_Over"];
            _st.Begin();
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            _st = (Storyboard)this.Resources["Img_NonOver"];
            _st.Begin();
        }

        #endregion
        
    }
}