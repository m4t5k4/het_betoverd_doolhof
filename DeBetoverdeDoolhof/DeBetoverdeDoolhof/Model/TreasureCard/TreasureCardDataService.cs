using Dapper;
using DeBetoverdeDoolhof.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeBetoverdeDoolhof.Model
{
    public class TreasureCardDataService
    {
        // Ophalen ConnectionString uit App.config
        private static string connectionString = ConfigurationManager.ConnectionStrings["azure"].ConnectionString;

        // Aanmaken van een object uit de IDbConnection class en instantiëren van een SqlConnection.
        // Dit betekent dat de connectie met de database automatisch geopend wordt.
        private static IDbConnection db = new SqlConnection(connectionString);

        public ObservableCollection<TreasureCard> Get()
        {
            string sql = "Select * from TreasureCard";
            ObservableCollection<TreasureCard> treasureCards = db.Query<TreasureCard>(sql).ToObservableCollection();
            return treasureCards;
        }

        public void InsertTreasureCard(TreasureCard treasureCard)
        {
            string sql = "Insert into MazeCard (playerID, name, isFound, image) values (@playerID, @name, @isFound, @image)";
            db.Query(sql, treasureCard);
        }

        public void UpdateTreasureCard(TreasureCard treasureCard)
        {
            string sql = "Update MazeCard set playerID = @playerID, name = @name, image = @Image, isFound = @isFound where id = @id";
            db.Execute(sql, new { treasureCard.PlayerID, treasureCard.Name, treasureCard.Image, treasureCard.IsFound, treasureCard.Id });
        }

        public void DeleteTreasureCard(int id)
        {
            string sql = "Delete TreasureCard where ID = @ID";
            db.Execute(sql, new { id });
        }

        public void Seed()
        {
            db.Execute("Delete from TreasureCard");
            InsertTreasureCards();
        }

        public void InsertTreasureCards()
        {
            TreasureCard armor = new TreasureCard(0, "armor", "/Resources/treasures/armor.png", false);
            InsertTreasureCard(armor);

            TreasureCard bat = new TreasureCard(0, "bat", "/Resources/treasures/bat.png", false);
            InsertTreasureCard(bat);

            TreasureCard book = new TreasureCard(0, "book", "/Resources/treasures/book.png", false);
            InsertTreasureCard(book);

            TreasureCard chandelier = new TreasureCard(0, "chandelier", "/Resources/treasures/chandelier.png", false);
            InsertTreasureCard(chandelier);

            TreasureCard chest = new TreasureCard(0, "chest", "/Resources/treasures/chest.png", false);
            InsertTreasureCard(chest);

            TreasureCard coins = new TreasureCard(0, "coins", "/Resources/treasures/coins.png", false);
            InsertTreasureCard(coins);

            TreasureCard crown = new TreasureCard(0, "crown", "/Resources/treasures/crown.png", false);
            InsertTreasureCard(crown);

            TreasureCard dragonfly = new TreasureCard(0, "dragonfly", "/Resources/treasures/dragonfly.png", false);
            InsertTreasureCard(dragonfly);

            TreasureCard drake = new TreasureCard(0, "drake", "/Resources/treasures/drake.png", false);
            InsertTreasureCard(drake);

            TreasureCard fairy = new TreasureCard(0, "fairy", "/Resources/treasures/fairy.png", false);
            InsertTreasureCard(fairy);

            TreasureCard gem = new TreasureCard(0, "gem", "/Resources/treasures/gem.png", false);
            InsertTreasureCard(gem);

            TreasureCard ghost = new TreasureCard(0, "ghost", "/Resources/treasures/ghost.png", false);
            InsertTreasureCard(ghost);

            TreasureCard keys = new TreasureCard(0, "keys", "/Resources/treasures/keys.png", false);
            InsertTreasureCard(keys);

            TreasureCard map = new TreasureCard(0, "map", "/Resources/treasures/map.png", false);
            InsertTreasureCard(map);

            TreasureCard ogre = new TreasureCard(0, "ogre", "/Resources/treasures/ogre.png", false);
            InsertTreasureCard(ogre);

            TreasureCard owl = new TreasureCard(0, "owl", "/Resources/treasures/owl.png", false);
            InsertTreasureCard(owl);

            TreasureCard rat = new TreasureCard(0, "rat", "/Resources/treasures/rat.png", false);
            InsertTreasureCard(rat);

            TreasureCard ring = new TreasureCard(0, "ring", "/Resources/treasures/ring.png", false);
            InsertTreasureCard(ring);

            TreasureCard salamander = new TreasureCard(0, "salamander", "/Resources/treasures/salamander.png", false);
            InsertTreasureCard(salamander);

            TreasureCard scarab = new TreasureCard(0, "scarab", "/Resources/treasures/scarab.png", false);
            InsertTreasureCard(scarab);

            TreasureCard skull = new TreasureCard(0, "skull", "/Resources/treasures/skull.png", false);
            InsertTreasureCard(skull);

            TreasureCard spider = new TreasureCard(0, "spider", "/Resources/treasures/spider.png", false);
            InsertTreasureCard(spider);

            TreasureCard sword = new TreasureCard(0, "sword", "/Resources/treasures/sword.png", false);
            InsertTreasureCard(sword);

            TreasureCard wish_ghost = new TreasureCard(0, "wish_ghost", "/Resources/treasures/wish_ghost.png", false);
            InsertTreasureCard(wish_ghost);
        }
    }
}
