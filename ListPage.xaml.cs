using Duca_Lavinia_Lab7.Models;

namespace Duca_Lavinia_Lab7;

public partial class ListPage : ContentPage
{
	public ListPage()
	{
		InitializeComponent();
	}

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        await App.Database.DeleteShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductPage((ShopList)
        this.BindingContext)
        {
            BindingContext = new Product()
        });
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var shopl = (ShopList)BindingContext;
        listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
    }

    async void OnDeleteItemClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var product = (Product)button.BindingContext;

        if (product.ListProducts != null && product.ListProducts.Any())
        {
            foreach (var listProduct in product.ListProducts)
            {
                await App.Database.DeleteProductAsync(new Product { ID = listProduct.ProductID });
            }
        }

        await App.Database.DeleteProductAsync(product);

        var shopl = (ShopList)BindingContext;
        listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
    }

}