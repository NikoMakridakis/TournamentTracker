using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.DataAccess;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        /// <summary>
        /// Creates Connection of type IDataConnection.
        /// </summary>
        public static IDataConnection Connection { get; private set; }

        /// <summary>
        /// Sets the databases where data can be saved to.
        /// </summary>
        /// <param name="db">Database source</param>
        public static void InitialzeConnections(DatabaseType db)
        {
            if (db == DatabaseType.Sql)
            {
                SqlConnector sql = new SqlConnector();
                Connection = sql;
            }

            else if (db == DatabaseType.TextFile)
            {
                // TODO - create the Text Connector
                TextConnector text = new TextConnector();
                Connection = text;
            }
        }
        /// <summary>
        /// Returns connectionString from App.Config based on the input name string.
        /// </summary>
        /// <param name="name">The connectionStrings name from App.Config</param>
        /// <returns>connectionString from App.Config: Server.\sql2019;Database=Tournament;Trusted_Connection=True;</returns>
        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
