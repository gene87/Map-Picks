using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Map_Picks
{
    public partial class Form2 : Form
    {
        public int average = new int();
        public double stdv = new double();
        public int min = new int();
        public int max = new int();
        public List<int> heroinfo = new List<int>();
        public string conclusion = "";
        public Form2(Hero h)
        {
            InitializeComponent();
            //Store the values of the hero
            store_values(h);
            //Find the values of the statistics
            find_statistics(h);
            //Set the name of the hero
            name_label.Text = h.name;
            //Set the values of the statistics
            average_value.Text = average.ToString();
            stdv_value.Text = stdv.ToString();
            min = heroinfo.Min();
            max = heroinfo.Max();
            min_value.Text = min.ToString();
            max_value.Text = max.ToString();
            find_conclusion();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void store_values(Hero h)
        {
            heroinfo.Add(Convert.ToInt32(h.blackheart_rank));
            heroinfo.Add(Convert.ToInt32(h.boe_rank));
            heroinfo.Add(Convert.ToInt32(h.braxis_rank));
            heroinfo.Add(Convert.ToInt32(h.cursed_rank));
            heroinfo.Add(Convert.ToInt32(h.dragon_rank));
            heroinfo.Add(Convert.ToInt32(h.garden_rank));
            heroinfo.Add(Convert.ToInt32(h.infernal_rank));
            heroinfo.Add(Convert.ToInt32(h.mines_rank));
            heroinfo.Add(Convert.ToInt32(h.sky_rank));
            heroinfo.Add(Convert.ToInt32(h.tomb_rank));
            heroinfo.Add(Convert.ToInt32(h.towers_rank));
            heroinfo.Add(Convert.ToInt32(h.warhead_rank));
        }

        private void find_statistics(Hero h)
        {
            average = Convert.ToInt32(heroinfo.Average());
            int sumofsquares = 0;
            foreach (int j in heroinfo)
            {
                int difference = j - average;
                sumofsquares += difference * difference;
            }
            stdv = Math.Sqrt(sumofsquares / heroinfo.Count);
        }

        private void find_conclusion()
        {
            //Decide which case the hero is in

            if (stdv > 15)
            {
                if (average > 60)//case 1 
                {

                    conclusion_label.Text = "This hero is very good at some maps and will fit into most team compositions." +
                        "They should be picked on the maps they are good at or based on player preference.";
                }
                else //case 2 
                {
                    conclusion_label.Text = "This hero is very good at certain maps and does not fit into many team compositions.  They should" +
                        " only be picked on the maps they are strong on.";
                }
            }
            else
            {
                if (average > 60)//case 3
                {
                    conclusion_label.Text = "This hero is generally very good and fits into most team compositions.  They should be picked" +
                        " based on player preference rather than map.";
                }
                else //case 4
                {
                    conclusion_label.Text = "This hero is good at certain maps and will probably fit into most team compositions.  " +
                        "They should be picked on the maps they are good at or for a particular team composition.";
                }
            }
        }
    }
}
