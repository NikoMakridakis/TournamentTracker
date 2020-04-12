using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    /// <summary>
    /// Represents a SQL Connector which inherits the ability to create and save data from the IDataConnection interface.
    /// </summary>
    public class SqlConnector : IDataConnection
    {
        //TODO - Make the CreatePrize method save to the database.
        public PrizeModel CreatePrize(PrizeModel model)
        {
            model.Id = 1;
            return model;
        }
    }
}
