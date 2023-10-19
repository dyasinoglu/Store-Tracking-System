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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace StoreProject
{
    public partial class WorkerPage : Form //A worker form
    {
        SqlConnection connection = new SqlConnection("server=.; Initial Catalog=STORE2;Integrated Security=SSPI");
        SqlCommand command;
        SqlDataAdapter da;

        public int kulSSN;
        public WorkerPage()
        {
            InitializeComponent();
        }

        private void WorkerPage_Load(object sender, EventArgs e)
        {
            labSSN.Text=kulSSN.ToString();
          
            try
            {

                connection.Open();

                //Showing details about the Worker who entered the system by his/her SSN info
                 da = new SqlDataAdapter("select pr.ProductID,pr.ProductName,c.ParentSSNo, count(*) AmountOfProduct \r\nfrom ProductAction p inner join Product pr on p.ProductID=pr.ProductID\r\n inner join CompanyUser c on c.SSNo=p.SSNo\r\nwhere  c.SSNo=" + kulSSN + "\r\ngroup by c.Name,c.Surname,pr.ProductID,pr.ProductName,c.ParentSSNo", connection);
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

        private void button3_Click(object sender, EventArgs e)//Return button 
        {
            LogIn_Page lp=new LogIn_Page(); 
            lp.Show();  
            this.Hide();    
        }
        public List<Int32> searchList(String query) //create a list from user's SSNs to check it while making request to exchange a prdouct
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                List<Int32> ulis = new List<Int32>();
                String quer = "select c.SSNo from CompanyUser c";
                ulis = searchList(quer); //filling the list with users

                //check for textbos and row selection from gridview if null or not

                if (!string.IsNullOrWhiteSpace(excSSN.Text) || !string.IsNullOrEmpty(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
                {
                    if (ulis.Contains(int.Parse(excSSN.Text)))
                    {
                        var confirmResult = MessageBox.Show("Ürünü devretmek istediğinizden emin misiniz?", "Devir İşlemini Onayla", MessageBoxButtons.YesNo);
                        DateTime dateExc = DateTime.Now;


                        if (confirmResult == (DialogResult.Yes))
                        {

                            //after clicking to button named as 'devir' the request will be inserted to the notification table which will be shown in the according user's page with actionname =Devir
                            DateTime dtm = DateTime.Now;
                            string notfQuery = "insert into NotificTable(giverSSN,receiverSSN,ProductID,actionOn,actName)values (@gvr,@rcvr,@pid,@dateOn,'Devir')";

                            command = new SqlCommand(notfQuery, connection);
                            connection.Open();

                            command.Parameters.AddWithValue("@gvr", int.Parse(labSSN.Text));
                            command.Parameters.AddWithValue("@rcvr", int.Parse(excSSN.Text));
                            command.Parameters.AddWithValue("@pid", int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                            command.Parameters.AddWithValue("@dateOn", dtm);
                            command.ExecuteNonQuery();


                            cleardata();
                            connection.Close();
                            MessageBox.Show("Ürün Devir Talebi İletildi.");

                        }

                    }
                    else
                    {
                        MessageBox.Show("Girdiniğiniz sicil no ile bir kullanıcı bulunamdı!");
                    }


                }
                else 
                {
                    MessageBox.Show("İşlem için lütfen gerekli alanları doldurunuz!"); 
                }
              }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
            
        }
        private void cleardata()
        {
            
            excSSN.Clear();
            textProdName.Clear();
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if(!string.IsNullOrEmpty(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
                 {
                    
                    var confirmResult = MessageBox.Show("Ürünü iade etmek istediğinizden emin misiniz?", "İade İşlemini Onayla", MessageBoxButtons.YesNo);
                    DateTime dateExc = DateTime.Now;


                    if (confirmResult == (DialogResult.Yes))
                    {
                        DateTime dtm = DateTime.Now;
                        //after clicking the 'iade' button the request will be inserted to the noticifaciton table with action name=İade
                        string notfQuery = "insert into NotificTable(giverSSN,ProductID,actionOn,actName)values (@gvr,@pid,@dateOn,'İade')";

                        command = new SqlCommand(notfQuery, connection);
                        connection.Open();

                        command.Parameters.AddWithValue("@gvr", int.Parse(labSSN.Text));
                        command.Parameters.AddWithValue("@pid", int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                     
                        command.Parameters.AddWithValue("@dateOn", dtm);
                        command.ExecuteNonQuery();


                    
                        connection.Close();
                        MessageBox.Show("Ürün İade Talebi İletildi.");

                    }
                  
                }
                else
                {
                    MessageBox.Show("Lütfen iade işlemi için bir ürün seçiniz!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(textProdName.Text)) 
            {
                DateTime dtm = DateTime.Now;
                string notfQuery = "insert into NotificTable(receiverSSN,actionOn,productName,actName)values (@recvr,@dateOn,@ProdName,'Talep')";

                command = new SqlCommand(notfQuery, connection);
                connection.Open();

                command.Parameters.AddWithValue("@recvr", int.Parse(labSSN.Text));
                command.Parameters.AddWithValue("@ProdName", textProdName.Text);


                command.Parameters.AddWithValue("@dateOn", dtm);
                command.ExecuteNonQuery();


                cleardata();
                connection.Close();
                MessageBox.Show("Ürün Talebi Şefinize İletildi.");
            }
            else 
            {
                MessageBox.Show("Lütfen ürün adı giriniz!");
            }
           
        }
    }
}
