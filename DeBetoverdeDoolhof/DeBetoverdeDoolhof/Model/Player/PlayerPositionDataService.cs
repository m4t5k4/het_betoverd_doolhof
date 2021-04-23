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
    public class PlayerPositionDataService
    {
        // Ophalen ConnectionString uit App.config
        private static string connectionString = ConfigurationManager.ConnectionStrings["azure"].ConnectionString;

        // Aanmaken van een object uit de IDbConnection class en instantiëren van een SqlConnection.
        // Dit betekent dat de connectie met de database automatisch geopend wordt.
        private static IDbConnection db = new SqlConnection(connectionString);

        public void Seed()
        {
            db.Execute("Delete from PlayerPosition");
        }

        public ObservableCollection<PlayerPosition> GetPlayerPositions()
        {
            string sql = "Select * from PlayerPosition";
            ObservableCollection<PlayerPosition> players = db.Query<PlayerPosition>(sql).ToObservableCollection();
            return players;
        }

        public void InsertPlayerPosition(PlayerPosition playerPosition)
        {
            string sql = "Insert into PlayerPosition (playerId, row, [column]) values (@playerId, @row, @column)";
            db.Query(sql, playerPosition);
        }

        public void UpdatePlayerPosition(PlayerPosition playerPosition)
        {
            string sql = "Update PlayerPosition set row = @row, [column] = @column where playerId = @playerId";
            db.Execute(sql, new { playerPosition.Row, playerPosition.Column, playerPosition.PlayerId });
        }

        public void DeletePlayerPosition(int id)
        {
            string sql = "Delete PlayerPosition where ID = @ID";
            db.Execute(sql, new { id });
        }
    }
}
