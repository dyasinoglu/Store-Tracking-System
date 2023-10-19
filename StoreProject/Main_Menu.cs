using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace StoreProject
{
    public partial class Main_Menu : Form
    {
        public Main_Menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //First button opens the Worker Records form
        {
            Worker_Record wr = new Worker_Record();
            wr.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e) //Second button opens the Product Informations form
        
            {
            Product_Information product = new Product_Information();
            product.Show();
            this.Hide();

        }

        private void button3_Click(object sender, EventArgs e) //Third button opens the Product Records form
        
            {
            Product_Record prec= new Product_Record();
            prec.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e) //The close/return button opens the Log In page
        {
            LogIn_Page lgp = new LogIn_Page();
            lgp.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e) //The button which brings the all informations about users and products they have
        {
            All_Records rec = new All_Records();
            rec.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)//Opens the history record page 
        {
            HistoryRecord hr=new HistoryRecord();   
            hr.Show();
            this.Hide();    
        }
    }
}
