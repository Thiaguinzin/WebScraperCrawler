using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Configuration;

namespace WebScraperCrawler
{
    public class EnviadorEmail
    {
        public void EnviarEmail(List<Filme> filme)
        {
            MimeMessage message = new MimeMessage();

            message.From.Add(new MailboxAddress("Thiago Dev", "devteste199@hotmail.com"));
            //message.To.Add(MailboxAddress.Parse("karenolivm@gmail.com"));
            message.To.Add(MailboxAddress.Parse("nunesthiago08@gmail.com"));
            message.Subject = "Filmes mais populares (IMDB)";

            string template = TemplateEmail(filme);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = template;
            message.Body = bodyBuilder.ToMessageBody();

            SmtpClient client = new SmtpClient();
            string email = ConfigurationManager.AppSettings["EMAIL_REMETENTE"];
            string senha = ConfigurationManager.AppSettings["SENHA_REMETENTE"];

            try
            {
                client.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                client.Authenticate(email, senha);
                client.Send(message);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }

        public string TemplateEmail(List<Filme> filmes)
        {
            var html = "";

            html += String.Format("<h2 style='text-align:center'> Filmes mais populares ({0}) </h2>", DateTime.Now.ToString("dd/MM/yyyy"));
            html += "<body>";
            html += "<table style='width:100%; margin-left: auto; margin-right: auto'>";
            html += "<thead>";
            html += "<tr>";
            html += "<th style='padding: 5px; border: solid 1px #777; background-color: #F5C518;'> TOP 100 </th>";
            html += "</tr>";
            html += "</thead>";

            html += "<tbody>";
            foreach (var filme in filmes)
            {
                html += "<tr>";
                html += String.Format("<td style='padding: 5px; border: solid 1px #777; background-color: lightgray;'> <img src='{0}' width=45' height='67'> {1}. <a href='{2}'> <strong>{3}</strong> </a> ({4}) </td>", filme.Imagem, filme.Posicao, filme.Href, filme.Nome, filme.Nota);
                html += "</tr>";
            }

            html += "</tbody>";
            html += "</table>";
            html += "</body>";

            return html;
        }

    }
}
