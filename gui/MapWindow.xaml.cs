using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Xml.Serialization;
using net.brotherus.game;

namespace net.brotherus.game {

    internal class MouseCapture {
        public Double VerticalOffset { get; set; }
        public Double HorizontalOffset { get; set; }
        public Point Point { get; set; }
    }

    public partial class MapWindow : Window 
    {
        private Game _gameData;
        private bool _dragging;
        private MouseCapture _dragCapture;

        public MapWindow() 
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            InitializeSystemList();
            if (File.Exists(App.Configuration.CurrentGameFileName))
            {
                LoadGameData(App.Configuration.CurrentGameFileName);
            }
            else
            {
                GameData = new Game();
            }
        }

        void MapCanvas_MouseDown( object sender, MouseButtonEventArgs e ) {
            _dragCapture = new MouseCapture {
                VerticalOffset = MapScroller.VerticalOffset,
                HorizontalOffset = MapScroller.HorizontalOffset,
                Point = e.GetPosition( MapScroller ),
            };
            _dragging = true;
        }

        void MapCanvas_MouseUp( object sender, MouseButtonEventArgs e ) {
            if ( _dragging ) {
                _dragging = false;
                var pos = e.GetPosition( this );
                var dy = pos.Y - _dragCapture.Point.Y;
                var dx = pos.X - _dragCapture.Point.Y;
                if ( Math.Abs( dx ) > 5 || Math.Abs( dy ) > 5 ) { 
                    // Prevent interpretation as a click if we really dragged
                }
            }
        }

        void MapCanvas_MouseMove( object sender, MouseEventArgs e ) {
            if ( e.LeftButton != MouseButtonState.Pressed ) {
                _dragging = false;
                return;
            }
            if ( _dragging ) {
                var pos = e.GetPosition( this );
                var dy = pos.Y - _dragCapture.Point.Y;
                var dx = pos.X - _dragCapture.Point.Y;
                MapScroller.ScrollToVerticalOffset( _dragCapture.VerticalOffset - dy );
                MapScroller.ScrollToHorizontalOffset( _dragCapture.HorizontalOffset - dx );
            }
        }

        public Game GameData
        {
            get { return this._gameData; }
            set
            {
                this._gameData = value;
                this.mapCanvas.Game = this._gameData;
            }
        }

        public string CurrentGameFileName
        {
            get { return App.Configuration.CurrentGameFileName;  }
            set {
                App.Configuration.CurrentGameFileName = value;
                this.Title = "Lords Of Space " + value;
            }
        }

        private void LoadGameData(string gameDataFileName)
        {
            GameData = XmlIO.LoadXml<Game>(gameDataFileName);
            CurrentGameFileName = gameDataFileName;            
        }

        private void SaveGameData(string gameDataFileName)
        {
            XmlIO.SaveXml(gameDataFileName, GameData);
            CurrentGameFileName = gameDataFileName;            
        }

        private void InitializeSystemList() {
            foreach (var system in App.Configuration.Systems.OfType<PlanetSystem>()) 
            {
                if (!(system is HomeSystem)) {
                    this.tileSystemCombo.Items.Add(system);
                }
            }
            foreach (var system in App.Configuration.Systems.OfType<HomeSystem>())
            {
                this.homeSystemCombo.Items.Add(system);
            }
            foreach (var system in App.Configuration.Systems.OfType<EmptySystem>())
            {
                this.specialSystemCombo.Items.Add(system);
            }
            foreach (var system in App.Configuration.Systems.OfType<RedSystem>())
            {
                this.specialSystemCombo.Items.Add(system);
            }
        }

        private void Window_Closed(object sender, EventArgs e) 
        {
            if (!string.IsNullOrEmpty(CurrentGameFileName))
            {
                try
                {
                    SaveGameData(CurrentGameFileName);
                }
                catch (DirectoryNotFoundException)
                {
                    CurrentGameFileName = null;
                }
            }
            App.SaveConfiguration();
        }

        /// <summary>
        /// Change system of the selected tile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tileSystem_SelectionChanged(object sender, SelectionChangedEventArgs e) 
        {
            ComboBox combo = (ComboBox) sender;
            SystemType newSystem = (SystemType) combo.SelectedValue;
            if (newSystem != null) 
            {
                newSystem = newSystem.CloneThroughXML();
                newSystem.Location = SelectedSystem.Location;
                SelectedSystem = newSystem;
                combo.SelectedValue = null;
            }
        }

        public SystemType SelectedSystem
        {
            get { return this.mapCanvas.SelectedSystem; }
            set { this.mapCanvas.SelectedSystem = value; }
        }

        private void MapSizeClicked(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            int size = Convert.ToInt32(menuItem.Tag);
            GameData = new Game(size);
        }

        private void OpenGameClicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Open Game";
            dialog.InitialDirectory = App.AppFolder + @"\Games";
            dialog.Filter = "Game XML files|*.xml";
            dialog.ShowDialog();
            LoadGameData(dialog.FileName);
        }

        private void SaveGameClicked(object sender, RoutedEventArgs e)
        {
            if (CurrentGameFileName != null)
            {
                SaveGameData(CurrentGameFileName);
            }
            else
            {
                SaveGameAsClicked(sender, e);
            }
        }

        private void SaveGameAsClicked(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Save Game";
            dialog.InitialDirectory = App.AppFolder + @"\Games";
            dialog.Filter = "Game XML files|*.xml";
            dialog.ShowDialog();
            SaveGameData(dialog.FileName);
        }

    } // class

} // namespace
