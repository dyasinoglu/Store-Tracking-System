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
using System.Runtime.CompilerServices;

namespace StoreProject
{
    public partial class HistoryRecord : Form
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter da;
        public HistoryRecord()
        {
            InitializeComponent();
           


        }
           //to return to Main Menu Page
        private void button1_Click(object sender, EventArgs e)
        {
            Main_Menu mn = new Main_Menu();
            mn.Show();
           this.Hide();
        }

        public void HistoryRecord_Load(object sender, EventArgs e)
        {

            try
            {
                connection = new SqlConnection("server=.; Initial Catalog=STORE2;Integrated Security=SSPI");
                connection.Open();

                //displaying data from ProductActionLog which is automatically filled by a trigger after delete and update operations
                da = new SqlDataAdapter("select cu.SSNo Veren_Sicil_No,cu.Name+' '+cu.Surname Ürünü_Veren,pl.actionName İşlem,pl.receiverSSN Alan_Sicil_No,c.Name+' '+c.Surname Ürünü_Alan,p.ProductName Ürün,pl.ProdID Ürün_Kodu,pl.actionDate Tarih \r\nfrom  ProductActionLog pl  left join CompanyUser cu  on cu.SSNo=pl.giverSSN   \r\n\t                           inner join Product p on p.ProductID=pl.ProdID\r\n\t\t\t\t\t\t\t   left join CompanyUser c on c.SSNo=pl.receiverSSN  order by pl.actionDate ", connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;




                connection.Close();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

      

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection("server=.; Initial Catalog=STORE2;Integrated Security=SSPI");
                connection.Open();

                //displaying data from CompanyUser which has been deleted recently
                da = new SqlDataAdapter("select i.SSN Sicil_No,i.Position Pozisyon,i.ParentSsn Üst_Kişi_Sicil,i.Depart Departman, i.deletedOn Çıkış_Tarih from InfoArchieve i ", connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;




                connection.Close();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.HistoryRecord_Load(sender, e); 
        }
    }
    }

