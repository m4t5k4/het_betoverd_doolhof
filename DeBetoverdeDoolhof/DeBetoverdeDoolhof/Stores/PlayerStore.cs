using DeBetoverdeDoolhof.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeBetoverdeDoolhof.Stores
{
    public class PlayerStore
    {
        public event Action<List<Player>> PlayersCreated;

        public void CreatePlayers(List<Player> players)
        {
            PlayersCreated?.Invoke(players);
        }
    }
}
