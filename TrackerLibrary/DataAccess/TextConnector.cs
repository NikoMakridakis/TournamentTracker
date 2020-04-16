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
        private const string PeopleFile = "PersonModels.csv";
        private const string TeamFile = "TeamModels.csv";


        /// <summary>
        /// Saves a List of type PersonModel as the Text File "PersonModels.csv" at ...\TournamentTracker\TextData\PersonModels.csv
        /// </summary>
        /// <param name="model">model of type PersonModel</param>
        /// <returns></returns>
        public PersonModel CreatePerson(PersonModel model)
        {
            //Loads the text file and converts it to a list of type PersonModel
            List<PersonModel> people = PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();

            int currentId = 1;

            //Finds the max Id and sets currentId to the max Id + 1
            if (people.Count > 0)
            {
                currentId = people.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

            //Add the new record with the new Id (max Id +1)
            people.Add(model);

            people.SaveToPeopleFile(PeopleFile);

            return model;
        }

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

        public List<PersonModel> GetPerson_All()
        {
            return PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();
        }

        public TeamModel CreateTeam(TeamModel model)
        {
            List<TeamModel> teams = TeamFile.FullFilePath().LoadFile().ConvertToTeamModels(PeopleFile);

            int currentId = 1;

            //Finds the max Id and sets currentId to the max Id + 1
            if (teams.Count > 0)
            {
                currentId = teams.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = currentId;

            teams.Add(model);

            teams.SaveToTeamFile(TeamFile);
            
            return model;
        }
    }
}
