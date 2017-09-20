using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class Shift
    {
        public Rectangle ShiftSpot;
        public Snake.Directions Direction;

        public Shift()
        {

        }
        public Shift(Rectangle snapShot, Snake.Directions dir)
        {
            ShiftSpot = snapShot;
            Direction = dir;
        }
    }
}
