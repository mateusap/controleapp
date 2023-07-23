using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControleApp
{
    public partial class Form1 : Form
    {
        public const int WM_NCLBUTTODOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        static string connstring = "server=localhost;userid=root;password=;database=new_schema";
        MySqlConnection connection = new MySqlConnection(connstring);
        private static ListView listview;

        public Form1()
        {
            InitializeComponent();



        }



        private void button1_Click(object sender, EventArgs e)
        {

            using (var conn = new MySqlConnection(connstring))
            {
                conn.Open();
                DataTable dt = new DataTable();
                MySqlCommand command = new MySqlCommand("SELECT * FROM new_schema.pasta1", conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.SelectCommand = command;
                adapter.Fill(dt);

                listBox1.DataSource = dt;
                listBox1.DisplayMember = "nome";
                listBox1.ValueMember = "nome";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    ListViewItem item = new ListViewItem(row["código"].ToString());
                    item.SubItems.Add(row["nome"].ToString());

                    listView1.Items.Add(item);
                }
                conn.Close();


            }
        }
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            connection.Open();
            string uf;
            string nome = listView1.SelectedItems[0].SubItems[1].Text;
            MySqlCommand command = new MySqlCommand($"SELECT * FROM new_schema.pasta1 WHERE nome = '{nome}'", connection);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                uf = reader["uf"].ToString();

                textBox1.Text = uf;
                reader.Close();
            }
            else
            {
                reader.Close();
                MessageBox.Show("erro na busca");
            }
            connection.Close();
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
            ListViewItem item = listView1.FindItemWithText(textBox2.Text, true, 0);
            if (item != null) { item.Selected = true; }
            
        }
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ListViewItem item = listView1.FindItemWithText(textBox2.Text, true, 0);
                if (item != null) { item.Selected = true;}

            }
        }
    }
}
