﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

// Obstacle objects are not being used 

namespace SnakeGame
{
    class Obstacle : GamePiece
    {
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
