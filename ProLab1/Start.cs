using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProLab1
{
    public partial class Start : Form
    {

        int w_width, w_height;
        public Start()
        {
            InitializeComponent();
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string t_1 = textBox1.Text;
            int.TryParse(t_1, out w_width);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string t_2 = textBox2.Text;
            int.TryParse(t_2, out w_height);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out w_width))
            {
                MessageBox.Show("Please fill the first box correctly!");
                return;
            }

            if (!int.TryParse(textBox2.Text, out w_height))
            {
                MessageBox.Show("Please fill the second box correctly!");
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Map map = new Map();
            map.Size = new Size(w_width, w_height);
            map.Show();
        }

    }
}
