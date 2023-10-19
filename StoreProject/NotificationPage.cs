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
    public partial class NotificationPage : Form
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter ada,da;

        public int labssn; //will be used in select query to show the notifications to determine whose worker made request in other word who will see the notification
        DateTime dateRet = DateTime.Now;
        public NotificationPage()  //The form to show the notification about product actions requests to users
        {
            InitializeComponent();
         
        }

        private void NotificationPage_Load(object sender, EventArgs e) //show the request from the users (workers)
        {
           //Showing the existing requests that includes exchnage and return of the products
           
            connection = new SqlConnection("server=.; Initial Catalog=STORE2;Integrated Security=SSPI");
            connection.Open();
            ada = new SqlDataAdapter("\r\n\t\t\tselect n.notfID,n.giverSSN Sicil_No,c.Name+' '+c.Surname Ürün_Devreden,n.receiverSSN Sicil_No,cu.Name+' '+cu.Surname Ürünü_Alan,p.ProductID Ürün_Kodu,p.ProductName Değişim_Ürünü,n.ProductName Talep_Ürünü, n.actName İşlem_Türü,n.actionOn Tarih\r\n\t\tfrom NotificTable n  left outer join CompanyUser c  on c.SSNo=n.giverSSN \r\n\t\t                     left outer join CompanyUser cu on cu.SSNo=n.receiverSSN\r\n\t\t\t                  left outer join Product p on p.ProductID=n.ProductID  where c.ParentSSNo=" + labssn+"or cu.ParentSSNo="+labssn, connection);
            DataTable dt = new DataTable();
            ada.Fill(dt);
            dataGridView1.DataSource = dt;
            
          
        }

        private void button1_Click(object sender, EventArgs e) //the confirm button
        {
            try
            {
             

                if (dataGridView1.SelectedRows[0].Cells[0].Value!=null) //check the gridview if there is a notfication or not
               {

                  
                    if (dataGridView1.SelectedRows[0].Cells[8].Value.Equals("İade")) //if the user's request is returning a product
                   {

                        var confirmResult = MessageBox.Show("İade işlemini onaylamak istediğinizden emin misiniz?", "İadeyi Onayla", MessageBoxButtons.YesNo);
                        if (confirmResult == (DialogResult.Yes))
                        {



                           //After approval, send the request to the admin's screen with inserting to the ProductRequest table
                            string retQuery = "insert into ProductRequest(recSSN,reqOn,prodID,actName,isConfirmed)values (@recvr,@dateOn,@ProdID,'İade','Beklemede')";

                            command = new SqlCommand(retQuery, connection);




                            //Take the data from dataGridview to insert into the according table
                            command.Parameters.AddWithValue("@recvr", Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value));
                            command.Parameters.AddWithValue("@ProdID", Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[5].Value));
                            command.Parameters.AddWithValue("@dateOn", dateRet);
                           
                            command.ExecuteNonQuery();
                          
                            
                            MessageBox.Show("İade talebi depoya iletildi.");

                            //After approval delete the request from NotificTable and from the screen                           
                            string resQuery = "delete from NotificTable where NotificTable.notfID=@NID";
                            command= new SqlCommand(resQuery, connection);
                           
                            command.Parameters.AddWithValue("@NID", dataGridView1.SelectedRows[0].Cells[0].Value);
                            command.ExecuteNonQuery();

                           

                            this.NotificationPage_Load(sender, e);

                        }
                    }
                    else if (dataGridView1.SelectedRows[0].Cells[8].Value.Equals("Devir")) //if the user's request is about exchanging
                    {
                        var confirmRes = MessageBox.Show("Devir işlemini onaylamak istediğinizden emin misiniz?", "İadeyi Onayla", MessageBoxButtons.YesNo);
                        if (confirmRes == (DialogResult.Yes))
                        {   

                            //After confirming the request delete the product frm worker who made the request and add t to the receiver ssn's belongings
                            string excquery = "update ProductAction set ProductAction.SSNo=@recvSSN,ProductAction.ConfirmOn=@date from ProductAction where ProductAction.SSNo=@givrSSN and ProductAction.ProductID=@ProdID";


                            command = new SqlCommand(excquery, connection);



                            //Take the data from dataGridview to insert into the according table
                            command.Parameters.AddWithValue("@givrSSN", Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value));
                            command.Parameters.AddWithValue("@recvSSN", Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[3].Value));
                            command.Parameters.AddWithValue("@ProdID", Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[5].Value));
                            command.Parameters.AddWithValue("@date", dateRet);



                            command.ExecuteNonQuery();

                            MessageBox.Show("Devir İşlemi Onaylandı");

                            //after the action, delete the request
                            
                            string resQuery = "delete from NotificTable where NotificTable.notfID=@NID";
                            command = new SqlCommand(resQuery, connection);
                          
                            command.Parameters.AddWithValue("@NID", dataGridView1.SelectedRows[0].Cells[0].Value);
                            command.ExecuteNonQuery();

                            


                           
                            this.NotificationPage_Load(sender, e);
                        }
                    }
                    else if (dataGridView1.SelectedRows[0].Cells[8].Value.Equals("Talep")) //if the user's request is about getting a product
                    {
                        var confirmRes = MessageBox.Show("Talep işlemini onaylamak istediğinizden emin misiniz?", "İadeyi Onayla", MessageBoxButtons.YesNo);
                        if (confirmRes == (DialogResult.Yes))
                        {

                            //After confirming the request insert it to the ProductRequest table which can be seen by the admin
                            string excquery = "insert into ProductRequest(recSSN,reqOn,prodName,actName,isConfirmed)values (@recvr,@dateOn,@ProdName,'Talep','Beklemede')";


                            command = new SqlCommand(excquery, connection);
                           


                            //Take the data from dataGridview to insert into the according table
                           
                            command.Parameters.AddWithValue("@recvr", Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[3].Value));
                            command.Parameters.AddWithValue("@ProdName", dataGridView1.SelectedRows[0].Cells[7].Value.ToString());
                            command.Parameters.AddWithValue("@dateOn", dateRet);



                            command.ExecuteNonQuery();

                            MessageBox.Show("Talep Depoya İletildi");

                            //after the action, delete the request from NotificTable

                            string resQuery = "delete from NotificTable where NotificTable.notfID=@NID";
                            command = new SqlCommand(resQuery, connection);

                             command.Parameters.AddWithValue("@NID", dataGridView1.SelectedRows[0].Cells[0].Value);
                             command.ExecuteNonQuery();




                            this.NotificationPage_Load(sender, e);
                        }
                    }







                }
            
               
                else 
                {
                    MessageBox.Show("Onay için işlem bulunamadı!");
                }



            }

            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);

                connection.Close();
            }

        }
       

        private void button3_Click(object sender, EventArgs e)
        {
         
         
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e) //for canceling the request
        {

            if (dataGridView1.SelectedRows[0].Cells[0].Value != null)
            {

            
                var confirmRes = MessageBox.Show("İşlemi iptal etmek istediğinizden emin misiniz?", "İptali Onayla", MessageBoxButtons.YesNo);
                if (confirmRes == (DialogResult.Yes))
                {

                    //to cancel the action only delete the notification from the screen
                    string cancelQuery = "delete from NotificTable where NotificTable.notfID=@NID";
              
                    command = new SqlCommand(cancelQuery, connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@NID", dataGridView1.SelectedRows[0].Cells[0].Value);
                    command.ExecuteNonQuery();

                    MessageBox.Show("İşlem iptal edildi!");


                    connection.Close();
                    this.NotificationPage_Load(sender, e);
                }
               
            }
            else 
            {
                MessageBox.Show("İptal için işlem bulunmamaktadır!");
            }
        }


       
    }
}
