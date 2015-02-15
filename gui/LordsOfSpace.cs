using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace net.brotherus.game {
    public class LordsOfSpace {

        [STAThread]
        public static void Main() {
            var app = new Application( );
            app.Run( new MapWindow( ) );
        }
    }
}
