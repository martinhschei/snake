using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        Random rnd;
        Snake snake;
        List<Obstacle> obstacles = new List<Obstacle>();
        List<Artifact> artifacts = new List<Artifact>();

        public Form1()
        {
            InitializeComponent();

            rnd = new Random();

            CreateArtifacts();
            CreateSnake();
            

            /*
            // create 10 artifacts per obstacle
            int artPerObs = 10;
            Obstacle o;
            Artifact a;
            for (int i = 0; i < amountObs; i++)
            {
                o = new Obstacle(rnd.Next(0, ClientRectangle.Width), rnd.Next(0, ClientRectangle.Height));
                obstacles.Add(o);

                for (int j = 0; j < artPerObs; j++)
                {
                    do
                    {
                        a = new Artifact(rnd.Next(0, ClientRectangle.Width), rnd.Next(0, ClientRectangle.Height));
                    } while (Collides(a.GetRectangle(), o.GetRectangle()));

                    
                }
            }
            */
        }

        private void CreateArtifacts()
        {
            Artifact a = new Artifact(rnd.Next(0, ClientRectangle.Width), rnd.Next(0, ClientRectangle.Height));
            artifacts.Add(a);
        }

        private void CreateSnake()
        {
            snake = new Snake();
            snake.OnNewScore += Snake_OnNewScore;
            Text = ("Snake - the game - 0 points");
        }

        private void Snake_OnNewScore(int points)
        {
            Text = ("Snake - the game - " + points + " points");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            GamePlay.Enabled = true;
            Explosions.Enabled = true;
        }

        private void GamePlay_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if(snake.GameOn)
            {
                e.Graphics.FillRectangle(snake.GetHeadBrush(), snake.GetSnakeHead());

                snake.GetSnakeBody().ToList().ForEach(bodyPart =>
                {
                    e.Graphics.FillRectangle(bodyPart.GetBrush(), bodyPart.GetRectangle());
                });


                artifacts.ForEach(a =>
                {
                    if (a.HasBeenHit() == false)
                    {
                        e.Graphics.FillRectangle(a.GetBrush(), a.GetRectangle());
                    }
                    else if (a.HasBeenHit() == true)
                    {
                        /*
                        if (!a.isExploding && a.explosionDone == false)
                        {
                            Console.WriteLine("Here");
                            e.Graphics.FillRectangle(Brushes.Lavender, a.GetRectangle());
                        }
                        */
                        if (a.isExploding)
                        {
                            e.Graphics.FillRectangles(Snake.GetRandomBrush(), a.GetExplosionMaterial());
                        }
                    }
                });

                CollisionCheck();

                snake.MoveEverything();
            }
            else
            {
                GamePlay.Enabled = false;
                Explosions.Enabled = false;

                if(MessageBox.Show("Play again?", "Game over", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    CreateSnake();
                    GamePlay.Enabled = true;
                    Explosions.Enabled = true;
                }
                else
                {
                    Close();
                }
               
            }

        }

        private void CollisionCheck()
        {
            if(snake.GetSnakeHead().X >= Width || snake.GetSnakeHead().X < 0)
            {
                snake.GameOn = false;
            }

            else if(snake.GetSnakeHead().Y >= Height || snake.GetSnakeHead().Y < 0)
            {
                snake.GameOn = false;
            }

            Artifact a = artifacts.SingleOrDefault(ar => ar.HasBeenHit() == false && ar.GetRectangle().IntersectsWith(snake.GetSnakeHead()));
            if(a != null)
            {
                a.Hit();

                for(int i = 0; i < a.GetArtifactPoints(); i++)
                {
                    snake.GrowBody();
                }

                Snake_OnNewScore(snake.GetPoints());
                Artifact newA = new Artifact(rnd.Next(0, ClientRectangle.Width), rnd.Next(0, ClientRectangle.Height));
                artifacts.Add(newA);
            }
            

            snake.GetSnakeBody().ToList().ForEach(bodyPart =>
            {
                if ((snake.GetSnakeHead().IntersectsWith(bodyPart.GetRectangle()) && (bodyPart != snake.GetSnakeBody().ToList().Last())))
                {
                    snake.ShredBody();
                }

            });

        }

        private void Explosions_Tick(object sender, EventArgs e)
        {
            artifacts.ForEach(a => a.PropellExplosion());
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyCode == Keys.Down)
            {
                snake.MoveInNewDirection(Snake.Directions.DOWN);
            }
            else if (e.KeyCode == Keys.Up)
            {
                snake.MoveInNewDirection(Snake.Directions.UP);
            }
            else if (e.KeyCode == Keys.Left)
            {
                snake.MoveInNewDirection(Snake.Directions.LEFT);
            }
            else if (e.KeyCode == Keys.Right)
            {
                snake.MoveInNewDirection(Snake.Directions.RIGHT);
            }
  
        }

    }
}

