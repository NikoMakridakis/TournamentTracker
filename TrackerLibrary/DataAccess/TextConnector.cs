using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    /// <summary>
    /// Represents a Text Connector which inherits the ability to create and save data from the IDataConnection interface.
    /// </summary>
    public class TextConnector : IDataConnection
    {
        //TODO - Make the CreatePrize method save to text files.
        public PrizeModel CreatePrize(PrizeModel model)
        {
            model.Id = 1;
            return model;
        }
    }
}
