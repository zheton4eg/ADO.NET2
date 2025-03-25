using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Academy
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();

            Connector connector = new Connector
                (
                 ConfigurationManager.ConnectionStrings["PV_319_IMPORT"].ConnectionString
                );
            //dgv - DataGridView
            dgvStudents.DataSource = connector.Select("*","Students");
        }
    }
}
