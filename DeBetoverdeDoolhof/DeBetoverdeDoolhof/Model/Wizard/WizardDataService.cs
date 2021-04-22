using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using DeBetoverdeDoolhof.Extensions;

namespace DeBetoverdeDoolhof.Model
{
    public class WizardDataService
    {
        // Ophalen ConnectionString uit App.config
        private static string connectionString = ConfigurationManager.ConnectionStrings["azure"].ConnectionString;

        // Aanmaken van een object uit de IDbConnection class en instantiëren van een SqlConnection.
        // Dit betekent dat de connectie met de database automatisch geopend wordt.
        private static IDbConnection db = new SqlConnection(connectionString);

        public void Seed()
        {
            string count = "SELECT COUNT(*) FROM Wizard";
            int result = db.ExecuteScalar<int>(count);
            if (result != 4)
            {
                db.Execute("Delete from Wizard");

                Wizard air = new Wizard("yellow", "0,0", "/Resources/wizards/air.png");
                string sql = "Insert into Wizard (color, startPosition, image) values (@color, @startPosition, @image)";
                db.Query(sql, air);

                Wizard earth = new Wizard("green", "6,6", "/Resources/wizards/earth.png");
                db.Query(sql, earth);

                Wizard fire = new Wizard("red", "6,0", "/Resources/wizards/fire.png");
                db.Query(sql, fire);

                Wizard water = new Wizard("blue", "0,6", "/Resources/wizards/water.png");
                db.Query(sql, water);
            }
        }

        public ObservableCollection<Wizard> GetWizards()
        {
            string sql = "Select * from Wizard order by id";
            ObservableCollection<Wizard> wizards = db.Query<Wizard>(sql).ToObservableCollection();
            return wizards;
        }
    }
}
