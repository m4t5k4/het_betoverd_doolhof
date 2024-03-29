﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeBetoverdeDoolhof.Model
{
    public class Board: ObservableCollection<Square>
    {
        private MazeCardDataService _mazeCardDataService;
        public MazeCard freeMazeCard = new MazeCard();
        public Board(MazeCardDataService mazeCardDataService)
        {
            _mazeCardDataService = mazeCardDataService;
            CreateBoard();
            //PutPlayersOnStart();
        }

        private void PutPlayersOnStart()
        {
            throw new NotImplementedException();
        }

        private void CreateBoard()
        {
            //startplaatsen
            MazeCard yellow = _mazeCardDataService.GetByName("yellow")[0];
            MazeCard blue = _mazeCardDataService.GetByName("blue")[0];
            MazeCard red = _mazeCardDataService.GetByName("red")[0];
            MazeCard green = _mazeCardDataService.GetByName("green")[0];
            this.Add(new Square(1, 0, 0, yellow.Name, yellow.Image, 0));
            this.Add(new Square(2, 0, 6, blue.Name, blue.Image, 0));
            this.Add(new Square(3, 6, 0, red.Name, red.Image, 0));
            this.Add(new Square(4, 6, 6, green.Name, green.Image, 0));

            //other fixed pieces
            //row 0
            MazeCard skull = _mazeCardDataService.GetByName("skull")[0];
            MazeCard sword = _mazeCardDataService.GetByName("sword")[0];
            this.Add(new Square(5, 0, 2, skull.Name, skull.Image, 0));
            this.Add(new Square(6, 0, 4, sword.Name, sword.Image, 0));

            MazeCard coins = _mazeCardDataService.GetByName("coins")[0];
            MazeCard keys = _mazeCardDataService.GetByName("keys")[0];
            MazeCard gem = _mazeCardDataService.GetByName("gem")[0];
            MazeCard armor = _mazeCardDataService.GetByName("armor")[0];
            this.Add(new Square(7, 2, 0, coins.Name, coins.Image, 0));
            this.Add(new Square(8, 2, 2, keys.Name, keys.Image, 0));
            this.Add(new Square(9, 2, 4, gem.Name, gem.Image, 0));
            this.Add(new Square(10, 2, 6, armor.Name, armor.Image, 0));

            MazeCard book = _mazeCardDataService.GetByName("book")[0];
            MazeCard crown = _mazeCardDataService.GetByName("crown")[0];
            MazeCard chest = _mazeCardDataService.GetByName("chest")[0];
            MazeCard chandelier = _mazeCardDataService.GetByName("chandelier")[0];
            this.Add(new Square(11, 4, 0, book.Name, book.Image, 0));
            this.Add(new Square(12, 4, 2, crown.Name, crown.Image, 0));
            this.Add(new Square(13, 4, 4, chest.Name, chest.Image, 0));
            this.Add(new Square(14, 4, 6, chandelier.Name, chandelier.Image, 0));

            MazeCard map = _mazeCardDataService.GetByName("map")[0];
            MazeCard ring = _mazeCardDataService.GetByName("ring")[0];
            this.Add(new Square(15, 6, 2, map.Name, map.Image, 0));
            this.Add(new Square(16, 6, 4, ring.Name, ring.Image, 0));

            //randomize 
            List<MazeCard> bat = _mazeCardDataService.GetByName("bat");
            List<MazeCard> corner = _mazeCardDataService.GetByName("corner");
            List<MazeCard> dragonfly = _mazeCardDataService.GetByName("dragonfly");
            List<MazeCard> drake = _mazeCardDataService.GetByName("drake");
            List<MazeCard> fairy = _mazeCardDataService.GetByName("fairy");
            List<MazeCard> ghost = _mazeCardDataService.GetByName("ghost");
            List<MazeCard> ogre = _mazeCardDataService.GetByName("ogre");
            List<MazeCard> owl = _mazeCardDataService.GetByName("owl");
            List<MazeCard> rat = _mazeCardDataService.GetByName("rat");
            List<MazeCard> salamander = _mazeCardDataService.GetByName("salamander");
            List<MazeCard> scarab = _mazeCardDataService.GetByName("scarab");
            List<MazeCard> spider = _mazeCardDataService.GetByName("spider");
            List<MazeCard> straight = _mazeCardDataService.GetByName("straight");
            List<MazeCard> wish_ghost = _mazeCardDataService.GetByName("wish_ghost");

            List<List<MazeCard>> mazeCards = new List<List<MazeCard>>() { bat, dragonfly, drake, fairy, ghost, ogre, owl, rat, salamander, scarab, spider, wish_ghost };
            List<List<MazeCard>> straights = new List<List<MazeCard>>();
            for (int i = 0; i < 12; i++)
            {
                straights.Add(straight);
            }
            List<List<MazeCard>> corners = new List<List<MazeCard>>();
            for (int i = 0; i < 10; i++)
            {
                corners.Add(corner);
            }
            mazeCards.AddRange(straights);
            mazeCards.AddRange(corners);
            mazeCards = mazeCards.OrderBy(a => Guid.NewGuid()).ToList();

            var rng = new Random();
            int count = 0;
            int id = 17;
            for (int row = 0; row < 7; row+=2)
            {
                for (int col = 1; col < 6; col+=2)
                {
                    int rotation = rng.Next(0,4);
                    List<MazeCard> randomCard = mazeCards[count];
                    this.Add(new Square(id, row, col, randomCard[rotation].Name, randomCard[rotation].Image, randomCard[rotation].Rotation));
                    count++;
                    id++;
                }
            }
            
            for (int row = 1; row < 6; row+=2)
            {
                for (int col = 0; col < 7; col++)
                {
                    int rotation = rng.Next(0, 4);
                    List<MazeCard> randomCard = mazeCards[count];
                    this.Add(new Square(id, row, col, randomCard[rotation].Name, randomCard[rotation].Image, randomCard[rotation].Rotation));
                    count++;
                    id++;
                }
            }

            freeMazeCard = mazeCards.Last()[0];
            List<MazeCard> lastCard = mazeCards.Last();
        }
    }
}
