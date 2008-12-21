using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace net.brotherus.game
{

    public class MapCanvas : Canvas
    {
        private Game game;

        public static readonly DependencyProperty SelectedTileProperty = DependencyProperty.Register(
          "SelectedTile",
          typeof(TileImage),
          typeof(MapCanvas),
          new FrameworkPropertyMetadata()
        );

        public TileImage SelectedTile
        {
            get { return (TileImage)this.GetValue(SelectedTileProperty); }
            set { this.SetValue(SelectedTileProperty, value); }
        }

        public MapCanvas()
        {
            ScalePercent = 50;
        }

        public Game Game
        {
            get { return this.game; }
            set
            {
                this.game = value;
                Children.Clear();
                foreach (SystemType system in this.game.Map)
                {
                    var tileImage = new TileImage { SpaceSystem = system };
                    tileImage.RefreshPicture();
                    tileImage.MouseUp += new System.Windows.Input.MouseButtonEventHandler(Tile_MouseUp);
                    Children.Add(tileImage);
                }
            }
        }

        public SystemType SelectedSystem
        {
            get { return SelectedTile.SpaceSystem; }
            set {
                // Update the new system to the Map array the system in the selected location to the new one
                List<SystemType> systems = Game.Map.Where(system => system.Location != value.Location).ToList();
                systems.Add(value);
                systems.Sort();
                Game.Map = systems.ToArray();
                // Update the system to the GUI as well to make it visible
                SelectedTile.SpaceSystem = value; 
            }
        }

        void Tile_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (SelectedTile != null)
            {
                SelectedTile.Selected = false;
                Canvas.SetZIndex(SelectedTile, 0);
            }
            var newTile = (TileImage)sender;
            SelectedTile = newTile;
            Canvas.SetZIndex(SelectedTile, 10);
            newTile.Selected = true;
        }

        public double ScalePercent { set { LayoutTransform = new ScaleTransform(value / 100.0, value / 100.0); } }

        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (UIElement content in Children)
            {
                var tile = content as TileImage;
                if (tile != null)
                {
                    tile.Measure(new Size { Width = double.PositiveInfinity, Height = double.PositiveInfinity });
                }
            }
            return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            double minX = double.MaxValue;
            double minY = double.MaxValue;
            double maxX = double.MinValue;
            double maxY = double.MinValue;
            foreach (UIElement content in Children)
            {
                var tile = content as TileImage;
                if (tile != null)
                {
                    minX = Math.Min(minX, tile.placementX);
                    minY = Math.Min(minY, tile.placementY);
                    maxX = Math.Max(maxX, tile.placementX + tile.DesiredSize.Width);
                    maxY = Math.Max(maxY, tile.placementY + tile.DesiredSize.Height);
                }
            }
            foreach (UIElement content in Children)
            {
                var tile = content as TileImage;
                if (tile != null)
                {
                    Canvas.SetLeft(tile, tile.placementX - minX);
                    Canvas.SetTop(tile, tile.placementY - minY);
                }
            }
            Width = maxX - minX;
            Height = maxY - minY;
            return base.ArrangeOverride(arrangeBounds);
        }


    } // class

} // namespace
