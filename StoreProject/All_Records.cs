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



namespace StoreProject
{
    public partial class All_Records : Form
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter da;
        public All_Records()
        {
            InitializeComponent();

            connection = new SqlConnection("server=.; Initial Catalog=STORE2;Integrated Security=SSPI");
            connection.Open();
            da = new SqlDataAdapter("select cu.SSNo Sicil_No,cu.Name+' '+cu.Surname Personel,cu.Position Pozisyon,c.Name+' '+c.Surname Üst_Kişi,d.DepartmentName Departman,p.ProductID Ürün_Kodu,p.ProductName Ürün,pa.RegisterOn Kayıt_Tarihi\r\nfrom CompanyUser c inner join CompanyUser cu on cu.ParentSSNo=c.SSNo\r\n                   inner join Department d on d.DepartmentNo=cu.DeptNo \r\n\t\t\t\t   inner join ProductAction pa on pa.SSNo=cu.SSNo\r\n\t\t\t\t   inner join Product p on p.ProductID=pa.ProductID\r\n\t\t\t\t  order by cu.SSNo ", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main_Menu mn = new Main_Menu();
            mn.Show();
            this.Hide();
        }
    }
}
