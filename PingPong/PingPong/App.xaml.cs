using PingPong.Data;
using System.Collections.ObjectModel;

namespace PingPong;

public partial class App : Application
{
	static Database database;


	public static Database Database
	{
		get
		{
			if(database == null)
			{
				database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PingPong.db3"));
			}
			return database;
		}
	}


	public App()
	{
		InitializeComponent();

		MainPage = new NavigationPage(new MainPage());
	}
}
