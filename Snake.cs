using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SnakeGame
{
    public class Snake
    {

        // can be extended with more brushes to allow a multicolored snake
        private static Brush[] brushes = new Brush[] {
          
            Brushes.Red,
            Brushes.Yellow,
            Brushes.WhiteSmoke,
            Brushes.PaleVioletRed

            };

        public delegate void HeadDirectionChange(Shift s);
        public event HeadDirectionChange OnHeadDirectionChange;

        public enum Directions { UP, DOWN, LEFT, RIGHT };

        public Directions currentDirection, lastDirection;

        private int moveMomemtum = 1;
        private int sleepFactor = 20;
        private Thread mover;
        private Point StartPoint;
        private Stack<SnakeBodyPart> body;
        private SnakeHead head;
        public bool GameOn;

        public int BodyPartWidth = 10;

        public Snake()
        {
            // use a Stack so we can pop() the last added body part(s) when player hits an Obstacle
            body = new Stack<SnakeBodyPart>();
            head = new SnakeHead();

            currentDirection = Directions.RIGHT;
            lastDirection = Directions.RIGHT;

            StartPoint = new Point(25, 450);
            GameOn = true;
        }

        public static Brush GetRandomBrush()
        {
            Random rnd = new Random();
            return brushes[rnd.Next(0, Snake.brushes.Count())];
        }

        public Rectangle GetSnakeHead()
        {
            return head.GetHeadRectangle();
        }

        public Stack<SnakeBodyPart> GetSnakeBody()
        {
            return body;
        }

        public Brush GetHeadBrush()
        {
            return head.GetBrush();
        }

        public void ShredBody()
        {
            if (body.Count > 0)
            {
                body.Pop();
            }
        }

        public void GrowBody(int points)
        {
            SnakeBodyPart sbp = new SnakeBodyPart();
            Rectangle snakeHead = GetSnakeHead();

            int snakeHeadWidth = snakeHead.Width;
            int snakeBodyPartCount = GetSnakeBody().Count();
            int snakeBodyPartWidth = BodyPartWidth;
  
            if (currentDirection == Directions.DOWN)
            {
                 sbp = new SnakeBodyPart(AddBodyPart_Down(snakeBodyPartCount, snakeBodyPartWidth), currentDirection);
            }

            else if (currentDirection == Directions.UP)
            {
                sbp = new SnakeBodyPart(AddBodyPart_Up(snakeBodyPartCount, snakeBodyPartWidth), currentDirection);
            }

            else if (currentDirection == Directions.LEFT)
            {
                sbp = new SnakeBodyPart(AddBodyPart_Left(snakeBodyPartCount, snakeBodyPartWidth), currentDirection);
            }

            else if (currentDirection == Directions.RIGHT)
            {
                sbp = new SnakeBodyPart(AddBodyPart_Right(snakeBodyPartCount, snakeBodyPartWidth), currentDirection);
            }

            OnHeadDirectionChange += sbp.NewDirection;
            body.Push(sbp);
        }

        private Point AddBodyPart_Left(int BodyPartCount, int BodyPartWidth)
        {
            if (BodyPartCount > 0)
            {
                return new Point(
                    GetSnakeHead().X + (BodyPartWidth * BodyPartCount) + BodyPartWidth, GetSnakeHead().Y);
            }
            else
            {
                return new Point(
                    GetSnakeHead().X + BodyPartWidth, GetSnakeHead().Y);
            }
        }

        private Point AddBodyPart_Right(int BodyPartCount, int BodyPartWidth)
        {
            if (BodyPartCount > 0)
            {
                return new Point(
                    GetSnakeHead().X - (BodyPartWidth * BodyPartCount) - BodyPartWidth, GetSnakeHead().Y);
            }
            else
            {
                return new Point(
                    GetSnakeHead().X - BodyPartWidth, GetSnakeHead().Y);
            }
        }

        private Point AddBodyPart_Up(int BodyPartCount, int BodyPartWidth)
        {
            if (BodyPartCount > 0)
            {
                return new Point(
                    GetSnakeHead().X, GetSnakeHead().Y + (BodyPartWidth * BodyPartCount) + BodyPartWidth);
            }
            else
            {
                return new Point(
                    GetSnakeHead().X, GetSnakeHead().Y + BodyPartWidth);
            }
        }

        private Point AddBodyPart_Down(int BodyPartCount, int BodyPartWidth)
        {
            if (BodyPartCount > 0)
            {
                return new Point(
                    GetSnakeHead().X, GetSnakeHead().Y - (BodyPartWidth * BodyPartCount) - BodyPartWidth);
            }
            else
            {
                return new Point(
                    GetSnakeHead().X, GetSnakeHead().Y - BodyPartWidth);
            }
        }

        /*
        public void StartMoving()
        {
            mover = new Thread(MoveLoop);
            mover.Start();
        }
        
        private void MoveLoop()
        {
            while(true)
            {
                // needed because otherwise it runs too fast!
                Thread.Sleep(sleepFactor);

                switch (currentDirection)
                {
                    case (Directions.UP):
                        {
                            head.MoveInDirection(currentDirection, moveMomemtum);
                            body.ToList().ForEach(b => b.Move(moveMomemtum));
                            break;
                        }
                    case (Directions.DOWN):
                        {
                            head.MoveInDirection(currentDirection, moveMomemtum);
                            body.ToList().ForEach(b => b.Move(moveMomemtum));
                            break;
                        }
                    case (Directions.LEFT):
                        {
                            head.MoveInDirection(currentDirection, moveMomemtum);
                            body.ToList().ForEach(b => b.Move(moveMomemtum));
                            break;
                        }
                    case (Directions.RIGHT):
                        {
                            head.MoveInDirection(currentDirection, moveMomemtum);
                            body.ToList().ForEach(b => b.Move(moveMomemtum));
                            break;
                        }
                }
            }
        }

        */

        public void MoveEverything()
        {
            switch (currentDirection)
            {
                case (Directions.UP):
                    {
                        head.MoveInDirection(currentDirection, moveMomemtum);
                        body.ToList().ForEach(b => b.Move(moveMomemtum));
                        break;
                    }
                case (Directions.DOWN):
                    {
                        head.MoveInDirection(currentDirection, moveMomemtum);
                        body.ToList().ForEach(b => b.Move(moveMomemtum));
                        break;
                    }
                case (Directions.LEFT):
                    {
                        head.MoveInDirection(currentDirection, moveMomemtum);
                        body.ToList().ForEach(b => b.Move(moveMomemtum));
                        break;
                    }
                case (Directions.RIGHT):
                    {
                        head.MoveInDirection(currentDirection, moveMomemtum);
                        body.ToList().ForEach(b => b.Move(moveMomemtum));
                        break;
                    }
            }
        }

       
        public void MoveInNewDirection(Directions newDirection)
        {
            // are we really changing direction?
            if(newDirection != currentDirection)
            {
                // only allow "flipping" direction when snake has no body
                if (body.Count > 0)
                {
                    if (newDirection == Directions.DOWN && currentDirection == Directions.UP)
                    {
                        return;
                    }

                    if (newDirection == Directions.UP && currentDirection == Directions.DOWN)
                    {
                        return;
                    }

                    if (newDirection == Directions.LEFT && currentDirection == Directions.RIGHT)
                    {
                        return;
                    }

                    if (newDirection == Directions.RIGHT && currentDirection == Directions.LEFT)
                    {
                        return;
                    }
                }
 
                // take a snapshot of the first body part before direction changes
                if(OnHeadDirectionChange != null && OnHeadDirectionChange.GetInvocationList().Count() > 0)
                    OnHeadDirectionChange(new Shift(GetSnakeHead(), newDirection));

                lastDirection = currentDirection;
                currentDirection = newDirection;
            }
        }
    }
}
