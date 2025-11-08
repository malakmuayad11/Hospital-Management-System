using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital_System.Main
{
    public partial class frmManageScreenNoAddButton : frmManageScreen
    {
        public frmManageScreenNoAddButton()
        {
            InitializeComponent();
            base.BTN.Visible = false;
        }
    }
}
