using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeBetoverdeDoolhof.Model
{
    public class PlayerPosition
    {
        private int playerId;

        public int PlayerId
        {
            get { return playerId; }
            set { playerId = value; }
        }

        private int row;

        public int Row
        {
            get { return row; }
            set { row = value; }
        }

        private int column;

        public int Column
        {
            get { return column; }
            set { column = value; }
        }

        public PlayerPosition(int playerId, int row, int column)
        {
            PlayerId = playerId;
            Row = row;
            Column = column;
        }

        public PlayerPosition()
        {

        }
    }
}
