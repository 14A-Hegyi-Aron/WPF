namespace PingPong;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}

	private async void Button_Clicked(object sender, EventArgs e)
	{
		if(!String.IsNullOrEmpty(playerOneEnt.Text) && !String.IsNullOrEmpty(playerTwoEnt.Text))
		{
			await Navigation.PushAsync(new GamePage(new Match(playerOneEnt.Text, playerTwoEnt.Text)));
		}
		else
		{
			await DisplayAlert("HIBA!", "A játékosok nevét kötelező megadni!", "OK");
		}
	}
}

