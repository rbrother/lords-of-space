using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace net.brotherus.game
{
    partial class Configuration
    {
        /// <summary>
        /// Create planet system through cloning, so that connection the the planetsystem "template" is severed
        /// </summary>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public SystemType CreateSystem(string name, HexLocation location, string title)
        {
            SystemType spaceSystem = Systems.Single(s => s.name == name).CloneThroughXML();
            spaceSystem.Location = location;
            spaceSystem.name = title;
            return spaceSystem;
        }
    } // class

} // namespace
