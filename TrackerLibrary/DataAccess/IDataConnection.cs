﻿using System;
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
        void CreatePrize(PrizeModel model);

        void CreatePerson(PersonModel model);

        void CreateTeam(TeamModel model);

        void CreateTournament(TournamentModel model);

        void UpdateMatchup(MatchupModel model);

        void CompleteTournament(TournamentModel model);

        List<TeamModel> GetTeam_All();

        List<PersonModel> GetPerson_All();

        List<PrizeModel> GetPrizes_All();

        List<TournamentModel> GetTournament_All();
    }
}
