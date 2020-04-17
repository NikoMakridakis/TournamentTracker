using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrackerUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Initialize the database connections.
            TrackerLibrary.GlobalConfig.InitialzeConnections(TrackerLibrary.DatabaseType.TextFile);

            //Launches CreatePrizeForm on startup.
            //Application.Run(new CreatePrizeForm());

            //Launches CreateTeamForm on startup.
            //Application.Run(new CreateTeamForm());

            //Launches CreateTournamentForm on startup.
            Application.Run(new CreateTournamentForm());

            //Startup form of application.
            //Application.Run(new TournamentDashboardForm());
        }
    }
}
