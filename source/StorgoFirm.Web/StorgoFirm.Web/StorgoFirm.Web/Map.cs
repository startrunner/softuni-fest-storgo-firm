using StorgoFirm.Web.ViewModels;

namespace StorgoFirm.Web
{
    public static class Map
    {
        public static Sport ToModel(EventSportViewModel viewModel)
        {
            return new Sport {
                Name = viewModel.Name
            };
        }

        public static League ToModel(EventLeagueViewModel viewModel)
        {
            return new League {
                Name = viewModel.Name,
                SportId = (long)viewModel.SportId
            };
        }

        public static SportEvent ToModel(SportEventViewModel viewModel)
        {
            return new SportEvent {
                Name = viewModel.Name,
                DateUtc = viewModel.DateUtc,
                HomeTeamScore = viewModel.HomeTeamScore,
                AwayTeamScore = viewModel.AwayTeamScore,
                HomeTeamOdds = viewModel.HomeTeamOdds,
                AwayTeamOdds = viewModel.AwayTeamOdds,
                DrawOdds = viewModel.DrawOdds,
                SportId = (long)viewModel.Sport.Id,
                LeagueId = (long)viewModel.League.Id,
            };
        }

        public static EventLeagueViewModel ToViewModel(League model)
        {
            return new EventLeagueViewModel {
                Id = (ulong)model.Id,
                Name = model.Name,
                SportId = (ulong)model.SportId
            };
        }

        public static EventSportViewModel ToViewModel(Sport model)
        {
            return new EventSportViewModel {
                Id = (ulong)model.Id,
                Name = model.Name
            };
        }

        public static SportEventViewModel ToViewModel(SportEvent model)
        {
            return new SportEventViewModel {
                Id = (ulong)model.Id,
                AwayTeamScore = model.AwayTeamScore,
                HomeTeamScore = model.HomeTeamScore,
                AwayTeamOdds = model.AwayTeamOdds,
                HomeTeamOdds = model.HomeTeamOdds,
                DrawOdds = model.DrawOdds,
                DateUtc = model.DateUtc,
                League = ToViewModel(model.League),
                Name = model.Name,
                Sport = ToViewModel(model.Sport)
            };
        }
    }
}
