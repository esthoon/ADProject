using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using Team3ADProject.Model;


namespace Team3ADProject.Code
{



    public class BusinessLogic
    {
    static LogicUniversityEntities ctx= new LogicUniversityEntities();


        //List all adjustment form
        public static List<adjustment> GetAdjustment()
        {

            return ctx.adjustments.Where(x => x.adjustment_status == "pending" && x.adjustment_price <= 250).ToList();
            

        }

        public static void Updateadj(int id, string comment)
        {
            adjustment adj = ctx.adjustments.Where(x => x.adjustment_id == id).First<adjustment>();
            adj.adjustment_status = "Approved";
            adj.manager_remark = comment;
            ctx.SaveChanges();

        }

        public static void rejectAdj(int id, string comment)
        {
            adjustment adj = ctx.adjustments.Where(x => x.adjustment_id == id).First<adjustment>();
            adj.adjustment_status = "Rejected";
            adj.manager_remark = comment;
            ctx.SaveChanges();

        }

        public static List<adjustment> Search(DateTime date)
        {
            return ctx.adjustments.Where(x => x.adjustment_date == date).ToList<adjustment>();
        }

        
        public static List<purchase_order> GetPurchaseOrders()
        {
            return ctx.purchase_order.OrderBy(x=>x.suppler_id).Where(x => x.purchase_order_status == "awaiting approval").ToList();

        }

        public static void sendMail()
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("adteam3@gmail.com");
            mail.To.Add("adteam3@gmail.com");
            mail.Subject = "Test Mail";
            mail.Body = "Hello. this is a testing email";

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("adteam3@gmail.com", "testing!23");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
            
        }


       
    }
}