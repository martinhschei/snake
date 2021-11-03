using System.Drawing;
using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SnakeGame
{
    public abstract class GamePiece
    {
        protected Random rnd;
        protected Rectangle me;
        protected Brush myBrush;
            
        public bool isExploding;
        private int explotionLoops = 0;
        public bool explosionDone = false;
        private int explotionDuration = 15;
        private int explotionParticles = 35;
        protected List<Rectangle> explosionMaterial;

        public GamePiece()
        {
            rnd = new Random();
            explosionMaterial = new List<Rectangle>();
        }

        public Brush GetBrush()
        {
            return myBrush;
        }

        public Rectangle GetRectangle()
        {
            return me;
        }

        public void SetPosition(Point p)
        {
            me.Location = p;
        }

        public bool IntersectWith(GamePiece gp)
        {
            return me.IntersectsWith(gp.GetRectangle());
        }

        public void StartExplosion()
        {
            PrepareExplosion();
            isExploding = true;

            for(int i = 0; i < explosionMaterial.ToArray().Length; i++)
            {
                explosionMaterial[i] = new Rectangle(
                    new Point(
                        me.Location.X,me.Location.Y),
                        explosionMaterial[i].Size);
            }
        }

        private void PrepareExplosion()
        {
            for (int i = 0; i < explotionParticles; i++)
            {
                explosionMaterial.Add(
                    new Rectangle(me.Location, new Size(rnd.Next(1, 4), rnd.Next(1, 4))));
            }
        }

        public void PropellExplosion()
        {
            if (isExploding)
            {
                explotionLoops++;

                if (explotionLoops > explotionDuration)
                {
                    isExploding = false;
                    explosionDone = true;
                }
                else
                {
                    for (int i = 0; i < explosionMaterial.ToArray().Length; i++)
                    {
                        if (i % 2 == 0)
                        {
                            explosionMaterial[i] = new Rectangle(
                                new Point(
                                    explosionMaterial[i].X + rnd.Next(1, 10),
                                    explosionMaterial[i].Y - rnd.Next(1, 10)),
                                    explosionMaterial[i].Size);
                        }
                        else
                        {
                            explosionMaterial[i] = new Rectangle(
                               new Point(
                                   explosionMaterial[i].X - rnd.Next(1, 10),
                                   explosionMaterial[i].Y + rnd.Next(1, 10)),
                                   explosionMaterial[i].Size);
                        }
                    }
                }
            }
        }
        
        public Rectangle[] GetExplosionMaterial()
        {
            return explosionMaterial.ToArray();
        }

        public void MoveLeft(int x)
        {
            me.X -= x;
        }

        public void MoveRight(int x)
        {
            me.X += x;
        }

        public void MoveUp(int y)
        {
            me.Y -= y;
        }

        public void MoveDown(int y)
        {
            me.Y += y;
        }


    }
}