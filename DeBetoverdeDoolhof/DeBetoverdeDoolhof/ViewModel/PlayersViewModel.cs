using DeBetoverdeDoolhof.Extensions;
using DeBetoverdeDoolhof.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DeBetoverdeDoolhof.ViewModel
{
    class PlayersViewModel: BaseViewModel
    {

        private ObservableCollection<Player> players;

        public ObservableCollection<Player> Players
        {
            get
            {
                return players;
            }
            set
            {
                players = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<Wizard> wizards;
        public ObservableCollection<Wizard> Wizards { get { return wizards; } set { wizards = value; NotifyPropertyChanged(); } }

        private Player selectedPlayer;
        public Player SelectedPlayer
        {
            get
            {
                return selectedPlayer;
            }
            set
            {
                selectedPlayer = value;
                NotifyPropertyChanged();
            }
        }

        private Wizard selectedWizard;
        public Wizard SelectedWizard
        {
            get
            {
                return selectedWizard;
            }
            set
            {
                selectedWizard = value;
                NotifyPropertyChanged();
            }
        }

        private string name;
        public string Name { get { return name; } set { name = value; NotifyPropertyChanged(); } }

        private ICommand assignToPlayerCommand;
        public ICommand AssignToPlayerCommand { get { return assignToPlayerCommand; } set { assignToPlayerCommand = value; } }

        private ICommand updatePlayerCommand;
        public ICommand UpdatePlayerCommand { get { return updatePlayerCommand; } set { updatePlayerCommand = value; } }

        private ICommand deletePlayerCommand;
        public ICommand DeletePlayerCommand { get { return deletePlayerCommand; } set { deletePlayerCommand = value; } }

        public PlayersViewModel()
        {
            PlayerDataService playerDataService = new PlayerDataService();
            Players = playerDataService.GetPlayers();

            WizardDataService wizardDataService = new WizardDataService();
            Wizards = wizardDataService.GetWizards();

            BindCommands();
        }

        private void BindCommands()
        {
            assignToPlayerCommand = new BaseCommand(AssignToPlayer);
            updatePlayerCommand = new BaseCommand(UpdatePlayer);
            deletePlayerCommand = new BaseCommand(DeletePlayer);
        }

        private void AssignToPlayer()
        {
            PlayerDataService playerDataService = new PlayerDataService();
            Player addPlayer = new Player();
            addPlayer.Name = Name;
            addPlayer.Score = 0;
            addPlayer.Position = SelectedWizard.StartPosition;
            addPlayer.IsWinner = false;
            addPlayer.WizardID = SelectedWizard.Id;
            playerDataService.InsertPlayer(addPlayer);
            Players = playerDataService.GetPlayers();
        }

        private void DeletePlayer()
        {
            Console.WriteLine(SelectedPlayer);
            if(SelectedPlayer != null)
            {
                PlayerDataService playerDataService = new PlayerDataService();
                playerDataService.DeletePlayer(SelectedPlayer.Id);
                Players = playerDataService.GetPlayers();
            }
        }

        private void UpdatePlayer()
        {
            if (SelectedPlayer != null)
            {
                PlayerDataService playerDataService = new PlayerDataService();
                SelectedPlayer.Name = Name;
                SelectedPlayer.Position = SelectedWizard.StartPosition;
                SelectedPlayer.WizardID = SelectedWizard.Id;
                playerDataService.UpdatePlayer(SelectedPlayer);
                Players = playerDataService.GetPlayers();
            }
        }
    }
}
