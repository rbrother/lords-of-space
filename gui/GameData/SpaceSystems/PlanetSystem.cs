using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace net.brotherus.game {

    public partial class PlanetSystem {

        public override string ToString() {
            if (this.Planet != null && this.Planet.Length > 0) {
                return String.Join(" - ", this.Planet.Select(p => p.name).ToArray());
            }
            else {
                return base.name;
            }
        }

    } // class

} // namespace
