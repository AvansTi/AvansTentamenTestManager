using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AvansTentamenManager
{

    class GmailMailer
    {
        static string[] Scopes = { GmailService.Scope.GmailSend };
        static string ApplicationName = "Avans tentamen result mailer";
        private static GmailService service;

        static GmailMailer()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Gmail API service.
            service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }


        public static void SendMail(string to, string subject, string bodyText, string file)
        {
            MailMessage message = new MailMessage();
            message.To.Add(to);
            //message.From = new MailAddress("me");
            message.Subject = subject;
            message.Body = bodyText;
            message.Attachments.Add(new Attachment(file));
            sendMessage("me", message);
        }



        public static string Encode(string text)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(text);

            return System.Convert.ToBase64String(bytes)
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");
        }


        public static Message sendMessage(String userId, MailMessage mailMessage)
        {
            Message message = new Message();
            message.Raw = Encode(MimeKit.MimeMessage.CreateFromMailMessage(mailMessage).ToString());
            message = service.Users.Messages.Send(message, userId).Execute();
    	    return message;
        }

    }
}
