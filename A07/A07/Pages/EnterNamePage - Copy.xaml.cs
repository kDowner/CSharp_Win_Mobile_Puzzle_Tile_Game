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


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace A07
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 



    public sealed partial class EnterNamePage : Page
    {

        private List<Icon> Icons;

        private Record NewRecord;
        private int Time;

        public EnterNamePage()
        {
            this.InitializeComponent();

            //Time = time;

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
            int recordTime = (int) e.Parameter;
            Time = recordTime;
        }




        private void AddRecord_PointerPressed(object sender, PointerRoutedEventArgs e)
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
    }
}
