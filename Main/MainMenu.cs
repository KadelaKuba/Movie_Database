using Main.ORM.DAO.Sqls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MovieDetail movieDetail = new MovieDetail();
            movieDetail.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NumberOfActors rates = new NumberOfActors();
            rates.Show();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
