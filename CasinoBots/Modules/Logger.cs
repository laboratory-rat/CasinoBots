using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CasinoBots.Modules
{
    class Logger
    {

        #region Instance
        protected static Logger _instance { get; set; } = null;
        public static Logger Inistance
        {
            get
            {
                if (_instance == null)
                    _instance = new Logger();

                return _instance;
            }

            set { _instance = value; }
        }
        #endregion

        public static string HHash = "";
        public static void RandomiseHHash()
        {
            HHash = DateTime.UtcNow.GetHashCode().ToString();
        }

        #region Public

        public bool SessionActive { get; private set; }
        public int SessionNumber { get; set; }

        public LoggerContainer Container { get; set; }

        #endregion

        #region Private

        #endregion

        #region Const

        public const string SAVE_DIR = "Sessions";

        #endregion

        public Logger()
        {
            NewSession();
        }

        public void NewSession()
        {
            if (SessionActive)
                CloseSession();

            OpenSession();
        }

        public void OpenSession()
        {
            SessionNumber++;
            SessionActive = true;

            Container = new LoggerContainer(SessionNumber);
        }

        public void CloseSession()
        {
            SessionActive = false;
            
            if(Container != null)
            {
                Container.Finish();
                SaveSession();

                Container = null;
            }
        }

        public void SaveSession()
        {
            string folder = Path.Combine(SAVE_DIR, HHash);
            string file = Path.Combine(folder, Container.Number.ToString());

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(typeof(LoggerContainer));
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, Container);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(file);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Can not write session log!");
                Debug.Write(ex);
            }
        }

    }
        

    public class LoggerContainer
    {
        public int Number { get; set; }
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime FinishTime { get; set; }

        public List<LoggerMessage> Messages { get; set; } = new List<LoggerMessage>();

        public LoggerContainer(int number)
        {
            Number = number;
        }

        public LoggerContainer()
        {

        }

        public void Add(string message)
        {
            Messages.Add(new LoggerMessage(message));
        }

        public void Finish()
        {
            FinishTime = DateTime.UtcNow;
        }

    }

    public class LoggerMessage
    {
        public DateTime Time { get; set; }
        public string Message { get; set; }

        public LoggerMessage(string message)
        {
            Message = message;
        }

        public LoggerMessage()
        {

        }
    }
}
