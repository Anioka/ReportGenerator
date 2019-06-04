using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Data;

namespace Report_Generator.Model
{
    class UsersDBClass
    {
        string conection;

        public UsersDBClass()
        {
            conection = ConfigurationManager.ConnectionStrings["databaseConnectionString"].ConnectionString;
        }

        //select fun
        public DataSet ConnectToUsersTable()
        {
            /*User user;
            List<User> users = new List<User>();*/
            //string query = "SELECT * FROM users WHERE users.user_name NOT LIKE \"admin\" ORDER BY RAND()";

            string query = "SELECT * FROM users WHERE users.user_name NOT LIKE \"admin\" && rdmdb.users.privilege_id <> 3 ORDER BY RAND()";

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

        public List<User> ConnectToUserNamesTable()
        {
            User user;
            List<User> users = new List<User>();
            //string query = "SELECT * FROM users WHERE users.user_name NOT LIKE \"admin\" ORDER BY RAND()";

            string query = "SELECT users.user_name FROM users WHERE users.user_name NOT LIKE \"admin\" && users.privilege_id <> 3 ORDER BY RAND()";

            //DataSet ds = new DataSet();
            try
            {
                 using (MySqlConnection conn = new MySqlConnection(conection))
                {
                    conn.Open();
                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                user = new User()
                                {
                                    UserName = reader.GetString("user_name")
                                };
                                users.Add(user);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Could not retrieve data: no valid connection hosts");
                //MessageBox.Show(ex.ToString());
                Debug.Write(ex.ToString());
            }
            return users;
        }
    }
}
