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
    public partial class frmUpdateStudent : Form
    {
        public Student student = new Student();
        public frmUpdateStudent()
        {
            InitializeComponent();
        }
        private void frmUpdateStudent_Load(object sender, EventArgs e)
        {
            txtName.Text = student.name;
            DisplayScores();
        }
        private void DisplayScores()
        {
            lstScores.Items.Clear();
            foreach (var s in student.scores)
            {
                lstScores.Items.Add(s);
            }
            ClearControls();
        }
        private void ClearControls()
        {
            btnRemove.Enabled = false;
            btnUpdate.Enabled = false;
        }
        private void EnableControls()
        {
            btnUpdate.Enabled = true;
            btnRemove.Enabled = true;
        }
        private void lstScores_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableControls();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddUpdateScore frmAddUpdateScore = new frmAddUpdateScore();
            frmAddUpdateScore.isAddScore = true;
            DialogResult result = frmAddUpdateScore.ShowDialog();
            if (result == DialogResult.OK)
            {
                student.scores.Add(frmAddUpdateScore.score);
                DisplayScores();
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int currentScoreIndex = lstScores.SelectedIndex;
            decimal currentScore = (decimal) lstScores.SelectedItem;
            frmAddUpdateScore frmAddUpdateScore = new frmAddUpdateScore();
            frmAddUpdateScore.isAddScore = false;
            frmAddUpdateScore.score = currentScore;
            DialogResult result = frmAddUpdateScore.ShowDialog();
            if (result == DialogResult.OK)
            {
                student.scores.RemoveAt(currentScoreIndex);
                student.scores.Insert(currentScoreIndex, frmAddUpdateScore.score);
                DisplayScores();
            }
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure that you want to delete the selected score?", "Confirm", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                student.scores.Remove((decimal)lstScores.SelectedItem);
                DisplayScores();
            }
        }

        private void btnClearScores_Click(object sender, EventArgs e)
        {
            student.scores.Clear();
            DisplayScores();
        }
    }
}
