using DeBetoverdeDoolhof.Extensions;
using DeBetoverdeDoolhof.Model;
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
            AddPieceToBoardCommand = new AddPieceToBoard(this);
            RotateMazeCardCommand = new BaseCommand(RotateMazeCard);
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

        private void RotateMazeCard()
        {
            MazeCardDataService mazeCardDataService = new MazeCardDataService();
            List<MazeCard> currentSet = mazeCardDataService.GetByName(FreeMazeCard.Name);
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
                for (int i = 6; i > 0; i--)
                {
                    if (i == 6)
                    {
                        MazeCard temp = FreeMazeCard;
                        Square s = Board.FirstOrDefault(x => x.Id == colCards[i].Id);

                        //free card updaten
                        temp.Name = s.Name;
                        temp.Image = s.Image;
                        FreeMazeCard = temp;

                        //deze kaart = kaart ervoor
                        Square prevS = colCards[i - 1];
                        Square newSquare = new Square(s.Id, s.Row, s.Column, prevS.Name, prevS.Image, prevS.Rotation);
                        Board.Remove(s);
                        Board.Add(newSquare);
                    } else
                    {
                        if (i != 1)
                        {
                            Square s = Board.FirstOrDefault(x => x.Id == colCards[i].Id);
                            Square prevS = colCards[i - 1];
                            Square newSquare = new Square(s.Id, s.Row, s.Column, prevS.Name, prevS.Image, prevS.Rotation);
                            Board.Remove(s);
                            Board.Add(newSquare);
                        }
                        else
                        {
                            //row 1
                            Square s = Board.FirstOrDefault(x => x.Id == colCards[i].Id);
                            Square prevS = colCards[i - 1];
                            Square newSquare = new Square(s.Id, s.Row, s.Column, prevS.Name, prevS.Image, prevS.Rotation);
                            Board.Remove(s);
                            Board.Add(newSquare);

                            //row 0
                            Square s1 = Board.FirstOrDefault(x => x.Id == colCards[0].Id);
                            Square newSquare2 = new Square(s1.Id, s1.Row, s1.Column, insertCard.Name, insertCard.Image, insertCard.Rotation);
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
                for (int i = 6; i > 0; i--)
                {
                    if (i == 6)
                    {
                        Square s = Board.FirstOrDefault(x => x.Id == rowCards[i].Id);
                        MazeCard temp = FreeMazeCard;
                        temp.Name = s.Name;
                        temp.Image = s.Image;
                        FreeMazeCard = temp;

                        Square prevS = rowCards[i - 1];
                        Square newSquare = new Square(s.Id, s.Row, s.Column, prevS.Name, prevS.Image, prevS.Rotation);
                        Board.Remove(s);
                        Board.Add(newSquare);
                    } else
                    {
                        if (i != 1)
                        {
                            Square s = Board.FirstOrDefault(x => x.Id == rowCards[i].Id);
                            Square prevS = rowCards[i - 1];
                            Square newSquare = new Square(s.Id, s.Row, s.Column, prevS.Name, prevS.Image, prevS.Rotation);
                            Board.Remove(s);
                            Board.Add(newSquare);
                        }
                        else
                        {
                            //row 1
                            Square s = Board.FirstOrDefault(x => x.Id == rowCards[i].Id);
                            Square prevS = rowCards[i - 1];
                            Square newSquare = new Square(s.Id, s.Row, s.Column, prevS.Name, prevS.Image, prevS.Rotation);
                            Board.Remove(s);
                            Board.Add(newSquare);

                            //row 0
                            Square s1 = Board.FirstOrDefault(x => x.Id == rowCards[0].Id);
                            Square newSquare2 = new Square(s1.Id, s1.Row, s1.Column, insertCard.Name, insertCard.Image, insertCard.Rotation);
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
                for (int i = 6; i > 0; i--)
                {
                    if (i == 6)
                    {
                        MazeCard temp = FreeMazeCard;
                        Square s = Board.FirstOrDefault(x => x.Id == colCards[i].Id);
                        temp.Name = s.Name;
                        temp.Image = s.Image;
                        FreeMazeCard = temp;

                        Square prevS = colCards[i - 1];
                        Square newSquare = new Square(s.Id, s.Row, s.Column, prevS.Name, prevS.Image, prevS.Rotation);
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
                            Board.Remove(s);
                            Board.Add(newSquare);
                        }
                        else
                        {
                            //row 1
                            Square s = Board.FirstOrDefault(x => x.Id == colCards[i].Id);
                            Square prevS = colCards[i - 1];
                            Square newSquare = new Square(s.Id, s.Row, s.Column, prevS.Name, prevS.Image, prevS.Rotation);
                            Board.Remove(s);
                            Board.Add(newSquare);

                            //row 0
                            Square s1 = Board.FirstOrDefault(x => x.Id == colCards[0].Id);
                            Square newSquare2 = new Square(s1.Id, s1.Row, s1.Column, insertCard.Name, insertCard.Image, insertCard.Rotation);
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
                List<Square> rowCards = Board.Where(x => x.Row == row).OrderByDescending(x => x.Column).ToList();
                MazeCard insertCard = new MazeCard(FreeMazeCard.Position, FreeMazeCard.Name, FreeMazeCard.Image, FreeMazeCard.Rotation);
                for (int i = 6; i > 0; i--)
                {
                    if (i == 6)
                    {
                        Square s = Board.FirstOrDefault(x => x.Id == rowCards[i].Id);
                        MazeCard temp = FreeMazeCard;
                        temp.Name = s.Name;
                        temp.Image = s.Image;
                        FreeMazeCard = temp;

                        Square prevS = rowCards[i - 1];
                        Square newSquare = new Square(s.Id, s.Row, s.Column, prevS.Name, prevS.Image, prevS.Rotation);
                        Board.Remove(s);
                        Board.Add(newSquare);
                    }
                    else
                    {
                        if (i != 1)
                        {
                            Square s = Board.FirstOrDefault(x => x.Id == rowCards[i].Id);
                            Square prevS = rowCards[i - 1];
                            Square newSquare = new Square(s.Id, s.Row, s.Column, prevS.Name, prevS.Image, prevS.Rotation);
                            Board.Remove(s);
                            Board.Add(newSquare);
                        }
                        else
                        {
                            //row 1
                            Square s = Board.FirstOrDefault(x => x.Id == rowCards[i].Id);
                            Square prevS = rowCards[i - 1];
                            Square newSquare = new Square(s.Id, s.Row, s.Column, prevS.Name, prevS.Image, prevS.Rotation);
                            Board.Remove(s);
                            Board.Add(newSquare);

                            //row 0
                            Square s1 = Board.FirstOrDefault(x => x.Id == rowCards[0].Id);
                            Square newSquare2 = new Square(s1.Id, s1.Row, s1.Column, insertCard.Name, insertCard.Image, insertCard.Rotation);
                            Board.Remove(s1);
                            Board.Add(newSquare2);
                        }
                    }
                }
            }
        }
    }
}
