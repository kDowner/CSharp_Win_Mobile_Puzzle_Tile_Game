/*
 * FILE				: GamePage.xaml.cs
 * PROJECT			: A07 (PROG2121)
 * FIRST VERSION	: 2020-12-14 (Rev.07)
 * AUTHOR			: Dusan Sasic & Kevin Downer
 * DESCRIPTION		: The Second Game Page of the Program 'Puzzler'.
 * The Application simulates a virtual slide block game,
 * where the objective is to arrange the tiles in order.
 * This page specifically allows the User to play the moving block-number game.
 * When the User arrives at the page, a Timer for score begins and the are
 * presented with a 4x4 set of jumbled blocks to arrange in order to win.
 * Components on this page:
 * 'Timer' title, 'Timer' countdown, 'Puzzler' block-game and 'Back' button.
 */

using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using Windows.Storage;
using Windows.UI.Xaml.Navigation;


namespace A07
{
   public sealed partial class GamePage : Page
   {
      //Constants CHALLENGE LEVEL CHANGE!
      private const int MIX_LEVEL = 10;

      //Tools
      System.Timers.Timer gTimer;


      //Data Members
      public int Time { get; set; }
      private int Min { get; set; }
      private int TotalTime { get; set; }


      //Data holders
      private ObservableCollection<Tile> Tiles;
      private List<Tile> WinTiles;


      //Variables
      private int EmptyIndex;


      Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
      Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

      /* CONSTRUCTOR
      NAME        : GamePage
      DESCRIPTION : initializes the Main Game page elements for the game.
      PARAMETERS  : none
      RETURN      : void
      */
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
         Min = 0;
         TotalTime = 0;

         //Event handler
         App.Current.EnteredBackground += Current_EnteredBackground;
         App.Current.Resuming += Current_Resuming;

         //Mixes the tiles
         MixTiles();
      }


      /* FUNCTION
      NAME        : Current_Resuming
      DESCRIPTION : Resumes the tile state of the game for User.
      PARAMETERS  : object : sender object : e
      RETURN      : void
      */
      private void Current_Resuming(object sender, object e)
      {
         for (int i = 0; i < Tiles.Count; i++)
         {
            string num = localSettings.Values["tile" + i].ToString();
            Tiles[i].Number = num;
         }

         Time = (int)localSettings.Values["time"];
         gTimer.Start();
      }


      /* FUNCTION
      NAME        : Current_EnteredBackground
      DESCRIPTION : Saves the game state of the application for the User
      PARAMETERS  : object : sender EnteredBackgroundEventArgs : e
      RETURN      : void
      */
      private void Current_EnteredBackground(object sender, Windows.ApplicationModel.EnteredBackgroundEventArgs e)
      {
         gTimer.Stop();

         for (int i = 0; i < Tiles.Count; i++)
         {
            localSettings.Values["tile" + i] = Tiles[i].Number;
         }

         localSettings.Values["time"] = Time;
      }


      /* FUNCTION
      NAME        : OnNavigatedTo
      DESCRIPTION : Button click, checks the User has entered a name for the game.
      PARAMETERS  : NavigationEventArgs : e
      RETURN      : void
      */
      protected override void OnNavigatedTo(NavigationEventArgs e)
      {
         if (e.Parameter != null)
         {

            if (localSettings.Values["time"] != null)
            {
               Time = (int)localSettings.Values["time"];
            }

            this.Tiles = (ObservableCollection<Tile>)e.Parameter;

            for (int i = 0; i < Tiles.Count; i++)
            {
               Tile test = Tiles[i];
               if (Tiles[i].Number == " ")
               {
                  EmptyIndex = i;
                  break;
               }
            }
         }
         //Timer started
         gTimer.Start();
      }


      /* FUNCTION
      NAME        : GTimer_Tick
      DESCRIPTION : Timer accumulation set in motion for User.
      PARAMETERS  : object : sender ElapsedEventArgs : e
      RETURN      : void
      */
      private void GTimer_Tick(object sender, ElapsedEventArgs e)
      {
         Time++;
         TotalTime++;
         //Count up
         UpdateTimer();
      }


      /* FUNCTION
      NAME        : UpdateTimer
      DESCRIPTION : Sets up the arrangement of the time displayed to the User.
      PARAMETERS  : object : sender RoutedEventArgs : e
      RETURN      : void
      */
      private async void UpdateTimer()
      {
         var dispatcher = txt_TimeValue.Dispatcher;

         if (!dispatcher.HasThreadAccess)
         {
            await dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => { UpdateTimer(); });
         }
         else
         {
            //Minute mark
            if (Time >= 60)
            {
               Min++;
               Time = 0;
            }
            //Arrange time display to be pretty
            txt_TimeValue.Text = Min + ":" + Time;
         }
      }


      /* FUNCTION
      NAME        : GridView_ItemClick
      DESCRIPTION : Checks the arrangement of the tiles by User.
                    If arranged in order, the game is over and
                    the User is sent to the EnterName page.
      PARAMETERS  : object : sender ItemClickEventArgs : e
      RETURN      : void
      */
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
         //All in order, winner!
         if (flag)
         {
            gTimer.Stop();
            Frame.Navigate(typeof(EnterNamePage), TotalTime);
            Time = 0;
         }
      }


      /* FUNCTION
      NAME        : SwitchTiles
      DESCRIPTION : Checks the boundary area of a tile to see if the User
                    can move their tile into an empty space.
      PARAMETERS  : int : indexToSwitch
      RETURN      : void
      */
      private void SwitchTiles(int indexToSwitch)
      {
         //Flag initially false until proven otherwise
         bool canSwitch = false;

         //Boundary checks for tile possible move to space
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

         //Switch the tile position
         if (canSwitch)
         {
            Tile tempTile = Tiles[indexToSwitch];
            Console.WriteLine(Tiles[EmptyIndex].ToString());
            Tiles[indexToSwitch] = Tiles[EmptyIndex];
            Tiles[EmptyIndex] = tempTile;

            EmptyIndex = indexToSwitch;
         }
      }


      /* FUNCTION
      NAME        : MixTile
      DESCRIPTION : Mixes up the tiles for the game setup start.
      PARAMETERS  : none
      RETURN      : void
      */
      private void MixTiles()
      {
         Random ran = new Random();
         int r;
         int low_bar;
         int high_bar;

         for (int i = 0; i < MIX_LEVEL; i++)
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



      //private void btn_Win_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
      //{
      //   gTimer.Stop();
      //   Frame.Navigate(typeof(EnterNamePage), TotalTime);
      //   Time = 0;
      //}


      /* FUNCTION
      NAME        : btn_Back_Tapped
      DESCRIPTION : Button click, takes the User back to the Main starting page.
                    Suspends the current game state and puzzle block arrangement
                    in case user wants to continue later.
      PARAMETERS  : object : sender TappedRoutedEventArgs : e
      RETURN      : void
      */
      private void btn_Back_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
      {
         //Freeze the timer
         gTimer.Stop();
         //Gets the state of the blocks
         for (int i = 0; i < Tiles.Count; i++)
         {
            localSettings.Values["tile" + i] = Tiles[i].Number;
         }
         //Holds the state values
         localSettings.Values["time"] = Time;
         localSettings.Values["empty"] = EmptyIndex;
         //button enabled, go to Main Page
         bool flag = true;
         Frame.Navigate(typeof(MainPage), flag);
      }

      //private void btn_Back_Click(object sender, RoutedEventArgs e)
      //{
      //   gTimer.Stop();

      //   for (int i = 0; i < Tiles.Count; i++)
      //   {
      //      localSettings.Values["tile" + i] = Tiles[i].Number;
      //   }

      //   localSettings.Values["time"] = Time;
      //   localSettings.Values["empty"] = EmptyIndex;

      //   bool flag = true;
      //   Frame.Navigate(typeof(MainPage), flag);
      //}
   }
}
