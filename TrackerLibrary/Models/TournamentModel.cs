using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// Represents one tournament.
    /// </summary>
    public class TournamentModel
    {
        /// <summary>
        /// Represents the tournament name.
        /// </summary>
        public string TournamentName { get; set; }
        /// <summary>
        /// Represents the entry fee of the tournament.
        /// </summary>
        public decimal EntryFee { get; set; }
        /// <summary>
        /// Represents the set of teams entered in the tournament.
        /// </summary>
        public List<TeamModel> EnteredTeams { get; set; }
        /// <summary>
        /// Represents the set of prizes in the tournament.
        /// </summary>
        public List<PrizeModel> Prizes { get; set; } = new List<PrizeModel>();
        /// <summary>
        /// Represents the set of matchups.
        /// </summary>
        public List<List<MatchupModel>> Rounds { get; set; } = new List<List<MatchupModel>>();

    }
}
