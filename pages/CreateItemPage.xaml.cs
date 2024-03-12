using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using SimpleMAUICRUD.Classes;
using System.Threading.Tasks;

namespace SimpleMAUICRUD.pages;

public partial class CreateItemPage : ContentPage
{
    private int _itemId;
    private AppDb dbHelper;
    public int ItemId
    {
        get => _itemId;
        set
        {
            _itemId = value;
            if(_itemId > 0)
                LoadItem(value);
        }
    }

    public CreateItemPage(AppDb appDb)
    {
        InitializeComponent();
        dbHelper = appDb;

#if WINDOWS
        var backItem = new ToolbarItem
        {
            Text = "Back to Home",
            Priority = 0,
            Order = ToolbarItemOrder.Primary,
            Command = new Command(async () =>
            {
                await Shell.Current.GoToAsync("..");
            })
        };
        this.ToolbarItems.Add(backItem);
#endif
    }

    public async void LoadItem(int itemId)
    {
        var item = await dbHelper.GetItemAsync(itemId);
        nameEntry.Text = item.Name;
        descriptionEntry.Text = item.Description;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var newItem = new Item
        {
            Id = _itemId,
            Name = nameEntry.Text,
            Description = descriptionEntry.Text
        };

        // Assuming dbHelper is accessible via Dependency Injection or passed through constructor
        await dbHelper.SaveItemAsync(newItem);

        // Show a toast notification
        string msg = _itemId > 0 ? "Item updated successfully" : "Item added successfully";
        var toast = Toast.Make(msg, ToastDuration.Short);
        await toast.Show();

        // Navigate back to the previous page
        // await Shell.Current.GoToAsync("..");
        // Navigate back to the first flyout item
        if (Application.Current.MainPage is MainFlyoutPage mainFlyoutPage)
        {
            mainFlyoutPage.NavigateToFirstItem();
        }
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(".."); // Navigate back to the previous page
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        this.Title = "Create Item";
    }

}
