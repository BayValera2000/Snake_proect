using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Змейка
{
    public partial class Form1 : Form
    {
        private int rI, rJ;
        private PictureBox fruit;
        private int divx, divy;
        public int width = 800;
        public int height = 800;
        public int sideOfSides = 40;
        private PictureBox[] snake = new PictureBox[400];
        private int score = 0;
        private Label ScoreLabel;
        public Form1()
        {
            InitializeComponent();
            this.Width = width + 100;
            this.Height = height;
            this.Text = "snake";
            divx = 1;
            divy = 0;
            generateMap();
            timer1.Tick += new EventHandler(update);
            timer1.Interval = 200;
            timer1.Start();
            this.KeyDown += new KeyEventHandler(OKP);
            fruit = new PictureBox();
            fruit.BackColor = Color.Yellow;
            fruit.Size = new Size(sideOfSides, sideOfSides);
            generateFruit();
            snake[0] = new PictureBox();
            snake[0].Location = new Point(200, 200);
            snake[0].Size = new Size(sideOfSides, sideOfSides);
            snake[0].BackColor = Color.Blue;
            this.Controls.Add(snake[0]);
            ScoreLabel = new Label();
            ScoreLabel.Text = "Score: 0";
            ScoreLabel.Location = new Point(810, 10);
            this.Controls.Add(ScoreLabel);
        }
        private void eatItself()
        {
            for( int i = 1;i <= score; i++)
            {
                if (snake[0].Location == snake[i].Location)
                {
                    for(int j = i; j <= score; j++)
                    {
                        this.Controls.Remove(snake[j]);
                    }    
                    score = score - (score - i + 1);
                }
            }
        }
        private void checkBorders()
        {
            if (snake[0].Location.X < 40)
            {
                for(int i = 1; i <= score; i++)
                {
                    this.Controls.Remove(snake[i]);
                }
                score = 0;
                ScoreLabel.Text = "Score " + score;
                divx = 1;
            }
        }
        public void moveSnake()
        {
            for(int i = score; i >= 1; i--)
            {
                snake[i].Location = snake[i-1].Location;
            }
            snake[0].Location = new Point(snake[0].Location.X + divx * sideOfSides, snake[0].Location.Y + divy * sideOfSides);
            eatItself();
            checkBorders();
        }
        public void eatFruit()
        {
            if (snake[0].Location.X == rI && snake[0].Location.Y == rJ)
            {
                ScoreLabel.Text = "Score: " + ++score;
                snake[score] = new PictureBox();
                snake[score].Location = new Point(snake[score - 1].Location.X + divx * sideOfSides, snake[score - 1].Location.Y - divy * sideOfSides);
                snake[score].Size = new Size(sideOfSides, sideOfSides);
                snake[score].BackColor = Color.Blue;
                this.Controls.Add(snake[score]);
                generateFruit();
            }
        }

        public void generateFruit()
        {
            Random random = new Random();
            rI = random.Next(0, width - sideOfSides);
            int tempI = rI % sideOfSides;
            rI -= tempI;
            rJ = random.Next(0, width - sideOfSides);
            int tempJ = rJ % sideOfSides;
            rJ -= tempJ;
            fruit.Location = new Point(rI, rJ);
            this.Controls.Add(fruit);
        }

        public void generateMap()
        {
            for (int i = 0; i < width/sideOfSides; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Size = new Size(width, 1);
                pic.Location = new Point(0, sideOfSides * i);
                this.Controls.Add(pic);
            }
            for (int i = 0; i < width / sideOfSides + 1; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Size = new Size(1, height);
                pic.Location = new Point(sideOfSides * i, 0);
                this.Controls.Add(pic);
            }
        }

        public void update(Object myObject, EventArgs eventargs)
        {
            eatFruit();
            moveSnake();
            //cube.Location = new Point(cube.Location.X + divx * sideOfSides, cube.Location.Y + divy * sideOfSides);
        }

        private void OKP(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "Right":
                    divx = 1;
                    divy = 0;
                    break;
                case "Left":
                    divx = -1;
                    divy = 0;
                    break;
                case "Up":
                    divy = -1;
                    divx = 0;
                    break;
                case "Down":
                    divy = 1;
                    divx = 0;
                    break;
            }

        }
    }
}
