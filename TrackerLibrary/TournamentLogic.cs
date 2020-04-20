using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary
{
    public static class TournamentLogic
    {
        public static void CreateRounds(TournamentModel model)
        {
            List<TeamModel> randomizedTeams = RandomizeTeamOrder(model.EnteredTeams);
            int rounds = FindNumberOfRounds(randomizedTeams.Count);
            int byes = NumberOfByes(rounds, randomizedTeams.Count);

            model.Rounds.Add(CreateFirstRound(byes, randomizedTeams));

            CreateOtherRounds(model, rounds);

        }

        /// <summary>
        /// Creates all of the rounds, excluding the first round.
        /// </summary>
        /// <param name="model">The model of type TournamentModel</param>
        /// <param name="rounds">The number of rounds</param>
        private static void CreateOtherRounds(TournamentModel model, int rounds)
        {
            int round = 2;
            List<MatchupModel> previousRound = model.Rounds[0];
            List<MatchupModel> currRound = new List<MatchupModel>();
            MatchupModel currMatchup = new MatchupModel();

            while (round <= rounds)
            {
                foreach (MatchupModel match in previousRound)
                {
                    currMatchup.Entries.Add(new MatchupEntryModel { ParentMatchup = match });
                
                    if (currMatchup.Entries.Count > 1)
                    {
                        currMatchup.MatchupRound = round;
                        currRound.Add(currMatchup);
                        currMatchup = new MatchupModel();
                    }
                }

                model.Rounds.Add(currRound);
                previousRound = currRound;

                currRound = new List<MatchupModel>();
                round += 1;

            }
        }

        /// <summary>
        /// Creates a list of MatchupModel for the first round.
        /// </summary>
        /// <param name="byes">The number of byes, or placeholder teams</param>
        /// <param name="teams">The number of teams</param>
        /// <returns></returns>
        private static List<MatchupModel> CreateFirstRound(int byes, List<TeamModel> teams)
        {
            List<MatchupModel> output = new List<MatchupModel>();
            MatchupModel curr = new MatchupModel();

            foreach (TeamModel team in teams)
            {
                curr.Entries.Add(new MatchupEntryModel { TeamCompeting = team });

                if (byes > 0 || curr.Entries.Count > 1)
                {
                    curr.MatchupRound = 1;
                    output.Add(curr);
                    curr = new MatchupModel();

                    if (byes > 0)
                    {
                        byes -= 1;
                    }
                }
            }

            return output;
        }

        /// <summary>
        /// Adds number of byes depending on the number of teams and rounds.
        /// The number of byes represents a placeholder team for the first round.
        /// </summary>
        /// <param name="rounds">The number of rounds</param>
        /// <param name="numberOfTeams">The number of teams</param>
        /// <returns></returns>
        private static int NumberOfByes(int rounds, int numberOfTeams)
        {
            int output = 0;
            int totalTeams = 1;

            for (int i = 1; i <= rounds; i++)
            {
                totalTeams *= 2;
            }

            output = totalTeams - numberOfTeams;

            return output;
        }

        /// <summary>
        /// Finds the number of rounds to be played from the total team count.
        /// </summary>
        /// <param name="teamCount">The number of competing teams</param>
        /// <returns>The number of rounds to be played</returns>
        private static int FindNumberOfRounds(int teamCount)
        {
            int output = 1;
            int val = 2;

            while (val < teamCount)
            {
                output += 1;
                val *= 2;
            }

            return output;
        }

        /// <summary>
        /// Randomizes the order of the teams.
        /// </summary>
        /// <param name="teams">List of teams of type TeamModel</param>
        /// <returns>Random list of teams of type TeamModel</returns>
        private static List<TeamModel> RandomizeTeamOrder(List<TeamModel> teams)
        {
            //randomizes list by using Guid
            return teams.OrderBy(x => Guid.NewGuid()).ToList();
        }

    }
}
