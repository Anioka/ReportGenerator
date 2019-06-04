using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Report_Generator.Model;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data;

namespace Report_Generator
{
    class ProjectsDBClass 
    {
        string conection;
        public ProjectsDBClass()
        {
            conection = ConfigurationManager.ConnectionStrings["databaseConnectionString"].ConnectionString;
        }
        
        //select fun
        public DataSet SelectOldProjects()
        {
            /*OldProjects project;
            List<OldProjects> projects = new List<OldProjects>();*/
            //string query = "SELECT * FROM users WHERE users.user_name NOT LIKE \"admin\" ORDER BY RAND()";

            //string query = "SELECT naziv, datum, ime, opis, timlider, statuschange, code, deadline FROM workingprojects";

            string query = "SELECT naziv, ime, timlider, code FROM workingprojects";

            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(conection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                    {
                        conn.Open();
                        adapter.Fill(ds, "model");
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Could not retrieve data: no valid connection hosts");
                //MessageBox.Show(ex.ToString());
                Debug.Write(ex.ToString());
            }
            return ds;
        }

        //select fun
        public DataSet SelectNewProjects()
        {
            /*NewProjects project;
            List<NewProjects> projects = new List<NewProjects>();*/
            //string query = "SELECT * FROM users WHERE users.user_name NOT LIKE \"admin\" ORDER BY RAND()";

            string query = "SELECT task_name, start_date, finished_date, deadline, project_time_spent FROM projects";

            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(conection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                    {
                        conn.Open();
                        adapter.Fill(ds, "model");
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Could not retrieve data: no valid connection hosts");
                //MessageBox.Show(ex.ToString());
                Debug.Write(ex.ToString());
            }
            return ds;
        }

        //select fun
        public DataSet SelectNewProjectsForSelectedDate(string date1, string date2)
        {
            /*NewProjects project;
            List<NewProjects> projects = new List<NewProjects>();*/
            //string query = "SELECT * FROM users WHERE users.user_name NOT LIKE \"admin\" ORDER BY RAND()";

            string query = "SELECT task_name, start_date, finished_date, deadline, project_time_spent FROM projects where start_date between ?date1 and ?date2 order by start_date asc";

            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(conection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("?date1", date1);
                        adapter.SelectCommand.Parameters.AddWithValue("?date2", date2);

                        conn.Open();
                        adapter.Fill(ds, "model");
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Could not retrieve data: no valid connection hosts");
                //MessageBox.Show(ex.ToString());
                Debug.Write(ex.ToString());
            }
            return ds;
        }

        //tasks for the selected person
        public DataSet ConnectToTaskTable()
        {
            //string query = "select tasks.description, users.user_name, tasks.date_started, tasks.date_finished, tasks.end_date, tasks.task_time_spent from tasks Left Join tasks_users on tasks.id = tasks_users.tasks_id Left Join users on tasks_users.users_id = users.id";

            string query = "select users.user_name, projects.task_name, tasks.description /*AS TASK*/, tasks.date_started /*AS BEGINDATE*/, tasks.end_date /*AS ENDDATE*/, tasks.date_finished, tasks.task_time_spent /*rmdb.tasks.finished AS FINISHED*/ from tasks Left Join tasks_users on tasks.id = tasks_users.tasks_id Left Join users on tasks_users.users_id = users.id Left Join projects on tasks.tasks_id = projects.id order by users.user_name";
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(conection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                    {
                        conn.Open();
                        adapter.Fill(ds, "model");
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Could not retrieve data: no valid connection hosts");
                //MessageBox.Show(ex.ToString());
                Debug.Write(ex.ToString());
            }
            return ds;
        }


        //tasks for the selected person between dates
        public DataSet SelectPeopleForSelectedDate(string date1, string date2, string username)
        {
            string query = "select users.user_name, projects.task_name, tasks.description, tasks.date_started, tasks.end_date, tasks.date_finished, tasks.task_time_spent from tasks Left Join tasks_users on tasks.id = tasks_users.tasks_id Left Join users on tasks_users.users_id = users.id Left Join projects on tasks.tasks_id = projects.id where date_started between ?date1 and ?date2 && users.user_name like ?uname order by users.user_name";

            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(conection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("?date1", date1);
                        adapter.SelectCommand.Parameters.AddWithValue("?date2", date2);
                        adapter.SelectCommand.Parameters.AddWithValue("?uname", username);

                        conn.Open();
                        adapter.Fill(ds, "model");
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Could not retrieve data: no valid connection hosts");
                //MessageBox.Show(ex.ToString());
                Debug.Write(ex.ToString());
            }
            return ds;
        }


        public List<NewProjects> ConnectToTaskTable(string param)
        {
            NewProjects task;
            List<NewProjects> tasks = new List<NewProjects>();
            string query = "Select tasks.description from tasks, projects where tasks.tasks_id = projects.id && projects.task_name like ?param";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(conection))
                {
                    conn.Open();
                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("?param", param); //TODO: sredi parametre da moze da se upise u listu
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                task = new NewProjects()
                                {
                                    ProjectsName = reader.GetString("description")
                                };
                                tasks.Add(task);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
            return tasks;
        }

        //tasks for the selected person
        //public DataSet ConnectToUnifishedTaskTableFromDate(DateTime date1, DateTime date2)
        //{
        //    string query = "select tasks.description, users.user_name from tasks Left Join tasks_users on tasks.id = tasks_users.tasks_id Left Join users on tasks_users.users_id = users.id WHERE start_date like ?date1 && deadline like ?date2";

        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        using (MySqlConnection conn = new MySqlConnection(conection))
        //        {
        //            using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
        //            {
        //                adapter.SelectCommand.Parameters.AddWithValue("?date1", date1);
        //                adapter.SelectCommand.Parameters.AddWithValue("?date2", date2);

        //                conn.Open();
        //                adapter.Fill(ds, "model");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show("Could not retrieve data: no valid connection hosts");
        //        //MessageBox.Show(ex.ToString());
        //        Debug.Write(ex.ToString());
        //    }
        //    return ds;
        //}
    }
}
