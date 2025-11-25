using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;

namespace Tool.GeneralTool
{
    public class GeneralTool
    {
        public static string ComputeSha1Password(string password)
        {
            try
            {
                byte[] temp = SHA1.HashData(Encoding.UTF8.GetBytes(password));

                StringBuilder passwordSh1 = new();
                for (int i = 0; i < temp.Length; i++)
                {
                    passwordSh1.Append(temp[i].ToString("x2"));
                }

                return passwordSh1.ToString();
            }
            catch
            {
                return "";
            }
        }

        public static string GenerateRandomPassword()
        {
            int length = 5;

            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public static bool MailGonder(string from, string fromSifre, MailAddress to, string subject, string sablonDetay, FileContentResult fileResult)
        {
            try
            {

                #region Attachment

                Attachment data = null;
                if (fileResult != null)
                {
                    using var memoryStream = new MemoryStream(fileResult.FileContents);
                    FileStreamResult fileStreamResult = new(memoryStream, fileResult.ContentType)
                    {
                        FileDownloadName = fileResult.FileDownloadName
                    };

                    data = new Attachment(fileStreamResult.FileStream, fileStreamResult.FileDownloadName, fileResult.ContentType);
                    ContentDisposition disposition = data.ContentDisposition;
                }

                #endregion

                string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");


                NetworkCredential credentials;
                MailAddress recipient = to;
                if (string.Equals(env, "Development", StringComparison.OrdinalIgnoreCase))
                {

                    credentials = new NetworkCredential("mahsunn@gmail.com", "iois gmdi ryev tpzp");
                    subject = "!!Yazılım TEST Mailidir!! " + subject + " (Original To: " + to.Address + ")From:" + from + ")";
                    recipient = new MailAddress("mahsunn@gmail.com");

                }
                else
                {
                    credentials = new NetworkCredential(from, fromSifre);
                }

                using var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false, // Doğru ayarlandı
                    EnableSsl = true,
                    Credentials = credentials,
                };

                using var message = new MailMessage(new MailAddress(from), recipient)
                {
                    Subject = subject,
                    Body = sablonDetay,
                    IsBodyHtml = true
                };

                if (data != null)
                {
                    message.Attachments.Add(data);
                }
                smtp.Send(message);

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("Mail gönderilirken hata oluştu: " + ex.Message, ex);
            }
        }


        public static string GetMimeType(string fileUzanti)
        {
            try
            {
                string contentType = "";

                if (fileUzanti == ".doc")
                    contentType = "application/msword";
                else if (fileUzanti == ".docx")
                    contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                else if (fileUzanti == ".pdf")
                    contentType = "application/pdf";
                else
                    contentType = "";

                return contentType;
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// Generate random number between 1 and 1000
        /// </summary>
        /// <returns></returns>
        public static int GenerateRandomNumber()
        {
            Random random = new Random();
            return random.Next(1, 1000);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static int GenerateRandomNumber(int minValue, int maxValue)
        {
            Random random = new();
            return random.Next(minValue, maxValue);
        }

    }
}
