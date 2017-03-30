using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;

namespace Map_Picks
{
    public partial class Form1 : Form
    {
        ImageList maps = new ImageList();
        public PictureBox[] map_pic = new PictureBox[12];
        public List<PictureBox> heroes_pic = new List<PictureBox>();
        ImageList heroList = new ImageList();
        public List<Hero> HeroesDB = new List<Hero>();
        public ImageList ranks = new ImageList();
        public List<string[]> values = new List<string[]>();
        public List<List<string>> Graphs = new List<List<string>>();
        public List<string> boe = new List<string>();
        public List<string> braxis = new List<string>();
        public List<string> garden = new List<string>();
        public List<string> dragon = new List<string>();
        public List<string> mines = new List<string>();
        public List<string> tomb = new List<string>();
        public List<string> warhead = new List<string>();
        public List<string> blackheart = new List<string>();
        public List<string> infernal = new List<string>();
        public List<string> towers = new List<string>();
        public List<string> cursed = new List<string>();
        public List<string> sky = new List<string>();

        public Form1()
        {
            InitializeComponent();
            Setup_Rankings();
            Setup_Maps();
            Setup_Heroes();
            Setup_HeroesDB();
            Setup_graphs();
        }

        private void Setup_graphs()
        {
            //Add the values from the csv file to the lists for graphing
            for (int k = 1; k < 64; k++)
            {
                boe.Add(values[k][1]);
                braxis.Add(values[k][2]);
                garden.Add(values[k][3]);
                dragon.Add(values[k][4]);
                mines.Add(values[k][5]);
                tomb.Add(values[k][6]);
                warhead.Add(values[k][7]);
                blackheart.Add(values[k][8]);
                infernal.Add(values[k][9]);
                towers.Add(values[k][10]);
                cursed.Add(values[k][11]);
                sky.Add(values[k][12]);
            }

            Graphs.Add(boe);
            Graphs.Add(braxis);
            Graphs.Add(garden);
            Graphs.Add(dragon);
            Graphs.Add(mines);
            Graphs.Add(tomb);
            Graphs.Add(warhead);
            Graphs.Add(blackheart);
            Graphs.Add(infernal);
            Graphs.Add(towers);
            Graphs.Add(cursed);
            Graphs.Add(sky);
        }

        private void graphing(string name, int index)
        {
            //Clear the chart
            chart1.Series.Clear();
            chart1.Titles.Clear();
            //Set up the chart
            chart1.Titles.Add(name);
            //chart1.ChartAreas[0].AxisY.Title = "Ranking";
            chart1.ChartAreas[0].AxisX.Title = "Hero";
            chart1.ChartAreas[0].AxisY.Maximum = 100;
            //chart1.ChartAreas[0].AxisX.Maximum = 10;
            //Create a Series
            chart1.Series.Add(name);
            chart1.Legends.Clear();
            for (int i = 0; i < Graphs[index].Count(); i++)
            {
                chart1.Series[name].Points.AddXY(HeroesDB[i].name,Convert.ToInt16(Graphs[index][i]));
            }
            chart1.Series[name].ChartArea = "ChartArea1";
            chart1.ChartAreas[0].AxisX.LabelStyle.Interval = 1;
            int name_index = new int();
            //Search through heroes to find highlighted
            foreach (var h in HeroesDB)
            {
                if (h.highlight == true)//Find highlighted
                {
                    //Find the index of the map information
                    for (int j = 0; j < Graphs[index].Count; j++)
                    {
                        if (index == 0)//boe
                        {
                            if (h.boe_rank == Graphs[index][j])
                            {
                                name_index = j;
                                break;
                            }
                        }
                        if (index == 1)//braxis
                        {
                            if (h.braxis_rank == Graphs[index][j])
                            {
                                name_index = j;
                                break;
                            }
                        }
                        if (index == 2)//garden of terror
                        {
                            if (h.garden_rank == Graphs[index][j])
                            {
                                name_index = j;
                                break;
                            }
                        }
                        if (index == 3)//dragonshire
                        {
                            if (h.dragon_rank == Graphs[index][j])
                            {
                                name_index = j;
                                break;
                            }
                        }
                        if (index == 4)//haunted mines
                        {
                            if (h.mines_rank == Graphs[index][j])
                            {
                                name_index = j;
                                break;
                            }
                        }
                        if (index == 5)//tomb of the spider queen
                        {
                            if (h.tomb_rank == Graphs[index][j])
                            {
                                name_index = j;
                                break;
                            }
                        }
                        if (index == 6)//warhead junction
                        {
                            if (h.warhead_rank == Graphs[index][j])
                            {
                                name_index = j;
                                break;
                            }
                        }
                        if (index == 7)//blackhearts
                        {
                            if (h.blackheart_rank == Graphs[index][j])
                            {
                                name_index = j;
                                break;
                            }
                        }
                        if (index == 8)//infernal shrines
                        {
                            if (h.infernal_rank == Graphs[index][j])
                            {
                                name_index = j;
                                break;
                            }
                        }
                        if (index == 9)//towers of doom
                        {
                            if (h.towers_rank == Graphs[index][j])
                            {
                                name_index = j;
                                break;
                            }
                        }
                        if (index == 10)//cursed hollow
                        {
                            if (h.cursed_rank == Graphs[index][j])
                            {
                                name_index = j;
                                break;
                            }
                        }
                        if (index == 11)//sky temple
                        {
                            if (h.sky_rank == Graphs[index][j])
                            {
                                name_index = j;
                                break;
                            }
                        }
                    }
                }
            }
            //Set the point at the proper index to the colour red
            chart1.Series[name].Points[name_index].Color = Color.Red;
            //int f = Convert.ToInt16(chart1.Series[name].Points[name_index].YValues[0]);
            chart1.Series[name].Sort(PointSortOrder.Descending);
            //chart1.DataManipulator.Filter(CompareMethod.MoreThan, f + 20, name);
            //chart1.DataManipulator.Filter(CompareMethod.LessThan, f - 20, name);
        }

        private void Rankings(string name, int index)
        {
            //Set each hero to not highlighted
            foreach (var h in HeroesDB)
            {
                h.highlight = false;
            }
            //Find the hero in HeroesDB
            foreach (var h in HeroesDB)
            {
                if (h.name == name)
                {
                    //Set the highlight to be true
                    h.highlight = true;
                    //Set the label of the Maps to the hero rank
                    boe_label.Visible = true;
                    boe_label.Text = h.boe_rank;
                    braxis_label.Visible = true;
                    braxis_label.Text = h.braxis_rank;
                    garden_label.Visible = true;
                    garden_label.Text = h.garden_rank;
                    dragon_label.Visible = true;
                    dragon_label.Text = h.dragon_rank;
                    mines_label.Visible = true;
                    mines_label.Text = h.mines_rank;
                    tomb_label.Visible = true;
                    tomb_label.Text = h.tomb_rank;
                    warhead_label.Visible = true;
                    warhead_label.Text = h.warhead_rank;
                    blackheart_label.Visible = true;
                    blackheart_label.Text = h.blackheart_rank;
                    infernal_label.Visible = true;
                    infernal_label.Text = h.infernal_rank;
                    towers_label.Visible = true;
                    towers_label.Text = h.towers_rank;
                    cursed_label.Visible = true;
                    cursed_label.Text = h.cursed_rank;
                    sky_label.Visible = true;
                    sky_label.Text = h.sky_rank;
                }
            }
            heroes_pic[index].BorderStyle = BorderStyle.Fixed3D;
        }

        private void Setup_HeroesDB()
        {
            //Use the values from the csv file to create the list of Heroes.
            for (int i = 1; i < 64; i++)
            {
                HeroesDB.Add(new Hero { name = values[i][0] , boe_rank = values[i][1], braxis_rank = values[i][2], garden_rank = values[i][3],
                dragon_rank = values[i][4], mines_rank = values[i][5], tomb_rank = values[i][6], warhead_rank = values[i][7],
                blackheart_rank = values[i][8], infernal_rank = values[i][9], towers_rank = values[i][10], cursed_rank = values[i][11],
                sky_rank = values[i][12], highlight = false});
            }
        }

        public void Setup_Rankings()
        {
            //Open the csv file with the map rankings
            using (var fs = File.OpenRead(@"C:\Users\Eugene\Documents\Heroes_Map_Info.csv"))
            using (var reader = new StreamReader(fs))
            {
                List<string> listA = new List<string>();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();//Read the line
                    listA.Add(line);//Add the line to the list
                }
                for (int i = 0; i < listA.Count; i++)
                {
                    values.Add(listA[i].Split(','));//Split up the strings with commas in between and add to list
                    //for (int j = 0; j < values.Count; j++)
                    //{
                     //   values[i][j] = values[i][j].Trim();
                    //}
                }
            }
        }

        private void Setup_Maps()
        {
            maps.ImageSize = new Size(130, 100);
            maps.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\BoE.jpg"));
            maps.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\braxis.jpg"));
            maps.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\garden.jpg"));
            maps.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\dragon.jpg"));
            maps.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\mines.jpg"));
            maps.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\tomb.jpg"));
            maps.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\warhead.jpg"));
            maps.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\blackheart.jpg"));
            maps.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\infernal.jpg"));
            maps.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\towers.jpg"));
            maps.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\cursed.jpg"));
            maps.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\sky.jpg"));

            map_pic[0] = boe_pic;
            map_pic[1] = braxis_pic;
            map_pic[2] = garden_pic;
            map_pic[3] = dragon_pic;
            map_pic[4] = mines_pic;
            map_pic[5] = tomb_pic;
            map_pic[6] = warhead_pic;
            map_pic[7] = blackheart_pic;
            map_pic[8] = infernal_pic;
            map_pic[9] = towers_pic;
            map_pic[10] = cursed_pic;
            map_pic[11] = sky_pic;

            //Set the map images to the boxes
            for (int i = 0; i < 12; i++)
            {
                map_pic[i].Image = maps.Images[i];
            }
        }

        private void Setup_Heroes()
        {
            heroList.ImageSize = new Size(50, 50);
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Abathur_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Alarak_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Anub'arak_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Artanis_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Arthas_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Auriel_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Azmodan_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Brightwing_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Chen_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Cho_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Chromie_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Dehaka_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Diablo_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Falstad_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Gall_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Gazlowe_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Greymane_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Gul'dan_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Illidan_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Jaina_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Johanna_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Kael'thas_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Kerrigan_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Kharazim_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\L90ETC_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Leoric_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Li_li_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Li-Ming_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Lt-Morales_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Lucio_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Lunara_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Malfurion_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Medivh_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Muradin_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Murky_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Nazeebo_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Nova_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Ragnaros_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Raynor_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Rehgar_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Rexxar_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Samuro_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Sgt-Hammer_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Sonya_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Stitches_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Sylvanas_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Tassadar_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\The-Butcher_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Thrall_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Tracer_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Tychus_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Tyrael_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Tyrande_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Uther_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\valeera_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Valla_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Varian_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Vikings_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Zagara_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Zarya_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Zeratul_Icon.png"));
            heroList.Images.Add(Image.FromFile(@"C:\Users\Eugene\Pictures\Zuljin_Icon.png"));

            heroes_pic.Add(Abathur_pic);
            heroes_pic.Add(Alarak_pic);
            heroes_pic.Add(Anub_pic);
            heroes_pic.Add(Artanis_pic);
            heroes_pic.Add(Arthas_pic);
            heroes_pic.Add(Auriel_pic);
            heroes_pic.Add(Azmodan_pic);
            heroes_pic.Add(Brightwing_pic);
            heroes_pic.Add(Chen_pic);
            heroes_pic.Add(Cho_pic);
            heroes_pic.Add(Chromie_pic);
            heroes_pic.Add(Dehaka_pic);
            heroes_pic.Add(Diablo_pic);
            heroes_pic.Add(Falstad_pic);
            heroes_pic.Add(Gall_pic);
            heroes_pic.Add(Gazlowe_pic);
            heroes_pic.Add(Greymane_pic);
            heroes_pic.Add(Guldan_pic);
            heroes_pic.Add(Illidan_pic);
            heroes_pic.Add(Jaina_pic);
            heroes_pic.Add(Johanna_pic);
            heroes_pic.Add(Kaelthas_pic);
            heroes_pic.Add(Kerrigan_pic);
            heroes_pic.Add(Kharazim_pic);
            heroes_pic.Add(ETC_pic);
            heroes_pic.Add(Leoric_pic);
            heroes_pic.Add(Lili_pic);
            heroes_pic.Add(Liming_pic);
            heroes_pic.Add(LtMorales_pic);
            heroes_pic.Add(Lucio_pic);
            heroes_pic.Add(Lunara_pic);
            heroes_pic.Add(Malfurion_pic);
            heroes_pic.Add(Medivh_pic);
            heroes_pic.Add(Muradin_pic);
            heroes_pic.Add(Murky_pic);
            heroes_pic.Add(Nazeebo_pic);
            heroes_pic.Add(Nova_pic);
            heroes_pic.Add(Ragnaros_pic);
            heroes_pic.Add(Raynor_pic);
            heroes_pic.Add(Rehgar_pic);
            heroes_pic.Add(Rexxar_pic);
            heroes_pic.Add(Samuro_pic);
            heroes_pic.Add(SgtHammer_pic);
            heroes_pic.Add(Sonya_pic);
            heroes_pic.Add(Stitches_pic);
            heroes_pic.Add(Sylvanas_pic);
            heroes_pic.Add(Tassadar_pic);
            heroes_pic.Add(Butcher_pic);
            heroes_pic.Add(Thrall_pic);
            heroes_pic.Add(Tracer_pic);
            heroes_pic.Add(Tychus_pic);
            heroes_pic.Add(Tyrael_pic);
            heroes_pic.Add(Tyrande_pic);
            heroes_pic.Add(Uther_pic);
            heroes_pic.Add(Valeera_pic);
            heroes_pic.Add(Valla_pic);
            heroes_pic.Add(Varian_pic);
            heroes_pic.Add(Vikings_pic);
            heroes_pic.Add(Zagara_pic);
            heroes_pic.Add(Zarya_pic);
            heroes_pic.Add(Zeratul_pic);
            heroes_pic.Add(Zuljin_pic);
            //Set the hero images to the pictureboxes
            for (int i = 0; i < 62; i++)
            {
                heroes_pic[i].Image = heroList.Images[i];
            }
        }

        private void Reset_Borders()
        {
            foreach(var h in heroes_pic)
            {
                h.BorderStyle = BorderStyle.None;
            }
        }

        private void Abathur_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Abathur",0);
        }

        private void flowLayoutPanel1_Paint(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void boe_pic_Click_1(object sender, EventArgs e)
        {
            graphing("Battlefield of Eternity",0);
        }

        private void braxis_pic_Click_1(object sender, EventArgs e)
        {
            graphing("Braxis Holdout",1);
        }

        private void garden_pic_Click_1(object sender, EventArgs e)
        {
            graphing("Garden of Terror",2);
        }

        private void dragon_pic_Click_1(object sender, EventArgs e)
        {
            graphing("Dragonshire",3);
        }

        private void mines_pic_Click_1(object sender, EventArgs e)
        {
            graphing("Haunted Mines",4);
        }

        private void tomb_pic_Click_1(object sender, EventArgs e)
        {
            graphing("Tomb of the Spider Queen",5);
        }

        private void warhead_pic_Click_1(object sender, EventArgs e)
        {
            graphing("Warhead Junction",6);
        }

        private void blackheart_pic_Click_1(object sender, EventArgs e)
        {
            graphing("Blackheart's Bay",7);
        }

        private void infernal_pic_Click_1(object sender, EventArgs e)
        {
            graphing("Infernal Shrines",8);
        }

        private void towers_pic_Click_1(object sender, EventArgs e)
        {
            graphing("Towers of Doom",9);
        }

        private void cursed_pic_Click_1(object sender, EventArgs e)
        {
            graphing("Cursed Hollow",10);
        }

        private void sky_pic_Click_1(object sender, EventArgs e)
        {
            graphing("Sky Temple",11);
        }

        private void Alarak_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Alarak",1);
        }

        private void Anub_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Anub'arak",2);
        }

        private void Artanis_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Artanis",3);
        }

        private void Arthas_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Arthas", 4);
        }

        private void Auriel_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Auriel", 5);
        }

        private void Azmodan_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Azmodan", 6);
        }

        private void Brightwing_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Brightwing", 7);
        }

        private void Chen_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Chen", 8);
        }

        private void Cho_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Cho", 9);
        }

        private void Chromie_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Chromie", 10);
        }

        private void Dehaka_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Dehaka", 11);
        }

        private void Diablo_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Diablo", 12);
        }

        private void Falstad_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Falstad", 13);
        }

        private void Gall_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Gall", 14);
        }

        private void Gazlowe_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Gazlowe", 15);
        }

        private void Greymane_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Greymane", 16);
        }

        private void Guldan_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Gul'dan", 17);
        }

        private void Illidan_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Illidan", 18);
        }

        private void Jaina_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Jaina", 19);
        }

        private void Johanna_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Johanna", 20);
        }

        private void Kaelthas_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Kael'thas", 21);
        }

        private void Kerrigan_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Kerrigan", 22);
        }

        private void Kharazim_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Kharazim", 23);
        }

        private void ETC_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("ETC", 24);
        }

        private void Leoric_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Leoric", 25);
        }

        private void Lili_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Li Li", 26);
        }

        private void Liming_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Li-Ming", 27);
        }

        private void LtMorales_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Lt Morales", 28);
        }

        private void Lucio_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Lucio", 29);
        }

        private void Lunara_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Lunara", 30);
        }

        private void Malfurion_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Malfurion", 31);

        }

        private void Medivh_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Medivh", 32);
        }

        private void Muradin_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Muradin", 33);
        }

        private void Murky_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Murky", 34);
        }

        private void Nazeebo_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Nazeebo", 35);
        }

        private void Nova_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Nova", 36);
        }

        private void Ragnaros_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Ragnaros", 37);
        }

        private void Raynor_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Raynor", 38);
        }

        private void Rehgar_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Rehgar", 39);
        }

        private void Rexxar_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Rexxar", 40);
        }

        private void Samuro_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Samuro", 41);
        }

        private void SgtHammer_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Sgt Hammer", 42);
        }

        private void Sonya_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Sonya", 43);
        }

        private void Stitches_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Stitches", 44);
        }

        private void Sylvanas_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Sylvanas", 45);
        }

        private void Tassadar_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Tassadar", 46);
        }

        private void Butcher_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Butcher", 47);
        }

        private void Thrall_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Thrall", 48);
        }

        private void Tracer_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Tracer", 49);
        }

        private void Tychus_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Tychus", 50);
        }

        private void Tyrael_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Tyrael", 51);
        }

        private void Tyrande_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Tyrande", 52);
        }

        private void Uther_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Uther", 53);
        }

        private void Valeera_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Valeera", 54);
        }

        private void Valla_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Valla", 55);
        }

        private void Varian_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Varian", 56);
        }

        private void Vikings_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Vikings", 57);
        }

        private void Zagara_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Zagara", 58);
        }

        private void Zarya_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Zarya", 59);
        }

        private void Zeratul_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Zeratul", 60);
        }

        private void Zuljin_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Zul'jin", 61);
        }

        private void Probius_pic_Click(object sender, EventArgs e)
        {
            Reset_Borders();
            Rankings("Probius", 62);
        }

        private void stats_button_Click(object sender, EventArgs e)
        {
            //Find the selected Hero
            Hero selected = new Hero();
            foreach(var h in HeroesDB)
            {
                if(h.highlight == true)
                {
                    selected = h;
                }
            }
            // Create a new instance of the Form2 class
            Form2 newForm = new Form2(selected);

            // Show the settings form
            newForm.Show();
        }
    }
}
