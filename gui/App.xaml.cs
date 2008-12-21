using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace net.brotherus.game {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application 
    {
        //private static XmlSerializer configSerializer = new XmlSerializer(typeof(Configuration));

        private static Configuration gameConfiguration = null;

        public static string AppFolder
        {
            get
            {
                string codeBase = new Uri(typeof(App).Assembly.CodeBase).AbsolutePath;
                return new FileInfo(codeBase).Directory.FullName;
            }
        }

        private static string ConfigurationFileName { get { return AppFolder + @"\Configuration.xml"; } }

        public static SystemType CreateSystem(string name, HexLocation location, string description)
        {
            return Configuration.CreateSystem(name, location, description);
        }

        public static Configuration Configuration 
        {
            get
            {
                if (gameConfiguration == null)
                {
                    gameConfiguration = XmlIO.LoadXml<Configuration>(ConfigurationFileName);
                }
                return gameConfiguration;
            }
        }

        public static void SaveConfiguration()
        {
            XmlIO.SaveXml(ConfigurationFileName, Configuration);
        }

    } // class

} // namespace
