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
using System.Windows.Forms.VisualStyles;

namespace StoreProject
{
    public partial class LogIn_Page : Form
    {

        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter da;
        public LogIn_Page()
        {
            InitializeComponent();

            
        }
        
        public void LogIn_Page_Load(object sender, EventArgs e)
        {

            connection = new SqlConnection("server=.; Initial Catalog=STORE2;Integrated Security=SSPI");
            command = new SqlCommand();
            command.Connection = connection;
   
            
         
          

        }
        public List<Int32> userAdditiontoList(String query) //The list of user SSNs to check if the entered SSN is in the database or not 
        {

            connection.Open();
            List<Int32> userList = new List<Int32>();
          

            command = new SqlCommand(query,connection);  

            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                userList.Add(Convert.ToInt32(dataReader[0].ToString()));
            }
           connection.Close(); 
            return userList;
        }
        
        public void button3_Click(object sender, EventArgs e)
        {
          
            try
            {
                Boolean userFlag = false;
             
                
                
                String workerQuery = "select c.SSNo from CompanyUser c where c.Position='Worker'";//For all SSN of users whose position is 'Worker'
                String allQuery = "Select c.SSNo from CompanyUser c";//For all user's SSNs 
           
                List<Int32> userNoList = new List<Int32>();
                List<Int32> workerNoList = new List<Int32>();
                
                userNoList = userAdditiontoList(allQuery);
                workerNoList=userAdditiontoList(workerQuery);



                NotificationPage n = new NotificationPage();
                n.labssn = int.Parse(textUser.Text);

                textPassw.UseSystemPasswordChar = true;
       

                

                if(string.IsNullOrWhiteSpace(textUser.Text) || string.IsNullOrWhiteSpace(textPassw.Text))//Checking the textboxes if they ar eempty or not
                {
                    MessageBox.Show("Lütfen kullanıcı ve parola bilgilerini eksiksiz giriniz!");

                }
                else 
                {
                    if (userNoList.Contains(int.Parse(textUser.Text))) //Check for SSN entered by the user is valid or not
                    {
                        userFlag = true;
                    }
                  
                    if (int.Parse(textUser.Text) == 1 && textPassw.Text.Equals("admin"))//Conditions for admin logging in
                    {

                        Main_Menu mn = new Main_Menu(); //If admin logs in then the Main Menu page is opened
                        mn.Show();
                        this.Hide();
                    }

                    else if (userFlag && workerNoList.Contains(int.Parse(textUser.Text)))//Conditions for Worker's logging in 
                        
                    {

                        WorkerPage wp = new WorkerPage(); //Worker Page is opened which is different from the other user's page
                        wp.kulSSN = int.Parse(textUser.Text);
                        wp.Show();
                        this.Hide();
                    }
                    else if (userFlag && !workerNoList.Contains(int.Parse(textUser.Text)))
                    {
                        User_Page up = new User_Page(); //User's Page which has ability to confirm some actions in the form
                        up.kullanıcıSSN = int.Parse(textUser.Text);
                          
                        up.Show();
                        this.Hide();
                    }
                    else if (int.Parse(textUser.Text) != 1 && !userFlag) //When the entered SSN is not in the database/list error message comes up
                    {
                        MessageBox.Show("Girdiğiniz sicil no ile bir kullanıcı bulunamadı!");
                    }
                    
                }

               
                connection.Close();
            

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
            
            
        
        }

      
    }
}
