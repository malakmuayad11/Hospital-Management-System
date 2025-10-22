using System;
using System.Drawing;
using System.Windows.Forms;

namespace Hospital_System
{
    public partial class frmManageScreen : Form
    {
        public frmManageScreen() => InitializeComponent();

        protected override void OnPaint(PaintEventArgs pe) => base.OnPaint(pe);

        public DataGridView DGV
        {
            get => dgvTodaysAppointments;
            set => dgvTodaysAppointments = value;
        }

        public ContextMenuStrip CMS
        {
            get => cms;
            set => cms = value;
        }

        public PictureBox Pb
        {
            get => pictureBox1;
            set => pictureBox1 = value;
        }

        public Button BTN
        {
            get => btn;
            set => btn = value;
        }

        public Image Image
        {
            get => pictureBox1.Image;
            set => pictureBox1.Image = value;
        }

        public string RecordsNumber
        {
            get => lblRecords.Text;
            set => lblRecords.Text = value;
        }
    }
}
