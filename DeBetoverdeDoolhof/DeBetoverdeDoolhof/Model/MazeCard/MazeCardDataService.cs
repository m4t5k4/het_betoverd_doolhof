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

        public ObservableCollection<MazeCard> Get()
        {
            string sql = "Select * from MazeCard";
            ObservableCollection<MazeCard> mazeCards = db.Query<MazeCard>(sql).ToObservableCollection();
            return mazeCards;
        }

        public List<MazeCard> GetByName(string name)
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
