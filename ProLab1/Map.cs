using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ProLab1.Map;

namespace ProLab1
{
    public partial class Map : System.Windows.Forms.Form
    {

        bool is_game_over;
        int how_many_chests, character_speed, how_many_wasps, how_many_birds;
        int counterbird = 0;
        int counterwasp = 0;

        private List<Rock> rocks;
        private List<Wall> walls;
        private List<Tree> trees;
        private List<Mountain> mountains;
        private List<Wasp> wasps;
        private List<Bird> birds;
        private List<Chest> chests;
        int[,] matrix = new int[150,150];


        public Map()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            reset_game();
        }

        public class Locations
        {
            public int X { get; set; }

            public int Y { get; set; }

            public Locations(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }

        public class Character
        {
            public int ID { get; set; }

            public string Name { get; set; }

            public PictureBox character_pbox { get; set; }

            public Character(ImageList ch_imagelist, int image_index, int id, string name, int x_char, int y_char)
            {
                ID = id;
                Name = name;
                character_pbox = new PictureBox();
                character_pbox.Image = ch_imagelist.Images[image_index];
                Size size = new Size(30, 30);
                character_pbox.SizeMode = PictureBoxSizeMode.StretchImage;
                character_pbox.Size = size;
                Point point = new Point(x_char, y_char);
                character_pbox.Location = point;
                character_pbox.Visible = true;
            }
        }

        public abstract class Obstacle
        {
             public Locations o_locations { get; set; }

             public Obstacle(Locations locations_obs)
             {
                o_locations = locations_obs;
             }

             public abstract int size_info();
        }

        public class Tree : Obstacle
        {
            public int size_tree { get; set; }

            public PictureBox tree_pbox { get; set; }

            public Tree(int sizetree, Locations tree_loc, ImageList t_imagelist, int image_index) : base(tree_loc)
            {
                size_tree = sizetree;
                tree_pbox = new PictureBox();
                tree_pbox.Image = t_imagelist.Images[image_index];
                Size size = new Size(sizetree, sizetree);
                tree_pbox.Size = size;
                tree_pbox.SizeMode = PictureBoxSizeMode.StretchImage;
                tree_pbox.Location = new Point(tree_loc.X, tree_loc.Y);
                tree_pbox.Visible = true;
            }

            public override int size_info()
            {
                return size_tree;
            }
        }

        public class Rock : Obstacle
        {
            public int size_rock { get; set; }

            public PictureBox rock_pbox { get; set; }

            public Rock(int sizerock, Locations rock_loc, ImageList r_imagelist, int image_index) : base(rock_loc)
            {
                size_rock = sizerock;
                rock_pbox = new PictureBox();
                rock_pbox.Image = r_imagelist.Images[image_index];
                Size size = new Size(sizerock, sizerock);
                rock_pbox.Size = size;
                rock_pbox.SizeMode = PictureBoxSizeMode.StretchImage;
                rock_pbox.Location = new Point(rock_loc.X, rock_loc.Y);
                rock_pbox.Visible = true;
            }

            public override int size_info()
            {
                return size_rock;
            }
        }

        public class Mountain : Obstacle
        {
            public int size_mountain { get; set; }

            public PictureBox mountain_pbox { get; set; }

            public Mountain(int sizemountain, Locations mountain_loc, ImageList m_imagelist, int image_index) : base(mountain_loc)
            {
                size_mountain = sizemountain;
                mountain_pbox = new PictureBox();
                mountain_pbox.Image = m_imagelist.Images[image_index];
                Size size = new Size(sizemountain, sizemountain);
                mountain_pbox.Size = size;
                mountain_pbox.SizeMode = PictureBoxSizeMode.StretchImage;
                mountain_pbox.Location = new Point(mountain_loc.X, mountain_loc.Y);
                mountain_pbox.Visible = true;
            }

            public override int size_info()
            {
                return size_mountain;
            }
        }

        public class Wall : Obstacle
        {
            public int size_wall { get; set; }

            public PictureBox wall_pbox { get; set; }

            public Wall(int sizewallx, int sizewally, Locations wall_loc, ImageList w_imagelist, int image_index) : base(wall_loc)
            {
                size_wall = sizewallx;
                wall_pbox = new PictureBox();
                wall_pbox.Image = w_imagelist.Images[image_index];
                Size size = new Size(sizewallx, sizewally);
                wall_pbox.Size = size;
                wall_pbox.SizeMode = PictureBoxSizeMode.StretchImage;
                wall_pbox.Location = new Point(wall_loc.X, wall_loc.Y);
                wall_pbox.Visible = true;
            }

            public override int size_info()
            {
                return size_wall;
            }
        }

        public class Wasp : Obstacle
        {
            private int size_wasp { get; set; }

            public PictureBox wasp_pbox { get; set; }

            public Wasp(int sizewasp, Locations wasp_loc, ImageList wasp_imagelist, int image_index) : base(wasp_loc)
            {
                size_wasp = sizewasp;
                wasp_pbox = new PictureBox();
                wasp_pbox.Image = wasp_imagelist.Images[image_index];
                Size size = new Size(sizewasp, sizewasp);
                wasp_pbox.Size = size;
                wasp_pbox.SizeMode = PictureBoxSizeMode.StretchImage;
                wasp_pbox.Location = new Point(wasp_loc.X, wasp_loc.Y);
                wasp_pbox.Visible = true;
            }

            public override int size_info()
            {
                return size_wasp;
            }
        }

        public class Bird : Obstacle
        {
            private int size_bird { get; set; }

            public PictureBox bird_pbox { get; set; }

            public Bird(int sizebird, Locations bird_loc, ImageList b_imagelist, int image_index) : base(bird_loc)
            {
                size_bird = sizebird;
                bird_pbox = new PictureBox();
                bird_pbox.Image = b_imagelist.Images[image_index];
                Size size = new Size(sizebird, sizebird);
                bird_pbox.Size = size;
                bird_pbox.SizeMode = PictureBoxSizeMode.StretchImage;
                bird_pbox.Location = new Point(bird_loc.X, bird_loc.Y);
                bird_pbox.Visible = true;
            }

            public override int size_info()
            {
                return size_bird;
            }
        }

        public class Chest
        {
            public PictureBox chest_pbox { get; set; }

            public Chest(Locations chest_loc, ImageList c_imagelist, int image_index)
            {
                chest_pbox = new PictureBox();
                chest_pbox.Image = c_imagelist.Images[image_index];
                Size size = new Size(20, 20);
                chest_pbox.Size = size;
                chest_pbox.SizeMode = PictureBoxSizeMode.StretchImage;
                chest_pbox.Location = new Point(chest_loc.X, chest_loc.Y);
                chest_pbox.Visible = true;
            }
        }

        private int tick_counter = 0;
        bool time = true;

        private void game_timer_tick(object sender, EventArgs e)
        {
            if(tick_counter % 16 < 8)
            {
                time = true;
            }
            else
            {
                time = false;
            }
            move_wasp(time);
            move_bird(time);
            tick_counter++;
        }

        private void reset_game()
        {
            which_chests.Text = "0 chests are collected";
            how_many_chests = 0;

            character_speed = 10;
            is_game_over = false;

            bool collision_occured;


            rocks = new List<Rock>();
            trees = new List<Tree>();
            walls = new List<Wall>();
            chests = new List<Chest>();
            wasps = new List<Wasp>();
            birds = new List<Bird>();
            mountains = new List<Mountain>();
            Bitmap bitmap = new Bitmap(this.Width, this.Height);

            Random random = new Random();

            for (int i = 0; i < 1; i++)
            {
                int x;
                int y;
                do
                {
                    x = random.Next(30, this.Width / 2 - 160);
                    y = random.Next(30, this.Height - 170);
                    collision_occured = false;

                    for (int k = 0; k < 16; k++)
                    {
                        for (int z = 0; z < 16; z++)
                        {
                            if (matrix[y / 10 + z, x / 10 + k] == 1)
                            {
                                collision_occured = true;
                                break;
                            }
                        }
                    }

                } while (collision_occured || x % 10 != 0 || y % 10 != 0);

                for (int k = 0; k < 15; k++)
                {
                    for (int z = 0; z < 15; z++)
                    {
                        matrix[y / 10 + z, x / 10 + k] = 1;
                    }
                }

                Mountain mountain = new Mountain(150, new Locations(x, y), m_imagelist, 0);
                mountains.Add(mountain);
                this.Controls.Add(mountain.mountain_pbox);
            }

            for (int i = 0; i < 1; i++)
            {
                int x;
                int y;
                do
                {
                    x = random.Next(this.Width / 2 + 10, this.Width - 160);
                    y = random.Next(30, this.Height - 170);
                    collision_occured = false;

                    for (int k = 0; k < 16; k++)
                    {
                        for (int z = 0; z < 16; z++)
                        {
                            if (matrix[y / 10 + z, x / 10 + k] == 1)
                            {
                                collision_occured = true;
                                break;
                            }
                        }
                    }

                } while (collision_occured || x % 10 != 0 || y % 10 != 0);

                for (int k = 0; k < 15; k++)
                {
                    for (int z = 0; z < 15; z++)
                    {
                        matrix[y / 10 + z, x / 10 + k] = 1;
                    }
                }

                Mountain mountain = new Mountain(150, new Locations(x, y), m_imagelist, 1);
                mountains.Add(mountain);
                this.Controls.Add(mountain.mountain_pbox);
            }

            for (int i = 0; i < 2; i++)
            {
                int x;
                int y;
                do
                {
                    x = random.Next(30, this.Width - 100);
                    y = random.Next(30, this.Height - 100);
                    collision_occured = false;

                    for (int k = 0; k < 12; k++)
                    {
                        for (int z = 0; z < 4; z++)
                        {
                            if (matrix[y / 10 + z, x / 10 + k] == 1 || matrix[y / 10 - 1, x / 10 + k] == 1 || matrix[y / 10 + z, x / 10 - 1] == 1)
                            {
                                collision_occured = true;
                                break;
                            }
                        }
                    }

                } while (collision_occured || x % 10 != 0 || y % 10 != 0);

                for(int k = 0; k < 10; k++)
                {
                    matrix[y/10, x/10 + k] = 1;
                }

                Wall wall = new Wall(100, 10, new Locations(x, y), w_imagelist, 0);
                walls.Add(wall);
                this.Controls.Add(wall.wall_pbox);
            }

            for (int i = 0; i < 2; i++)
            {
                int x;
                int y;
                do
                {
                    x = random.Next(30, this.Width - 100);
                    y = random.Next(30, this.Height - 100);
                    collision_occured = false;

                    for (int k = 0; k < 4; k++)
                    {
                        for (int z = 0; z < 12; z++)
                        {
                            if (matrix[y / 10 + z, x / 10 + k] == 1 || matrix[y / 10 - 1, x / 10 + k] == 1 || matrix[y / 10 + z, x / 10 - 1] == 1)
                            {
                                collision_occured = true;
                                break;
                            }
                        }
                    }

                } while (collision_occured || x % 10 != 0 || y % 10 != 0);

                for (int k = 0; k < 10; k++)
                {
                    matrix[y / 10 + k, x / 10] = 1;
                }

                Wall wall = new Wall(10, 100, new Locations(x, y), w_imagelist, 1);
                walls.Add(wall);
                this.Controls.Add(wall.wall_pbox);
            }

            for (int i = 0; i < 3; i++)
            {
                int x;
                int y;
                do
                {
                    x = random.Next(30, this.Width - 30);
                    y = random.Next(30, this.Height - 30);
                    collision_occured = false;

                    for (int k = 0; k < 3; k++)
                    {
                        for(int z = 0; z < 3; z++)
                        {
                            if (matrix[y / 10 + z, x / 10 + k] == 1 || matrix[y / 10 - 1, x / 10 + k] == 1 || matrix[y / 10 + z, x / 10 - 1] == 1)
                            {
                                collision_occured = true;
                                break;
                            }
                        }
                    }

                } while (collision_occured || x % 10 != 0 || y % 10 != 0);

                for (int k = 0; k < 2; k++)
                {
                    for (int z = 0; z < 2; z++)
                    {
                        matrix[y / 10 + z, x / 10 + k] = 1;
                    }
                }

                Rock rock = new Rock(20, new Locations(x, y), r_imagelist, 0);
                rocks.Add(rock);
                this.Controls.Add(rock.rock_pbox);
            }

            for (int i = 0; i < 3; i++)
            {
                int x;
                int y;
                do
                {
                    x = random.Next(40, this.Width - 40);
                    y = random.Next(40, this.Height - 40);
                    collision_occured = false;

                    for (int k = 0; k < 4; k++)
                    {
                        for (int z = 0; z < 4; z++)
                        {
                            if (matrix[y / 10 + z, x / 10 + k] == 1 || matrix[y / 10 - 1, x / 10 + k] == 1 || matrix[y / 10 + z, x / 10 - 1] == 1)
                            {
                                collision_occured = true;
                                break;
                            }
                        }
                    }

                } while (collision_occured || x % 10 != 0 || y % 10 != 0);

                for (int k = 0; k < 3; k++)
                {
                    for(int z = 0; z < 3; z++)
                    {
                        matrix[y / 10 + z, x / 10 + k] = 1;
                    }
                }

                Rock rock = new Rock(30, new Locations(x, y), r_imagelist, 1);
                rocks.Add(rock);
                this.Controls.Add(rock.rock_pbox);
            }

            for (int i = 0; i < 2; i++)
            {
                int x;
                int y;
                do
                {
                    x = random.Next(30, this.Width - ((this.Width / 2) + 30));
                    y = random.Next(30, this.Height - 40);
                    collision_occured = false;

                    for (int k = 0; k < 3; k++)
                    {
                        for (int z = 0; z < 3; z++)
                        {
                            if (matrix[y / 10 + z, x / 10 + k] == 1 || matrix[y / 10 - 1, x / 10 + k] == 1 || matrix[y / 10 + z, x / 10 - 1] == 1)
                            {
                                collision_occured = true;
                                break;
                            }
                        }
                    }

                } while (collision_occured || x % 10 != 0 || y % 10 != 0);

                for (int k = 0; k < 2; k++)
                {
                    for (int z = 0; z < 2; z++)
                    {
                        matrix[y / 10 + z, x / 10 + k] = 1;
                    }
                }

                Tree tree = new Tree(20, new Locations(x, y), t_imagelist, 0);
                trees.Add(tree);
                this.Controls.Add(tree.tree_pbox);
            }

            for (int i = 0; i < 3; i++)
            {
                int x;
                int y;
                do
                {
                    x = random.Next(40, this.Width - ((this.Width / 2) + 30));
                    y = random.Next(30, this.Height - 50);
                    collision_occured = false;

                    for (int k = 0; k < 4; k++)
                    {
                        for (int z = 0; z < 4; z++)
                        {
                            if (matrix[y / 10 + z, x / 10 + k] == 1 || matrix[y / 10 - 1, x / 10 + k] == 1 || matrix[y / 10 + z, x / 10 - 1] == 1)
                            {
                                collision_occured = true;
                                break;
                            }
                        }
                    }

                } while (collision_occured || x % 10 != 0 || y % 10 != 0);

                for (int k = 0; k < 3; k++)
                {
                    for (int z = 0; z < 3; z++)
                    {
                        matrix[y / 10 + z, x / 10 + k] = 1;
                    }
                }

                Tree tree = new Tree(30, new Locations(x, y), t_imagelist, 1);
                trees.Add(tree);
                this.Controls.Add(tree.tree_pbox);
            }

            for (int i = 0; i < 2; i++)
            {
                int x;
                int y;
                do
                {
                    x = random.Next(((this.Width / 2) + 30), this.Width - 30);
                    y = random.Next(30, this.Height - 40);
                    collision_occured = false;

                    for (int k = 0; k < 3; k++)
                    {
                        for (int z = 0; z < 3; z++)
                        {
                            if (matrix[y / 10 + z, x / 10 + k] == 1 || matrix[y / 10 - 1, x / 10 + k] == 1 || matrix[y / 10 + z, x / 10 - 1] == 1)
                            {
                                collision_occured = true;
                                break;
                            }
                        }
                    }

                } while (collision_occured || x % 10 != 0 || y % 10 != 0);

                for (int k = 0; k < 2; k++)
                {
                    for (int z = 0; z < 2; z++)
                    {
                        matrix[y / 10 + z, x / 10 + k] = 1;
                    }
                }

                Tree tree = new Tree(20, new Locations(x, y), t_imagelist, 2);
                trees.Add(tree);
                this.Controls.Add(tree.tree_pbox);
            }

            for (int i = 0; i < 3; i++)
            {
                int x;
                int y;
                do
                {
                    x = random.Next(((this.Width / 2) + 30), this.Width - 40);
                    y = random.Next(30, this.Height - 50);
                    collision_occured = false;

                    for (int k = 0; k < 4; k++)
                    {
                        for (int z = 0; z < 4; z++)
                        {
                            if (matrix[y / 10 + z, x / 10 + k] == 1 || matrix[y / 10 - 1, x / 10 + k] == 1 || matrix[y / 10 + z, x / 10 - 1] == 1)
                            {
                                collision_occured = true;
                                break;
                            }
                        }
                    }

                } while (collision_occured || x % 10 != 0 || y % 10 != 0);

                for (int k = 0; k < 3; k++)
                {
                    for (int z = 0; z < 3; z++)
                    {
                        matrix[y / 10 + z, x / 10 + k] = 1;
                    }
                }

                Tree tree = new Tree(30, new Locations(x, y), t_imagelist, 3);
                trees.Add(tree);
                this.Controls.Add(tree.tree_pbox);
            }

            int count = random.Next(1, 3);


            switch (count)
            {
                case 1:
                    how_many_wasps = 1;
                    how_many_birds = 2;
                    for (int i = 0; i < 1; i++)
                    {
                        int x;
                        int y;
                        do
                        {
                            x = random.Next(30, this.Width - 110);
                            y = random.Next(30, this.Height - 40);
                            collision_occured = false;

                            for (int k = 0; k < 11; k++)
                            {
                                for (int z = 0; z < 3; z++)
                                {
                                    if (matrix[y / 10 + z, x / 10 + k] == 1 || matrix[y / 10 - 1, x / 10 + k] == 1 || matrix[y / 10 + z, x / 10 - 1] == 1)
                                    {
                                        collision_occured = true;
                                        break;
                                    }
                                }
                            }

                        } while (collision_occured || x % 10 != 0 || y % 10 != 0);

                        for (int k = 0; k < 10; k++)
                        {
                            for (int z = 0; z < 2; z++)
                            {
                                matrix[y / 10 + z, x / 10 + k] = 1;
                            }
                        }

                        Wasp wasp = new Wasp(20, new Locations(x, y), wasp_imagelist, 0);
                        wasps.Add(wasp);
                        this.Controls.Add(wasp.wasp_pbox);
                    }

                    for (int i = 0; i < 2; i++)
                    {
                        int x;
                        int y;
                        do
                        {
                            x = random.Next(30, this.Width - 40);
                            y = random.Next(30, this.Height - 110);
                            collision_occured = false;

                            for (int k = 0; k < 3; k++)
                            {
                                for (int z = 0; z < 11; z++)
                                {
                                    if (matrix[y / 10 + z, x / 10 + k] == 1 || matrix[y / 10 - 1, x / 10 + k] == 1 || matrix[y / 10 + z, x / 10 - 1] == 1)
                                    {
                                        collision_occured = true;
                                        break;
                                    }
                                }
                            }

                        } while (collision_occured || x % 10 != 0 || y % 10 != 0);

                        for (int k = 0; k < 2; k++)
                        {
                            for (int z = 0; z < 10; z++)
                            {
                                matrix[y / 10 + z, x / 10 + k] = 1;
                            }
                        }

                        Bird bird = new Bird(20, new Locations(x, y), b_imagelist, 0);
                        birds.Add(bird);
                        this.Controls.Add(bird.bird_pbox);
                    }
                    break;

                case 2:
                    how_many_wasps = 2;
                    how_many_birds = 1;
                    for (int i = 0; i < 2; i++)
                    {
                        int x;
                        int y;
                        do
                        {
                            x = random.Next(30, this.Width - 110);
                            y = random.Next(30, this.Height - 40);
                            collision_occured = false;

                            for (int k = 0; k < 11; k++)
                            {
                                for (int z = 0; z < 3; z++)
                                {
                                    if (matrix[y / 10 + z, x / 10 + k] == 1 || matrix[y / 10 - 1, x / 10 + k] == 1 || matrix[y / 10 + z, x / 10 - 1] == 1)
                                    {
                                        collision_occured = true;
                                        break;
                                    }
                                }
                            }

                        } while (collision_occured || x % 10 != 0 || y % 10 != 0);

                        for (int k = 0; k < 10; k++)
                        {
                            for (int z = 0; z < 2; z++)
                            {
                                matrix[y / 10 + z, x / 10 + k] = 1;

                            }
                        }

                        Wasp wasp = new Wasp(20, new Locations(x, y), wasp_imagelist, 0);
                        wasps.Add(wasp);
                        this.Controls.Add(wasp.wasp_pbox);
                    }

                    for (int i = 0; i < 1; i++)
                    {
                        int x;
                        int y;
                        do
                        {
                            x = random.Next(30, this.Width - 40);
                            y = random.Next(30, this.Height - 110);
                            collision_occured = false;

                            for (int k = 0; k < 3; k++)
                            {
                                for (int z = 0; z < 11; z++)
                                {
                                    if (matrix[y / 10 + z, x / 10 + k] == 1 || matrix[y / 10 - 1, x / 10 + k] == 1 || matrix[y / 10 + z, x / 10 - 1] == 1)
                                    {
                                        collision_occured = true;
                                        break;
                                    }
                                }
                            }

                        } while (collision_occured || x % 10 != 0 || y % 10 != 0);

                        for (int k = 0; k < 2; k++)
                        {
                            for (int z = 0; z < 10; z++)
                            {
                                matrix[y / 10 + z, x / 10 + k] = 1;
                            }
                        }

                        Bird bird = new Bird(20, new Locations(x, y), b_imagelist, 0);
                        birds.Add(bird);
                        this.Controls.Add(bird.bird_pbox);
                    }
                break;
            }

            for (int i = 0; i < 8; i++)
            {
                int x;
                int y;
                do
                {
                    x = random.Next(30, this.Width - 40);
                    y = random.Next(30, this.Height - 40);
                    collision_occured = false;

                    for (int k = 0; k < 4; k++)
                    {
                        for (int z = 0; z < 4; z++)
                        {
                            if (matrix[y / 10 + z, x / 10 + k] == 1 || matrix[y / 10 - 1, x / 10 + k] == 1 || matrix[y / 10 + z, x / 10 - 1] == 1 || matrix[y / 10 + z, x / 10 + k] == 3 || matrix[y / 10 - 1, x / 10 + k] == 3 || matrix[y / 10 + z, x / 10 - 1] == 3)
                            {
                                collision_occured = true;
                                break;
                            }
                        }
                    }

                } while (collision_occured || x % 10 != 0 || y % 10 != 0);

                for (int k = 0; k < 2; k++)
                {
                    for (int z = 0; z < 2; z++)
                    {
                        matrix[y / 10 + z, x / 10 + k] = 3;
                    }
                }

                Chest chest = new Chest(new Locations(x, y), c_imagelist, 0);
                chests.Add(chest);
                this.Controls.Add(chest.chest_pbox);
            }

            for (int i = 0; i < 7; i++)
            {
                int x;
                int y;
                do
                {
                    x = random.Next(30, this.Width - 40);
                    y = random.Next(30, this.Height - 40);
                    collision_occured = false;

                    for (int k = 0; k < 4; k++)
                    {
                        for (int z = 0; z < 4; z++)
                        {
                            if (matrix[y / 10 + z, x / 10 + k] == 1 || matrix[y / 10 - 1, x / 10 + k] == 1 || matrix[y / 10 + z, x / 10 - 1] == 1 || matrix[y / 10 + z, x / 10 + k] == 3 || matrix[y / 10 - 1, x / 10 + k] == 3 || matrix[y / 10 + z, x / 10 - 1] == 3)
                            {
                                collision_occured = true;
                                break;
                            }
                        }
                    }

                } while (collision_occured || x % 10 != 0 || y % 10 != 0);

                for (int k = 0; k < 2; k++)
                {
                    for (int z = 0; z < 2; z++)
                    {
                        matrix[y / 10 + z, x / 10 + k] = 3;
                    }
                }

                Chest chest = new Chest(new Locations(x, y), c_imagelist, 1);
                chests.Add(chest);
                this.Controls.Add(chest.chest_pbox);
            }

            for (int i = 0; i < 6; i++)
            {
                int x;
                int y;
                do
                {
                    x = random.Next(30, this.Width - 40);
                    y = random.Next(30, this.Height - 40);
                    collision_occured = false;

                    for (int k = 0; k < 4; k++)
                    {
                        for (int z = 0; z < 4; z++)
                        {
                            if (matrix[y / 10 + z, x / 10 + k] == 1 || matrix[y / 10 - 1, x / 10 + k] == 1 || matrix[y / 10 + z, x / 10 - 1] == 1 || matrix[y / 10 + z, x / 10 + k] == 3 || matrix[y / 10 - 1, x / 10 + k] == 3 || matrix[y / 10 + z, x / 10 - 1] == 3)
                            {
                                collision_occured = true;
                                break;
                            }
                        }
                    }

                } while (collision_occured || x % 10 != 0 || y % 10 != 0);

                for (int k = 0; k < 2; k++)
                {
                    for (int z = 0; z < 2; z++)
                    {
                        matrix[y / 10 + z, x / 10 + k] = 3;
                    }
                }

                Chest chest = new Chest(new Locations(x, y), c_imagelist, 2);
                chests.Add(chest);
                this.Controls.Add(chest.chest_pbox);
            }

            for (int i = 0; i < 7; i++)
            {
                int x;
                int y;
                do
                {
                    x = random.Next(30, this.Width - 40);
                    y = random.Next(30, this.Height - 40);
                    collision_occured = false;

                    for (int k = 0; k < 4; k++)
                    {
                        for (int z = 0; z < 4; z++)
                        {
                            if (matrix[y / 10 + z, x / 10 + k] == 1 || matrix[y / 10 - 1, x / 10 + k] == 1 || matrix[y / 10 + z, x / 10 - 1] == 1 || matrix[y / 10 + z, x / 10 + k] == 3 || matrix[y / 10 - 1, x / 10 + k] == 3 || matrix[y / 10 + z, x / 10 - 1] == 3)
                            {
                                collision_occured = true;
                                break;
                            }
                        }
                    }

                } while (collision_occured || x % 10 != 0 || y % 10 != 0);

                for (int k = 0; k < 2; k++)
                {
                    for (int z = 0; z < 2; z++)
                    {
                        matrix[y / 10 + z, x / 10 + k] = 3;
                    }
                }

                Chest chest = new Chest(new Locations(x, y), c_imagelist, 3);
                chests.Add(chest);
                this.Controls.Add(chest.chest_pbox);
            }

            for (int i = 0; i < 1; i++)
            {
                int x;
                int y;
                do
                {
                    x = random.Next(30, this.Width - 70);
                    y = random.Next(30, this.Height - 70);
                    collision_occured = false;

                    for (int k = 0; k < 4; k++)
                    {
                        for (int z = 0; z < 4; z++)
                        {
                            if (matrix[y / 10 + z, x / 10 + k] == 1 || matrix[y / 10 - 1, x / 10 + k] == 1 || matrix[y / 10 + z, x / 10 - 1] == 1 || matrix[y / 10 + z, x / 10 + k] == 3 || matrix[y / 10 - 1, x / 10 + k] == 3 || matrix[y / 10 + z, x / 10 - 1] == 3)
                            {
                                collision_occured = true;
                                break;
                            }
                        }
                    }

                } while (collision_occured || x % 10 != 0 || y % 10 != 0);

                for (int k = 0; k < 3; k++)
                {
                    for (int z = 0; z < 3; z++)
                    {
                        matrix[y / 10 + z, x / 10 + k] = 2;
                    }
                }

                Character character = new Character(ch_imagelist, 0, 220202064, "sev", x, y);
                Controls.Add(character.character_pbox);
            }

            for (int z = 0; z < this.Width / 10; z++)
            {
                for(int j = 0; j < this.Height / 10; j++)
                {
                    Console.Write(matrix[z, j] + " ");
                }
                Console.WriteLine();
            }
        }
        
        private void move_wasp(bool time)
        {
            foreach (Wasp wasp in wasps)
            {
                int speed = 10;
                int directionX = 0;

                if (time)
                {
                    directionX = 1;
                }
                else
                {
                    directionX = -1;
                }

                int newx = wasp.o_locations.X + (speed * directionX);
                wasp.o_locations.X = newx;
                wasp.wasp_pbox.Location = new Point(newx, wasp.o_locations.Y);
            }
        }

        private void move_bird(bool time)
        {
            foreach (Bird bird in birds)
            {
                int speed = 10;
                int directionY = 0;

                if (time)
                {
                    directionY = 1;
                }
                else
                {
                    directionY = -1;
                }

                int newy = bird.o_locations.Y + (speed * directionY);
                bird.o_locations.Y = newy;
                bird.bird_pbox.Location = new Point(bird.o_locations.X, newy);
            }
        }

        private void red_wasp(Wasp wasp, int count)
        {
            if (count < how_many_wasps)
            {
                int x = wasp.o_locations.X;
                int y = wasp.o_locations.Y;
                int width = 100;
                int height = 20;

                using (Graphics g = this.CreateGraphics())
                {
                    using (Brush brush = new SolidBrush(Color.Red))
                    {
                        g.FillRectangle(brush, x, y, width, height);

                        using (Pen pen = new Pen(Color.Red, 2))
                        {
                            g.DrawRectangle(pen, x, y, width, height);
                        }
                    }
                }
            }
        }

        private void red_bird(Bird bird, int count)
        {
            if (count < how_many_birds)
            {
                int x = bird.o_locations.X;
                int y = bird.o_locations.Y;
                int width = 20;
                int height = 100;

                using (Graphics g = this.CreateGraphics())
                {
                    using (Brush brush = new SolidBrush(Color.Red))
                    {
                        g.FillRectangle(brush, x, y, width, height);

                        using (Pen pen = new Pen(Color.Red, 2))
                        {
                            g.DrawRectangle(pen, x, y, width, height);
                        }
                    }
                }
            }
        }

        private void paint(object sender, PaintEventArgs e)
        {

            int width = this.Width;
            int height = this.Height;
            Bitmap pixels = new Bitmap(width, height);


            for (int x = 0; x < width/2; x+=10)
            {
                for (int y = 0; y < height; y++)
                {
                    Color color = pixel_left();
                    pixels.SetPixel(x, y, color);
                }
            }

            for (int x = 0; x < width/2; x++)
            {
                for (int y = 0; y < height; y+=10)
                {
                    Color color = pixel_left();
                    pixels.SetPixel(x, y, color);
                }
            }

            for (int x = width / 2; x < width; x+=10)
            {
                for (int y = 0; y < height; y++)
                {
                    Color color = pixel_right();
                    pixels.SetPixel(x, y, color);
                }
            }

            for (int x = width / 2; x < width; x++)
            {
                for (int y = 0; y < height; y+=10)
                {
                    Color color = pixel_right();
                    pixels.SetPixel(x, y, color);
                }
            }

            foreach(Wasp w in wasps)
            {
                red_wasp(w, counterwasp);
                counterwasp++;
            }

            foreach (Bird b in birds)
            {
                red_bird(b, counterbird);
                counterbird++;
            }

            e.Graphics.DrawImage(pixels, 0, 0);
        }

        private Color pixel_left()
        {
            return Color.SeaGreen;
        }

        private Color pixel_right()
        {
            return Color.SteelBlue;
        }

        private void game_over(string message)
        {


        }

        private void find_shortest_path()
        {


        }

    }
}
