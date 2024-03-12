using SimpleMAUICRUD.Classes;
using SimpleMAUICRUD.factories;

namespace SimpleMAUICRUD.pages
{
    public partial class ItemListPage : ContentPage
    {
        AppDb dbHelper;
        private readonly IItemPageFactory _pageFactory;

        public ItemListPage(AppDb appDb, IItemPageFactory pageFactory)
        {
            _pageFactory = pageFactory;
            InitializeComponent();
            dbHelper = appDb;
            CheckAndRequestStoragePermissions();
        }

        private void InitializeDatabase()
        {
            UpdateListView();
        }

        private async void HandlePermissionDenied()
        {
            // Display an alert to the user explaining why the permission is needed
            bool retry = await DisplayAlert(
                "Permission Denied",
                "Storage permission is required to access the database. Without it, the app cannot function properly. Would you like to try again?",
                "Retry",
                "Exit");

            if (retry)
            {
                // The user chose to try again, so request the permissions again
                CheckAndRequestStoragePermissions();
            }
            else
            {
                // The user chose to exit, close the app gracefully
                System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
            }
        }

        private async void CheckAndRequestStoragePermissions()
        {
            var statusRead = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            var statusWrite = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

            if (statusRead != PermissionStatus.Granted || statusWrite != PermissionStatus.Granted)
            {
                statusRead = await Permissions.RequestAsync<Permissions.StorageRead>();
                statusWrite = await Permissions.RequestAsync<Permissions.StorageWrite>();
            }

            // After re-checking, proceed if both read and write permissions are granted
            if (statusRead == PermissionStatus.Granted && statusWrite == PermissionStatus.Granted)
            {
                // Permissions granted - you can initialize your database here
                InitializeDatabase();
            }
            else
            {
                // If the permissions are still not granted, handle the denial again
                SemanticScreenReader.Announce("Permissions are required!");
                HandlePermissionDenied();
            }
        }

        private async void OnAddItemClicked(object sender, EventArgs e)
        {
            // Navigate to the item creation page
            //await Shell.Current.GoToAsync(nameof(CreateItemPage));
            //await Shell.Current.GoToAsync("///CreateItemPage"); 
            //await Navigation.PushAsync(new CreateItemPage(dbHelper));
            var page = _pageFactory.CreateItemPage();
            await Navigation.PushAsync(page);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.Title = "Items List";
            UpdateListView(); // Make sure to update the list when coming back to this page
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            if (itemsListView.SelectedItem is Item selectedItem)
            {
                await dbHelper.DeleteItemAsync(selectedItem.Id);
                UpdateListView();
            }
        }

        private async void OnItemTapped(object sender, EventArgs e)
        {
            var textCell = (TextCell)sender;
            var item = (Item)textCell.BindingContext;
            if (item != null)
            {
                // Pass the item's ID to the detail page
                //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?itemId={item.Id}");
                var page = _pageFactory.ItemDetailsPage(item.Id);
                await Navigation.PushAsync(page);
            }
        }

        private async void UpdateListView()
        {
            if (dbHelper != null)
                itemsListView.ItemsSource = await dbHelper.GetItemsAsync();
        }
    }

}
