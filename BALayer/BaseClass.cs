using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;

namespace BALayer
{
    public class BaseClass
    {
        private double intHeightRatio, intWidthRatio, ratio;
        private int intHeight, intWidth;
        private string strError;

        private double Ratio
        {
            get { return ratio; }
            set { ratio = value; }
        }

        public bool ValidateLogin()
        {
            if (HttpContext.Current.Session["HRMAdminId"] == null || HttpContext.Current.Session["HRMAdminId"].ToString() == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private double IntWidthRatio
        {
            get { return intWidthRatio; }
            set { intWidthRatio = value; }
        }

        private double IntHeightRatio
        {
            get { return intHeightRatio; }
            set { intHeightRatio = value; }
        }

        private int IntWidth
        {
            get { return intWidth; }
            set { intWidth = value; }
        }

        private int IntHeight
        {
            get { return intHeight; }
            set { intHeight = value; }
        }

        public bool DeleteImage(string strImageName)
        {
            if (File.Exists(strImageName))
            {
                File.Delete(strImageName);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool GenerateThumbnails(Stream sourcePath, string targetPath, int width, int height)
        {
            try
            {
                using (var image = System.Drawing.Image.FromStream(sourcePath))
                {
                    if (image.Height > height || image.Width > width)
                    {
                        IntWidthRatio = (double)image.Width / (double)width;
                        IntHeightRatio = (double)image.Height / (double)height;
                        Ratio = Math.Max(IntHeightRatio, IntWidthRatio);
                        IntHeight = (int)(image.Height / Ratio);
                        IntWidth = (int)(image.Width / Ratio);
                    }
                    else
                    {
                        intWidth = image.Width;
                        IntHeight = image.Height;
                    }
                    // can given width of image as we want
                    var newWidth = intWidth;//(int)(image.Width * 0.5);
                    // can given height of image as we want
                    var newHeight = IntHeight; //(int)(image.Height * 0.5);
                    var thumbnailImg = new System.Drawing.Bitmap(newWidth, newHeight);
                    var thumbGraph = System.Drawing.Graphics.FromImage(thumbnailImg);
                    thumbGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    thumbGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    thumbGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    var imageRectangle = new System.Drawing.Rectangle(0, 0, newWidth, newHeight);
                    thumbGraph.DrawImage(image, imageRectangle);
                    thumbnailImg.Save(targetPath, image.RawFormat);
                }
                return true;
            }
            catch (Exception ex)
            {
                strError = "HRM,BaseClass.cs,GenerateThumbnails(), " + ex.Message.ToString();
                SendMail(strError);
                return false;
            }
        }

        public string EncryptedPassword(string ValueToEncrypt)
        {
            try
            {
                System.Security.Cryptography.RijndaelManaged cryptObj = new

        RijndaelManaged();

                byte[] KEY_128 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6 };

                byte[] IV_128 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6 };

                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, cryptObj.CreateEncryptor(KEY_128, IV_128), CryptoStreamMode.Write);
                StreamWriter sw = new StreamWriter(cs);
                sw.Write(ValueToEncrypt);
                sw.Flush();
                cs.FlushFinalBlock();
                ms.Flush();
                return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
            }
            catch (Exception ex)
            {
                //strError1 = "UniTrade, Mailroute, EncryptedPassword(), " + ex.Message;
                //SendMail(strError1);
                return "";
            }
        }

        public string DecryptedPassword(string ValueToDecrypt)
        {
            try
            {
                System.Security.Cryptography.RijndaelManaged cryptObj = new

        RijndaelManaged();

                byte[] KEY_128 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6 };

                byte[] IV_128 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6 };

                byte[] buf = Convert.FromBase64String(ValueToDecrypt);
                MemoryStream ms = new MemoryStream(buf);
                CryptoStream cs = new CryptoStream(ms, cryptObj.CreateDecryptor(KEY_128,
                    IV_128), CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(cs);
                return sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                //strError1 = "UniTrade, Mailroute, DecryptedPassword(), " + ex.Message;
                //SendMail(strError1);
                return "";
            }
        }

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public void SendMail(string strError)
        {
            try
            {
                var fromAddress = new MailAddress("ssbalakumar@gmail.com", "no-reply Mail");
                var toAddress = new MailAddress("ssbalakumar@gmail.com", "info Mail");
                const string subject = "HRM Development";
                string body = strError;

                var loginInfo = new NetworkCredential("ssbalakumar@gmail.com", "Tekardia_Balan89");
                var msg = new System.Net.Mail.MailMessage();
                var smtpClient = new SmtpClient("smtp.gmail.com", 587);

                msg.From = fromAddress;
                msg.To.Add(toAddress);
                msg.Subject = subject;
                msg.Body = body;
                msg.IsBodyHtml = true;

                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = loginInfo;
                smtpClient.Send(msg);
            }
            catch (Exception ex)
            {
                strError = "HRM,BaseClass.cs,SendMail(), " + ex.Message.ToString();
                SendMail(strError);
            }
        }

        public void GeneralSendMail(string strFrom, string strTo, string strSubject, string strBody)
        {
            try
            {
                var fromAddress = new MailAddress(strFrom);
                var toAddress = new MailAddress(strTo);

                var loginInfo = new NetworkCredential("ssbalakumar@gmail.com", "Tekardia_Balan89");
                var msg = new System.Net.Mail.MailMessage();
                var smtpClient = new SmtpClient("smtp.gmail.com", 587);

                msg.From = fromAddress;
                msg.To.Add(toAddress);
                msg.Subject = strSubject;
                msg.Body = strBody;
                msg.IsBodyHtml = true;

                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = loginInfo;
                smtpClient.Send(msg);
            }
            catch (Exception ex)
            {
                strError = "HRM,BaseClass.cs,GeneralSendMail(), " + ex.Message.ToString();
                SendMail(strError);
            }
        }

        public string HttpContent(string url)
        {
            System.Net.WebRequest objRequest = System.Net.HttpWebRequest.Create(url);
            StreamReader sr = new StreamReader(objRequest.GetResponse().GetResponseStream());
            string result = sr.ReadToEnd();
            sr.Close();
            return result;
        }
    }
}