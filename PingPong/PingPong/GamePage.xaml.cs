using System.Runtime.CompilerServices;

namespace PingPong;

public partial class GamePage : ContentPage
{
    private Match matchData { get; set; }

    public int ElsoJatekosPont { get; set; }
    public int MasodikJatekosPont { get; set; }


    public GamePage(Match m)
    {
        InitializeComponent();
        this.BindingContext = this;
        matchData = m;
    }

    private async void PlayerScore_Tapped(object sender, EventArgs e)
    {
        var frame = (Frame)sender;
        var layout = (VerticalStackLayout)frame.Children[0];
        var label = (Label)layout.Children[1];
        if (CheckMatchEnd())
        {
            matchData.SetScore(int.Parse(p1Point.Text), int.Parse(p2Point.Text));
            App.Database.SaveMatchData(matchData);
            await Navigation.PushAsync(new AfterGamePage());
        }
        else
        {
            label.Text = (int.Parse(label.Text) + 1).ToString();
        }
    }

    private bool CheckMatchEnd()
    {
        int playerOnePoint = int.Parse(p1Point.Text);
        int playerTwoPoint = int.Parse(p2Point.Text);
        if (Math.Abs(playerOnePoint - playerTwoPoint) < 2)
            return false;
        if (playerOnePoint == 11 && playerTwoPoint < 10)
            return true;
        if (playerTwoPoint == 11 && playerOnePoint < 10)
            return true;
        return Math.Abs(playerOnePoint - playerTwoPoint) == 2 && (playerOnePoint > 10 || playerTwoPoint > 10);
    }
}