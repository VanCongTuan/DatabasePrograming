using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace LTCSDLMayBay.SendEmail
{ public class Email { 
    public void SendMail(string mailto, string madatcho, dynamic nguoi_Lon, dynamic tre_Em, dynamic cbs, List<int> soGhe, dynamic venl, dynamic vete)
    {
        string email_from = "2151050512tuan@ou.edu.vn";  // email người gửi
        string email_to = mailto;  // email người nhận
        string password = "gmty cxng ckud uglr";  // password phải sinh ra trong bảo vệ 2 lớp bảo mật của google
        string subject = "Thu xac nhan thong tin chuyen bay";
        string body = "";

        foreach (var cb in cbs)
        {
            body += "Ten chuyen bay: " + cb.MaCB + ".\n";
            body += "Ga di: " + cb.noidi + ".\n";
            body += "Ga den: " + cb.noiden + ".\n";
            body += "Ma dat cho:" + madatcho + ".\n ";
        }

        for (int i = 0; i < nguoi_Lon.Length; i++)
        {
            body += "Ten hanh khach thứ" + (i + 1) + ":" + nguoi_Lon[i].hoten + ".\n";
            if (venl != "")
            {
                body += "Mã vé :" + venl[i] + ".\n";
                body += "Số ghế :" + soGhe[i] + ".\n";
            }
        }

        for (int i = 0; i < tre_Em.Length; i++)
        {
            body += "Ten hanh khach(trẻ em)" + (i + 1) + ":" + tre_Em[i].hotenTreEm + ".\n";
            if (vete != "")
            {
                body += "Mã vé :" + vete[i] + ".\n";
                body += "Số ghế :" + soGhe[nguoi_Lon.Length + i] + ".\n";
            }
        }

        MailMessage message = new MailMessage(email_from, email_to);
        message.Subject = subject;
        message.Body = body;

        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential(email_from, password);
        smtp.EnableSsl = true;
        smtp.Send(message);
        Console.WriteLine("Check your email ;)");
    }
}
}