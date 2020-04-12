using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;

namespace TrackerUI
{
    public partial class CreatePrizeForm : Form
    {
        public CreatePrizeForm()
        {
            InitializeComponent();
        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                //Creates new model object of type PrizeModel using the inputs from the CreatePrizeForm windows form.
                //The constructor converts the string inputs to their respective data types and sets them to their respective properties.
                PrizeModel model = new PrizeModel(placeNameValue.Text, placeNumberValue.Text, prizeAmountValue.Text, prizePercentageValue.Text);

                //For each data connection type, the model is created on the database.
                foreach (IDataConnection db in GlobalConfig.Connections)
                {
                    db.CreatePrize(model);
                }

                //If the form was valid, the input string values on the CreatePrizeForm windows form will be reset.
                placeNameValue.Text = "";
                placeNumberValue.Text = "";
                prizeAmountValue.Text = "0";
                prizePercentageValue.Text = "0";
            }

            //Displays simple error message box if the forms is invalid.
            else
            {
                MessageBox.Show("This form has invalid information. Please check it and try again.");
            }

        }

        //Validate fields from the Create Prize form.
        private bool ValidateForm()
        {
            //Sets default output value to true.
            bool output = true;

            //Sets default placeNumber value to 0.
            int placeNumber = 0;

            //Takes input string placeNumberValue.Text and tries to convert it to int.
            //The converted int value will be set to placeNumber
            //True or false will be returned depending on if the value can be converted or not.
            bool placeNumberValidNumber = int.TryParse(placeNumberValue.Text, out placeNumber);

            //If the number is not valid, the output is set to false.
            if (placeNumberValidNumber == false)
            {
                output = false;
            }

            //If the number is 0 or a negative value, the output is set to false.
            if (placeNumber < 1)
            {
                output = false;
            }

            //If the place name is empty, the output is set to false.
            if (placeNameValue.Text.Length == 0)
            {
                output = false;
            }

            //Sets the default prizeAmount value as a decimal and to 0.
            decimal prizeAmount = 0;

            //Sets the default prizePercentage value to 0.
            double prizePercentage = 0;

            //Takes input string prizeAmountValue.Text and tries to convert it to decimal.
            //The converted int value will be set to prizeAmount
            //True or false will be returned depending on if the value can be converted or not.
            bool prizeAmountValid = decimal.TryParse(prizeAmountValue.Text, out prizeAmount);

            //Takes input string prizePercentageValue.Text and tries to convert it to int.
            //The converted int value will be set to prizePercentage
            //True or false will be returned depending on if the value can be converted or not.
            bool prizePercentageValid = double.TryParse(prizePercentageValue.Text, out prizePercentage);

            //If either the amount or the percentage failed to convert, outputs false.
            if (prizeAmountValid == false || prizePercentageValid == false)
            {
                output = false;
            }

            //If the amount and percentage are both 0, outputs false.
            if (prizeAmount <= 0 && prizePercentage <= 0)
            {
                output = false;
            }

            //If the percentage is not a value from 0 to 100, outputs false.
            if (prizePercentage < 0 || prizePercentage > 100)
            {
                output = false;
            }

            return output;
        }
    }
}
