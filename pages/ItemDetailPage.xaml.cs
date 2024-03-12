using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.DependencyInjection;
using SimpleMAUICRUD.Classes;
using SimpleMAUICRUD.factories;

namespace SimpleMAUICRUD.pages;

[QueryProperty(nameof(ItemId), "itemId")]
public partial class ItemDetailPage : ContentPage
{
    private int _itemId;
    private AppDb dbHelper;
    private readonly IItemPageFactory _pageFactory;
    public int ItemId
    {
        get => _itemId;
        set
        {
            _itemId = value;
            if (value > 0)
                LoadItem(value);
        }
    }

    public ItemDetailPage(AppDb appDb, IItemPageFactory pageFactory)
    {
        _pageFactory = pageFactory;
        InitializeComponent();
        dbHelper = appDb;
    }


    public async void LoadItem(int itemId)
    {
        var item = await dbHelper.GetItemAsync(itemId);
        nameEntry.Text = item.Name;
        descriptionEntry.Text = item.Description;
    }

    private async void UpdateClicked(object sender, EventArgs e)
    {
        var page = _pageFactory.CreateItemPage(_itemId);
        await Navigation.PushAsync(page);

        //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?itemId={_itemId}");
    }

    private async void DeleteClicked(object sender, EventArgs e)
    {
        await dbHelper.DeleteItemAsync(_itemId);
        var toast = Toast.Make("Item deleted successfully", ToastDuration.Short);
        await toast.Show();
        await Shell.Current.GoToAsync(".."); // Navigate back to the main page
    }

    // This method is called when the page appears on the screen
    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadItem(_itemId);
    }
}
