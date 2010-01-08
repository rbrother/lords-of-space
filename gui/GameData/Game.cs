using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace net.brotherus.game
{
    public partial class Game
    {
        public Game()
            : this(7)
        {
        }

        public Game(int size)
        {
            CreateEmptyMap(size);
        }

        public void CreateEmptyMap(int size)
        {
            List<SystemType> systems = new List<SystemType>();
            int radius = size / 2;
            HexLocation center = new HexLocation(radius, radius);
            for (int y = 0; y <= size; ++y)
            {
                for (int x = 0; x <= size; ++x)
                {
                    HexLocation loc = new HexLocation(x, y);
                    int distanceFromCenter = loc.DistanceTo(center);
                    if (distanceFromCenter <= radius)
                    {
                        systems.Add(App.CreateSystem(SetupTileName(distanceFromCenter), loc, loc.ToString()));
                    }
                }
            }
            this.Map = systems.ToArray();
        }

        private string SetupTileName(int distanceFromCenter)
        {
            if (distanceFromCenter <= 3)
            {
                string[] tileNames = new string[] 
                {
                    "Setup-Yellow", "Setup-Yellow", "Setup-Red", "Setup-Blue"
                };
                return tileNames[distanceFromCenter];
            }
            else
            {
                return "Setup-DarkBlue";
            }
        }

    }

}
