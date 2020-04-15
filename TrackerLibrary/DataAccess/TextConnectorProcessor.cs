﻿using System;
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

    }
}
