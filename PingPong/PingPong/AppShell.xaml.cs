namespace PingPong;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute("gameScreen", typeof(GamePage));
		Routing.RegisterRoute("gameOverScreen", typeof(GameOverPage));
		Routing.RegisterRoute("resultPage", typeof(ResultPage));

	}
}
