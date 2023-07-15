using System.Net;
using System.Net.Mail;
using System.IO;
using System;



namespace EmailSmtp
{
    class Test
    {
        static void Main()
        {
            try
            {
                var mail = new MailMessage()
                {
                    From = new MailAddress("abc123@naver.com"), //보내는 이메일
                    Subject = "...", //이메일 제목
                    Body = "..." //이메일 내용
                };

                mail.To.Add(new MailAddress("abc123@outlook.com")); //받는 이메일

                string filePath = @"..\Vision\Exe\사고접수";
                string[] files = Directory.GetFiles(filePath);


                if (files.Length > 0)
                {
                    string firstFilePath = files[0];
                    string fileName = Path.GetFileName(firstFilePath);
                    var attachment = new Attachment(firstFilePath);
                    attachment.ContentDisposition.FileName = fileName;
                    mail.Attachments.Add(attachment);
                }

                var client = new SmtpClient()
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Port = 587, //네이버 포트 번호
                    Host = "smtp.naver.com", //네이버 smtp주소
                    EnableSsl = true,
                    Credentials = new NetworkCredential("abc123@naver.com", "abc123")
                    //보낼 이메일의 아이디와 비밀번호
                };

                client.Send(mail);

            }
            catch (Exception ex)
            {
                Console.WriteLine("전송 실패 " + ex.Message);
                Console.ReadKey();
                return;
            }
            Console.WriteLine("...."); //이메일 내용
            Environment.Exit(0);
        }

    }

}