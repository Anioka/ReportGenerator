using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Report_Generator.Model;
using System.Windows.Forms;
using System.Diagnostics;

namespace Report_Generator
{
    public partial class Form1 : Form
    {
        DataSet ds;
        ProjectsDBClass pdbc;
        UsersDBClass udbc;

        bool clickedNew;
        bool clickedOld;

        public Form1()
        {
            InitializeComponent();
            pdbc = new ProjectsDBClass();
            udbc = new UsersDBClass();
            ds = new DataSet();

            clickedNew = false;
            clickedOld = true;
            List<User> cbUsers = new List<User>();
            cbUsers = udbc.ConnectToUserNamesTable();
            foreach (User u in cbUsers)
                //MessageBox.Show(d.DeviceName);
                cbUser.Items.Add(u.UserName);
        }

        private void butExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void llNewProj_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                ds = pdbc.SelectNewProjects();
                dgvEdits.DataSource = ds.Tables[0];
                dgvEdits.Update();
                dgvEdits.Refresh();

                clickedNew = true;
                clickedOld = false;
            }
            catch { }
        }

        private void llOldproj_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                ds = pdbc.SelectOldProjects();
                dgvEdits.DataSource = ds.Tables[0];
                dgvEdits.Update();
                dgvEdits.Refresh();

                clickedOld = true;
                clickedNew = false;
            }
            catch { }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])
            {
                try
                {
                    ds = pdbc.SelectNewProjects();
                    dgvProjects.DataSource = ds.Tables[0];
                    dgvProjects.Update();
                    dgvProjects.Refresh();
                }
                catch { }
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])
            {
                try
                {
                    ds = pdbc.ConnectToTaskTable();
                    dgvPeople.DataSource = ds.Tables[0];
                    dgvPeople.Update();
                    dgvPeople.Refresh();
                }
                catch { }
            }
            /*else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage3"])
            {
                try
                {
                    ds = pdbc.SelectOldProjects();
                    dgvEdits.DataSource = ds.Tables[0];
                    dgvEdits.Update();
                    dgvEdits.Refresh();
                }
                catch { }
            }*/
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.dgvProjects.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPeople.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEdits.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            load();
        }

        private void load()
        {
            try
            {
                ds = pdbc.SelectNewProjects();
                dgvProjects.DataSource = ds.Tables[0];
                dgvProjects.Update();
                dgvProjects.Refresh();
            }
            catch { }
        }

        

        private void butProj_Click(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show(monthCalendar3.SelectionRange.Start.Date.ToString("yyyy-MM-dd"));
                //MessageBox.Show(monthCalendar4.SelectionRange.Start.Date.ToString());
                ds = pdbc.SelectNewProjectsForSelectedDate(monthCalendar3.SelectionRange.Start.Date.ToString("yyyy-MM-dd"), monthCalendar4.SelectionRange.Start.Date.ToString("yyyy-MM-dd"));
                dgvProjects.DataSource = ds.Tables[0];
                dgvProjects.Update();
                dgvProjects.Refresh();
            }
            catch { }
        }

        private void butPep_Click(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show(monthCalendar3.SelectionRange.Start.Date.ToString("yyyy-MM-dd"));
                //MessageBox.Show(monthCalendar4.SelectionRange.Start.Date.ToString());
                ds = pdbc.SelectPeopleForSelectedDate(monthCalendar1.SelectionRange.Start.Date.ToString("yyyy-MM-dd"), monthCalendar2.SelectionRange.Start.Date.ToString("yyyy-MM-dd"), cbUser.SelectedItem.ToString());
                dgvPeople.DataSource = ds.Tables[0];
                dgvPeople.Update();
                dgvPeople.Refresh();
            }
            catch { }
        }

        private void dgvEdits_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if(clickedOld)
                {
                    textBox1.Text = dgvEdits.Rows[e.RowIndex].Cells[0].Value.ToString();
                    textBox2.Text = dgvEdits.Rows[e.RowIndex].Cells[1].Value.ToString();
                    textBox3.Text = dgvEdits.Rows[e.RowIndex].Cells[2].Value.ToString();
                    textBox4.Text = dgvEdits.Rows[e.RowIndex].Cells[3].Value.ToString();
                }
                if (clickedNew)
                {

                    lbTasks.Items.Clear();

                    List<NewProjects> tasks1 = new List<NewProjects>();

                    /*if (dgvEdits.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                    {*/
                    tasks1 = pdbc.ConnectToTaskTable(dgvEdits.Rows[e.RowIndex].Cells[0].Value.ToString());
                    // }

                    if (tasks1.Count > 0)
                    {
                        foreach (NewProjects t in tasks1)
                            lbTasks.Items.Add(t.ProjectsName);
                    }
                }
            }
            catch { }
        }

        private void PDFbut_Click(object sender, EventArgs e)
        {
            PDFCreationClass.CreatePDF(ds);
        }

    }
}
