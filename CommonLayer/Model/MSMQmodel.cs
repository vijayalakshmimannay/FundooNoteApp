using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.Model
{
    public class MSMQmodel
    {
        MessageQueue messageQueue = new MessageQueue();

        /// <summary>
        /// Sends the data2 queue.
        /// </summary>
        /// <param name="Token">The token.</param>
        public void sendData2Queue(string Token)
        {

            messageQueue.Path = @".\private$\Token";

            if(!MessageQueue.Exists(messageQueue.Path))
                {

                //Exists
                MessageQueue.Create(messageQueue.Path);

            }   

            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            messageQueue.ReceiveCompleted += MessageQueue_ReceiveCompleted;
            messageQueue.Send(Token);
            messageQueue.BeginReceive();
            messageQueue.Close();
        }


        /// <summary>
        /// Handles the ReceiveCompleted event of the MessageQueue control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ReceiveCompletedEventArgs"/> instance containing the event data.</param>
        
        private void MessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var msg = messageQueue.EndReceive(e.AsyncResult);
            string Token = msg.Body.ToString();
            string subject = "Fundoo Notes Reset Link";
           // string Body = Token;
            string Body = $"Fundoo Notes Reset Password: <a href=http://localhost:4200/resetpage/{Token}> Click Here</a>";

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("aarna.bridgelabz@gmail.com");
            mail.To.Add("aarna.bridgelabz@gmail.com");
            mail.Subject = "subject";

            mail.IsBodyHtml = true;
            string htmlBody;

            htmlBody = "Write some HTML code here";

            mail.Body = "<body><p>Dear Lakshmi,<br><br>" +
                "We have sent you a link for resetting your password.<br>" +
                "Please copy it and paste in your swagger authorization.</body>" + Body;


            var SMTP = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("aarna.bridgelabz@gmail.com", "xefccvjasgiluudj"),
                EnableSsl = true
            };
            SMTP.Send(mail);
            messageQueue.BeginReceive();
        }
    }
}
