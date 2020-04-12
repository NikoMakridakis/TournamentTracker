using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        /// <summary>
        /// Creates and initializes an empty Connections list of IDataConnection types.
        /// Private set - Only methods inside GlobalConfig class can change the values of Connections.
        /// Public get - Any method can read the values of Connections.
        /// </summary>
        public static List<IDataConnection> Connections { get; private set; } = new List<IDataConnection>();

        /// <summary>
        /// Sets the databases where data can be saved to.
        /// </summary>
        /// <param name="database">SQL Server</param>
        /// <param name="textFiles">Text File</param>
        public static void InitialzeConnections(bool database, bool textFiles)
        {
            if (database == true)
            {
                // TODO - Set up the SQL Connector
                SqlConnector sql = new SqlConnector();
                Connections.Add(sql);
            }

            if (textFiles == true)
            {
                // TODO - create the Text Connector
                TextConnector text = new TextConnector();
                Connections.Add(text);
            }

        }
    }
}
