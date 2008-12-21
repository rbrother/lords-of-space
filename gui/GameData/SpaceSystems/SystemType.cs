using System;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace net.brotherus.game {
    
    public partial class SystemType : IComparable {

        protected static string PicsFolder { get { return App.AppFolder + "/pics"; } }

        public SystemType() {
        }

        private BitmapImage picture = null;

        [System.Xml.Serialization.XmlIgnore]
        public BitmapImage Picture
        {
            get {
                if (this.picture == null) {
                    this.picture = new BitmapImage(PicUrl);
                }
                return this.picture;
            }
        }

        [System.Xml.Serialization.XmlIgnore]
        protected Uri PicUrl { get { return new Uri(PicsFolder + "/" + this.picFileName); } }

        [System.Xml.Serialization.XmlIgnore]
        public HexLocation Location
        {
            get { return new HexLocation(this.x, this.y);  }
            set { 
                this.x = value.X;
                this.xSpecified = true;
                this.y = value.Y;
                this.ySpecified = true;
            }
        }

        public override string ToString() { return this.name == null ? "" : this.name; }

        public int CompareTo(object obj)
        {
            SystemType other = obj as SystemType;
            return Location.CompareTo(other.Location);
        }

    } // class

} // namespace
