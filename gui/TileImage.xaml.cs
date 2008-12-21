using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace net.brotherus.game {
    /// <summary>
    /// Interaction logic for TileImage.xaml
    /// </summary>
    public partial class TileImage : UserControl {

        public TileImage() {
            InitializeComponent();
        }

        private SystemType system;
        private bool selected = false;

        public void RefreshPicture() {
            if (SpaceSystem != null) {
                this.image.Source = SpaceSystem.Picture;
                this.text.Content = SpaceSystem.name;
            }
        }

        public SystemType SpaceSystem { 
            get { return this.system;  }
            set { 
                this.system = value;
                RefreshPicture();
            }
        }

        public string SystemName {
            get { return SpaceSystem.name; }
            set {
                SpaceSystem.name = value;
                this.text.Content = value;
            }
        }

        public string SystemType { get { return SpaceSystem.GetType().Name;  } }

        public bool Selected {
            get { return this.selected; }
            set {
                this.selected = value;
                if (this.selected) {
                    BitmapEffect = new OuterGlowBitmapEffect { GlowColor = Color.FromRgb(255, 255, 255), GlowSize = 30 };
                }
                else {
                    BitmapEffect = null;
                }
            }
        }

        public double placementX { get { return (SpaceSystem.x - SpaceSystem.y) * 326.0; } }

        public double placementY { get { return (SpaceSystem.x + SpaceSystem.y) * 189.0; } }


    }
}
