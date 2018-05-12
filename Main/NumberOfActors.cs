using Main.ORM;
using Main.ORM.DAO.Sqls;
using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace Main
{
    public partial class NumberOfActors : Form
    {
        public NumberOfActors()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NumberOfActorsView.Items.Clear();
            Collection<Movie> movies = MovieTable.Select();
            foreach (Movie movie in movies)
            {
                string[] row = { movie.Id.ToString(), movie.Title, movie.Year.ToString(), movie.Premiere.ToString(), MovieTable.Select_Actor_Number(movie.Id).ToString() };
                var listViewItem = new ListViewItem(row);
                NumberOfActorsView.Items.Add(listViewItem);
            }
        }
    }
}
