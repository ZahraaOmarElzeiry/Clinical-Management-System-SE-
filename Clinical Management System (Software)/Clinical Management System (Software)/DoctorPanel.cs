using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clinical_Management_System__Software_
{
    public partial class DoctorPanel : Form
    {

        int account_id;
        public DoctorPanel(int id)
        {
            InitializeComponent();
            account_id = id;
        }
    }
}
