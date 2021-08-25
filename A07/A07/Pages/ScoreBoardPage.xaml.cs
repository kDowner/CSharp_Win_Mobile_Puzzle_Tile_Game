/*
 * FILE				: ScoreBoardPage.xaml.cs
 * PROJECT			: A07 (PROG2121)
 * FIRST VERSION	: 2020-12-14 (Rev.07)
 * AUTHOR			: Dusan Sasic & Kevin Downer
 * DESCRIPTION		: The Final Score Board Page of the Program 'Puzzler'.
 * The Application simulates a virtual slide block game,
 * where the objective is to arrange the tiles in order.
 * This page displays all the Users that have won the game and their avatars
 * and times for completion in a scrolling window.
 * Components on this page:
 * 'best Times' title, 'User' avatar, 'User' name and times, 'Main Menu' button.
 */

using A07.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace A07
{
   public sealed partial class ScoreBoardPage : Page
   {
      //Initialization
      private ObservableCollection<Record> Records = new ObservableCollection<Record>();
      private Record LastRecord;
      Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;


      /* CONSTRUCTOR
      NAME        : ScoreBoardPage
      DESCRIPTION : initializes the Score Board page and suspension check for the game.
      PARAMETERS  : none
      RETURN      : void
      */
      public ScoreBoardPage()
      {
         this.InitializeComponent();

         App.Current.Suspending += Current_Suspending;
      }


      /* FUNCTION
      NAME        : Current_Suspending
      DESCRIPTION : Initiates storting the info. for the suspend state.
      PARAMETERS  : object : sender SuspendingEventArgs : e
      RETURN      : void
      */
      private void Current_Suspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
      {
         WriteToFile();
      }


      /* FUNCTION
      NAME        : WriteToFile
      DESCRIPTION : Appends the score data to the existing score storage file.
      PARAMETERS  : none
      RETURN      : void
      */
      private async void WriteToFile()
      {
         StorageFile sampleFile = await localFolder.CreateFileAsync("dataFile.txt", CreationCollisionOption.OpenIfExists);

         await FileIO.AppendTextAsync(sampleFile, LastRecord.Name + "[||]" + LastRecord.Path + "[||]" + LastRecord.Time + "\n");
      }


      /* FUNCTION
      NAME        : OnNavigatedTo
      DESCRIPTION : Navigates to the records and checks if existing, otherwise creates.
      PARAMETERS  : NavigationEventArgs : e
      RETURN      : void
      */
      protected override void OnNavigatedTo(NavigationEventArgs e)
      {
         if (e.Parameter != null)
            LastRecord = (Record)e.Parameter;
         else
            LastRecord = new Record();

         Records = LastRecord.GetRecords();
      }


      /* FUNCTION
      NAME        : btn_BackToMain_Tapped
      DESCRIPTION : Button click, moves User back to Main Game page.
      PARAMETERS  : object : sender TappedRoutedEventArgs : e
      RETURN      : void
      */
      private void btn_BackToMain_Tapped(object sender, TappedRoutedEventArgs e)
      {
         bool flag = false;
         Frame.Navigate(typeof(MainPage), flag);
      }

      //private void btn_BackToMain_PointerPressed(object sender, PointerRoutedEventArgs e)
      //{
      //   bool flag = false;
      //   Frame.Navigate(typeof(MainPage), flag);
      //}
   }
}
