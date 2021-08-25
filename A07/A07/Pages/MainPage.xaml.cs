/*
 * FILE				: MainPage.xaml.cs
 * PROJECT			: A07 (PROG2121)
 * FIRST VERSION	: 2020-12-14 (Rev.07)
 * AUTHOR			: Dusan Sasic & Kevin Downer
 * DESCRIPTION		: The Main start Page of the Program 'Puzzler'.
 * The Application simulates a virtual slide block game,
 * where the objective is to arrange the tiles in order.
 * This page specifically welcomes the User to the game application.
 * Components on this page:
 * There is a Title of the game, and four main button:
 * 'Start the Game', 'Continue', 'Scoreboard' and 'Exit' buttons.
 * The User chooses the action by activating one of the buttons.
 */

using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;


namespace A07
{
   public sealed partial class MainPage : Page
   {
      //Initialization
      public bool enabled { get; set; }


      /* CONSTRUCTOR
      NAME        : MainPage
      DESCRIPTION : initializes the Start page for the game.
      PARAMETERS  : none
      RETURN      : void
      */
      public MainPage()
      {
         this.InitializeComponent();
      }


      /* FUNCTION
      NAME        : btn_StartGame_Tapped
      DESCRIPTION : Button click, navigates to the Game Page state.
      PARAMETERS  : object : sender TappedRoutedEventArgs : e
      RETURN      : void
      */
      private void btn_StartGame_Tapped(object sender, TappedRoutedEventArgs e)
      {
         enabled = false;
         Frame.Navigate(typeof(GamePage));
      }

      //private void btn_StartGame_Click(object sender, RoutedEventArgs e)
      //{
      //   enabled = false;
      //   Frame.Navigate(typeof(GamePage));
      //}

      //private void btn_StartGame_PointerPressed(object sender, PointerRoutedEventArgs e)
      //{
      //   enabled = false;
      //   Frame.Navigate(typeof(GamePage));
      //}


      /* FUNCTION
      NAME        : btn_ContinueGame_Tapped
      DESCRIPTION : Button click, resumes the User's game at the state it was paused.
      PARAMETERS  : object : sender TappedRoutedEventArgs : e
      RETURN      : void
      */
      private void btn_ContinueGame_Tapped(object sender, TappedRoutedEventArgs e)
      {
         //State elements
         Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
         Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

         //Object Tiles holder prepped
         ObservableCollection<Tile> Tiles = new ObservableCollection<Tile>();

         //Temp tiles holder add string setup
         for (int i = 0; i < 16; i++)
         {
            string num = localSettings.Values["tile" + i].ToString();
            Tile tempTile = new Tile(num);
            Tiles.Add(tempTile);
         }

         //Game page anew
         GamePage gp = new GamePage();

         //Navigate to Game Page
         Frame.Navigate(typeof(GamePage), Tiles);
      }

      //private void btn_ContinueGame_Click(object sender, RoutedEventArgs e)
      //{

      //   Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
      //   Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

      //   ObservableCollection<Tile> Tiles = new ObservableCollection<Tile>();

      //   for (int i = 0; i < 16; i++)
      //   {
      //      //int num = (int)localSettings.Values["tile" + i];
      //      string num = localSettings.Values["tile" + i].ToString();
      //      Tile tempTile = new Tile(num);
      //      Tiles.Add(tempTile);
      //   }

      //   GamePage gp = new GamePage();

      //   Frame.Navigate(typeof(GamePage), Tiles);
      //}


      /* FUNCTION
      NAME        : btn_ScoreBoard_Tapped
      DESCRIPTION : Button click, navigates to the Score Board results page.
      PARAMETERS  : object : sender TappedRoutedEventArgs : e
      RETURN      : void
      */
      private void btn_ScoreBoard_Tapped(object sender, TappedRoutedEventArgs e)
      {
         Frame.Navigate(typeof(ScoreBoardPage), null);
      }

      //private void btn_ScoreBoard_PointerPressed(object sender, PointerRoutedEventArgs e)
      //{
      //   Frame.Navigate(typeof(ScoreBoardPage), null);
      //}


      /* FUNCTION
      NAME        : btn_Exit_Tapped
      DESCRIPTION : Button click, closes the application gracefully.
      PARAMETERS  : object : sender TappedRoutedEventArgs : e
      RETURN      : void
      */
      private void btn_Exit_Tapped(object sender, TappedRoutedEventArgs e)
      {
         Application.Current.Exit();
      }

      //private void btn_Exit_PointerPressed(object sender, PointerRoutedEventArgs e)
      //{
      //   Application.Current.Exit();
      //}


      /* FUNCTION
      NAME        : OnNavigatedTo
      DESCRIPTION : Check for the continue game state.
      PARAMETERS  : NavigationEventArgs : e
      RETURN      : void
      */
      protected override void OnNavigatedTo(NavigationEventArgs e)
      {
         if (e.Parameter != "")
         {
            btn_ContinueGame.IsEnabled = (bool)e.Parameter;
            enabled = (bool)e.Parameter;
         }
      }
   }
}
