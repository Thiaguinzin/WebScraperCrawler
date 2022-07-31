using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebScraperCrawler
{
    public partial class Service : ServiceBase
    {
        public Service()
        {
            InitializeComponent();
            ServiceName = "WebScraperCrawler";
        }
        public void Start(string[] args)
        {
            OnStart(args);
        }

        protected override void OnStart(string[] args)
        {
            var th = new Thread(new Gerenciador().Iniciar)
            {
                Priority = ThreadPriority.Lowest,
                IsBackground = false
            };
            th.Start();

        }

        protected override void OnStop()
        {
        }
    }
}
