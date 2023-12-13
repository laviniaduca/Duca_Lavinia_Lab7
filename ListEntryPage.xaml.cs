using Duca_Lavinia_Lab7.Models;

namespace Duca_Lavinia_Lab7;

public partial class ListEntryPage : ContentPage
{
	public ListEntryPage()
	{
        InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            listView.ItemsSource = await App.Database.GetShopListsAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving shopping lists: {ex}");
        }
    }

    async void OnShopListAddedClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ListPage
        {
            BindingContext = new ShopList()
        });
    }

    async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            await Navigation.PushAsync(new ListPage
            {
                BindingContext = e.SelectedItem as ShopList
            });
        }
    }

}