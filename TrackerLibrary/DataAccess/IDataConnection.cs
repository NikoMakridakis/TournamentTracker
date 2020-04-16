using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    /// <summary>
    /// Allows a data connection to create and save data.
    /// </summary>
    public interface IDataConnection
    {
        /// <summary>
        /// Saves a new prize to the data connection.
        /// </summary>
        /// <param name="model">The prize information of type PrizeModel.</param>
        /// <returns>The prize information.</returns>
        PrizeModel CreatePrize(PrizeModel model);


        /// <summary>
        /// Saves a new person to the data connection.
        /// </summary>
        /// <param name="model">The person information of type PersonModel.</param>
        /// <returns>The prize information.</returns>
        PersonModel CreatePerson(PersonModel model);

        /// <summary>
        /// Saves a new team to the data connection.
        /// </summary>
        /// <param name="model">The team information of type TeamModel.</param>
        /// <returns>The team information.</returns>
        TeamModel CreateTeam(TeamModel model);

        /// <summary>
        /// Returns all of the stored people.
        /// </summary>
        /// <returns></returns>
        List<PersonModel> GetPerson_All();

        /// <summary>
        /// Returns all of the stored teams.
        /// </summary>
        /// <returns></returns>
        List<TeamModel> GetTeam_All();


    }
}
