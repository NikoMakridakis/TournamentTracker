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
        /// <param name="model">The prize information.</param>
        /// <returns>The prize information.</returns>
        PrizeModel CreatePrize(PrizeModel model);

        PersonModel CreatePerson(PersonModel model);
    }
}
