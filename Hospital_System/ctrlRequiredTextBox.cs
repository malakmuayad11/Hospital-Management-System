using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Hospital_System
{
    public partial class ctrlRequiredTextBox : UserControl
    {
        public event EventHandler TextChanged;

        public char PasswordChar
        {
            get => textBox1.PasswordChar;
            set => textBox1.PasswordChar = value;
        }

        public string Text
        {
            get => textBox1.Text;
            set => textBox1.Text = value;
        }

        public bool Multiline
        {
            get => textBox1.Multiline;
            set => textBox1.Multiline = value;
        }
        public ctrlRequiredTextBox()
        {
            InitializeComponent();
            textBox1.TextChanged += (s, e) => TextChanged?.Invoke(this, e);
        }

        private void textBox1_Validating(object sender, CancelEventArgs e) =>
            clsValidation.ValidateRequiredTextBox(this.textBox1, this.errorProvider1, e);
    }
}
