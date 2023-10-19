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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace StoreProject
{
    public partial class Product_Information : Form //The form to show informations about the products that exist in the warehouse
    {

        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter da;
        public Product_Information()
        {
            InitializeComponent();

          

        }

        private void button1_Click(object sender, EventArgs e) //return to Main Menu page
        {
            Main_Menu product = new Main_Menu();
            product.Show();
            this.Hide();
        }
      


        private void Product_Information_Load(object sender, EventArgs e)
        {

             //Retreiving data from product and productaction tables in database by using join command in SQL 

            connection = new SqlConnection("server=.; Initial Catalog=STORE2;Integrated Security=SSPI");
            command = new SqlCommand();
            command.Connection = connection;
           
            connection.Open();
            da = new SqlDataAdapter("select pr.ProductID Ürün_Kodu,pr.ProductName Ürün,p.ProductTypeID Ürün_Tipi_No,p.ProductTypeName Ürün_Tipi,pa.SSNo Kayıtlı_Sicil_No\r\nfrom Product pr inner join ProductType p on p.ProductTypeID= pr.ProductTypeID\r\n                left outer join ProductAction pa on pa.ProductID=pr.ProductID\r\n\t\t\t\tleft join CompanyUser c on c.SSNo=pa.SSNo\r\norder by pa.SSNo desc", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try 
            {
                String[] comb = comboBox1.Text.Split(); //using the first char in the combobox list to take only the department no
                if((!string.IsNullOrWhiteSpace(textPName.Text) || (!string.IsNullOrWhiteSpace(comboBox1.Text)))){ 
                   
                    //insertion a a new product to the product table
                    string query = "insert into Product values(@Pname,@PtypeNo)";
                    command.CommandText = query;
                    connection.Open();
                    command.Parameters.AddWithValue("@Pname", textPName.Text);
                    command.Parameters.AddWithValue("@PtypeNo", comb[0]);

                    command.ExecuteNonQuery();
                    cleardata();
                    connection.Close();
                    MessageBox.Show("Yeni Ürün Başarıyla Kaydedildi.");

                    this.Product_Information_Load(sender, e);
                }
                else 
                {
                    MessageBox.Show("Lütfen alanları boş bırakmayınız!");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
        private void cleardata()
        {
            textPName.Clear();
            comboBox1.SelectedItem = null;
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {


                  if (!string.IsNullOrEmpty(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
                   {
                    var confirmResult = MessageBox.Show("Ürünü silmek istediğinizden emin misiniz?", "Silmeyi Onayla", MessageBoxButtons.YesNo);
                        if (confirmResult == (DialogResult.Yes))
                        {

                        //deleting a product from the table 
                        string query = "delete from Product where Product.ProductID=@DelPID";
                        connection.Open();
                        command = new SqlCommand(query, connection);

                        command.Parameters.AddWithValue("@DelPID", int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                        command.ExecuteNonQuery();

                        connection.Close();
                        MessageBox.Show("Girmiş olduğunuz ürün silindi!");

                        this.Product_Information_Load(sender, e);
                        }
                    }
                    else 
                    {
                        MessageBox.Show("Ürün silmek için lütfen bir ürün seçiniz!");
                    }
                 
                
               
               
            }
             catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

            } 
    }
}
