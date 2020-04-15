using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// Represents one prize.
    /// </summary>
    public class PrizeModel
    {
        /// <summary>
        /// Represents the unique identifier of the prize.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Represents the place number of the prize.
        /// </summary>
        public int PlaceNumber { get; set; }

        /// <summary>
        /// Represents the place name of the prize.
        /// </summary>
        public string PlaceName { get; set; }

        /// <summary>
        /// Represents the prize amount.
        /// </summary>
        public decimal PrizeAmount { get; set; }

        /// <summary>
        /// Represents the prize percentage.
        /// </summary>
        public double PrizePercentage { get; set; }

        public PrizeModel()
        {

        }

        //This constructor will convert the input strings from the Create Prize windows form to their correct data types.
        //It will take the converted data values and set them to their respective properties.
        public PrizeModel(string placeName, string placeNumber, string prizeAmount, string prizePercentage)
        {
            //The placeName string from the Create Prize form is set to the PlaceName property
            PlaceName = placeName;

            //Tries to convert the input string placeNumber to int placeNumberValue.
            //If successs, sets the converted int placeNumberValue to the PlaceNumber property.
            int placeNumberValue = 0;
            int.TryParse(placeNumber, out placeNumberValue);
            PlaceNumber = placeNumberValue;

            //Tries to convert the input string prizeAmount to decimal prizeAmountValue.
            //If successs, sets the converted decimal prizeAmountValue to the PrizeAmount property.
            decimal prizeAmountValue = 0;
            decimal.TryParse(prizeAmount, out prizeAmountValue);
            PrizeAmount = prizeAmountValue;

            //Tries to convert the input string prizePercentage to double prizePercentageValue.
            //If successs, sets the converted double prizePercentageValue to the PrizePercentage property.
            double prizePercentageValue = 0;
            double.TryParse(prizePercentage, out prizePercentageValue);
            PrizePercentage = prizePercentageValue;
        }
    }
}
