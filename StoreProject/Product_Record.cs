using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using StoreProject.STORE2DataSetTableAdapters;

namespace StoreProject
{
    public partial class Product_Record : Form
    {

        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter da,adap;
      
        public Product_Record()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)//Return to Main Menu page
        {
            Main_Menu product = new Main_Menu();
            product.Show();
            this.Hide();
        }
        public List<Int32> searchList(String query)//Check list of SSNs to use while registering the productID
        {

            connection.Open();
            List<Int32> userList = new List<Int32>();


            command = new SqlCommand(query, connection);

            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                userList.Add(Convert.ToInt32(dataReader[0].ToString()));
            }
            connection.Close();
            return userList;
        }

        private void button2_Click(object sender, EventArgs e) //Showing the details about which user has what product and when registered
        {
            try 
            {
                List<Int32> uslist= new List<Int32>();
                string qry = "select c.SSNo from CompanyUser c";
                uslist=searchList(qry);


                if ((!string.IsNullOrWhiteSpace(textSsn.Text) || (!string.IsNullOrEmpty(dataGridView1.SelectedRows[0].Cells[5].Value.ToString())))) //dataGridView1.SelectedRows[0].Cells[5].Value.ToString() is to check or use the ProductID 
                {
                    if (string.IsNullOrEmpty(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
                    {


                        if (uslist.Contains(int.Parse(textSsn.Text))) //if textbox is not empty and the productID cell in data grid view is not empti insert the selected product to the prodcut Action table with written SSN
                        {
                            string query = "insert into ProductAction (SSNo,ProductID)values(@PSsn,@PID)";
                            command = new SqlCommand(query, connection);
                            connection.Open();
                            command.Parameters.AddWithValue("@PSsn", textSsn.Text);
                            command.Parameters.AddWithValue("@PID", int.Parse(dataGridView1.SelectedRows[0].Cells[5].Value.ToString()));

                            command.ExecuteNonQuery();
                            cleardata();

                            MessageBox.Show("Yeni Ürün Ataması Gerçekleştirildi.");


                            this.Product_Record_Load(sender, e);
                            connection.Close();
                        }
                        else
                        {
                            MessageBox.Show("Girdiğiniz sicil no ile bir kullanıcı bulunamadı!");
                        }
                    }
                    else 
                    {
                        MessageBox.Show("Ürün başka bir kullanıcıya kayıtlı olduğundan yeni bir kullanıcıya kaydedilemez!");
                    }

                }
                else
                {
                    MessageBox.Show("Lütfen gerekli alanları boş bırakmayınız!");
                }
                cleardata();
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
         
          
        }
        private void cleardata()
        {
            textSsn.Clear();
          

        }

      

        private void Product_Record_Load(object sender, EventArgs e)
        {

            //display all data about the product action table to the screen 
            button4.Visible = false;
            button5.Visible = false;
         
            connection = new SqlConnection("server=.; Initial Catalog=STORE2;Integrated Security=SSPI");
            command = new SqlCommand();
            connection.Open();
            da = new SqlDataAdapter("\r\nselect c.SSNo Sicil_No,c.Name+' '+c.Surname Personel,c.Position,d.DepartmentName,d.DepartmentNo , p.ProductID Ürün_Kodu,p.ProductName Kayıtlı_Ürün,pac.RegisterOn Ürün_Kayıt_Tarihi\r\nfrom CompanyUser c inner join Department d on d.DepartmentNo=c.DeptNo\r\ninner join ProductAction pac on c.SSNo=pac.SSNo                   \r\nright outer join Product p on p.ProductID=pac.ProductID \r\n order by Personel desc ", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();

          

        }

        private void button3_Click(object sender, EventArgs e)
        {
            label3.Visible  = false;
            label1.Visible = false;
            textSsn.Visible = false;
            button2.Visible = false;

            button4.Visible = true;
            button5.Visible = true;

            command = new SqlCommand();
            connection.Open();
            adap = new SqlDataAdapter("\r\n\r\n  select  p.reqID,p.recSSN Sicil_No,c.Name+' '+c.Surname,c.Position,d.DepartmentName,c.ParentSSNo,p.ProdID, p.ProdName,p.actName Islem, p.reqOn,p.isConfirmed\r\n  from ProductRequest p inner join CompanyUser c on c.SSNo=p.recSSN                   \r\n  inner join Department d on d.DepartmentNo=c.DeptNo order by reqOn ", connection);
            DataTable dtab = new DataTable();
            adap.Fill(dtab);
            dataGridView1.DataSource = dtab;
            dataGridView1.Refresh();
            connection.Close();





        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string actNam = Convert.ToString(dataGridView1.SelectedRows[0].Cells[8].Value);

                if (!string.IsNullOrEmpty(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
                {

                    if (!string.IsNullOrEmpty(dataGridView1.SelectedRows[0].Cells[7].Value.ToString()))
                    {
                        var confirmRes = MessageBox.Show("Talep işlemini onaylamak istediğinizden emin misiniz?", "Talebi Onayla", MessageBoxButtons.YesNo);
                        if (confirmRes == (DialogResult.Yes))
                        {

                            //After confirming the request, update the isConfirmed part as 'Onay'
                            string excquery = "update ProductRequest set isConfirmed = 'Onaylandı',reqOn = @date from ProductRequest where reqID= (select max(reqID) from ProductRequest where recSSN =" + int.Parse(dataGridView1.SelectedRows[0].Cells[1].Value.ToString()) + ")";


                            command = new SqlCommand(excquery, connection);
                            connection.Open();





                            command.Parameters.AddWithValue("@date", DateTime.Now);



                            command.ExecuteNonQuery();

                            MessageBox.Show("Talep İşlemi Onaylandı");



                            connection.Close();
                            this.Refresh();
                        }
                    }
                    else if (!string.IsNullOrEmpty(dataGridView1.SelectedRows[0].Cells[6].Value.ToString()))
                    {
                        var confirmResu = MessageBox.Show("İade işlemini onaylamak istediğinizden emin misiniz?", "İadeyi Onayla", MessageBoxButtons.YesNo);
                        if (confirmResu == (DialogResult.Yes))
                        {

                            //After confirming the request, update the isConfirmed part as 'Onay'
                            string excquery = "update ProductAction set ProductAction.SSNo = null ,ProductAction.ConfirmOn = @dateOn from ProductAction where ProductAction.SSNo=@giverSSN and ProductAction.ProductID=@prodID";

                            command = new SqlCommand(excquery, connection);
                            connection.Open();





                            command.Parameters.AddWithValue("@dateOn", DateTime.Now);
                            command.Parameters.AddWithValue("@giverSSN", int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                            command.Parameters.AddWithValue("@prodID", int.Parse(dataGridView1.SelectedRows[0].Cells[6].Value.ToString()));

                            string cancquery = "update ProductRequest set ProductRequest.isConfirmed = 'Onaylandı',ProductRequest.reqOn = @date from ProductRequest where reqID= (select max(reqID) from ProductRequest where recSSN =" + int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()) + ")";


                            command = new SqlCommand(cancquery, connection);
                            command.Parameters.AddWithValue("@date", DateTime.Now);

                            command.ExecuteNonQuery();

                            MessageBox.Show("İade İşlemi Onaylandı");








                            connection.Close();
                            this.Product_Record_Load(sender, e);

                        }




                    }
                    else
                    {
                        MessageBox.Show("Onay için işlem bulunamadı!");
                    }

                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label3.Visible = true;
            label1.Visible = true;
            textSsn.Visible = true;
            button2.Visible = true;

            button4.Visible = false;
            button5.Visible = false;
            this.Product_Record_Load(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows[0].Cells[0].Value != null)
            {


                var confirmRes = MessageBox.Show("İşlemi iptal etmek istediğinizden emin misiniz?", "İptali Onayla", MessageBoxButtons.YesNo);
                if (confirmRes == (DialogResult.Yes))
                {

                    //to cancel the action only delete the notification from the screen
                    string cancelQuery = "update ProductRequest set isConfirmed = 'İptal',reqOn = @date from ProductRequest where reqID= (select max(reqID) from ProductRequest where recSSN =" + int.Parse(dataGridView1.SelectedRows[0].Cells[1].Value.ToString()) + ")";

                    command = new SqlCommand(cancelQuery, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@date", DateTime.Now);
                    command.ExecuteNonQuery();

                    MessageBox.Show("İşlem iptal edildi");


                    connection.Close();
                    this.Product_Record_Load(sender, e);
                }
                this.Show();
            }
            else
            {
                MessageBox.Show("İptal için işlem bulunmamaktadır!");
            }
        }
    }
}
