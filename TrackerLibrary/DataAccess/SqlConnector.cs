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
        //name of database
        private const string db = "Tournament";
        public PersonModel CreatePerson(PersonModel model)
        {
            //While using the database connection, saves Person
            //Closes the database connection when not in use.
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
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
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
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

        public TeamModel CreateTeam(TeamModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                //The values to be passed into the StoredProcedure dbo.spTeams_Insert
                var p = new DynamicParameters();
                p.Add("@TeamName", model.TeamName);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                //Executes the StoredProcedure dbo.spTeams_Insert
                connection.Execute("dbo.spTeams_Insert", p, commandType: CommandType.StoredProcedure);

                //Looks at List p, gets the "@id" value, returns model.Id of type int.
                model.Id = p.Get<int>("@id");

                foreach (PersonModel tm in model.TeamMembers)
                {
                    //The values to be passed into the StoredProcedure dbo.spTeamMembers_Insert
                    p = new DynamicParameters();
                    p.Add("@TeamId", model.Id);
                    p.Add("@PersonId", tm.Id);

                    //Executes the StoredProcedure dbo.spTeamMembers_Insert
                    connection.Execute("dbo.spTeamMembers_Insert", p, commandType: CommandType.StoredProcedure);
                }

                return model;
            }
        }

        public List<PersonModel> GetPerson_All()
        {
            List<PersonModel> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<PersonModel>("dbo.spPeople_GetAll").ToList();
            }

            return output;
        }
    }
}
