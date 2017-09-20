using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SnakeGame
{
    class Obstacle : GamePiece
    {
        // save timestamp from last hit and allow another hit after some seconds.
        public Obstacle(int x, int y)
        {
            myBrush = Brushes.Red;
            me = new Rectangle(x, y, 20, 40);
        }

        public void Hit()
        {

        }
    }
}
