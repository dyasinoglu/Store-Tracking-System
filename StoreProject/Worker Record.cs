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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Linq.Expressions;
using System.Windows.Forms.VisualStyles;

namespace StoreProject
{
    public partial class Worker_Record : Form
    {

        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter da;

      DateTime time=DateTime.Now;
              
        public Worker_Record()
        {
            InitializeComponent();

           
            


           //Page to display the worker informations and do some actions by clickling a button from Maın Menu page that admin can reach
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Main_Menu mn = new Main_Menu();
            mn.Show();
            this.Hide();
        }

    
        private void Worker_Record_Load(object sender, EventArgs e)
        {
           //Displaying the worker informations
            connection = new SqlConnection( "server=.; Initial Catalog=STORE2;Integrated Security=SSPI");
            connection.Open();
            da = new SqlDataAdapter("select cu.SSNo Sicil_No,cu.Name+' '+cu.Surname Personel,cu.Position Pozisyon,c.Name+' '+c.Surname Üst_Kişi,c.SSNo Üst_Kişi_Sicil_No,d.DepartmentName Departman,d.DepartmentNo Departman_No\r\nfrom CompanyUser c inner join CompanyUser cu on cu.ParentSSNo=c.SSNo\r\n                   inner join Department d on d.DepartmentNo=cu.DeptNo ", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
           
            command = new SqlCommand();
            command.Connection = connection;
            connection.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
          
           
            try
            {
              
                //check for the textbox if they are empty or not, if empty give some error message
               
                if (!string.IsNullOrWhiteSpace(textName.Text) || (!string.IsNullOrWhiteSpace(textSurn.Text)||(!string.IsNullOrWhiteSpace(comboPos.Text) || (!string.IsNullOrWhiteSpace(comboDep.Text))||(!string.IsNullOrWhiteSpace(textSsn.Text)))))
                {
                 
                      //Adding a new user with filling a few information boxes 
                        string query = "insert into CompanyUser values(@name,@surname,@DepNo,@position,@superSsn)";
                        connection.Open();
                        command.CommandText = query;
                       

  
                        command.Parameters.AddWithValue("@name", textName.Text);
                        command.Parameters.AddWithValue("@surname", textSurn.Text);
                        command.Parameters.AddWithValue("@DepNo", comboDep.Text[0]);
                        command.Parameters.AddWithValue("@position", comboPos.Text);
                        command.Parameters.AddWithValue("@superSsn", textSsn.Text);




                        command.ExecuteNonQuery();
                        cleardata();
                        connection.Close();
                        MessageBox.Show("Yeni Kullanıcı Başarıyla Kaydedildi.");
                   
                    
                        this.Worker_Record_Load(sender, e);
                  
                    
                    
                  


                }
                else
                {
                    MessageBox.Show("Lütfen kullanıcı bilgi alanlarını eksiksiz doldurunuz!");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
           
       
        }
        private void cleardata()
        {
            textName.Clear();
            textSurn.Clear();
            textSsn.Clear();
            comboDep.SelectedItem=null;
          
            comboPos.SelectedItem=null;
     



        }
        
        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {

                //check for textboxes wheter they are empty or not 


                if (!string.IsNullOrEmpty(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
                {
                    var confirmResult = MessageBox.Show("Kişiyi silmek istediğinizden emin misiniz?", "Silmeyi Onayla", MessageBoxButtons.YesNo);

                    if (confirmResult == (DialogResult.Yes))
                    {

                        //Deleting the user from the system after chooseng on the datagridview and clicking the 'sil' button 
                        String que = "insert into InfoArchieve(SSN,Position,ParentSsn,Depart,deletedOn) values(@DelSicil,@position,@parentssn,@Depart,@date)";
                        connection.Open();
                        command = new SqlCommand(que, connection);
                        command.Parameters.AddWithValue("@DelSicil", int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                        command.Parameters.AddWithValue("@position", dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
                        command.Parameters.AddWithValue("@parentssn", int.Parse(dataGridView1.SelectedRows[0].Cells[4].Value.ToString()));
                        command.Parameters.AddWithValue("@Depart", dataGridView1.SelectedRows[0].Cells[5].Value.ToString());
                        command.Parameters.AddWithValue("@date", time);
                        command.ExecuteNonQuery();
                       
                        string query = "delete from CompanyUser  where CompanyUser.SSNo=@DelSicil";
                        command = new SqlCommand(query, connection);
                        
                        command.Parameters.AddWithValue("@DelSicil", int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                        command.ExecuteNonQuery();

                        cleardata();
                        MessageBox.Show(int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()) + " sicil no'lu kullanıcı silindi!");

                        connection.Close();

                        this.Worker_Record_Load(sender, e);
                    }
                  
                }
                else
                {
                    MessageBox.Show("Lütfen silmek için bir kullanıcı seçiniz!");
                }
                  

                
                
            
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Silmek istediğiniz kişiye bağlı personel bulunmaktadır. Silmeye devam etmek istiyorsanız bağlı personelin üst kişi bilgisini güncellemeniz gerekmektedir!");
            
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                 
                

                 if(!string.IsNullOrEmpty(dataGridView1.SelectedRows[0].ToString())) 
                 {
                    var confirmResult = MessageBox.Show("Kişi bilgilerini güncellemek istediğinizden emin misiniz?", "Güncellemeyi Onayla", MessageBoxButtons.YesNo);

                    if (confirmResult == (DialogResult.Yes))
                    {

                        //to update some important informations about the user with selecting from the datagridview and clicking the 'güncelle' button
                        int selectedSSN = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                        string query = "update c set c.Position=@position,c.ParentSSNo=@newParent,c.DeptNo=@newDept from CompanyUser c where c.SSNo="+selectedSSN;
                       
                        command = new SqlCommand(query, connection);
                        connection.Open();
                        command.Parameters.AddWithValue("@position", dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
                        command.Parameters.AddWithValue("@newParent", int.Parse(dataGridView1.SelectedRows[0].Cells[4].Value.ToString()));
                        command.Parameters.AddWithValue("@newDept", int.Parse(dataGridView1.SelectedRows[0].Cells[6].Value.ToString()));
                        command.ExecuteNonQuery();
                        connection.Close();
                        
                        MessageBox.Show("Kişi bilgileri güncellenmiştir");
                        
                        this.Worker_Record_Load(sender, e);
                    }
                }
                else 
                {
                    MessageBox.Show("Lütfen alanları eksiksiz doldurunuz!");
                }

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
          
            }
        }
    }





    }

