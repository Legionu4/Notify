using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Threading;
using System.Windows.Threading;
using System.ComponentModel;

namespace MyCustomWindowNamespace
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class CustomWindow : Window
    {
        System.Timers.Timer lifeTimeTimer;
        DoubleAnimation anim;
        int left;
        int top;
        DependencyProperty prop;
        int end;

        int loadAnimationTime = 3500;

        public CustomWindow()
        {
            InitializeComponent();

            TrayPos tpos = new TrayPos();
            tpos.getXY((int)Width, (int)Height, out top, out left, out prop, out end);
            Top = top;
            Left = left;
            anim = new DoubleAnimation(end, TimeSpan.FromMilliseconds(loadAnimationTime));
        }

        System.Timers.Timer timer;
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            AnimationClock clock = anim.CreateClock();
            ApplyAnimationClock(prop, clock);

            lblTime.Content = DateTime.Now.ToString("dd.MM.yyyy HH.mm.ss");
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += Timer_Elapsed;

            lifeTimeTimer = new System.Timers.Timer(25000);
            lifeTimeTimer.Elapsed += LifeTimeTimer_Elapsed;

            timer.Start();
            lifeTimeTimer.Start();
        }

        private void LifeTimeTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(Close));
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(UpdateLblTime));
        }

        private void UpdateLblTime()
        {
            lblTime.Content = DateTime.Now.ToString("dd.MM.yyyy HH.mm.ss");
        }

        private void CloseBtnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void DoubleAnimation_Completed(object sender, EventArgs e)
        {
            Hide();
        }

        private void rtMain_MouseEnter(object sender, MouseEventArgs e)
        {
            rtMain.StrokeThickness = 1;
            rtMain.Stroke = Brushes.Bisque;
        }

        private void rtMain_MouseLeave(object sender, MouseEventArgs e)
        {
            rtMain.StrokeThickness = 0;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Closing -= Window_Closing;
            e.Cancel = true;
            var animation = new DoubleAnimation(0, TimeSpan.FromSeconds(1));
            animation.Completed += (s, _e) => Close();
            BeginAnimation(OpacityProperty, animation);
            timer.Dispose();
            lifeTimeTimer.Dispose();
        }
    }
}
