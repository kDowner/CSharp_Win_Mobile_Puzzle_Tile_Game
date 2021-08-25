using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using Windows.Storage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace A07
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {


        //Constants
        private const int MIX_LEVEL = 20;

        //Tools
        System.Timers.Timer gTimer;

        //Data Members
        private int Time { get; set; }


        //Data holders
        private ObservableCollection<Tile> Tiles;
        private List<Tile> WinTiles;


        //Variables
        private int EmptyIndex;



        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        


        public GamePage()
        {

            this.InitializeComponent();


            //Initialization
            Tiles = new ObservableCollection<Tile>(TileManager.GetTiles());
            WinTiles = TileManager.GetTiles();
            

            //Setup the timer
            gTimer = new System.Timers.Timer();
            gTimer.Elapsed += GTimer_Tick;
            gTimer.Interval = 1000;
            gTimer.Enabled = true;
            gTimer.Start();


            //Initialization
            EmptyIndex = 15;
            Time = 0;


            //Event handler
            App.Current.EnteredBackground += Current_EnteredBackground;
            App.Current.Resuming += Current_Resuming;

            // Create sample file; replace if exists.

        }

        private void Current_Resuming(object sender, object e)
        {
            for(int i=0; i<Tiles.Count; i++)
            {
                //int num = (int)localSettings.Values["tile" + i];
                string num = localSettings.Values["tile" + i].ToString();
                Tiles[i].Number = num;
            }

            Time = (int) localSettings.Values["time"];
            gTimer.Start();
        }


        private void Current_EnteredBackground(object sender, Windows.ApplicationModel.EnteredBackgroundEventArgs e)
        {

            gTimer.Stop();

            for(int i=0; i<Tiles.Count; i++)
            {
                localSettings.Values["tile"+i] = Tiles[i].Number;
            }

            localSettings.Values["time"] = Time;
        }



        //Save Settings



        private void GTimer_Tick(object sender, ElapsedEventArgs e)
        {
            Time++;
            UpdateTimer();
        }



        private async void UpdateTimer()
        {

            var dispatcher = txt_TimeValue.Dispatcher;
            if (!dispatcher.HasThreadAccess)
            {
                await dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => { UpdateTimer(); });

            }
            else
            {
                txt_TimeValue.Text = Time.ToString();
            }

        }




      




        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {

            var tile = (Tile)e.ClickedItem;

            int ClickedIndex = Tiles.IndexOf(tile);


            SwitchTiles(ClickedIndex);

            bool flag = true;

            for (int i = 0; i < Tiles.Count - 1; i++)
            {
                int currNum = i + 1;

                if (Tiles[i].Number != currNum.ToString())
                    flag = false;
            }

            if (flag)
            {
                gTimer.Stop();
                Frame.Navigate(typeof(EnterNamePage), Time);
                Time = 0;
            }

        }





        private void SwitchTiles(int indexToSwitch)
        {

            

            bool canSwitch = false;

            if ((indexToSwitch + 1) == EmptyIndex)
            {
                if (EmptyIndex != 4 && EmptyIndex != 8 && EmptyIndex != 12)
                    canSwitch = true;
            }
            else if ((indexToSwitch - 1) == EmptyIndex)
            {
                if (EmptyIndex != 3 && EmptyIndex != 7 && EmptyIndex != 11)
                    canSwitch = true;
            }
            else if ((indexToSwitch + 4) == EmptyIndex)
            {
                canSwitch = true;
            }
            else if ((indexToSwitch - 4) == EmptyIndex)
            {
                canSwitch = true;
            }

            if (canSwitch)
            {
                Tile tempTile = Tiles[indexToSwitch];
                Tiles[indexToSwitch] = Tiles[EmptyIndex];
                Tiles[EmptyIndex] = tempTile;

                EmptyIndex = indexToSwitch;
           
            }

            
        }



        private void MixTiles()
        {
            Random ran = new Random();
            int r;
            int low_bar;
            int high_bar;

            for(int i=0; i<MIX_LEVEL; i++)
            {
                low_bar = EmptyIndex - 4;
                if (low_bar < 0)
                    low_bar = 0;
                high_bar = EmptyIndex + 5;
                if (high_bar > 16)
                    high_bar = 16;

                r = ran.Next(low_bar, high_bar);
                SwitchTiles(r);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MixTiles();
        }

        

        private void btn_Win_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            gTimer.Stop();
            Frame.Navigate(typeof(EnterNamePage), Time);
            Time = 0;
        }
    }


}
