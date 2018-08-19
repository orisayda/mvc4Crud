using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Collections;


namespace Mvc4Crud.Models
{
    [Table("Events")]
    public class Tasks
    {
    
        [Key]
        public int event_id { get; set; }
        public int user_id { get; set; }
        public string title { get; set; }
        //public string location { get; set; }
        public string description { get; set; }

        public string priority { get; set; }
        public Byte isTimeSet { get; set; }
        
        //public string start_date { get; set; }
        //public DateTime start_date { get; set; }
        public DateTime? start_date { get; set; }
        //public string end_date { get; set; }
        public DateTime? end_date { get; set; }


        //public DateTime end_date { get; set; }


       /* public string start_time?  { get;// { return this.start_time; } 
                                    set;// {  this.start_time = Convert.ToString(value);
                     //value;
                    //DateTime.MinValue.AddHours(Time).ToString(value);
                    //DateTime.MinValue.AddHours(Time).ToString(value);
                         
        
            } 
        }*/
        //public string end_time { get { return this.end_time; } set { this.end_time = value; } }

        
        //public DateTime? start_time { get { return this.start_time; } set { this.start_time = value; } }
        //public DateTime? end_time { get { return this.end_time; } set { this.end_time = value; } }
        
        //public DateTime? end_time { get; set; }


        public TimeSpan? start_time { get; set; }
        public TimeSpan? end_time { get; set; }
        

        //public DateTime? start_time { get; set; }
        //public DateTime? end_time { get; set; }
        //public string start_time { get {return this.start_time; } set { this.start_time = "not-null"; } }
        
        
        //public string end_time { get; set; }
        //public DateTime created_at { get; set; }
//        public bool status { get; set; }
        public string status { get; set; }

        //public string sortByCheck { get; set; }

        //public PagedList.IPagedList<Tasks> paged_task_list{ get; set; }
        //public List<Tasks> zipCodeList { get; set; }

      public int AddEvent(string user_id,Tasks evt)
        {
          
         
          SqlConnection con = new SqlConnection(CommonVariables.ConnectionString);          

          
          //string query = "INSERT INTO [tdl].[dbo].[Events] ([title],[location],[start_date],[end_date],[start_time],[end_time],[description],[created_at],[user_id],[updated_at]) " +
          string query = "INSERT INTO Events (title, start_date, end_date, start_time, end_time, description, user_id, status, priority,isTimeSet) " +
                                   " VALUES " +
" ('" + evt.title + "','" + evt.start_date.ToString() + "','" + evt.end_date.ToString() + "','" + evt.start_time + "','" + evt.end_time + "','" + evt.description + "','" + user_id + "' , '0' , '" + evt.priority + "' , '" + evt.isTimeSet + "');";
            
          
          SqlCommand cmd = new SqlCommand(query, con);
          con.Open();
          int i = cmd.ExecuteNonQuery();
          con.Close();
          return i;
        }

      public List<Tasks> GetAllEvents()
      {
          List<Tasks> events = new List<Tasks>();
          
          string query = "SELECT * FROM Events;";
          using (SqlConnection conn = new SqlConnection(CommonVariables.ConnectionString))
          {
              using (SqlCommand cmd = new SqlCommand(query, conn))
              {
                  //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                  conn.Open();
                  SqlDataReader reader = cmd.ExecuteReader();
                  while (reader.Read())
                  {
                      Tasks eve = new Tasks();

                      eve.event_id = Convert.ToInt16(reader["event_id"]);
                      eve.user_id = Convert.ToInt16(reader["user_id"]);
                      eve.title = Convert.ToString(reader["title"]);
                      //eve.location = Convert.ToString(reader["location"]);
                      eve.description = Convert.ToString(reader["description"]);

                     // eve.start_time =  Convert.ToString(reader.GetOrdinal("start_time"));
                     // eve.end_time =    Convert.ToString(reader.GetOrdinal("end_time"));

                      //var start_time = Convert.ToString(reader["start_time"]);
                      //DateTime date = DateTime.ParseExact(start_time, "H:mm:ss", null, System.Globalization.DateTimeStyles.None);

                      eve.start_time = (TimeSpan)reader["start_time"];
                      eve.end_time = (TimeSpan)reader["end_time"];
                      //var end_time = Convert.ToString(reader["end_time"]);
                     // eve.end_time = DateTime.ParseExact(end_time, "H:mm", null, System.Globalization.DateTimeStyles.None);                     
                      
                      //eve.end_time = Convert.ToDateTime(reader.GetOrdinal("end_time"));

                     // eve.start_date = Convert.ToString(reader.GetOrdinal("start_date"));
                      
                      eve.start_date = Convert.ToDateTime(reader["start_date"]);

                      eve.end_date = Convert.ToDateTime(reader["end_date"]);

                      eve.status = Convert.ToString(reader["status"]);    

                      //eve.start_time = reader.GetTimeSpan(reader.GetOrdinal("start_time"));
                      //eve.end_time = reader.GetTimeSpan(reader.GetOrdinal("end_time"));

                      //eve.start_date = reader.GetDateTime(reader.GetOrdinal("start_date"));
                      //eve.end_date = reader.GetDateTime(reader.GetOrdinal("end_date"));                
                      events.Add(eve);
                  }
              }
          }
          return events;
      }

      public List<Tasks> GetAllEventsById(int userId)
      {
          List<Tasks> events = new List<Tasks>();
          //int userId = ((int)HttpContext.Current.Session["user_id"]);
          string query = "SELECT * FROM Events WHERE user_id = '" + userId + "';";
          using (SqlConnection conn = new SqlConnection(CommonVariables.ConnectionString))
          {
              using (SqlCommand cmd = new SqlCommand(query, conn))
              {
                  //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                  conn.Open();
                  SqlDataReader reader = cmd.ExecuteReader();
                  while (reader.Read())
                  {
                      Tasks eve = new Tasks();

                      eve.event_id = Convert.ToInt16(reader["event_id"]);
                      eve.user_id = Convert.ToInt16(reader["user_id"]);
                      eve.title = Convert.ToString(reader["title"]);
                     // eve.location = Convert.ToString(reader["location"]);
                      eve.description = Convert.ToString(reader["description"]);

                      eve.start_time = (TimeSpan)reader["start_time"];
                      eve.end_time = (TimeSpan)reader["end_time"];
                      
                      //eve.end_time = Convert.ToDateTime(reader.GetOrdinal("end_time"));

                      //eve.start_time = Convert.ToString(reader["start_time"]);
                      //eve.end_time = Convert.ToString(reader["end_time"]);

                      eve.start_date = Convert.ToDateTime(reader["start_date"]);
                      eve.end_date = Convert.ToDateTime(reader["end_date"]);

                      eve.status = Convert.ToString(reader["status"]);    


                      
                      
                      events.Add(eve);
                  }
              }
          }
          return events;
     }

      public void UpdateEvent(Tasks UpdateEvent)
      {
          string connStr = CommonVariables.ConnectionString;
          using (SqlConnection conn = new SqlConnection(connStr))
          {
              int status;

              if (UpdateEvent.status.Equals("True"))
              {
                  status = 1;
              }
              else
              {
                  status = 0;
              }

              //if(UpdateEvent.start_time == null && UpdateEvent.end_time == null){
              //    UpdateEvent.isTimeSet = 0;
              //}
              //else
              //{
              //    UpdateEvent.isTimeSet = 1;
              //}

              // string query = "UPDATE Users SET first_name=@first_name,last_name = @last_name,email = @email,password = @password WHERE user_id = @user_id";
              string query = "UPDATE Events SET title= '" + UpdateEvent.title + "', status = '" + status + "', start_date = '" + UpdateEvent.start_date + "', end_date= '" + UpdateEvent.end_date + "', start_time = '" + UpdateEvent.start_time + "', end_time = '" + UpdateEvent.end_time + "', description = '" + UpdateEvent.description + "', priority = '" + UpdateEvent.priority + "', isTimeSet = '" + UpdateEvent.isTimeSet + "' WHERE event_id = " + UpdateEvent.event_id + ";";
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



      public bool DeleteEventsById(int event_id)
      {
          try
          {

              SqlConnection con = new SqlConnection(CommonVariables.ConnectionString); 
              string query = "DELETE FROM Events WHERE event_id = '"+ event_id +"';";
              SqlCommand cmd = new SqlCommand(query, con);
              con.Open();
              cmd.ExecuteNonQuery();
              con.Close();
              return true;
          }
          catch(Exception){
              return false;
          }        
            
      }

      public returnEvent EditEvent(int event_id)
      {

          //Events eventData = new Events();
          returnEvent returnEventData = new returnEvent();

          string connStr = CommonVariables.ConnectionString;

        using (SqlConnection conn = new SqlConnection(connStr))
        {
                string query = "SELECT * FROM Events WHERE event_id = @eventId";
                using(SqlCommand cmd = new SqlCommand(query,conn)){
                conn.Open();
                cmd.Parameters.AddWithValue("@eventId", event_id);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                if (reader.HasRows)
                {
                    returnEventData.event_id = Convert.ToInt16(reader["event_id"]);
                    returnEventData.user_id = Convert.ToInt16(reader["user_id"]);
                    returnEventData.title = Convert.ToString(reader["title"]);
                    //returnEventData.location = Convert.ToString(reader["location"]);
                    returnEventData.description = Convert.ToString(reader["description"]);
                    returnEventData.status = Convert.ToString(reader["status"]);

                    // eventData.start_time = Convert.ToString(reader.GetOrdinal("start_time"));
                    //  eventData.end_time = Convert.ToString(reader.GetOrdinal("end_time"));
                    TimeSpan start_time = (TimeSpan)reader["start_time"];
                    TimeSpan end_time = (TimeSpan)reader["end_time"];


                    DateTime time = DateTime.Today.Add(start_time);
                    string displayTime = time.ToString("hh:mm tt");

                    DateTime time1 = DateTime.Today.Add(end_time);
                    string displayTime1 = time1.ToString("hh:mm tt");

                    returnEventData.returnStartTime = displayTime;// (DateTime.Today + start_time).ToString("HH 'hrs' mm 'mins' ss 'secs'");

                    returnEventData.returnEndTime = displayTime1;// (DateTime.Today + end_time).ToString("HH 'hrs' mm 'mins' ss 'secs'");

                    //returnEventData.start_date = Convert.ToString(reader["start_date"]);
                    //returnEventData.end_date = Convert.ToString(reader["end_date"]);

                    DateTime date = DateTime.Parse(Convert.ToString(reader["start_date"]));
                    DateTime date1 = DateTime.Parse(Convert.ToString(reader["end_date"]));

                    returnEventData.start_date = date.ToString("yyyy-MM-dd");// Convert.ToString(reader["start_date"]);
                    returnEventData.end_date = date1.ToString("yyyy-MM-dd");// Convert.ToString(reader["end_date"]); 

                    returnEventData.priority = Convert.ToString(reader["priority"]);
                    returnEventData.isTimeSet = Convert.ToByte(reader["isTimeSet"]);

                    returnEventData.returnMsg = "true";
                }
                else
                {
                    returnEventData.returnMsg = "false";
                }
            }
        }

        return returnEventData;
          
      }




      public int GetLastId()
      {

          //Events eventData = new Events();
          returnEvent returnEventData = new returnEvent();

          string connStr = CommonVariables.ConnectionString;

          using (SqlConnection conn = new SqlConnection(connStr))
          {
              string query = "SELECT IDENT_CURRENT('Events') as event_id";
              using (SqlCommand cmd = new SqlCommand(query, conn))
              {
                  conn.Open();
                  
                  SqlDataReader reader = cmd.ExecuteReader();
                  reader.Read();

                  event_id = Convert.ToInt16(reader["event_id"]);                

              }
          }

          return event_id;
          
      }

      public List<Tasks> getNotifications(int user_id)
      {
          List<Tasks> events = new List<Tasks>();

          string connStr = CommonVariables.ConnectionString;         
          SqlConnection sqlConnection1 = new SqlConnection(CommonVariables.ConnectionString);
          SqlCommand cmd = new SqlCommand();
          SqlDataReader reader;

          cmd.CommandText = "[dbo].[getNotification]";
          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Connection = sqlConnection1;
          sqlConnection1.Open();

          cmd.Parameters.AddWithValue("@user_id", user_id);

          reader = cmd.ExecuteReader();

          while (reader.Read())
          {
              Tasks eve = new Tasks();              
              
              eve.title = Convert.ToString(reader["title"]);
              
              eve.start_date = Convert.ToDateTime(reader["start_date"]);
              eve.end_date = Convert.ToDateTime(reader["end_date"]);
              eve.priority = Convert.ToString(reader["priority"]);

              events.Add(eve);
          }
          

          sqlConnection1.Close();

          return events;
      }

      //public IEnumerator GetEnumerator()
      //{
      //    return new xxx();
      //}

    }
    //class xxx : IEnumerator
    //{
        
    //    public List<Events> allEvents = new List<Events>();
    //    public bool MoveNext()

    //    {

    //    System.Console.WriteLine("oveNext=");

    //    return true;

    //    }

    //    public void Reset()

    //    {

    //    System.Console.WriteLine("Reset");

    //    }

    //    public object Current

    //    {

    //    get

    //    {

    //    System.Console.WriteLine("Current");

    //    return "hi";

    //    }

    //    }

    //}

    public class addedEvent
    {
        public int success { get; set; }
        public string returnMsg { get; set; }

        public object eventData { get; set; }
    }

    public class returnEvent
    {
        public int event_id { get; set; }
        public int user_id { get; set; }
        public string title { get; set; }
        //public string location { get; set; }
        public string description { get; set; }

        public string priority { get; set; }

        public Byte isTimeSet { get; set; }

        public string start_date { get; set; }
        public string end_date { get; set; }
        public string returnStartTime { get; set; }
        public string returnEndTime { get; set; }        
        public string status { get; set; }

        public string returnMsg { get; set; }
    }

}