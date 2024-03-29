﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class SnakeBodyPart
    {
        private Brush defaultBrush;
        private Rectangle bodyPart;
        private Queue<Shift> nextShifts;
        private Snake.Directions myDirection;
        private Size defaultSize = new Size(10, 10);

        public SnakeBodyPart(Point p, Snake.Directions dir)
        {
            nextShifts = new Queue<Shift>();
            bodyPart = new Rectangle(p, defaultSize);
            myDirection = dir;
            defaultBrush = Brushes.SteelBlue;
        }

        public SnakeBodyPart()
        {

        }

        public Brush GetBrush()
        {
            return defaultBrush;
        }

        public Rectangle GetRectangle()
        {
            return bodyPart;
        }
        
        public void NewDirection(Shift s)
        {
            nextShifts.Enqueue(s);
        }

        public void Move(int momentum)
        {
            if (nextShifts.Count() > 0)
            {
                if (bodyPart.Location == nextShifts.ToList().First().ShiftSpot.Location)
                {
                    Shift s = nextShifts.Dequeue();
                    myDirection = s.Direction;
                }
            }
            switch (myDirection)
            {
                case (Snake.Directions.UP):
                    {
                        bodyPart.Y -= momentum;
                        break;
                    }
                case (Snake.Directions.DOWN):
                    {
                        bodyPart.Y += momentum;
                        break;
                    }
                case (Snake.Directions.LEFT):
                    {
                        bodyPart.X -= momentum;
                        break;
                    }
                case (Snake.Directions.RIGHT):
                    {
                        bodyPart.X += momentum;
                        break;
                    }
            }
        }
    }
}
