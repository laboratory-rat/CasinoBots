using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CasinoBots.Modules
{
    class Connector
    {
        private static Connector _instance { get; set; } = null;
        public static Connector Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Connector();
                return _instance;
            }
            set { _instance = value; }
        }

        public string Key { get; set; } = "a0e97110-9259-4349-a0e7-9276bf40f642";

        protected HttpClient _client = new HttpClient();


    }
}
