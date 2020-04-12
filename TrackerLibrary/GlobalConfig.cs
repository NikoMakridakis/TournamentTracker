using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        // Creates and initializes an empty Connections list of IDataConnection types.
        // private set - Only methods inside GlobalConfig class can change the values of Connections.
        // public get - Any method can read the values of Connections.
        public static List<IDataConnection> Connections { get; private set; } = new List<IDataConnection>();

        //This method is called at startup.
        //Chooses which places data can be saved to.
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
                // TODO - create the text connection
                TextConnector text = new TextConnector();
                Connections.Add(text);
            }

        }
    }
}
