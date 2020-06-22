using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
namespace GETCUSJO.Model
{
    public class BAL :DAL
    {
        // Get the connection string 
        #region Variables 
        static string connectString = ConfigurationManager.AppSettings["Constr"];
        static string SendGridAPI = ConfigurationManager.AppSettings["CallAPI"];
        static string SendGridMailFrom = ConfigurationManager.AppSettings["SendMail"];
        SqlConnection con;
        static string _baseUrl;
        #endregion

        // Read all the Email ID's from tha database
        #region Get Email Address From DataBase
        public override bool GetEmail()
        {
            _baseUrl = baseUrl;

            bool isEmailSend = true;
            try
            {
                con = new SqlConnection(connectString);
                SqlCommand command = new SqlCommand("GetEmail", con);
                command.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Email email;

                    foreach (DataRow dr in dt.Rows)
                    {
                        email = new Email();
                        email.FirstName = dr["FirstName"].ToString();
                        email.LastName = dr["LastName"].ToString();
                        email.EmailID = dr["EmailID"].ToString();
                        email.ID = dr["ID"].ToString();
                        email.Subject = "<strong>Welcome to GetCusJO</strong>";
                        email.Body = "Hello " + email.FirstName + " " + email.LastName + ",";
                        email.Body += "<br /><br />Please click the following link to Get the discount coupon";
                        email.Body += "<br /><a href = '" + _baseUrl + "Visit.aspx?ID=" + email.ID + "'>Click here to Get your Coupon.</a>";
                        email.Body += "<br /><br />Regars";
                        // Email Sending  Mail Sending ..... 
                        ShootMail(email);
                    }
                }

                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }

            return isEmailSend;
        }
        #endregion

        // Update Is Link Cliked 
        #region Update Is Link Cliked 
        public override bool UpdateonClick(int ID)
        {
            bool isEmailSend = true;
            try
            {
                con = new SqlConnection(connectString);
                SqlCommand cmd = new SqlCommand("usp_updateLink", con);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.Add("@emailid", SqlDbType.NVarChar, 50);
                cmd.Parameters["@emailid"].Direction = ParameterDirection.Output;

                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    
                    Email email = new Email();
                    email.EmailID = (string)cmd.Parameters["@emailid"].Value;
                    email.Subject = "<strong>Hurray You Got you coupon</strong>";
                    email.Body = "<strong>Thanks fo choosing us</strong>";
                    
                    ShootMail(email);
                }
                con.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                isEmailSend = false;
            }

            return isEmailSend;
        }
        #endregion 

        // Email Sending  Mail Sending .....
        #region Email Sending  Mail Send
        public void ShootMail(Email email)
        {
            using (MailMessage mm = new MailMessage(ConfigurationManager.AppSettings["FromEmail"], email.EmailID))
            {
                mm.Subject = email.Subject;
                mm.Body = email.Body;
                mm.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["Host"];
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(ConfigurationManager.AppSettings["Username"], ConfigurationManager.AppSettings["Password"]);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
                smtp.Send(mm);
                // Waiting Time
                System.Threading.Thread.Sleep(3000);
            }
        }
        #endregion
    }
}