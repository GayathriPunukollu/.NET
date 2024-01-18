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
    public partial class frmStudentScoresMain : Form
    {
        List<Student> students = new List<Student>();
        public frmStudentScoresMain()
        {
            InitializeComponent();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmStudentScoresMain_Load(object sender, EventArgs e)
        {
            students.Add (new Student
            {
                name = "Bipal Goyal",
                scores = new List<decimal> {90, 95, 100}
            });
            students.Add(new Student
            {
                name = "Tyusha Sarawagi",
                scores = new List<decimal> {80, 90, 100 }
            });
            students.Add(new Student
            {
                name = "Neelah Suwal",
                scores = new List<decimal> { 100, 98, 69 }
            });
            PopulateListWithStudents();
        }
        private void ClearControls()
        {
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
        }
        private void EnableControls()
        {
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }
        private void PopulateListWithStudents()
        {
            lstStudents.Items.Clear();
            foreach (Student S in students)
            {
                lstStudents.Items.Add(S);
            }
            ClearControls();
        }
        private void lstStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            Student selectedStudent = (Student) lstStudents.SelectedItem;
            DisplayScoresInformation(selectedStudent);
        }

        private void DisplayScoresInformation(Student student)
        {
            if (student.scores.Count > 0)
            {
                txtScoresCount.Text = student.scores.Count.ToString();
                txtAverage.Text = student.scores.Average().ToString();
                txtScoresTotal.Text = student.scores.Sum().ToString();
            }
            else
            {
                txtAverage.Text = "";
                txtScoresCount.Text = "";
                txtScoresTotal.Text = "";
            }
            EnableControls();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddStudent frmAddStudent = new frmAddStudent();
            DialogResult result = frmAddStudent.ShowDialog();
            if (result == DialogResult.OK)
            {
                bool doesStudentExist = CompareStudentWithListofStudents(frmAddStudent.newStudent); 
                if (!doesStudentExist)
                {
                    students.Add(frmAddStudent.newStudent);
                    PopulateListWithStudents();
                }
                else
                {
                    MessageBox.Show("This student's information has already been added!", "Error!");
                }
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure that you want to delete the selected student's information?", "Confirm", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                students.Remove((Student)lstStudents.SelectedItem);
                PopulateListWithStudents();
                ClearSelectedStudentValues();
            }
        }
        private void ClearSelectedStudentValues()
        {
            txtAverage.Text = "";
            txtScoresCount.Text = "";
            txtScoresTotal.Text = "";
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Student currentStudent = (Student) lstStudents.SelectedItem;
            frmUpdateStudent frmUpdateStudent = new frmUpdateStudent();
            frmUpdateStudent.student = (Student) currentStudent.Clone(); 
            DialogResult result = frmUpdateStudent.ShowDialog();
            if (result == DialogResult.OK)
            {
                int index = students.IndexOf(currentStudent);
                students.Remove(currentStudent);
                students.Insert(index, frmUpdateStudent.student);
                DisplayScoresInformation(frmUpdateStudent.student);
                PopulateListWithStudents();
            }
        }
        private bool CompareStudentWithListofStudents(Student student)
        {
            foreach (Student S in students)
            {
                if (S.name.Equals(student.name))
                    return true;
            }
            return false;
        }
    }
}
