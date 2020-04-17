using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess.TextHelpers
{
    public static class TextConnectorProcessor
    {
        /// <summary>
        /// Creates the file path: ...\TournamentTracker\TextData\PrizeModels.csv
        /// </summary>
        /// <param name="fileName">Name of the .csv text file</param>
        /// <returns>The complete file path: ...\TournamentTracker\TextData\PrizeModels.csv</returns>
        public static string FullFilePath(this string fileName)
        {
            return $"{ConfigurationManager.AppSettings["filePath"]}\\{fileName}";
        }

        /// <summary>
        /// Creates a list of string representing all of the lines within a text file.
        /// </summary>
        /// <param name="file">Complete file path: ...\TournamentTracker\TextData\PrizeModels.csv</param>
        /// <returns>Represents all the lines within a text file. </returns>
        public static List<string> LoadFile(this string file)
        {
            if (File.Exists(file) == false)
            {
                return new List<string>();
            }

            return File.ReadAllLines(file).ToList();
        }

        /// <summary>
        /// Creates a list of PrizeModel from a list of string within a text file.
        /// </summary>
        /// <param name="lines">Represents all the lines within a text file.</param>
        /// <returns>List of PrizeModel</returns>
        public static List<PrizeModel> ConvertToPrizeModels(this List<string> lines)
        {
            List<PrizeModel> output = new List<PrizeModel>();

            foreach (string line in lines)
            {
                //Splits the lines with a comma ','.
                string[] cols = line.Split(',');

                PrizeModel p = new PrizeModel();
                p.Id = int.Parse(cols[0]);
                p.PlaceNumber = int.Parse(cols[1]);
                p.PlaceName = cols[2];
                p.PrizeAmount = decimal.Parse(cols[3]);
                p.PrizePercentage = double.Parse(cols[4]);
                output.Add(p);
            }
            
            return output;
        }

        /// <summary>
        /// Creates a list of PersonModel from a list of string within a text file.
        /// </summary>
        /// <param name="lines">Represents all the lines within a text file.</param>
        /// <returns>List of PersonModel</returns>
        public static List<PersonModel> ConvertToPersonModels(this List<string> lines)
        {
            List<PersonModel> output = new List<PersonModel>();

            foreach(string line in lines)
            {

                //Splits the lines with a comma ','.
                string[] cols = line.Split(',');

                PersonModel p = new PersonModel();
                p.Id = int.Parse(cols[0]);
                p.FirstName = cols[1];
                p.LastName = cols[2];
                p.EmailAddress = cols[3];
                p.CellPhoneNumber = cols[4];
                output.Add(p);
            }

            return output;
        }

        /// <summary>
        /// Creates a list of TeamModel from a list of string within a text file.
        /// </summary>
        /// <param name="lines">Represents all the lines within a text file.</param>
        /// <returns></returns>
        public static List<TeamModel> ConvertToTeamModels(this List<string> lines, string peopleFileName)
        {
            List<TeamModel> output = new List<TeamModel>();
            List<PersonModel> people = peopleFileName.FullFilePath().LoadFile().ConvertToPersonModels();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                TeamModel t = new TeamModel();
                t.Id = int.Parse(cols[0]);
                t.TeamName = cols[1];

                string[] personIds = cols[2].Split('|');

                foreach (string id in personIds)
                {
                    t.TeamMembers.Add(people.Where(x => x.Id == int.Parse(id)).First());
                }

                output.Add(t);
            }

            return output;
        }

        /// <summary>
        /// Creates a list of TournamentModel from a list of string within a text file.
        /// </summary>
        /// <param name="lines">Represents all the lines within a text file.</param>
        /// <returns></returns>
        public static List<TournamentModel> ConvertToTournamentModels(
            this List<string> lines,
            string teamFileName,
            string peopleFileName,
            string prizeFileName)
        {
            //Id,TournamentName,EntryFee,(id|id|id|id - Entered Teams), (id|id|id|id - Prizes), (Rounds - list within list - id^id^id|id^id^id|id^id^id)
            List<TournamentModel> output = new List<TournamentModel>();
            List<TeamModel> teams = teamFileName.FullFilePath().LoadFile().ConvertToTeamModels(peopleFileName);
            List<PrizeModel> prizes = prizeFileName.FullFilePath().LoadFile().ConvertToPrizeModels();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                TournamentModel tm = new TournamentModel();
                tm.Id = int.Parse(cols[0]);
                tm.TournamentName = cols[1];
                tm.EntryFee = decimal.Parse(cols[2]);

                string[] teamIds = cols[3].Split('|');

                foreach (string id in teamIds)
                {
                    tm.EnteredTeams.Add(teams.Where(x => x.Id == int.Parse(id)).First());
                }

                string[] prizeIds = cols[4].Split('|');

                foreach (string id in prizeIds)
                {
                    tm.Prizes.Add(prizes.Where(x => x.Id == int.Parse(id)).First());
                }

                //TODO = Capture rounds information

                output.Add(tm);
            }

            return output;
        }

        /// <summary>
        /// Saves a new text file for each PrizeModel
        /// </summary>
        /// <param name="models">List of PrizeModel</param>
        /// <param name="fileName"></param>
        public static void SaveToPrizeFile(this List<PrizeModel>models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (PrizeModel p in models)
            {
                lines.Add($"{p.Id},{p.PlaceNumber},{p.PlaceName},{p.PrizeAmount},{p.PrizePercentage}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        /// <summary>
        /// Saves a new text file for each PersonModel
        /// </summary>
        /// <param name="models">List of PersonModel</param>
        /// <param name="fileName"></param>
        public static void SaveToPeopleFile(this List<PersonModel>models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (PersonModel p in models)
            {
                lines.Add($"{p.Id},{p.FirstName},{p.LastName},{p.EmailAddress},{p.CellPhoneNumber}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        /// <summary>
        /// Saves a new team file for each TeamModel
        /// </summary>
        /// <param name="models">List of TeamModel</param>
        /// <param name="fileName"></param>
        public static void SaveToTeamFile(this List<TeamModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (TeamModel t in models)
            {
                lines.Add($"{t.Id},{t.TeamName},{ConvertPeopleListToString(t.TeamMembers)}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }


        /// <summary>
        /// Saves a new tournament file for each TournamentModel
        /// </summary>
        /// <param name="models">List of TournamentModel</param>
        public static void SaveToTournamentFile(this List<TournamentModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (TournamentModel tm in models)
            {
                lines.Add($@"{tm.Id},
                    {tm.TournamentName},
                    {tm.EntryFee},
                    {ConvertTeamListToString(tm.EnteredTeams)},
                    {ConvertPrizeListToString(tm.Prizes)},
                    {ConvertRoundListToString(tm.Rounds)}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        /// <summary>
        /// Converts List of List of MatchupModel to string with each List seperated by '|'
        /// and each List of MatchupModel seperated by '^'.
        /// </summary>
        /// <param name="rounds">List of List of MatchupModel</param>
        /// <returns></returns>
        public static string ConvertRoundListToString(List<List<MatchupModel>> rounds)
        {
            //starts empty output string
            string output = "";

            if (rounds.Count == 0)
            {
                return "";
            }

            //adds the Id of each List of MatchupModel to the string, and seperates each with '|'
            foreach (List<MatchupModel> r in rounds)
            {
                output += $"{ConvertMatchupListToString(r)}|";
            }

            //removes the last '|' from the end of the string
            output = output.Substring(0, output.Length - 1);

            return output;
        }

        /// <summary>
        /// Converts List of MatchupModel to string with each prize seperated by '|'
        /// </summary>
        /// <param name="matchups">List of MatchupModel</param>
        /// <returns></returns>
        private static string ConvertMatchupListToString(List<MatchupModel> matchups)
        {
            //starts empty output string
            string output = "";

            if (matchups.Count == 0)
            {
                return "";
            }

            //adds the Id of each MatchupModel to the string, and seperates each with '^'
            foreach (MatchupModel m in matchups)
            {
                output += $"{m.Id}^";
            }

            //removes the last '|' from the end of the string
            output = output.Substring(0, output.Length - 1);

            return output;
        }

        /// <summary>
        /// Converts List of PrizeModel to string with each prize seperated by '|'
        /// </summary>
        /// <param name="prizes">List of PrizeModel</param>
        /// <returns></returns>
        public static string ConvertPrizeListToString(List<PrizeModel> prizes)
        {
            //starts empty output string
            string output = "";

            if (prizes.Count == 0)
            {
                return "";
            }

            //adds the Id of each PrizeModel to the string, and seperates each with '|'
            foreach (PrizeModel p in prizes)
            {
                output += $"{p.Id}|";
            }

            //removes the last '|' from the end of the string
            output = output.Substring(0, output.Length - 1);

            return output;
        }

        /// <summary>
        /// Converts List of TeamModel to string with each team seperated by '|'
        /// </summary>
        /// <param name="teams">List of TeamModel</param>
        /// <returns></returns>
        public static string ConvertTeamListToString(List<TeamModel> teams)
        {
            //starts empty output string
            string output = "";

            if (teams.Count == 0)
            {
                return "";
            }

            //adds the Id of each TeamModel to the string, and seperates each with '|'
            foreach (TeamModel t in teams)
            {
                output += $"{t.Id}|";
            }

            //removes the last '|' from the end of the string
            output = output.Substring(0, output.Length - 1);

            return output;
        }

        /// <summary>
        /// Converts List of PersonModel to string with each person seperated by '|'
        /// </summary>
        /// <param name="people">List of PersonModel</param>
        /// <returns></returns>
        private static string ConvertPeopleListToString(List<PersonModel> people)
        {
            //starts empty output string
            string output = "";

            if (people.Count == 0)
            {
                return "";
            }
            
            //adds the Id of each PersonModel to the string, and seperates each with '|'
            foreach (PersonModel p in people)
            {
                output += $"{p.Id}|";
            }

            //removes the last '|' from the end of the string
            output = output.Substring(0, output.Length - 1);

            return output;
        }

    }
}

