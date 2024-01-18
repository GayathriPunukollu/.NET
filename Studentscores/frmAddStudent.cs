using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentScors
{
    public partial class frmAddStudent : Form
    {
        public Student newStudent = new Student();
        public frmAddStudent()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (Validator.IsPresent(txtName))
            {
                newStudent.name = txtName.Text;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnAddScore_Click(object sender, EventArgs e)
        {
            if (Validator.IsPresent(txtScore) && Validator.IsDecimal(txtScore))
            {
                newStudent.scores.Add(Convert.ToDecimal(txtScore.Text));
                DisplayScores();
                txtScore.Text = "";
                txtScore.Focus();
            }
        }
        private void DisplayScores()
        {
            txtScores.Text = "";
            string scores = "";
            foreach (decimal num in newStudent.scores)
            {
                scores += num.ToString() + " ";
            }
            txtScores.Text = scores;
        }

        private void btnClearScores_Click(object sender, EventArgs e)
        {
            newStudent.scores.Clear();
            DisplayScores();
            txtScore.Focus();
        }
    }
}
