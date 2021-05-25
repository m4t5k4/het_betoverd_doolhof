using Dapper;
using DeBetoverdeDoolhof.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace DeBetoverdeDoolhof.Model
{
    public class MazeCardDataService
    {
        // Ophalen ConnectionString uit App.config
        private static string connectionString = ConfigurationManager.ConnectionStrings["azure"].ConnectionString;

        // Aanmaken van een object uit de IDbConnection class en instantiëren van een SqlConnection.
        // Dit betekent dat de connectie met de database automatisch geopend wordt.
        private static IDbConnection db = new SqlConnection(connectionString);

        private ObservableCollection<MazeCard> mazeCards;

        public ObservableCollection<MazeCard> MazeCards
        {
            get { return mazeCards; }
            set { mazeCards = value; }
        }

        public void FillMazeCardsCollection()
        {
            MazeCards = new ObservableCollection<MazeCard>();
            //fixed
            MazeCard armor = new MazeCard("0,0", "armor", "/Resources/maze/fixed/m_armor.png", 0);
            MazeCards.Add(armor);

            MazeCard blue = new MazeCard("0,0", "blue", "/Resources/maze/fixed/m_blue.png", 0);
            MazeCards.Add(blue);

            MazeCard book = new MazeCard("0,0", "book", "/Resources/maze/fixed/m_book.png", 0);
            MazeCards.Add(book);

            MazeCard chandelier = new MazeCard("0,0", "chandelier", "/Resources/maze/fixed/m_chandelier.png", 0);
            MazeCards.Add(chandelier);

            MazeCard chest = new MazeCard("0,0", "chest", "/Resources/maze/fixed/m_chest.png", 0);
            MazeCards.Add(chest);

            MazeCard coins = new MazeCard("0,0", "coins", "/Resources/maze/fixed/m_coins.png", 0);
            MazeCards.Add(coins);

            MazeCard crown = new MazeCard("0,0", "crown", "/Resources/maze/fixed/m_crown.png", 0);
            MazeCards.Add(crown);

            MazeCard gem = new MazeCard("0,0", "gem", "/Resources/maze/fixed/m_gem.png", 0);
            MazeCards.Add(gem);

            MazeCard green = new MazeCard("0,0", "green", "/Resources/maze/fixed/m_green.png", 0);
            MazeCards.Add(green);

            MazeCard keys = new MazeCard("0,0", "keys", "/Resources/maze/fixed/m_keys.png", 0);
            MazeCards.Add(keys);

            MazeCard map = new MazeCard("0,0", "map", "/Resources/maze/fixed/m_map.png", 0);
            MazeCards.Add(map);

            MazeCard red = new MazeCard("0,0", "red", "/Resources/maze/fixed/m_red.png", 0);
            MazeCards.Add(red);

            MazeCard ring = new MazeCard("0,0", "ring", "/Resources/maze/fixed/m_ring.png", 0);
            MazeCards.Add(ring);

            MazeCard skull = new MazeCard("0,0", "skull", "/Resources/maze/fixed/m_skull.png", 0);
            MazeCards.Add(skull);

            MazeCard sword = new MazeCard("0,0", "sword", "/Resources/maze/fixed/m_sword.png", 0);
            MazeCards.Add(sword);

            MazeCard yellow = new MazeCard("0,0", "yellow", "/Resources/maze/fixed/m_yellow.png", 0);
            MazeCards.Add(yellow);


            //rotatable
            int rotation = 0;
            for (int bats = 1; bats < 5; bats++)
            {
                MazeCard bat = new MazeCard("0,0", "bat", "/Resources/maze/rotatable/m_bat_" + bats + ".png", rotation);
                MazeCards.Add(bat);
                rotation =+ 90;
            }

            rotation = 0;
            for (int corners = 1; corners < 5; corners++)
            {
                MazeCard corner = new MazeCard("0,0", "corner", "/Resources/maze/rotatable/m_corner_" + corners + ".png", rotation);
                MazeCards.Add(corner);
                rotation = +90;
            }

            rotation = 0;
            for (int dragonflies = 1; dragonflies < 5; dragonflies++)
            {
                MazeCard dragonfly = new MazeCard("0,0", "dragonfly", "/Resources/maze/rotatable/m_dragonfly_" + dragonflies + ".png", rotation);
                MazeCards.Add(dragonfly);
                rotation = +90;
            }

            rotation = 0;
            for (int drakes = 1; drakes < 5; drakes++)
            {
                MazeCard drake = new MazeCard("0,0", "drake", "/Resources/maze/rotatable/m_drake_" + drakes + ".png", rotation);
                MazeCards.Add(drake);
                rotation = +90;
            }

            rotation = 0;
            for (int fairies = 1; fairies < 5; fairies++)
            {
                MazeCard fairy = new MazeCard("0,0", "fairy", "/Resources/maze/rotatable/m_fairy_" + fairies + ".png", rotation);
                MazeCards.Add(fairy);
                rotation = +90;
            }

            rotation = 0;
            for (int ghosts = 1; ghosts < 5; ghosts++)
            {
                MazeCard ghost = new MazeCard("0,0", "ghost", "/Resources/maze/rotatable/m_ghost_" + ghosts + ".png", rotation);
                MazeCards.Add(ghost);
                rotation = +90;
            }

            rotation = 0;
            for (int ogres = 1; ogres < 5; ogres++)
            {
                MazeCard ogre = new MazeCard("0,0", "ogre", "/Resources/maze/rotatable/m_ogre_" + ogres + ".png", rotation);
                MazeCards.Add(ogre);
                rotation = +90;
            }

            rotation = 0;
            for (int owls = 1; owls < 5; owls++)
            {
                MazeCard owl = new MazeCard("0,0", "owl", "/Resources/maze/rotatable/m_owl_" + owls + ".png", rotation);
                MazeCards.Add(owl);
                rotation = +90;
            }

            rotation = 0;
            for (int rats = 1; rats < 5; rats++)
            {
                MazeCard rat = new MazeCard("0,0", "rat", "/Resources/maze/rotatable/m_rat_" + rats + ".png", rotation);
                MazeCards.Add(rat);
                rotation = +90;
            }

            rotation = 0;
            for (int salamanders = 1; salamanders < 5; salamanders++)
            {
                MazeCard salamander = new MazeCard("0,0", "salamander", "/Resources/maze/rotatable/m_salamander_" + salamanders + ".png", rotation);
                MazeCards.Add(salamander);
                rotation = +90;
            }

            rotation = 0;
            for (int scarabs = 1; scarabs < 5; scarabs++)
            {
                MazeCard scarab = new MazeCard("0,0", "scarab", "/Resources/maze/rotatable/m_scarab_" + scarabs + ".png", rotation);
                MazeCards.Add(scarab);
                rotation = +90;
            }

            rotation = 0;
            for (int spiders = 1; spiders < 5; spiders++)
            {
                MazeCard spider = new MazeCard("0,0", "spider", "/Resources/maze/rotatable/m_spider_" + spiders + ".png", rotation);
                MazeCards.Add(spider);
                rotation = +90;
            }

            rotation = 0;
            for (int straights = 1; straights < 5; straights++)
            {
                MazeCard straight = new MazeCard("0,0", "straight", "/Resources/maze/rotatable/m_straight_" + straights + ".png", rotation);
                MazeCards.Add(straight);
                rotation = +90;
            }

            rotation = 0;
            for (int wish_ghosts = 1; wish_ghosts < 5; wish_ghosts++)
            {
                MazeCard wish_ghost = new MazeCard("0,0", "wish_ghost", "/Resources/maze/rotatable/m_wish_ghost_" + wish_ghosts + ".png", rotation);
                MazeCards.Add(wish_ghost);
                rotation = +90;
            }
        }


        public List<MazeCard> GetByName(string name)
        {

            List<MazeCard> list = MazeCards.ToList();
            list = list.Where(x => x.Name == name).ToList();
            return list;
        }

        public ObservableCollection<MazeCard> Get()
        {
            string sql = "Select * from MazeCard";
            ObservableCollection<MazeCard> mazeCards = db.Query<MazeCard>(sql).ToObservableCollection();
            return mazeCards;
        }

        public List<MazeCard> GetByName2(string name)
        {
            string sql = "Select * from MazeCard where name = @name";
            List<MazeCard> mazeCard = db.Query<MazeCard>(sql, new { name }).ToList();
            return mazeCard;
        }

        public void InsertMazeCard(MazeCard mazeCard)
        {
            string sql = "Insert into MazeCard (position, name, image, rotation) values (@position, @name, @image, @rotation)";
            db.Query(sql, mazeCard);
        }

        public void UpdateMazeCard(MazeCard mazeCard)
        {
            string sql = "Update MazeCard set name = @name, position = @position, image = @Image, rotation = @rotation where id = @id";
            db.Execute(sql, new { mazeCard.Position, mazeCard.Name, mazeCard.Image, mazeCard.Rotation, mazeCard.Id });
        }

        public void DeleteMazeCard(int id)
        {
            string sql = "Delete MazeCard where ID = @ID";
            db.Execute(sql, new { id });
        }

        public void ClearTable()
        {
            ObservableCollection<MazeCard> listOfIds = Get();
            db.Execute("delete from myTable where Id in (@Id)", listOfIds.AsEnumerable().Select(i => new { Id = i }).ToList());
        }

        public void Seed()
        {
            db.Execute("Delete from MazeCard");
            InsertMazeCards();
        }


        public void InsertMazeCards()
        {
            //fixed
            MazeCard armor = new MazeCard("0,0", "armor", "/Resources/maze/fixed/m_armor.png", 0);
            InsertMazeCard(armor);

            MazeCard blue = new MazeCard("0,0", "blue", "/Resources/maze/fixed/m_blue.png", 0);
            InsertMazeCard(blue);

            MazeCard book = new MazeCard("0,0", "book", "/Resources/maze/fixed/m_book.png", 0);
            InsertMazeCard(book);

            MazeCard chandelier = new MazeCard("0,0", "chandelier", "/Resources/maze/fixed/m_chandelier.png", 0);
            InsertMazeCard(chandelier);

            MazeCard chest = new MazeCard("0,0", "chest", "/Resources/maze/fixed/m_chest.png", 0);
            InsertMazeCard(chest);

            MazeCard coins = new MazeCard("0,0", "coins", "/Resources/maze/fixed/m_coins.png", 0);
            InsertMazeCard(coins);

            MazeCard crown = new MazeCard("0,0", "crown", "/Resources/maze/fixed/m_crown.png", 0);
            InsertMazeCard(crown);

            MazeCard gem = new MazeCard("0,0", "gem", "/Resources/maze/fixed/m_gem.png", 0);
            InsertMazeCard(gem);

            MazeCard green = new MazeCard("0,0", "green", "/Resources/maze/fixed/m_green.png", 0);
            InsertMazeCard(green);

            MazeCard keys = new MazeCard("0,0", "keys", "/Resources/maze/fixed/m_keys.png", 0);
            InsertMazeCard(keys);

            MazeCard map = new MazeCard("0,0", "map", "/Resources/maze/fixed/m_map.png", 0);
            InsertMazeCard(map);

            MazeCard red = new MazeCard("0,0", "red", "/Resources/maze/fixed/m_red.png", 0);
            InsertMazeCard(red);

            MazeCard ring = new MazeCard("0,0", "ring", "/Resources/maze/fixed/m_ring.png", 0);
            InsertMazeCard(ring);

            MazeCard skull = new MazeCard("0,0", "skull", "/Resources/maze/fixed/m_skull.png", 0);
            InsertMazeCard(skull);

            MazeCard sword = new MazeCard("0,0", "sword", "/Resources/maze/fixed/m_sword.png", 0);
            InsertMazeCard(sword);

            MazeCard yellow = new MazeCard("0,0", "yellow", "/Resources/maze/fixed/m_yellow.png", 0);
            InsertMazeCard(yellow);


            //rotatable
            int rotation = 0;
            for (int i = 1; i < 5; i++)
            {
                MazeCard bat = new MazeCard("0,0", "bat", "/Resources/maze/rotatable/m_bat_"+i+".png", rotation);
                InsertMazeCard(bat);
                rotation =+ 90;
            }

            rotation = 0;
            for (int i = 1; i < 5; i++)
            {
                MazeCard corner = new MazeCard("0,0", "corner", "/Resources/maze/rotatable/m_corner_" + i + ".png", rotation);
                InsertMazeCard(corner);
                rotation = +90;
            }

            rotation = 0;
            for (int i = 1; i < 5; i++)
            {
                MazeCard dragonfly = new MazeCard("0,0", "dragonfly", "/Resources/maze/rotatable/m_dragonfly_" + i + ".png", rotation);
                InsertMazeCard(dragonfly);
                rotation = +90;
            }

            rotation = 0;
            for (int i = 1; i < 5; i++)
            {
                MazeCard drake = new MazeCard("0,0", "drake", "/Resources/maze/rotatable/m_drake_" + i + ".png", rotation);
                InsertMazeCard(drake);
                rotation = +90;
            }

            rotation = 0;
            for (int i = 1; i < 5; i++)
            {
                MazeCard fairy = new MazeCard("0,0", "fairy", "/Resources/maze/rotatable/m_fairy_" + i + ".png", rotation);
                InsertMazeCard(fairy);
                rotation = +90;
            }

            rotation = 0;
            for (int i = 1; i < 5; i++)
            {
                MazeCard ghost = new MazeCard("0,0", "ghost", "/Resources/maze/rotatable/m_ghost_" + i + ".png", rotation);
                InsertMazeCard(ghost);
                rotation = +90;
            }

            rotation = 0;
            for (int i = 1; i < 5; i++)
            {
                MazeCard ogre = new MazeCard("0,0", "ogre", "/Resources/maze/rotatable/m_ogre_" + i + ".png", rotation);
                InsertMazeCard(ogre);
                rotation = +90;
            }

            rotation = 0;
            for (int i = 1; i < 5; i++)
            {
                MazeCard owl = new MazeCard("0,0", "owl", "/Resources/maze/rotatable/m_owl_" + i + ".png", rotation);
                InsertMazeCard(owl);
                rotation = +90;
            }

            rotation = 0;
            for (int i = 1; i < 5; i++)
            {
                MazeCard rat = new MazeCard("0,0", "rat", "/Resources/maze/rotatable/m_rat_" + i + ".png", rotation);
                InsertMazeCard(rat);
                rotation = +90;
            }

            rotation = 0;
            for (int i = 1; i < 5; i++)
            {
                MazeCard salamander = new MazeCard("0,0", "salamander", "/Resources/maze/rotatable/m_salamander_" + i + ".png", rotation);
                InsertMazeCard(salamander);
                rotation = +90;
            }

            rotation = 0;
            for (int i = 1; i < 5; i++)
            {
                MazeCard scarab = new MazeCard("0,0", "scarab", "/Resources/maze/rotatable/m_scarab_" + i + ".png", rotation);
                InsertMazeCard(scarab);
                rotation = +90;
            }

            rotation = 0;
            for (int i = 1; i < 5; i++)
            {
                MazeCard spider = new MazeCard("0,0", "spider", "/Resources/maze/rotatable/m_spider_" + i + ".png", rotation);
                InsertMazeCard(spider);
                rotation = +90;
            }

            rotation = 0;
            for (int i = 1; i < 5; i++)
            {
                MazeCard straight = new MazeCard("0,0", "straight", "/Resources/maze/rotatable/m_straight_" + i + ".png", rotation);
                InsertMazeCard(straight);
                rotation = +90;
            }

            rotation = 0;
            for (int i = 1; i < 5; i++)
            {
                MazeCard wish_ghost = new MazeCard("0,0", "wish_ghost", "/Resources/maze/rotatable/m_wish_ghost_" + i + ".png", rotation);
                InsertMazeCard(wish_ghost);
                rotation = +90;
            }
        }
    }
}
