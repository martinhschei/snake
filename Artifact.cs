using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SnakeGame
{
    class Artifact : GamePiece 
    {
        private int myPoints;
        private bool hasBeenHit;
        public int points;

        public Artifact(int x, int y)
        {
            myBrush = Brushes.Chartreuse;
            me = new Rectangle(x, y, 10, 10);

            myPoints = rnd.Next(1, 10);

            hasBeenHit = false;
            points = rnd.Next(1, 10);
        }

        public int GetArtifactPoints()
        {
            return points;
        }

        public bool HasBeenHit()
        {
            return hasBeenHit;
        }

        public void Hit()
        {
            hasBeenHit = true;
            isExploding = true;
            StartExplosion();
        }
         
    }
}
