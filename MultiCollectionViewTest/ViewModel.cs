using MultiCollectionViewTest.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MultiCollectionViewTest
{
    public class ViewModel : MyBindableObject
    {
        private ObservableCollection<Team> _wildCardTeams;
        private ObservableCollection<Team> _quarterFinalTeams;
        public Dictionary<int, Team> TeamDictionary { get; set; }

        private ICommand _wildCardTeamSelectionChangedCommand;
        public ICommand WildCardTeamSelectionChangedCommand
        {
            get { return _wildCardTeamSelectionChangedCommand = _wildCardTeamSelectionChangedCommand ?? new Command(async () => await OnWildCardSelectionChangedExecuted()); }
        }

        private ICommand _quarterFinalTeamSelectionChangedCommand;
        public ICommand QuarterFinalTeamSelectionChangedCommand
        {
            get { return _quarterFinalTeamSelectionChangedCommand = _quarterFinalTeamSelectionChangedCommand ?? new Command(async () => await OnQuarterFinalSelectionChangedExecuted()); }
        }

        public ObservableCollection<Team> WildCardTeams
        {
            get { return _wildCardTeams; }
            private set
            {
                if (value != _wildCardTeams)
                {
                    _wildCardTeams = value;
                    OnPropertyChanged(nameof(WildCardTeams));
                }
            }
        }

        IList<object> _selectedWildCardTeams;
        public IList<object> SelectedWildCardTeams
        {
            get
            {
                return _selectedWildCardTeams;
            }
            set
            {
                if (_selectedWildCardTeams != value)
                {
                    _selectedWildCardTeams = value;
                    OnPropertyChanged(nameof(SelectedWildCardTeams));
                }
            }
        }

        public ObservableCollection<Team> QuarterFinalTeams
        {
            get { return _quarterFinalTeams; }
            private set
            {
                if (value != _quarterFinalTeams)
                {
                    _quarterFinalTeams = value;
                    OnPropertyChanged(nameof(QuarterFinalTeams));
                }
            }
        }

        IList<object> _selectedQuarterFinalTeams;
        public IList<object> SelectedQuarterFinalTeams
        {
            get
            {
                return _selectedQuarterFinalTeams;
            }
            set
            {
                if (_selectedQuarterFinalTeams != value)
                {
                    _selectedQuarterFinalTeams = value;
                    OnPropertyChanged(nameof(SelectedQuarterFinalTeams));
                }
            }
        }



        public void LoadData()
        {
            var teams = new List<Team>();

            for (int i = 1; i <= 18; i++)
            {
                var team = new Team { Name = $"{i}", ShortName = $"{i}", TeamId = i };
                teams.Add(team);
            }
            TeamDictionary = teams.ToDictionary(g => g.TeamId, g => g);

            WildCardTeams = new ObservableCollection<Team>(teams.OrderBy(x => x.TeamId));

            var tmpWildCardCollection = new ObservableCollection<object>();
            tmpWildCardCollection.Add(TeamDictionary[1]);
            tmpWildCardCollection.Add(TeamDictionary[2]);
            tmpWildCardCollection.Add(TeamDictionary[3]);
            tmpWildCardCollection.Add(TeamDictionary[4]);
            tmpWildCardCollection.Add(TeamDictionary[5]);
            tmpWildCardCollection.Add(TeamDictionary[6]);
            tmpWildCardCollection.Add(TeamDictionary[7]);
            tmpWildCardCollection.Add(TeamDictionary[10]);
            tmpWildCardCollection.Add(TeamDictionary[12]);
            tmpWildCardCollection.Add(TeamDictionary[13]);
            tmpWildCardCollection.Add(TeamDictionary[14]);
            tmpWildCardCollection.Add(TeamDictionary[15]);

            SelectedWildCardTeams = tmpWildCardCollection;

            var tmpQuarterFinalCollection = new ObservableCollection<object>();
            tmpQuarterFinalCollection.Add(TeamDictionary[1]);
            tmpQuarterFinalCollection.Add(TeamDictionary[2]);
            tmpQuarterFinalCollection.Add(TeamDictionary[3]);
            tmpQuarterFinalCollection.Add(TeamDictionary[4]);
            tmpQuarterFinalCollection.Add(TeamDictionary[5]);
            tmpQuarterFinalCollection.Add(TeamDictionary[6]);
            tmpQuarterFinalCollection.Add(TeamDictionary[7]);
            tmpQuarterFinalCollection.Add(TeamDictionary[14]);

            SelectedQuarterFinalTeams = tmpQuarterFinalCollection;
        }

        private void SyncSelectedQuarterFinalItems(List<object> selectedWildCardList)
        {
            if (_selectedQuarterFinalTeams == null || _selectedQuarterFinalTeams.Count == 0) return;
            var result = _selectedQuarterFinalTeams.Cast<Team>().Where(x => !selectedWildCardList.Cast<Team>().Any(x2 => x2.TeamId == x.TeamId)).ToList();
            result.ForEach(x => _selectedQuarterFinalTeams.Remove(x));
        }

        private async Task OnWildCardSelectionChangedExecuted()
        {
            if (_selectedWildCardTeams != null)
            {
                var selectedList = _selectedWildCardTeams.ToList();
                if (selectedList.Count == 12)
                {
                    SyncSelectedQuarterFinalItems(selectedList);
                    QuarterFinalTeams = new ObservableCollection<Team>(selectedList.Cast<Team>().OrderBy(o => o.TeamId));
                    //if (!_isLoading) await SaveSelectedWildCardTeams(selectedList.Cast<Team>().ToList());
                }
                //Prevent user from selecting more than 12 teams
                if (selectedList.Count > 12)
                {
                    selectedList.RemoveAt(Array.IndexOf(selectedList.ToArray(), selectedList.Last()));
                    SelectedWildCardTeams = new List<object>(selectedList);
                }
            }
        }

        private async Task OnQuarterFinalSelectionChangedExecuted()
        {
            if (_selectedQuarterFinalTeams != null)
            {
                var selectedList = _selectedQuarterFinalTeams.ToList();
                if (selectedList.Count == 8)
                {
                    //SyncSelectedSemiFinalItems(selectedList);
                    //SemiFinalTeams = new ObservableCollection<Team>(selectedList.Cast<Team>().OrderBy(o => o.ShortName));
                    //if (!_isLoading) await SaveSelectedQuarterFinalsTeams(selectedList.Cast<Team>().ToList());
                }
                if (selectedList.Count > 8)
                {
                    selectedList.RemoveAt(Array.IndexOf(selectedList.ToArray(), selectedList.Last()));
                    SelectedQuarterFinalTeams = new List<object>(selectedList);
                }
            }
        }
    }
}
