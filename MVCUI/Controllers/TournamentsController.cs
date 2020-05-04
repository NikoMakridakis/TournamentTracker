using MVCUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace MVCUI.Controllers
{
    public class TournamentsController : Controller
    {
        // GET: Tournaments
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Details(int id, int roundId = 0)
        {
            List<TournamentModel> tournaments = GlobalConfig.Connection.GetTournament_All();
            try
            {
                TournamentMVCDetailsModel input = new TournamentMVCDetailsModel();
                TournamentModel t = tournaments.Where(x => x.Id == id).First();

                input.TournamentName = t.TournamentName;

                var orderedRounds = t.Rounds.OrderBy(x => x.First().MatchupRound).ToList();
                bool activeFound = false;


                for (int i = 0; i < orderedRounds.Count; i++)
                {
                    RoundStatus status = RoundStatus.Locked;

                    if (!activeFound)
                    {
                        if (orderedRounds[i].TrueForAll(x => x.Winner != null))
                        {
                            status = RoundStatus.Complete;
                        }
                        else
                        {
                            status = RoundStatus.Active;
                            activeFound = true;
                            if (roundId == 0)
                            {
                                roundId = i + 1;
                            }
                        }
                    }

                    input.Rounds.Add(new RoundMVCModel { RoundName = "Round " + (i + 1), Status = status, RoundNumber = i + 1 });
                }

                input.Matchups = GetMatchups(orderedRounds[roundId - 1]);

                return View(input);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private List<MatchupMVCModel> GetMatchups(List<MatchupModel> input)
        {
            List<MatchupMVCModel> output = new List<MatchupMVCModel>();

            foreach (var item in input)
            {
                int teamTwoId = 0;
                string teamOneName = "";
                string teamTwoName = "Bye";
                double teamTwoScore = 0;

                if (item.Entries[0].TeamCompeting == null)
                {
                    teamOneName = "To Be Determined";
                }
                else
                {
                    teamOneName = item.Entries[0].TeamCompeting.TeamName;
                }

                if (item.Entries.Count > 1)
                {
                    teamTwoId = item.Entries[1].Id;
                    if (item.Entries[1].TeamCompeting == null)
                    {
                        teamTwoName = "To Be Determined";
                    }
                    else
                    {
                        teamTwoName = item.Entries[1].TeamCompeting.TeamName;
                    }
                    teamTwoScore = item.Entries[1].Score;
                }

                output.Add(new MatchupMVCModel
                {
                    MatchupId = item.Id,
                    FirstTeamMatchupEntryId = item.Entries[0].Id,
                    FirstTeamName = teamOneName,
                    FirstTeamScore = item.Entries[0].Score,
                    SecondTeamMatchupEntryId = teamTwoId,
                    SecondTeamName = teamTwoName,
                    SecondTeamScore = teamTwoScore
                });
            }

            return output;
        }

        // GET: Tournaments/Create
        public ActionResult Create()
        {
            TournamentMVCCreateModel input = new TournamentMVCCreateModel();
            List<TeamModel> allTeams = GlobalConfig.Connection.GetTeam_All();
            List<PrizeModel> allPrizes = GlobalConfig.Connection.GetPrizes_All();

            input.EnteredTeams = allTeams.Select(x => new SelectListItem { Text = x.TeamName, Value = x.Id.ToString() }).ToList();
            input.Prizes = allPrizes.Select(x => new SelectListItem { Text = x.PlaceName, Value = x.Id.ToString() }).ToList();

            return View(input);
        }

        // POST: Tournaments/Create
        [ValidateAntiForgeryToken()]
        [HttpPost]
        public ActionResult Create(TournamentMVCCreateModel model)
        {
            try
            {
                if (ModelState.IsValid && model.SelectedEnteredTeams.Count > 0)
                {
                    TournamentModel t = new TournamentModel();
                    t.TournamentName = model.TournamentName;
                    t.EntryFee = model.EntryFee;
                    t.EnteredTeams = model.SelectedEnteredTeams.Select(x => new TeamModel { Id = int.Parse(x) }).ToList();
                    t.Prizes = model.SelectedPrizes.Select(x => new PrizeModel { Id = int.Parse(x) }).ToList();

                    //Wire our matchups
                    TournamentLogic.CreateRounds(t);

                    GlobalConfig.Connection.CreateTournament(t);

                    t.AlertUsersToNewRound();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Create");
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}