using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using TrackerLibrary.DataAccess.TextHelpers;

namespace TrackerLibrary.DataAccess
{
    /// <summary>
    /// Represents a Text Connector which inherits the ability to create and save data from the IDataConnection interface.
    /// </summary>
    public class TextConnector : IDataConnection
    {

        private const string PrizesFile = "PrizeModels.csv";

        /// <summary>
        /// Saves a List of type PrizeModel as the Text File "PrizeModels.csv" at ...\TournamentTracker\TextData\PrizeModels.csv
        /// </summary>
        /// <param name="model">model of type PrizeModel</param>
        /// <returns></returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            //Loads the text file and converts it to a list of type PrizeModel
            List<PrizeModel> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            int currentId = 1;

            //Finds the max Id and sets currentId to the max Id + 1
            if (prizes.Count > 0)
            {
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = currentId;

            //Add the new record with the new Id (max Id +1)
            prizes.Add(model);

            prizes.SaveToPrizeFile(PrizesFile);

            return model;
        }
    }
}
