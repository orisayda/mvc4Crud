using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Mvc4Crud;

using System.Web.Configuration;


namespace Mvc4Crud.DbHelpers
{
    public class DataConnector
    {
        private string databaseName = "tdl";
        public string DatabaseName
        {
            get { return databaseName; }
            set { databaseName = value; }
        }
        public string Password { get; set; }
        private SqlConnection connection = null;
       // private SqlCommand command = null;


        public SqlConnection Connection
        {
            get { return connection; }
        }
        /*
        public SqlCommand Command(string query,object conn)
        {
            get {return command;}
        }*/
        private static DataConnector _instance = null;
        public static DataConnector Instance()
        {
            if (_instance == null)
                _instance = new DataConnector();
            return _instance;
        }
        public bool IsConnect()
        {
            bool result = true;
            if (Connection == null)
            {
                if (String.IsNullOrEmpty(databaseName))
                    result = false;
                //string connstring = string.Format("Server=localhost; database={0}; UID=root; password=", databaseName);
                string connstring = string.Format("Server=\"localhost\"; database=\"aio\"; UID=\"root\"; password=\"\";Convert Zero Datetime=True");
                //string connstring = string.Format("Server=\"192.169.80.198\"; database=\"allinon2_aio\"; UID=\"allinon2_saimpasha\"; password=\"iSa4Y91ic8\"");
                connection = new SqlConnection(connstring);
                connection.Open();
                result = true;
            }
            return result;
        }
        public string now()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public void Close()
        {
            this.connection.Close();
        }

      
        public DataTable getUsers()
        {
            if (this.IsConnect())
            {
                string query = "SELECT * FROM users";
                
                var command = new SqlCommand(query, this.Connection);
                //var cmd =  Command(query, this.Connection);
                var reader = command.ExecuteReader();
                var users = new DataTable();
                users.Load(reader);
                this.Close();
                return users;
              
            }
            return null;
        }

        /*private object Command(string query, SqlConnection sqlConnection)
        {
            throw new NotImplementedException();
        }*/
        public DataTable getUser(string id)
        {
            if (this.IsConnect())
            {
                string query = "SELECT * FROM users WHERE id= '" + id + "'";
                var command = new SqlCommand(query, this.Connection);
                //var cmd = new MySqlCommand(query, this.Connection);
                var reader = command.ExecuteReader();
                var users = new DataTable();
                users.Load(reader);
                this.Close();
                return users;
            }
            return null;
        }
        public DataTable getUserLoads(string userID)
        {
            if (this.IsConnect())
            {
                string query = "SELECT * FROM loads WHERE user_id = '" + userID + "' AND package_id != 0";
                var command = new SqlCommand(query, this.Connection);
                //var cmd = new MySqlCommand(query, this.Connection);
                var reader = command.ExecuteReader();
                var loads = new DataTable();
                loads.Load(reader);
                this.Close();
                return loads;
            }
            return null;
        }
        public DataTable getUserPackages(string userID)
        {
            if (this.IsConnect())
            {
                string query = "SELECT * FROM loads WHERE user_id = '" + userID + "' AND package_id != 0";
                //var cmd = new MySqlCommand(query, this.Connection);
                var cmd = new SqlCommand(query, this.Connection);
                var reader = cmd.ExecuteReader();
                var packageLoads = new DataTable();
                packageLoads.Load(reader);
                this.Close();
                return packageLoads;
            }
            return null;
        }
        public DataTable getUserTransfers(string userID)
        {
            if (this.IsConnect())
            {
                string query = "SELECT * FROM transfers WHERE sender_id = '" + userID + "'";
               // var cmd = new MySqlCommand(query, this.Connection);
                var cmd = new SqlCommand(query, this.Connection);
                var reader = cmd.ExecuteReader();
                var transfers = new DataTable();
                transfers.Load(reader);
                this.Close();
                return transfers;
            }
            return null;
        }
        public DataTable getUserPayments(string userID)
        {
            if (this.IsConnect())
            {
                string query = "SELECT * FROM payments WHERE user_id = '" + userID + "'";
                //var cmd = new MySqlCommand(query, this.Connection);
                var cmd = new SqlCommand(query, this.Connection);
                var reader = cmd.ExecuteReader();
                var payments = new DataTable();
                payments.Load(reader);
                this.Close();
                return payments;
            }
            return null;
        }
        public DataTable getVoucherLoads(string userID)
        {
            if (this.IsConnect())
            {
                string query = "SELECT vouchers.title, v_loads.user_id, v_loads.voucher_code_id, vouchers.amount, v_loads.created,  voucher_codes.code  FROM v_loads INNER JOIN voucher_codes ON v_loads.voucher_code_id = voucher_codes.id INNER JOIN vouchers ON vouchers.id = voucher_codes.voucher_id WHERE v_loads.user_id = '" + userID + "'";
                //var cmd = new MySqlCommand(query, this.Connection);
                var cmd = new SqlCommand(query, this.Connection);
                var reader = cmd.ExecuteReader();
                var vLoads = new DataTable();
                vLoads.Load(reader);
                this.Close();
                return vLoads;
            }
            return null;
        }
        public DataTable getUserLogins(string userID)
        {
            if (this.IsConnect())
            {
                string query = "SELECT * FROM logins WHERE user_id = '" + userID + "'";
               // var cmd = new MySqlCommand(query, this.Connection);
                var cmd = new SqlCommand(query, this.Connection);
                var reader = cmd.ExecuteReader();
                var logins = new DataTable();
                logins.Load(reader);
                this.Close();
                return logins;
            }
            return null;
        }
        public DataTable getDevices()
        {
            if (this.IsConnect())
            {
                string query = "SELECT * FROM devices";
               // var cmd = new MySqlCommand(query, this.Connection);
                var cmd = new SqlCommand(query, this.Connection);
                var reader = cmd.ExecuteReader();
                var devices = new DataTable();
                devices.Load(reader);
                this.Close();
                return devices;
            }
            return null;
        }
        public DataTable getDeviceLoads(string device_id)
        {
            if (this.IsConnect())
            {
                string query = "SELECT * FROM loads WHERE device_id = '" + device_id + "'";
               // var cmd = new MySqlCommand(query, this.Connection);
                var cmd = new SqlCommand(query, this.Connection);
                var reader = cmd.ExecuteReader();
                var loads = new DataTable();
                loads.Load(reader);
                this.Close();
                return loads;
            }
            return null;
        }
        public DataTable getDevicePurchases(string device_id)
        {
            if (this.IsConnect())
            {
                string query = "SELECT * FROM purchases WHERE device_id = '" + device_id + "'";
                //var cmd = new MySqlCommand(query, this.Connection);
                var cmd = new SqlCommand(query, this.Connection);
                var reader = cmd.ExecuteReader();
                var purchases = new DataTable();
                purchases.Load(reader);
                this.Close();
                return purchases;
            }
            return null;
        }
        public DataTable getDevice(string device_id)
        {
            if (this.IsConnect())
            {
                string query = "SELECT * FROM devices WHERE id= '" + device_id + "'";
                //var cmd = new MySqlCommand(query, this.Connection);
                var cmd = new SqlCommand(query, this.Connection);
                var reader = cmd.ExecuteReader();
                var devices = new DataTable();
                devices.Load(reader);
                this.Close();
                return devices;
            }
            return null;
        }
        public void addDevice(string network_id, string comPort, string deviceName, string deviceNumber)
        {
            if (this.IsConnect())
            {
                string query2 = "INSERT INTO devices (network_id, comPort, name, number, created)VALUES('" + network_id + "', '" + comPort + "', '" + deviceName + "', '" + deviceNumber + "', '" + now() + "' )";
               // var cmd2 = new MySqlCommand(query2, this.Connection);
                var cmd2 = new SqlCommand(query2, this.Connection);
                cmd2.ExecuteNonQuery();
                this.Close();
            }
        }
        public void addDeviceBalance(string amount, string rate, string device_id)
        {
            string network_id = getNetworkIdFromDevice(device_id);
            if (this.IsConnect())
            {
                string query2 = "INSERT INTO purchases (device_id, amount, rate, datetime)VALUES('" + device_id + "', '" + amount + "', '" + rate + "', '" + now() + "' )";
                //var cmd2 = new MySqlCommand(query2, this.Connection);
                var cmd2 = new SqlCommand(query2, this.Connection);
                cmd2.ExecuteNonQuery();
                this.Close();
                if (this.IsConnect())
                {
                    query2 = "INSERT INTO stocks (network_id, amount, rate)VALUES('" + network_id + "', '" + amount + "', '" + rate + "' )";
                    //cmd2 = new MySqlCommand(query2, this.Connection);
                    cmd2 = new SqlCommand(query2, this.Connection);
                    cmd2.ExecuteNonQuery();
                    this.Close();
                }
            }
        }
        public void updateDeviceStatus(string status, string device_id)
        {
            if (this.IsConnect())
            {
                string query2 = "UPDATE devices SET status='" + status + "' WHERE id=" + device_id;
                //var cmd2 = new MySqlCommand(query2, this.Connection);
                var cmd2 = new SqlCommand(query2, this.Connection);
                cmd2.ExecuteNonQuery();
                this.Close();
            }
        }
        public void insertPurchase(string device_id, string amount, string rate)
        {
            if (this.IsConnect())
            {
                string query2 = "INSERT INTO purchases (device_id, amount, rate, datetime)VALUES('" + device_id + "', '" + amount + "', '" + rate + "', '" + now() + "' )";
                //var cmd2 = new MySqlCommand(query2, this.Connection);
                var cmd2 = new SqlCommand(query2, this.Connection);
                cmd2.ExecuteNonQuery();
                this.Close();
            }
        }
        public void insertStock(string network_id, string amount, string rate)
        {
            if (this.IsConnect())
            {
                string query2 = "INSERT INTO stocks (network_id, amount, rate)VALUES('" + network_id + "', '" + amount + "', '" + rate + "' )";
                //var cmd2 = new MySqlCommand(query2, this.Connection);
                var cmd2 = new SqlCommand(query2, this.Connection);
                cmd2.ExecuteNonQuery();
                this.Close();
            }
        }
        public void insertPayment(string amount, string paymentMethod, string user_id)
        {
            if (this.IsConnect())
            {
                string query2 = "INSERT INTO payments (user_id, amount, payment_method, datetime)VALUES('" + user_id + "', '" + amount + "', '" + paymentMethod + "', '" + now() + "' )";
                //var cmd2 = new MySqlCommand(query2, this.Connection);
                var cmd2 = new SqlCommand(query2, this.Connection);
                cmd2.ExecuteNonQuery();
                this.Close();
            }
        }
        public void updateDeviceBalance(string device_id, string amount)
        {
            if (this.IsConnect())
            {
                string query2 = "UPDATE devices SET balance=balance+'" + amount + "' WHERE id=" + device_id;
                //var cmd2 = new MySqlCommand(query2, this.Connection);
                var cmd2 = new SqlCommand(query2, this.Connection);
                cmd2.ExecuteNonQuery();
                this.Close();
            }
        }
        public void updateUserRole(string role, string user_id)
        {
            if (this.IsConnect())
            {
                string query2 = "UPDATE users SET role='" + role + "' WHERE id=" + user_id;
                //var cmd2 = new MySqlCommand(query2, this.Connection);
                var cmd2 = new SqlCommand(query2, this.Connection);
                cmd2.ExecuteNonQuery();
                this.Close();
            }
        }
        public void addUserBalance(string amount, string user_id)
        {
            if (this.IsConnect())
            {
                string query2 = "UPDATE users SET balance=balance+'" + amount + "' WHERE id=" + user_id;
                //var cmd2 = new MySqlCommand(query2, this.Connection);
                var cmd2 = new SqlCommand(query2, this.Connection);
                cmd2.ExecuteNonQuery();
                this.Close();
            }
        }
        public void updateUserStatus(string status, string user_id)
        {
            if (this.IsConnect())
            {
                string query2 = "UPDATE users SET status='" + status + "' WHERE id=" + user_id;
                //var cmd2 = new MySqlCommand(query2, this.Connection);
                var cmd2 = new SqlCommand(query2, this.Connection);
                cmd2.ExecuteNonQuery();
                this.Close();
            }
        }
        public string getNetworkIdFromDevice(string device_id)
        {
            if (this.IsConnect())
            {
                string query = "SELECT network_id FROM devices WHERE id = '" + device_id + "'";
                //var cmd = new MySqlCommand(query, this.Connection);
                var cmd = new SqlCommand(query, this.Connection);
                var reader = cmd.ExecuteReader();
                string networkId = string.Empty;
                while (reader.Read())
                {
                    networkId = reader.GetString(0);

                }
                this.Close();
                return networkId;
            }
            return null;
        }
        public string getUsername(string user_id)
        {
            if (this.IsConnect())
            {
                string query = "SELECT username FROM users WHERE id = '" + user_id + "'";
                //var cmd = new MySqlCommand(query, this.Connection);
                var cmd = new SqlCommand(query, this.Connection);
                var reader = cmd.ExecuteReader();
                string username = string.Empty;
                while (reader.Read())
                {
                    username = reader.GetString(0);

                }
                this.Close();
                return username;
            }
            return null;
        }
        public string getPackageName(string package_id)
        {
            if (this.IsConnect())
            {
                string query = "SELECT title FROM packages WHERE id = '" + package_id + "'";
                //var cmd = new MySqlCommand(query, this.Connection);
                var cmd = new SqlCommand(query, this.Connection);
                var reader = cmd.ExecuteReader();
                string packageTitle = string.Empty;
                while (reader.Read())
                {
                    packageTitle = reader.GetString(0);
                }
                this.Close();
                return packageTitle;
            }
            return null;
        }
        public DataTable getVouchers()
        {
            if (this.IsConnect())
            {
                string query = "SELECT * FROM vouchers";
                //var cmd = new MySqlCommand(query, this.Connection);
                var cmd = new SqlCommand(query, this.Connection);
                var reader = cmd.ExecuteReader();
                var devices = new DataTable();
                devices.Load(reader);
                this.Close();
                return devices;
            }
            return null;
        }
        public DataTable getVoucher(string voucher_id)
        {
            if (this.IsConnect())
            {
                string query = "SELECT * FROM vouchers WHERE id='" + voucher_id + "'";
                //var cmd = new MySqlCommand(query, this.Connection);
                var cmd = new SqlCommand(query, this.Connection);
                var reader = cmd.ExecuteReader();
                var devices = new DataTable();
                devices.Load(reader);
                this.Close();
                return devices;
            }
            return null;
        }
        public void insertVoucher(string amount, string title, string commission, string network_id)
        {
            if (this.IsConnect())
            {
                string query2 = "INSERT INTO vouchers (network_id, amount, commission, title, created)VALUES('" + network_id + "', '" + amount + "', '" + commission + "', '" + title + "', '" + now() + "' )";
                //var cmd2 = new MySqlCommand(query2, this.Connection);
                var cmd2 = new SqlCommand(query2, this.Connection);
                cmd2.ExecuteNonQuery();
                this.Close();
            }
        }
        public DataTable getVoucherStocks(string voucher_id)
        {
            if (this.IsConnect())
            {
                string query = "SELECT * FROM voucher_codes WHERE status = 'unused' AND voucher_id = '" + voucher_id + "'";
                //var cmd = new MySqlCommand(query, this.Connection);
                var cmd = new SqlCommand(query, this.Connection);
                var reader = cmd.ExecuteReader();
                var devices = new DataTable();
                devices.Load(reader);
                this.Close();
                return devices;
            }
            return null;
        }
        public DataTable getVoucherCashValues()
        {
            if (this.IsConnect())
            {
                string query = "SELECT vouchers.id, vouchers.network_id, vouchers.amount, vouchers.title, vouchers.quantity, SUM(vouchers.amount) - SUM(voucher_codes.rate*vouchers.amount*.01) AS cashValue FROM `vouchers` JOIN voucher_codes on voucher_codes.voucher_id = vouchers.id WHERE voucher_codes.status = 'unused' GROUP BY voucher_codes.voucher_id ORDER BY vouchers.network_id ASC ";
                //var cmd = new MySqlCommand(query, this.Connection);
                var cmd = new SqlCommand(query, this.Connection);
                var reader = cmd.ExecuteReader();
                var cashValues = new DataTable();
                cashValues.Load(reader);
                this.Close();
                return cashValues;
            }
            return null;
        }
        public DataTable getLoadStocks(string network_id)
        {
            if (this.IsConnect())
            {
                string query = "SELECT * FROM stocks WHERE network_id = '" + network_id + "'";
                //var cmd = new MySqlCommand(query, this.Connection);
                var cmd = new SqlCommand(query, this.Connection);
                var reader = cmd.ExecuteReader();
                var stock = new DataTable();
                stock.Load(reader);
                this.Close();
                return stock;
            }
            return null;
        }
        public double getLoadEarning(string network_id, bool byRetailer)
        {
            double earning = 0;
            if (this.IsConnect())
            {
                string query = string.Empty;
                if (byRetailer)
                {
                    query = "SELECT SUM(earning) FROM `loads` JOIN users ON users.id = loads.user_id WHERE loads.network_id =" + network_id + " AND users.role ='retailer' AND loads.package_id = 0";
                }
                else
                {
                    query = "SELECT SUM(earning) FROM `loads` JOIN users ON users.id = loads.user_id WHERE loads.network_id =" + network_id + " AND users.role ='customer' AND loads.package_id = 0";
                }
                //var cmd = new MySqlCommand(query, this.Connection);
                var cmd = new SqlCommand(query, this.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        earning = Convert.ToDouble(reader.GetString(0));
                    }



                }
                this.Close();
                return earning;
            }
            return earning;
        }
        public double getPackageEarning(string network_id, bool byRetailer)
        {
            double earning = 0;
            if (this.IsConnect())
            {
                string query = string.Empty;
                if (byRetailer)
                {
                    query = "SELECT SUM(earning) FROM `loads` JOIN users ON users.id = loads.user_id WHERE loads.network_id =" + network_id + " AND users.role ='retailer' AND loads.package_id != 0";
                }
                else
                {
                    query = "SELECT SUM(earning) FROM `loads` JOIN users ON users.id = loads.user_id WHERE loads.network_id =" + network_id + " AND users.role ='customer' AND loads.package_id != 0";
                }
                //var cmd = new MySqlCommand(query, this.Connection);
                var cmd = new SqlCommand(query, this.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        earning = Convert.ToDouble(reader.GetString(0));
                    }
                }
                this.Close();
                return earning;
            }
            return earning;
        }
        public double getLoadEarning()
        {
            double earning = 0;
            if (this.IsConnect())
            {
                string query = string.Empty;
                query = "SELECT SUM(earning) FROM `loads` ";
                //var cmd = new MySqlCommand(query, this.Connection);
                var cmd = new SqlCommand(query, this.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        earning = Convert.ToDouble(reader.GetString(0));
                    }
                }
                this.Close();
                return earning;
            }
            return earning;
        }
        public double getVoucherEarning(string network_id, bool byRetailer)
        {
            double earning = 0;
            if (this.IsConnect())
            {
                string query = string.Empty;
                if (byRetailer)
                {
                    query = "SELECT SUM(earning) FROM `v_loads` JOIN users ON users.id = v_loads.user_id  JOIN voucher_codes ON v_loads.voucher_code_id = voucher_codes.id JOIN vouchers ON vouchers.id = voucher_codes.voucher_id WHERE vouchers.network_id =" + network_id + " AND users.role ='retailer'";
                }
                else
                {
                    query = "SELECT SUM(earning) FROM `v_loads` JOIN users ON users.id = v_loads.user_id JOIN voucher_codes ON v_loads.voucher_code_id = voucher_codes.id JOIN vouchers ON vouchers.id = voucher_codes.voucher_id WHERE vouchers.network_id =" + network_id + " AND users.role ='customer'";
                }
                //var cmd = new MySqlCommand(query, this.Connection);
                var cmd = new SqlCommand(query, this.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        earning = Convert.ToDouble(reader.GetString(0));
                    }
                }
                this.Close();
                return earning;
            }
            return earning;
        }
        public double getVoucherEarning()
        {
            double earning = 0;
            if (this.IsConnect())
            {
                string query = string.Empty;
                query = "SELECT SUM(earning) FROM `v_loads`";
                //var cmd = new MySqlCommand(query, this.Connection);
                var cmd = new SqlCommand(query, this.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        earning = Convert.ToDouble(reader.GetString(0));
                    }
                }
                this.Close();
                return earning;
            }
            return earning;
        }
        public void insertVoucherCodes(List<string> codes, string rate, string vendor, string voucher_id)
        {
            if (this.IsConnect())
            {
                string query2 = "INSERT INTO voucher_codes (voucher_id, code, rate, vendor, created) VALUES ";
                foreach (string code in codes)
                    query2 += " ('" + voucher_id + "', '" + code + "', '" + rate + "', '" + vendor + "', '" + now() + "' ),";
                query2 = query2.Remove(query2.Length - 1, 1);
                //var cmd2 = new MySqlCommand(query2, this.Connection);
                var cmd2 = new SqlCommand(query2, this.Connection);
                cmd2.ExecuteNonQuery();
                this.Close();
            }

        }
        public void updateVoucherQuantity(string count, string voucher_id)
        {
            if (this.IsConnect())
            {
                string query2 = "UPDATE vouchers SET quantity=quantity+'" + count + "' WHERE id=" + voucher_id;
                //var cmd2 = new MySqlCommand(query2, this.Connection);
                var cmd2 = new SqlCommand(query2, this.Connection);
                cmd2.ExecuteNonQuery();
                this.Close();
            }
        }
    }
}