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
using System.Collections;

namespace StoreProject
{
    public partial class User_Page : Form
    {

         SqlConnection connection= new SqlConnection("server=.; Initial Catalog=STORE2;Integrated Security=SSPI"); 
         SqlCommand command;   
         SqlDataAdapter da;
        
        public int kullanıcıSSN;
       

        public User_Page()
        {
            InitializeComponent();
         
            
        }
      

        private void button4_Click(object sender, EventArgs e) //The return button to the Log In page
        {
            LogIn_Page logP = new LogIn_Page(); 
            logP.Show();
            this.Hide();
        }

        public void User_Page_Load(object sender, EventArgs e)
        {
            labelSSN.Text=kullanıcıSSN.ToString();
           
            try
            {
                
                connection.Open();

               //Query for showing the informations about the company user who entered wtih his/her SSN in the Data Grid View
                da = new SqlDataAdapter("Select pr.ProductID Kayıtlı_Ürün_Kodu,pr.ProductName Kayıtlı_Ürün,count(*) Ürün_Sayısı\r\nfrom ProductAction p inner join Product pr on p.ProductID=pr.ProductID\r\n inner join CompanyUser c on c.SSNo=p.SSNo\r\nwhere  c.SSNo=" + kullanıcıSSN + "\r\ngroup by c.Name,c.Surname,pr.ProductID,pr.ProductName", connection);
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
        public List<Int32> searchList(String query)//The list of the other user's SSN to check validiton while making exchange the products 
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


        private void button2_Click(object sender, EventArgs e) //The return button, for returning the product to the store
        {
            try
            {
                if (!string.IsNullOrEmpty(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))//Check for the selection of a row to choose a productID to return
                {

                    var confirmResult = MessageBox.Show("Ürünü depoya iade etmek  istediğinizden emin misiniz?", "İadeyi Onayla", MessageBoxButtons.YesNo);

                    DateTime dateRet = DateTime.Now;

                    if (confirmResult == (DialogResult.Yes)) //If user chooses the Yes  option to aprove the return of the product then the update query will be executed in the database and the product will be deleted from user's record (on the screen) and its SSN column will be null in the ProductAction table
                    {
                        string retQuery = "insert into ProductRequest(recSSN,reqOn,prodID,actName,isConfirmed)values (@recvr,@dateOn,@ProdID,'İade','Beklemede')";

                        command = new SqlCommand(retQuery, connection);
                        connection.Open();

                        //taking the data from datagridview for insertint into the according table
                        command.Parameters.AddWithValue("@dateOn", dateRet);
                        command.Parameters.AddWithValue("@ProdID", int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                        command.Parameters.AddWithValue("@recvr", int.Parse(labelSSN.Text));

                        command.ExecuteNonQuery();
                      
                        connection.Close();
                        MessageBox.Show("Ürün İade Talebi Depoya İletildi");

                        this.User_Page_Load(sender, e);
                       

                    }
                   
                }
                else
                {
                    MessageBox.Show("Lütfen iade edilecek ürünü seçiniz");
                }

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }


        }

        
        private void button1_Click(object sender, EventArgs e) //Exchange button
        {
            try
            {
                List<Int32> ulist= new List<Int32>();
                string que = "select c.SSNo from CompanyUser c";
              
                ulist = searchList(que);


                //Check for textbox partfor SSN not to be empty and the selection of a row from Data Grid View to choose a product to exchange it 
                if (!string.IsNullOrWhiteSpace(textBox1.Text) || !string.IsNullOrEmpty(dataGridView1.SelectedRows[0].Cells[0].Value.ToString())) //the productID column is cell[2] in data grid view
                {
                    if(ulist.Contains(int.Parse(textBox1.Text)))
                    {
                        var confirmResult = MessageBox.Show("Ürünü devretmek istediğinizden emin misiniz?", "Devir İşlemini Onayla", MessageBoxButtons.YesNo);
                        DateTime dateExc = DateTime.Now;


                        if (confirmResult == (DialogResult.Yes))// When the user clicks to Yes, the update query(below)will change the tables ProdcutAction with the giverSSN receiverSSN and the productID connection
                        {
                            string excquery = "update ProductAction set ProductAction.SSNo=@recvSSN,ProductAction.ConfirmOn=@date  from ProductAction where ProductAction.SSNo=@givrSSN and ProductAction.ProductID=@ProdID";


                            command = new SqlCommand(excquery, connection);
                            connection.Open();


                            command.Parameters.AddWithValue("@date", dateExc);
                            command.Parameters.AddWithValue("@recvSSN", int.Parse(textBox1.Text));
                            command.Parameters.AddWithValue("@ProdID", int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                            command.Parameters.AddWithValue("@givrSSN", int.Parse(labelSSN.Text));

                            
                            command.ExecuteNonQuery();

                            MessageBox.Show(dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + " numaralı ürün " + textBox1.Text + " sicil numaralı kişiye devredildi.");
                            cleardata();
                            connection.Close();



                            this.User_Page_Load(sender, e);


                        }
                    }
                    else 
                    {
                        MessageBox.Show("Girdiğiniz sicil no ile bir kullanıcı bulunamadı!");
                    }
                   
                   
                }
                else
                {
                    MessageBox.Show("Lütfen devir işlemi için gerekli alanları doldurunuz!");
                }
            
            
            }
            catch(Exception ex) 
            { 
            MessageBox.Show(ex.Message);

            }
        }
        private void cleardata()
        {
        
            textBox1.Clear();
            textBox2.Clear();
            

        }

        private void button3_Click(object sender, EventArgs e)//The confim button to go the the actions to confirm page/Notification page
        {
           NotificationPage np = new NotificationPage();
            np.labssn = int.Parse(labelSSN.Text);
            np.Show();
          
        }

     

        private void button5_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(textBox2.Text))
            {
                DateTime dtm = DateTime.Now;
                string notfQuery = "insert into ProductRequest(recSSN,reqOn,prodName,actName,isConfirmed)values (@recvr,@dateOn,@ProdName,'Talep','Beklemede')";

                command = new SqlCommand(notfQuery, connection);
                connection.Open();

                command.Parameters.AddWithValue("@recvr", int.Parse(labelSSN.Text));
                command.Parameters.AddWithValue("@ProdName", textBox2.Text);


                command.Parameters.AddWithValue("@dateOn", dtm);
                command.ExecuteNonQuery();


                cleardata();
                connection.Close();
                MessageBox.Show("Ürün Talebi Depoya İletildi.");
            }
            else
            {
                MessageBox.Show("Lütfen ürün adı giriniz!");
            }
        }
    }
}
