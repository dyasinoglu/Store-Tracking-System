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

namespace StoreProject
{
    public partial class Requests : Form
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter da;
        public Requests()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
                {
                    
                    if (dataGridView1.SelectedRows[0].Cells[7].Value.ToString().Equals("Talep"))
                    {
                        var confirmRes = MessageBox.Show("Talep işlemini onaylamak istediğinizden emin misiniz?", "Talebi Onayla", MessageBoxButtons.YesNo);
                        if (confirmRes == (DialogResult.Yes))
                        {

                            //After confirming the request pdate the isConfirmed part as 'Onay'
                            string excquery = "update NotificTable set isConfirmed = 'Onaylandı',actionOn = @date from NotificTable where notfID= (select max(notfID) from NotificTable where receiverSSN =" + int.Parse(dataGridView1.SelectedRows[0].Cells[1].Value.ToString()) + ")";


                            command = new SqlCommand(excquery, connection);
                            connection.Open();





                            command.Parameters.AddWithValue("@date", DateTime.Now);



                            command.ExecuteNonQuery();

                            MessageBox.Show("Talep İşlemi Onaylandı");



                            connection.Close();
                            this.Requests_Load (sender, e);
                        }
                   }
                  ////  else if (dataGridView1.SelectedRows[0].Cells[7].Value.Equals("İade"))
                  ////  {
                  //      var confirmRes = MessageBox.Show("İade işlemini onaylamak istediğinizden emin misiniz?", "İadeyi Onayla", MessageBoxButtons.YesNo);
                  //      if (confirmRes == (DialogResult.Yes))
                  //      {

                  //          //After confirming the request pdate the isConfirmed part as 'Onay'
                  //          string excquery = "update ProductAction set ProductAction.ProductID = null ,ProductAction.ConfirmOn = @dateOn from ProductAction where ProductAction.SSNo=" + int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

                  //          command = new SqlCommand(excquery, connection);
                  //          connection.Open();





                  //          command.Parameters.AddWithValue("@dateOn", DateTime.Now);

                  //          string cancquery = "update ProductRequest set ProductRequest.isConfirmed = 'Onaylandı',ProductRequest.reqOn = @date from ProductRequest where reqID= (select max(reqID) from ProductRequest where recSSN =" + int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()) + ")";


                  //          command = new SqlCommand(cancquery, connection);
                  //          command.Parameters.AddWithValue("@date", DateTime.Now);

                  //          command.ExecuteNonQuery();

                  //          MessageBox.Show("İade İşlemi Onaylandı");








                  //          connection.Close();
                  //          this.Requests_Load(sender, e);

                  //      }


                   
                }
                else
                {
                    MessageBox.Show("Onay için işlem bulunamadı!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Requests_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection("server=.; Initial Catalog=STORE2;Integrated Security=SSPI");
            command = new SqlCommand();
            connection.Open();
            da = new SqlDataAdapter("\t  select  n.notfID,n.receiverSSN Sicil_No,c.Name+' '+c.Surname,c.Position,d.DepartmentName,c.ParentSSNo,n.ProductID, n.ProductName,n.actName Islem, n.actionOn,n.isConfirmed\r\n  from NotificTable n inner join CompanyUser c on c.SSNo=n.receiverSSN                   \r\n  inner join Department d on d.DepartmentNo=c.DeptNo ", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows[0].Cells[0].Value != null)
            {


                var confirmRes = MessageBox.Show("İşlemi iptal etmek istediğinizden emin misiniz?", "İptali Onayla", MessageBoxButtons.YesNo);
                if (confirmRes == (DialogResult.Yes))
                {

                    //to cancel the action only delete the notification from the screen
                    string cancelQuery = "update NotificTable set isConfirmed = 'İptal',actionOn = @date from NotificTable where notfID= (select max(notfID) from ProductRequest where receiverSSN =" + int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()) + ")";

                    command = new SqlCommand(cancelQuery, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@date", DateTime.Now);
                    command.ExecuteNonQuery();

                    MessageBox.Show("İşlem iptal edildi");


                    connection.Close();
                    this.Requests_Load(sender, e);
                }
                this.Show();
            }
            else
            {
                MessageBox.Show("İptal için işlem bulunmamaktadır!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Product_Record pr = new Product_Record();
            pr.Show();
        }
    }
}
