using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    class SnakeHead
    {
        private Rectangle head;
        private Brush defaultBrush = Brushes.Yellow;
        
        public SnakeHead()
        {
            head = new Rectangle(new Point(50, 75), new Size(10, 10));
        }
            
        public Rectangle GetHeadRectangle()
        {
            return head;
        }

        public void MoveInDirection(Snake.Directions dir, int momentum)
        {
            switch (dir)
            {
                case (Snake.Directions.UP):
                    {
                        head.Y -= momentum;
                        break;
                    }
                case (Snake.Directions.DOWN):
                    {
                        head.Y += momentum;
                        break;
                    }
                case (Snake.Directions.LEFT):
                    {
                        head.X -= momentum;
                        break;
                    }
                case (Snake.Directions.RIGHT):
                    {
                        head.X += momentum;
                        break;
                    }
            }
        }

        public Brush GetBrush()
        {
            return defaultBrush;
        }
    }
}
