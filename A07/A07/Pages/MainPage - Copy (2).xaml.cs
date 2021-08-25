using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace A07
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {


        public MainPage()
        {
        
            this.InitializeComponent();

        }

        private void btn_StartGame_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(GamePage));
        }

        private void btn_ScoreBoard_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(ScoreBoardPage), null);
        }

        private void btn_Exit_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void btn_ContinueGame_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(GamePage));

        }

      


        //Frame.Navigate(typeof(GamePage));
        //Frame.Navigate(typeof(ScoreBoardPage), null);
        //Application.Current.Exit();





    }
}
