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

    

    public partial class formNew : Form
    {

        static public string ConnectionString;

        public formNew(string c)
        {
            InitializeComponent();
            ConnectionString = c;

        }


        public int anumber = 0;
        public int cnumber = 0;
        public int nextfreeplace;
  

        private void tbAkeret_Leave(object sender, EventArgs e)
        {
            string errorMessage = "";

            if (int.TryParse(tbAkeret.Text, out anumber))
            {
                if (anumber < 0) errorMessage += "negativ szam!";
            }
            else errorMessage+="nem szam";
            if (errorMessage != "")
            {
                MessageBox.Show(errorMessage);
                tbAkeret.Text = "0";
                anumber = 0;
            }

        }

        private void tbCkeret_Leave(object sender, EventArgs e)
        {
            string errorMessage = "";

            if (int.TryParse(tbCkeret.Text, out cnumber))
            {
                if (cnumber < 0) errorMessage += "negativ szam!";
            }
            else errorMessage += "nem szam";
            if (errorMessage != "")
            {
                MessageBox.Show(errorMessage);
                tbCkeret.Text = "0";
                cnumber = 0;
            }
        }

        private void formNew_Load(object sender, EventArgs e)
        {

            tbAkeret.Text = "0";
            tbCkeret.Text = "0";


            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var rc = new SqlCommand("select count(palyazat.id) from palyazat;",conn)
                    .ExecuteReader();

                while(rc.Read())
                {
                    nextfreeplace = (int)rc[0] + 1;
                    tbId.Text = nextfreeplace.ToString();
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            var command = new SqlCommand(
                    "INSERT INTO palyazat VALUES " +
                    $"('{nextfreeplace}', '{anumber}', '{cnumber}');", conn);
            var adapter = new SqlDataAdapter()
            {
                InsertCommand = command,
            };
            adapter.InsertCommand.ExecuteNonQuery();
            this.Close();

        }
    }
}
