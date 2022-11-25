namespace PingPong;

public partial class ResultPage : ContentPage
{
	public ResultPage()
	{
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		int sor = 0;
		var matchList = App.Database.GetAllMatches();
		foreach (var match in matchList)
		{
			Label label = new Label()
			{
				HorizontalTextAlignment = TextAlignment.Center,
				TextColor = Colors.White,
				FontSize = 20,
				Background = sor % 2 == 0 ? Colors.Green : Colors.DarkGreen,
				Text = $"{match.Idopont:yyyy MMMM dd HH:mm}\n{match.ElsoJatekosNev} {match.ElsoJatekosPont} - {match.MasodikJatekosPont} {match.MasodikJatekosNev}",
			};
			layoutVs.Children.Add(label);
			sor++;
		}
	}

}