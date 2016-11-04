using System.ServiceProcess;
using System.Diagnostics;
using System;
using System.IO;
using System.Timers;
using System.Threading;

namespace MyFirstWindowsService
{
    public partial class MyFirstWindowsService : ServiceBase
    {
        private EventLog eventLog1 = new EventLog();
        private readonly string _path = @"D:\ARM\git\Notify\CustomWindow\bin\Debug\CustomWindow.exe";
        private string args = "";

        public MyFirstWindowsService()
        {
            InitializeComponent();
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        #region SERVICE
        protected override void OnStart(string[] args)
        {
            Trace("OnStart");
            var timer = Timer;
        }
        protected override void OnPause()
        {
            Trace("OnPause");
            Timer.Stop();
            base.OnPause();
        }
        protected override void OnContinue()
        {
            Trace("OnContinue");
            var timer = Timer;
            base.OnContinue();
        }
        protected override void OnStop()
        {
            Trace("OnStop");
            Timer.Stop();
        }
        private static void Trace(params string[] msges)
        {
            using (FileStream file = new FileStream(@"D:\RkuzmovychService.txt", FileMode.Append))
            using (StreamWriter sw = new StreamWriter(file))
                foreach (string msg in msges)
                    sw.WriteLine($"[{DateTime.Now:yy-MM-dd HH:mm}] {msg}");
        }
        #endregion

        #region TIMER
        private System.Timers.Timer _timer = null;
        public System.Timers.Timer Timer
        {
            get
            {
                if (_timer == null)
                    _timer = CreateTimer();
                else
                    if (!_timer.Enabled)
                    _timer.Start();
                return _timer;
            }
        }

        private double TimerInterval
        {
            get
            {
                return 1000 * 20;
            }
        }

        private System.Timers.Timer CreateTimer()
        {
            System.Timers.Timer _timer = new System.Timers.Timer(TimerInterval);
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            _timer.Disposed += new EventHandler(_timer_Disposed);
            _timer.Enabled = true;
            _timer.Start();
            return _timer;
        }
        private void _timer_Disposed(object sender, EventArgs e)
        {
            Trace("Dispose");
            _timer = null;
        }
        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Trace("Elapsed");
            args = "FirstAgr SecondArg";
            Thread kill = new Thread(StartAndKillProcess);
            kill.Start();
        }

        private void StartAndKillProcess()
        {
            Process.Start(_path, args);
            //Thread.Sleep(15000);
            //try
            //{
            //    foreach (Process proc in Process.GetProcessesByName("CustomWindow"))
            //    {
            //        proc.Kill();
            //    }
            //}
            //catch (Exception ex) { Trace(ex.Message); }
        }
        #endregion


    }
}
