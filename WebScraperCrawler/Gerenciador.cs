using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WebScraperCrawler
{
    public class Gerenciador
    {

        string Link = "https://www.imdb.com/chart/moviemeter/";
        string XmlPath = "//*[@id='main']/div/span/div/div/div[3]/table/tbody/tr[position()>0]";

        public void Iniciar()
        {
            List<Filme> Filme = new List<Filme>();

            var web = new HtmlWeb();

            var doc = web.Load(Link);

            var nodes = doc.DocumentNode.SelectNodes(XmlPath);

            int index = 1;
            foreach (var node in nodes)
            {
                string Nota;
                if (node.SelectSingleNode("td[3]").InnerText.Replace("\n", "").Trim() == "")
                {
                    Nota = "SEM NOTA";
                }
                else
                {
                    Nota = node.SelectSingleNode("td[3]/strong").InnerText;
                }

                var filme = new Filme
                {
                    Posicao = index.ToString(),
                    Nome = node.SelectSingleNode("td[2]/a").InnerText,
                    Nota = Nota,
                    Href = "imdb.com" + node.SelectSingleNode("td[2]/a").OuterHtml.Split('\"')[1],
                    Imagem = node.SelectSingleNode("td[1]/a").OuterHtml.Split('\"')[3]
                };

                index++;
                Filme.Add(filme);
            }

            //Filme.ForEach(x => Console.Write("{0}. {1} ({2})\n", x.Posicao, x.Nome, x.Nota));

            EnviadorEmail enviar = new EnviadorEmail();
            enviar.EnviarEmail(Filme);

        }

    }
}
