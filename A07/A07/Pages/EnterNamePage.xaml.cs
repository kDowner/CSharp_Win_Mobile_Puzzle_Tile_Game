/*
 * FILE				: EnterNamePage.xaml.cs
 * PROJECT			: A07 (PROG2121)
 * FIRST VERSION	: 2020-12-14 (Rev.07)
 * AUTHOR			: Dusan Sasic & Kevin Downer
 * DESCRIPTION		: The Third Enter Name Page of the Program 'Puzzler'.
 * The Application simulates a virtual slide block game,
 * where the objective is to arrange the tiles in order.
 * This page allows the User to choose an avatar and enter their name or handle
 * for the final page score board entry.
 * Components on this page:
 * 'You Win' title, 'Enter Name' text, 'Name' textblock,
 * 'Avatar' selection, 'Add' button.
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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace A07
{
   public sealed partial class EnterNamePage : Page
   {
      //Initialization
      private List<Icon> Icons;
      private Record NewRecord;
      private int Time;

      public EnterNamePage()
      {
         this.InitializeComponent();

         Icons = new List<Icon>();
         Icons.Add(new Icon { IconPath = "../Assets/ProfileIcons/male-01.png" });
         Icons.Add(new Icon { IconPath = "../Assets/ProfileIcons/male-02.png" });
         Icons.Add(new Icon { IconPath = "../Assets/ProfileIcons/male-03.png" });
         Icons.Add(new Icon { IconPath = "../Assets/ProfileIcons/female-01.png" });
         Icons.Add(new Icon { IconPath = "../Assets/ProfileIcons/female-02.png" });
         Icons.Add(new Icon { IconPath = "../Assets/ProfileIcons/female-03.png" });

         AvatarComboBox.SelectedIndex = 0;
      }


      protected override void OnNavigatedTo(NavigationEventArgs e)
      {
         int recordTime = (int)e.Parameter;
         Time = recordTime;
      }


      private void AddRecord_Tapped(object sender, TappedRoutedEventArgs e)
      {
         string name = txt_Name.Text;
         string icon_path = ((Icon)AvatarComboBox.SelectedValue).IconPath;

         NewRecord = new Record(name, icon_path, Time);
         NewRecord.AddRecord(NewRecord);

         txt_Name.Text = "";
         AvatarComboBox.SelectedIndex = -1;
         txt_Name.Focus(FocusState.Programmatic);

         Frame.Navigate(typeof(ScoreBoardPage), NewRecord);
      }

      //private void AddRecord_PointerPressed(object sender, PointerRoutedEventArgs e)
      //{
      //   string name = txt_Name.Text;
      //   string icon_path = ((Icon)AvatarComboBox.SelectedValue).IconPath;

      //   NewRecord = new Record(name, icon_path, Time);
      //   NewRecord.AddRecord(NewRecord);

      //   txt_Name.Text = "";
      //   AvatarComboBox.SelectedIndex = -1;
      //   txt_Name.Focus(FocusState.Programmatic);

      //   Frame.Navigate(typeof(ScoreBoardPage), NewRecord);
      //}
   }
}
