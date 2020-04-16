using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// Represents one team.
    /// </summary>
    public class TeamModel
    {
        /// <summary>
        /// Represents the unique identifier of the Team.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Represents the team name.
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// Represents the set of people on the team.
        /// </summary>
        public List<PersonModel> TeamMembers { get; set; } = new List<PersonModel>();
    }
}
