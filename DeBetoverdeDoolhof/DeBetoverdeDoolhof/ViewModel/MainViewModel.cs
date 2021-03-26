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
    class MainViewModel : BaseViewModel
    {
        private DialogService dialogService;
        private ObservableCollection<Wizard> wizards;
        public ObservableCollection<Wizard> Wizards { get { return wizards; } set { wizards = value; NotifyPropertyChanged(); } }

        private ICommand changePlayersCommand;
        public ICommand ChangePlayersCommand { get { return changePlayersCommand; } set { changePlayersCommand = value; } }

        private ICommand showRulesCommand;
        public ICommand ShowRulesCommand { get { return showRulesCommand; } set { showRulesCommand = value; } }

        private ICommand showTreasuresCommand;
        public ICommand ShowTreasuresCommand { get { return showTreasuresCommand; } set { showTreasuresCommand = value; } }

        public MainViewModel()
        {
            WizardDataService wizardDataService = new WizardDataService();
            wizardDataService.Seed();
            Wizards = wizardDataService.GetWizards();

            dialogService = new DialogService();

            BindCommands();
        }

        private void BindCommands()
        {
            ChangePlayersCommand = new BaseCommand(ChangePlayers);
            ShowRulesCommand = new BaseCommand(ShowRules);
            ShowTreasuresCommand = new BaseCommand(ShowTreasures);
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
    }
}
