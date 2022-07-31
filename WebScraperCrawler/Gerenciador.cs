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
        
        class Filme
        {
            public string Posicao { get; set; }
            public string Nome { get; set; }
            public string Nota { get; set; }
        }

        public void Iniciar()
        {
            var Filme = new List<Filme>();

            var web = new HtmlWeb();

            var doc = web.Load(Link);

            var nodes = doc.DocumentNode.SelectNodes(XmlPath);

            int index = 1;
            foreach (var node in nodes)
            {
                string Nota;
                if (node.SelectSingleNode("td[3]").InnerText.Replace("\n","").Trim() == "")
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
                    Nota = Nota
                };

                index++;
                Filme.Add(filme);
            }

            Filme.ForEach(x => Console.Write("{0}. {1} ({2})\n", x.Posicao, x.Nome, x.Nota));

        }

    }
}
