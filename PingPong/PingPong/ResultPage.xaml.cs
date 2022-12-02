using PingPong.ViewModels;

namespace PingPong;

public partial class ResultPage : ContentPage
{
    MatchListModelView model;
    public ResultPage()
	{
		InitializeComponent();
        model = new MatchListModelView();
    }

	protected override void OnAppearing()
	{
        base.OnAppearing();
        this.BindingContext = model;
        model.GetData();
        lstView.ItemsSource = model.eredmenyek;
    }

	private async void lstView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		Match kivalasztott = (Match)lstView.SelectedItem;
		if (await DisplayAlert("Meger�s�t�s", "Bizos, hogy t�r�lni k�v�nja az elemet?", "Igen", "Nem"))
		{
			if (App.Database.DeleteMatchData(kivalasztott))
			{
				await DisplayAlert("Sikeres t�rl�s!", "A t�rl�s siker�lt!", "OK");
                model.eredmenyek.Remove(kivalasztott);
            }
			else
			{
				await DisplayAlert("Hiba!", "A t�rl�s sikertelen!", "OK");
			}
		}
        lstView.ItemSelected -= lstView_ItemSelected;
    }

    private void ImageButton_Clicked(System.Object sender, System.EventArgs e)
    {
        var gomb = (ImageButton)sender;
        if (gomb.RotationX == 180)
            gomb.RotateXTo(0);
        else
            gomb.RotateXTo(180);
    }
}