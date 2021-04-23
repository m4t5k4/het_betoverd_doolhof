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
    public class PlayerDataService
    {
        // Ophalen ConnectionString uit App.config
        private static string connectionString = ConfigurationManager.ConnectionStrings["azure"].ConnectionString;

        // Aanmaken van een object uit de IDbConnection class en instantiëren van een SqlConnection.
        // Dit betekent dat de connectie met de database automatisch geopend wordt.
        private static IDbConnection db = new SqlConnection(connectionString);

        public void Seed()
        {
            db.Execute("Delete from Player");
        }

        public ObservableCollection<Player> GetPlayers()
        {
            string sql = "Select * from Player";
            ObservableCollection<Player> players = db.Query<Player>(sql).ToObservableCollection();
            return players;
        }

        public void InsertPlayer(Player player)
        {
            string sql = "Insert into Player (name, score, position, isWinner, wizardID, image) values (@name, @score, @position, @isWinner, @wizardID, @image)";
            db.Query(sql, player);
        }

        public void UpdatePlayer(Player player)
        {
            string sql = "Update Player set name = @name, score = @score, position = @position, isWinner = @isWinner, wizardID = @wizardID, @image where id = @id";
            db.Execute(sql, new { player.Name, player.Score, player.Position, player.IsWinner, player.WizardID, player.Image, player.Id });
        }

        public void DeletePlayer(int id)
        {
            string sql = "Delete player where ID = @ID";
            db.Execute(sql, new { id });
        }
    }
}
