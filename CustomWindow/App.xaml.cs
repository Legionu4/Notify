using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace MyCustomWindowNamespace
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {
            App app = new App();
            app.StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
            app.Run();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //#if DEBUG
            //            //Arguments.args = e.Args;
            //            Arguments.args = new string[] { "Hello World", "Argument number two", "Argument number three" };
            //#else
            Arguments.args = e.Args;
            base.OnStartup(e);
            //#endif
        }
    }

}

