using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Net;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Mvc4Crud.Models
{


    public class CommonVariables
    {
       
        public static String ConnectionString
        {
            get { return WebConfigurationManager.ConnectionStrings["tdlConnection"].ToString(); }
        }
    }
   
    
    [Table("Users")]
    public class Users
    {
             
        public UserLogin userLogin { get; set; }
        [Key]
        public int user_id { get; set; }

        //public IEnumerable<string> userData { get; set; }

        [Required(ErrorMessage = "Please Enter First Name")]
        public string first_name { get; set; }
        
        [Required(ErrorMessage = "Please Enter Last Name")]
        public string last_name { get; set; }
        
        [Required(ErrorMessage = "Please Enter Email Address")]   
        [DataType(System.ComponentModel.DataAnnotations.DataType.EmailAddress)]
        public string email { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        [StringLength(8,ErrorMessage = "Password Should be at least 8 charector long")]
        public string password { get; set; }

       // [DataType(DataType.Password)]
       // [Display(Name = "Confirm password")]
       // [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
       // public string ConfirmPassword { get; set; }

        public int AddUser(Users cmt)
        {
            //Console.WriteLine(WebConfigurationManager.ConnectionStrings["tdlConnection"].ToString());

            SqlConnection con = new SqlConnection(CommonVariables.ConnectionString);

            //Console.WriteLine(WebConfigurationManager.ConnectionStrings.ToString());

            string query = "INSERT INTO Users (first_name,last_name,email,password,created) " +
                                   " VALUES " +
                                   " ('" + cmt.first_name + "','" + cmt.last_name + " ','" + cmt.email + "','" + cmt.password + "' , CURRENT_TIMESTAMP );";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
       
        public bool IsValidEmailAddress(string email)
        {
            try
            {
                var emailChecked = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsUsedEmailAddress(string email)
        {
            
            string connStr = CommonVariables.ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Users WHERE email = @email";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@email", email);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();

                    if(reader.HasRows){

                        return true;

                    }
                    else
                    {

                        return false;

                    }

                }
            }

        }

      /*  public SqlDataReader getUser(int user_id)
        {
            Users userData = new Users();
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["tdlConnection"].ToString());
            //Console.WriteLine(WebConfigurationManager.ConnectionStrings.ToString());
            string query = "SELECT * FROM Users WHERE user_id = '" + user_id + "';";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataTable userData = new DataTable();
            //da.Fill(userData);
            using (SqlDataReader reader = cmd.ExecuteReader()){
                if (reader.Read())
                {
                    Console.WriteLine(String.Format("{0}", reader["id"]));
                }
            }
            var userData = cmd.ExecuteNonQuery();
            con.Close();
            return userData;
        }*/
        public string encryption(String password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            System.Text.UTF8Encoding encode = new System.Text.UTF8Encoding();
            //encrypt the given password string into Encrypted data  
            encrypt = md5.ComputeHash(encode.GetBytes(password));
            System.Text.StringBuilder encryptdata = new System.Text.StringBuilder();
            //Create a new string by using the encrypted data  
            for (int i = 0; i < encrypt.Length; i++)
            {
                encryptdata.Append(encrypt[i].ToString());
            }
            return encryptdata.ToString();
        }


        /*
        internal string encryption(string p)
        {
            throw new NotImplementedException();
        }*/
    }

    [Table("Users")]
    public class UpdateUser
    {
       
        public int user_id { get; set; }
        [Required(ErrorMessage = "Please Enter First Name")]
        public string first_name { get; set; }
       [Required(ErrorMessage = "Please Enter Last Name")]
        public string last_name { get; set; }
        [Required(ErrorMessage = "Please Enter Email Address")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.EmailAddress)]
        public string email { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        [StringLength(8, ErrorMessage = "Password Should be at least 8 charector long")]
        public string password { get; set; }
    }
  


   
    public class UserLogin
    {
           
       
        [Display(Name = "Email")]

        [Required(ErrorMessage = "Please Enter Email Address")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.EmailAddress)]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        public bool IsValidEmailAddress(string email)
        {
            try
            {
                var emailChecked = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public string encryption(String password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            System.Text.UTF8Encoding encode = new System.Text.UTF8Encoding();
            //encrypt the given password string into Encrypted data  
            encrypt = md5.ComputeHash(encode.GetBytes(password));
            System.Text.StringBuilder encryptdata = new System.Text.StringBuilder();
            //Create a new string by using the encrypted data  
            for (int i = 0; i < encrypt.Length; i++)
            {
                encryptdata.Append(encrypt[i].ToString());
            }
            return encryptdata.ToString();
        }

        byte[] GenerateSalt(int length)
        {
            var bytes = new byte[length];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }

            return bytes;
        }

        byte[] GenerateHash(byte[] password, byte[] salt, int iterations, int length)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                return deriveBytes.GetBytes(length);
            }
        }


        public DataTable isAuth(UserLogin check)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["tdlConnection"].ToString());

            //Console.WriteLine(WebConfigurationManager.ConnectionStrings.ToString());
            string password = this.encryption(check.password);

            string query = "SELECT * FROM Users WHERE email = '" + check.email + "' AND password = '" + password + "';";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable userData = new DataTable();
            da.Fill(userData);
            con.Close();
            return userData;
        }

        //public int GetUserId(UserLogin get)
        //{
        //    SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["tdlConnection"].ToString());

        //    //Console.WriteLine(WebConfigurationManager.ConnectionStrings.ToString());
        //    string password = this.encryption(check.password);

        //    string query = "SELECT * FROM Users WHERE email = '" + check.email + "' AND password = '" + password + "';";
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    con.Open();
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataTable userData = new DataTable();
        //    da.Fill(userData);
        //    con.Close();
        //    return user_id;
        //}
     

    }
   
      
    
}