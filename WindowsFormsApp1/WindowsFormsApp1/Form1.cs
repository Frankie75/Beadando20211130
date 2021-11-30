using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class formMain : Form
    {

        // ido:16:35

        static public string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=palyazatok";

        public formMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var rc = new SqlCommand(
                    "select palyazat.id, palyazat.tervezetA + palyazat.tervezetC, count(palyazat.id), sum(szamla.ertek) " +
                    "from szamla, palyazat, koltsegtipus " +
                    "where szamla.palyazatId=palyazat.id and " +
                    "szamla.koltsegtipusId=koltsegtipus.id " +
                    "group by palyazat.id, palyazat.tervezetA + palyazat.tervezetC " +
                    "order by palyazat.id", conn).ExecuteReader();
                while (rc.Read())
                {


                    dataGridView1.Rows.Add(rc[0], $"{rc[1]:n} Ft", $"{rc[2]:n} db", $"{rc[3]:n} Ft");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var f = new formNew(ConnectionString);

            f.Show();

        }
    }
}
