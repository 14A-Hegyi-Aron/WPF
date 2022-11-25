namespace PingPong;

public partial class GameOverPage : ContentPage
{
	public GameOverPage()
	{
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		Match utolso = App.Database.GetLastMatch();
		winnerNameLbl.Text = $"Gyõztes:\n{(utolso.ElsoJatekosPont > utolso.MasodikJatekosPont ? utolso.ElsoJatekosNev : utolso.MasodikJatekosNev)}";
		scoresLbl.Text = $"{utolso.ElsoJatekosPont} - {utolso.MasodikJatekosPont}";
	}

	private async void NewGame_Clicked(object sender, EventArgs e)
	{
		await Navigation.PopToRootAsync();
	}

    private void Quit_Clicked(object sender, EventArgs e)
    {
		Application.Current.Quit();
    }

}