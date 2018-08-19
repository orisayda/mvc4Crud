using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc4Crud.Models;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace Mvc4Crud.ViewModel.User
{
    public class UserViewModel
    {
        //dataset ,datatable, list, collection and Array
        public List<Users> GetAllUsers()
        {
            List<Users> users = new List<Users>();
            string connStr = WebConfigurationManager.ConnectionStrings["tdlConnection"].ToString();
            string query = "SELECT * FROM Users;";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Users user = new Users();
                        user.user_id = Convert.ToInt16(reader["user_id"]);
                        user.first_name = Convert.ToString(reader["first_name"]);
                        user.last_name = Convert.ToString(reader["last_name"]);
                        user.email = Convert.ToString(reader["email"]);
                        user.password = Convert.ToString(reader["password"]);

                        users.Add(user);
                    }
                }
            }
            return users;
        }

        public UpdateUser GetUser(int Id)
        {
            UpdateUser userData = new UpdateUser();

            string connStr = WebConfigurationManager.ConnectionStrings["tdlConnection"].ToString();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Users WHERE user_id = @Id";
                using(SqlCommand cmd = new SqlCommand(query,conn)){
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Id", Id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    userData.user_id = Convert.ToInt16(reader["user_id"]);
                    userData.first_name = Convert.ToString(reader["first_name"]);
                    userData.last_name = Convert.ToString(reader["last_name"]);
                    userData.email = Convert.ToString(reader["email"]);
                    userData.password = Convert.ToString(reader["password"]);
                }
            }



            return userData;
        }

        public void UpdateUser(Users user)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["tdlConnection"].ToString();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // string query = "UPDATE Users SET first_name=@first_name,last_name = @last_name,email = @email,password = @password WHERE user_id = @user_id";
                 string query = "UPDATE Users SET first_name= '"+user.first_name+"',last_name = '"+user.last_name+"',email = '"+user.email+"',password = '"+user.password+"' WHERE user_id = "+user.user_id+";";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    //cmd.Parameters.AddWithValue("@user_id", user.user_id);
                    //cmd.Parameters.AddWithValue("@first_name", user.first_name);
                    //cmd.Parameters.AddWithValue("@last_name", user.last_name);
                    //cmd.Parameters.AddWithValue("@email", user.email);
                    //cmd.Parameters.AddWithValue("@password", user.password);
                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }


        public void Delete(int Id)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["tdlConnection"].ToString();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // string query = "UPDATE Users SET first_name=@first_name,last_name = @last_name,email = @email,password = @password WHERE user_id = @user_id";
                string query = "DELETE FROM Users WHERE user_id = "+Id+";";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    //cmd.Parameters.AddWithValue("@user_id", user.user_id);
                    //cmd.Parameters.AddWithValue("@first_name", user.first_name);
                    //cmd.Parameters.AddWithValue("@last_name", user.last_name);
                    //cmd.Parameters.AddWithValue("@email", user.email);
                    //cmd.Parameters.AddWithValue("@password", user.password);
                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }
    }
}