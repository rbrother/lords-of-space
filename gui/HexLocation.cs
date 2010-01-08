using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace net.brotherus.game
{
    public class HexLocation : IComparable
    {
        public int X { get; set; }
        public int Y { get; set; }

        public HexLocation(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            HexLocation other = obj as HexLocation;
            if (other == null) return false;
            return (X == other.X && Y == other.Y);
        }

        public static bool operator ==(HexLocation a, HexLocation b)
        {
            return object.Equals(a, b);
        }

        public static bool operator !=(HexLocation a, HexLocation b)
        {
            return !object.Equals(a, b);
        }

        public int CompareTo(object obj)
        {
            HexLocation other = (HexLocation)obj;
            if (Y != other.Y) return Y.CompareTo(other.Y);
            return X.CompareTo(other.X);
        }

        public override int GetHashCode()
        {
            return new System.Drawing.Point(X, Y).GetHashCode();
        }

        public int DistanceTo(HexLocation other)
        {
            if (this == other)
            {
                return 0;
            }
            else
            {
                return 1 + StepTowards(other).DistanceTo(other);
            }
        }

        public HexLocation StepTowards(HexLocation destination)
        {
            // Find minimum orthogonal distance after step
            int minDistance = NeighbourTiles.Min(p => p.OrthogonalDistanceTo(destination));
            // Return neighbour points that leads to minimum orthogonal distance
            return NeighbourTiles.First(p => p.OrthogonalDistanceTo(destination) == minDistance);
        }

        public int OrthogonalDistanceTo(HexLocation b)
        {
            return Math.Abs(X - b.X) + Math.Abs(Y - b.Y);
        }

        public IEnumerable<HexLocation> NeighbourTiles
        {
            get
            {
                return new HexLocation[] {
                    new HexLocation(X-1, Y-1),
                    new HexLocation(X, Y-1),
                    new HexLocation(X+1, Y),
                    new HexLocation(X+1, Y+1),
                    new HexLocation(X, Y+1),
                    new HexLocation(X-1, Y)
                };
            }
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", StringUtil.Letter(X), Y);
        }


    }
}
