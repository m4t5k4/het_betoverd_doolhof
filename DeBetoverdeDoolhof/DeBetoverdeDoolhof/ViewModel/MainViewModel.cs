using DeBetoverdeDoolhof.Extensions;
using DeBetoverdeDoolhof.Model;
using DeBetoverdeDoolhof.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace DeBetoverdeDoolhof.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private Board board;

        public Board Board
        {
            get { return board; }
            set { board = value; NotifyPropertyChanged(); }
        }

        private MazeCard freeMazeCard;

        public MazeCard FreeMazeCard
        {
            get { return freeMazeCard; }
            set { freeMazeCard = value; NotifyPropertyChanged(); }
        }

        private Player currentPlayer;

        public Player CurrentPlayer
        {
            get { return currentPlayer; }
            set { currentPlayer = value; NotifyPropertyChanged(); }
        }

        private string currentPlayerImage;

        public string CurrentPlayerImage
        {
            get { return currentPlayerImage; }
            set { currentPlayerImage = value; NotifyPropertyChanged(); }
        }

        private DialogService dialogService;
        private ObservableCollection<Wizard> wizards;
        public ObservableCollection<Wizard> Wizards { get { return wizards; } set { wizards = value; NotifyPropertyChanged(); } }

        private ICommand changePlayersCommand;
        public ICommand ChangePlayersCommand { get { return changePlayersCommand; } set { changePlayersCommand = value; } }

        private ICommand showRulesCommand;
        public ICommand ShowRulesCommand { get { return showRulesCommand; } set { showRulesCommand = value; } }

        private ICommand showTreasuresCommand;
        public ICommand ShowTreasuresCommand { get { return showTreasuresCommand; } set { showTreasuresCommand = value; } }
        public AddPieceToBoard AddPieceToBoardCommand { get; set; }

        private ICommand rotateMazeCardCommand;
        public ICommand RotateMazeCardCommand { get { return rotateMazeCardCommand; } set { rotateMazeCardCommand = value; } }

        public MovePlayerToCommand MovePlayerToCommand { get; set; }
        public ObservableCollection<Player> Players { get; private set; }

        private readonly PlayerStore _playerStore;
        private static WizardDataService _wizardDataService;
        private static MazeCardDataService _mazeCardDataService;
        private static PlayerDataService _playerDataService;
        private static PlayerPositionDataService _playerPositionDataService;


        public MainViewModel(
            PlayerStore playerStore, 
            WizardDataService wizardDataService, 
            MazeCardDataService mazeCardDataService, 
            PlayerDataService playerDataService,
            PlayerPositionDataService playerPositionDataService)
        {
            _playerStore = playerStore;
            _playerStore.PlayersCreated += OnPlayersCreated;
            _wizardDataService = wizardDataService;
            //WizardDataService wizardDataService = new WizardDataService();
            _wizardDataService.Seed();
            Wizards = _wizardDataService.GetWizards();

            //MazeCardDataService mazeCardDataService = new MazeCardDataService();
            _mazeCardDataService = mazeCardDataService;
            _mazeCardDataService.Seed();

            //PlayerDataService playerDataService = new PlayerDataService();
            _playerDataService = playerDataService;
            _playerDataService.Seed();

            _playerPositionDataService = playerPositionDataService;
            _playerPositionDataService.Seed();

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
            AddPieceToBoardCommand = new AddPieceToBoard(this);
            RotateMazeCardCommand = new BaseCommand(RotateMazeCard);
            MovePlayerToCommand = new MovePlayerToCommand(this);
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

        public void MovePlayerToMethod(Button button)
        {
            Grid x = (Grid)button.Content;
            Square s = (Square)x.DataContext;
            Square destination = Board.FirstOrDefault(y => y.Id == s.Id);
            ObservableCollection<PlayerPosition> observableCollections = _playerPositionDataService.GetPlayerPositions();
            PlayerPosition pp = observableCollections.FirstOrDefault(y => y.PlayerId == CurrentPlayer.Id);

            Square from = Board.FirstOrDefault(y => y.Row == pp.Row && y.Column == pp.Column);
            Square updatedFrom = new Square(from.Id, from.Row, from.Column, from.Name, from.Image, from.Rotation);
            foreach (Player player in from.PlayersOnSquare)
            {
                if (player.Id != CurrentPlayer.Id)
                {
                    updatedFrom.PlayersOnSquare.Add(player);
                }
            }
            Board.Remove(from); Board.Add(updatedFrom);

            Square updatedDestination = new Square(destination.Id, destination.Row, destination.Column, destination.Name, destination.Image, destination.Rotation);
            foreach (Player player in destination.PlayersOnSquare)
            {
                updatedDestination.PlayersOnSquare.Add(player);
            }
            updatedDestination.PlayersOnSquare.Add(CurrentPlayer);
            Board.Remove(destination); Board.Add(updatedDestination);

            PlayerPosition updated = new PlayerPosition(CurrentPlayer.Id, updatedDestination.Row, updatedDestination.Column);
            _playerPositionDataService.UpdatePlayerPosition(updated);
        }

        private void OnPlayersCreated(List<Player> players)
        {
            Players = players.ToObservableCollection();
            CurrentPlayer = Players[0];
            CurrentPlayerImage = Wizards.FirstOrDefault(x => x.Id == CurrentPlayer.WizardID).Image;
            SetStartingPositions();
        }

        private void SetStartingPositions()
        {
            foreach (Player player in Players)
            {
                Wizard wizard = Wizards.FirstOrDefault(x => x.Id == player.WizardID);
                Square s = Board.FirstOrDefault(x => x.Name == wizard.Color);
                Square newSquare = new Square(s.Id, s.Row, s.Column, s.Name, s.Image, s.Rotation);
                newSquare.PlayersOnSquare.Add(player);
                Board.Remove(s);
                Board.Add(newSquare);
                PlayerPosition pp = new PlayerPosition(player.Id, s.Row, s.Column);
                _playerPositionDataService.InsertPlayerPosition(pp);
            }
        }

        private void RotateMazeCard()
        {
            List<MazeCard> currentSet = _mazeCardDataService.GetByName(FreeMazeCard.Name);
            MazeCard currentCard = currentSet.FirstOrDefault(x => x.Image.Equals(FreeMazeCard.Image));
            int index = currentSet.IndexOf(currentCard);
            if (index == 3)
            {
                index = 0;
            } else
            {
                index++;
            }
            currentCard.Image = currentSet[index].Image;
            currentCard.Rotation = currentSet[index].Rotation;
            FreeMazeCard = currentCard;
        }

        public void AddPieceToBoard(Button button)
        {
            Debug.WriteLine(button);
            int col = Grid.GetColumn(button);
            int row = Grid.GetRow(button);
            Debug.WriteLine(col);
            if (row == 0)
            {
                //move col down
                col -= 1;
                List<Square> colCards = Board.Where(x => x.Column == col).OrderBy(x => x.Row).ToList();
                MazeCard insertCard = new MazeCard(FreeMazeCard.Position, FreeMazeCard.Name, FreeMazeCard.Image,  FreeMazeCard.Rotation);
                List<Player> playersOnSquare = new List<Player>();
                for (int i = 6; i > 0; i--)
                {
                    if (i == 6)
                    {
                        MazeCard temp = FreeMazeCard;
                        Square s = Board.FirstOrDefault(x => x.Id == colCards[i].Id);
                        //kaart gaat weg spelers bijhouden
                        foreach(Player player in s.PlayersOnSquare)
                        {
                            playersOnSquare.Add(player);
                        }

                        //free card updaten
                        temp.Name = s.Name;
                        temp.Image = s.Image;
                        FreeMazeCard = temp;

                        //deze kaart = kaart ervoor
                        Square prevS = colCards[i - 1];
                        Square newSquare = new Square(s.Id, s.Row, s.Column, prevS.Name, prevS.Image, prevS.Rotation);
                        foreach (Player player in prevS.PlayersOnSquare)
                        {
                            newSquare.PlayersOnSquare.Add(player);
                            PlayerPosition pp = new PlayerPosition(player.Id, newSquare.Row, newSquare.Column);
                            _playerPositionDataService.UpdatePlayerPosition(pp);
                        }
                        Board.Remove(s);
                        Board.Add(newSquare);
                    } else
                    {
                        if (i != 1)
                        {
                            Square s = Board.FirstOrDefault(x => x.Id == colCards[i].Id);
                            Square prevS = colCards[i - 1];
                            Square newSquare = new Square(s.Id, s.Row, s.Column, prevS.Name, prevS.Image, prevS.Rotation);
                            List<Player> tempList = prevS.PlayersOnSquare;
                            foreach (Player player in tempList)
                            {
                                newSquare.PlayersOnSquare.Add(player);
                                PlayerPosition pp = new PlayerPosition(player.Id, newSquare.Row, newSquare.Column);
                                _playerPositionDataService.UpdatePlayerPosition(pp);
                            }
                            Board.Remove(s);
                            Board.Add(newSquare);
                        }
                        else
                        {
                            //row 1
                            Square s = Board.FirstOrDefault(x => x.Id == colCards[i].Id);
                            Square prevS = colCards[i - 1];
                            Square newSquare = new Square(s.Id, s.Row, s.Column, prevS.Name, prevS.Image, prevS.Rotation);
                            List<Player> tempList = prevS.PlayersOnSquare;
                            foreach (Player player in tempList)
                            {
                                newSquare.PlayersOnSquare.Add(player);
                                PlayerPosition pp = new PlayerPosition(player.Id, newSquare.Row, newSquare.Column);
                                _playerPositionDataService.UpdatePlayerPosition(pp);
                            }
                            Board.Remove(s);
                            Board.Add(newSquare);

                            //row 0
                            Square s1 = Board.FirstOrDefault(x => x.Id == colCards[0].Id);
                            Square newSquare2 = new Square(s1.Id, s1.Row, s1.Column, insertCard.Name, insertCard.Image, insertCard.Rotation);
                            foreach (Player player in playersOnSquare)
                            {
                                newSquare2.PlayersOnSquare.Add(player);
                                PlayerPosition pp = new PlayerPosition(player.Id, newSquare2.Row, newSquare2.Column);
                                _playerPositionDataService.UpdatePlayerPosition(pp);
                            }
                            Board.Remove(s1);
                            Board.Add(newSquare2);
                        }
                    }
                }
            }

            if (col == 0)
            {
                //move row right
                row -= 1;
                List<Square> rowCards = Board.Where(x => x.Row == row).OrderBy(x => x.Column).ToList();
                MazeCard insertCard = new MazeCard(FreeMazeCard.Position, FreeMazeCard.Name, FreeMazeCard.Image, FreeMazeCard.Rotation);
                List<Player> playersOnSquare = new List<Player>();
                for (int i = 6; i > 0; i--)
                {
                    if (i == 6)
                    {
                        Square s = Board.FirstOrDefault(x => x.Id == rowCards[i].Id);
                        foreach (Player player in s.PlayersOnSquare)
                        {
                            playersOnSquare.Add(player);
                        }
                        MazeCard temp = FreeMazeCard;
                        temp.Name = s.Name;
                        temp.Image = s.Image;
                        FreeMazeCard = temp;

                        Square prevS = rowCards[i - 1];
                        Square newSquare = new Square(s.Id, s.Row, s.Column, prevS.Name, prevS.Image, prevS.Rotation);
                        foreach (Player player in prevS.PlayersOnSquare)
                        {
                            newSquare.PlayersOnSquare.Add(player);
                            PlayerPosition pp = new PlayerPosition(player.Id, newSquare.Row, newSquare.Column);
                            _playerPositionDataService.UpdatePlayerPosition(pp);
                        }
                        Board.Remove(s);
                        Board.Add(newSquare);
                    } else
                    {
                        if (i != 1)
                        {
                            Square s = Board.FirstOrDefault(x => x.Id == rowCards[i].Id);
                            Square prevS = rowCards[i - 1];
                            Square newSquare = new Square(s.Id, s.Row, s.Column, prevS.Name, prevS.Image, prevS.Rotation);
                            List<Player> tempList = prevS.PlayersOnSquare;
                            foreach (Player player in tempList)
                            {
                                newSquare.PlayersOnSquare.Add(player);
                                PlayerPosition pp = new PlayerPosition(player.Id, newSquare.Row, newSquare.Column);
                                _playerPositionDataService.UpdatePlayerPosition(pp);
                            }
                            Board.Remove(s);
                            Board.Add(newSquare);
                        }
                        else
                        {
                            //row 1
                            Square s = Board.FirstOrDefault(x => x.Id == rowCards[i].Id);
                            Square prevS = rowCards[i - 1];
                            Square newSquare = new Square(s.Id, s.Row, s.Column, prevS.Name, prevS.Image, prevS.Rotation);
                            List<Player> tempList = prevS.PlayersOnSquare;
                            foreach (Player player in tempList)
                            {
                                newSquare.PlayersOnSquare.Add(player);
                                PlayerPosition pp = new PlayerPosition(player.Id, newSquare.Row, newSquare.Column);
                                _playerPositionDataService.UpdatePlayerPosition(pp);
                            }
                            Board.Remove(s);
                            Board.Add(newSquare);

                            //row 0
                            Square s1 = Board.FirstOrDefault(x => x.Id == rowCards[0].Id);
                            Square newSquare2 = new Square(s1.Id, s1.Row, s1.Column, insertCard.Name, insertCard.Image, insertCard.Rotation);
                            foreach (Player player in playersOnSquare)
                            {
                                newSquare2.PlayersOnSquare.Add(player);
                                PlayerPosition pp = new PlayerPosition(player.Id, newSquare2.Row, newSquare2.Column);
                                _playerPositionDataService.UpdatePlayerPosition(pp);
                            }
                            Board.Remove(s1);
                            Board.Add(newSquare2);
                        }
                    }
                }
            }

            if (row == 8)
            {
                //move col up
                col -= 1;
                List<Square> colCards = Board.Where(x => x.Column == col).OrderByDescending(x => x.Row).ToList();
                MazeCard insertCard = new MazeCard(FreeMazeCard.Position, FreeMazeCard.Name, FreeMazeCard.Image, FreeMazeCard.Rotation);
                List<Player> playersOnSquare = new List<Player>();
                for (int i = 6; i > 0; i--)
                {
                    if (i == 6)
                    {
                        MazeCard temp = FreeMazeCard;
                        Square s = Board.FirstOrDefault(x => x.Id == colCards[i].Id);
                        foreach (Player player in s.PlayersOnSquare)
                        {
                            playersOnSquare.Add(player);
                        }
                        temp.Name = s.Name;
                        temp.Image = s.Image;
                        FreeMazeCard = temp;

                        Square prevS = colCards[i - 1];
                        Square newSquare = new Square(s.Id, s.Row, s.Column, prevS.Name, prevS.Image, prevS.Rotation);
                        foreach (Player player in prevS.PlayersOnSquare)
                        {
                            newSquare.PlayersOnSquare.Add(player);
                            PlayerPosition pp = new PlayerPosition(player.Id, newSquare.Row, newSquare.Column);
                            _playerPositionDataService.UpdatePlayerPosition(pp);
                        }
                        Board.Remove(s);
                        Board.Add(newSquare);
                    }
                    else
                    {
                        if (i != 1)
                        {
                            Square s = Board.FirstOrDefault(x => x.Id == colCards[i].Id);
                            Square prevS = colCards[i - 1];
                            Square newSquare = new Square(s.Id, s.Row, s.Column, prevS.Name, prevS.Image, prevS.Rotation);
                            List<Player> tempList = prevS.PlayersOnSquare;
                            foreach (Player player in tempList)
                            {
                                newSquare.PlayersOnSquare.Add(player);
                                PlayerPosition pp = new PlayerPosition(player.Id, newSquare.Row, newSquare.Column);
                                _playerPositionDataService.UpdatePlayerPosition(pp);
                            }
                            Board.Remove(s);
                            Board.Add(newSquare);
                        }
                        else
                        {
                            //row 1
                            Square s = Board.FirstOrDefault(x => x.Id == colCards[i].Id);
                            Square prevS = colCards[i - 1];
                            Square newSquare = new Square(s.Id, s.Row, s.Column, prevS.Name, prevS.Image, prevS.Rotation);
                            List<Player> tempList = prevS.PlayersOnSquare;
                            foreach (Player player in tempList)
                            {
                                newSquare.PlayersOnSquare.Add(player);
                                PlayerPosition pp = new PlayerPosition(player.Id, newSquare.Row, newSquare.Column);
                                _playerPositionDataService.UpdatePlayerPosition(pp);
                            }
                            Board.Remove(s);
                            Board.Add(newSquare);

                            //row 0
                            Square s1 = Board.FirstOrDefault(x => x.Id == colCards[0].Id);
                            Square newSquare2 = new Square(s1.Id, s1.Row, s1.Column, insertCard.Name, insertCard.Image, insertCard.Rotation);
                            foreach (Player player in playersOnSquare)
                            {
                                newSquare2.PlayersOnSquare.Add(player);
                                PlayerPosition pp = new PlayerPosition(player.Id, newSquare2.Row, newSquare2.Column);
                                _playerPositionDataService.UpdatePlayerPosition(pp);
                            }
                            Board.Remove(s1);
                            Board.Add(newSquare2);
                        }
                    }
                }
            }

            if (col == 8)
            {
                //move row left
                row -= 1;
                List<Square> rowCards = Board.Where(x => x.Row == row).OrderByDescending(x => x.Column).ToList(); //6 5 4 3 2 1 0
                MazeCard insertCard = new MazeCard(FreeMazeCard.Position, FreeMazeCard.Name, FreeMazeCard.Image, FreeMazeCard.Rotation);
                List<Player> playersOnSquare = new List<Player>();
                for (int i = 6; i > 0; i--)
                {
                    if (i == 6) //col 0
                    {
                        Square s = Board.FirstOrDefault(x => x.Id == rowCards[i].Id); //col 0
                        foreach (Player player in s.PlayersOnSquare)
                        {
                            playersOnSquare.Add(player);
                        }
                        MazeCard temp = FreeMazeCard;
                        temp.Name = s.Name;
                        temp.Image = s.Image;
                        FreeMazeCard = temp;

                        Square prevS = rowCards[i - 1];
                        Square newSquare = new Square(s.Id, s.Row, s.Column, prevS.Name, prevS.Image, prevS.Rotation);
                        foreach (Player player in prevS.PlayersOnSquare)
                        {
                            newSquare.PlayersOnSquare.Add(player);
                            PlayerPosition pp = new PlayerPosition(player.Id, newSquare.Row, newSquare.Column);
                            _playerPositionDataService.UpdatePlayerPosition(pp);
                        }
                        Board.Remove(s);
                        Board.Add(newSquare);
                    }
                    else
                    {
                        if (i != 1) //col 5 4 3 2 1
                        {
                            Square s = Board.FirstOrDefault(x => x.Id == rowCards[i].Id);
                            Square prevS = rowCards[i - 1]; //col 1 2 3 4
                            Square newSquare = new Square(s.Id, s.Row, s.Column, prevS.Name, prevS.Image, prevS.Rotation);
                            List<Player> tempList = prevS.PlayersOnSquare;
                            foreach (Player player in tempList)
                            {
                                newSquare.PlayersOnSquare.Add(player);
                                PlayerPosition pp = new PlayerPosition(player.Id, newSquare.Row, newSquare.Column);
                                _playerPositionDataService.UpdatePlayerPosition(pp);
                            }
                            Board.Remove(s);
                            Board.Add(newSquare);
                        }
                        else
                        {
                            //row 1
                            Square s = Board.FirstOrDefault(x => x.Id == rowCards[i].Id); //col 5
                            Square prevS = rowCards[i - 1]; //col 6
                            Square newSquare = new Square(s.Id, s.Row, s.Column, prevS.Name, prevS.Image, prevS.Rotation);
                            List<Player> tempList = prevS.PlayersOnSquare;
                            foreach (Player player in tempList)
                            {
                                newSquare.PlayersOnSquare.Add(player);
                                PlayerPosition pp = new PlayerPosition(player.Id, newSquare.Row, newSquare.Column);
                                _playerPositionDataService.UpdatePlayerPosition(pp);
                            }
                            Board.Remove(s);
                            Board.Add(newSquare);

                            //row 0
                            Square s1 = Board.FirstOrDefault(x => x.Id == rowCards[0].Id);
                            Square newSquare2 = new Square(s1.Id, s1.Row, s1.Column, insertCard.Name, insertCard.Image, insertCard.Rotation);
                            foreach (Player player in playersOnSquare)
                            {
                                newSquare2.PlayersOnSquare.Add(player);
                                PlayerPosition pp = new PlayerPosition(player.Id, newSquare2.Row, newSquare2.Column);
                                _playerPositionDataService.UpdatePlayerPosition(pp);
                            }
                            Board.Remove(s1);
                            Board.Add(newSquare2);
                        }
                    }
                }
            }
        }
    }
}
