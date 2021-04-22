using DeBetoverdeDoolhof.Extensions;
using DeBetoverdeDoolhof.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DeBetoverdeDoolhof.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public Board Board { get; }
        public MazeCard FreeMazeCard { get; }

        private DialogService dialogService;
        private ObservableCollection<Wizard> wizards;
        public ObservableCollection<Wizard> Wizards { get { return wizards; } set { wizards = value; NotifyPropertyChanged(); } }

        private ICommand changePlayersCommand;
        public ICommand ChangePlayersCommand { get { return changePlayersCommand; } set { changePlayersCommand = value; } }

        private ICommand showRulesCommand;
        public ICommand ShowRulesCommand { get { return showRulesCommand; } set { showRulesCommand = value; } }

        private ICommand showTreasuresCommand;
        public ICommand ShowTreasuresCommand { get { return showTreasuresCommand; } set { showTreasuresCommand = value; } }
        public ParameterCommand ParameterCommand { get; set; }


        public MainViewModel()
        {
            WizardDataService wizardDataService = new WizardDataService();
            wizardDataService.Seed();
            Wizards = wizardDataService.GetWizards();

            MazeCardDataService mazeCardDataService = new MazeCardDataService();
            mazeCardDataService.Seed();

            Board bord = new Board();
            Board = bord;
            FreeMazeCard = bord.freeMazeCard;


            dialogService = new DialogService();
            
            BindCommands();
        }

        private void BindCommands()
        {
            ChangePlayersCommand = new BaseCommand(ChangePlayers);
            ShowRulesCommand = new BaseCommand(ShowRules);
            ShowTreasuresCommand = new BaseCommand(ShowTreasures);
            ParameterCommand = new ParameterCommand(this);
        }

        private void ChangePlayers()
        {
            dialogService.ShowPlayers();
        }

        private void ShowRules()
        {
            dialogService.ShowRules();
        }

        private void ShowTreasures()
        {
            dialogService.ShowTreasures();
        }

        public void ParameterMethod(Object obj)
        {
            Debug.WriteLine(obj);
        }
    }
}
