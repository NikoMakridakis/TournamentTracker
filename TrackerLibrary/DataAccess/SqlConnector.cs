using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    /// <summary>
    /// Represents a SQL Connector which inherits the ability to create and save data from the IDataConnection interface.
    /// </summary>
    public class SqlConnector : IDataConnection
    {
        public PersonModel CreatePerson(PersonModel model)
        {
            //While using the database connection, saves Person
            //Closes the database connection when not in use.
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("Tournament")))
            {
                //The values to be passed into the StoredProcedure dbo.spPeople_Insert
                var p = new DynamicParameters();
                p.Add("@FirstName", model.FirstName);
                p.Add("@LastName", model.LastName);
                p.Add("@EmailAddress", model.EmailAddress);
                p.Add("@CellPhoneNumber", model.CellPhoneNumber);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                //Executes the StoredProcedure dbo.spPeople_Insert
                connection.Execute("dbo.spPeople_Insert", p, commandType: CommandType.StoredProcedure);

                //Looks at List p, gets the "@id" value, returns model.Id of type int.
                model.Id = p.Get<int>("@id");

                return model;
            }
        }

        public PrizeModel CreatePrize(PrizeModel model)
        {
            //While using the database connection, saves Prize
            //Closes the database connection when not in use.
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("Tournament")))
            {
                //The values to be passed into the StoredProcedure dbo.spPrizes_Insert
                var p = new DynamicParameters();
                p.Add("@PlaceNumber", model.PlaceNumber);
                p.Add("@PlaceName", model.PlaceName);
                p.Add("@PrizeAmount", model.PrizeAmount);
                p.Add("@PrizePercentage", model.PrizePercentage);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                //Executes the StoredProcedure dbo.spPrizes_Insert
                connection.Execute("dbo.spPrizes_Insert", p, commandType: CommandType.StoredProcedure);

                //Looks at List p, gets the "@id" value, returns model.Id of type int.
                model.Id = p.Get<int>("@id");

                return model;
            }
        }
        
    }
}
