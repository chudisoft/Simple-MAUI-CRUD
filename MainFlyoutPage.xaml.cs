using SimpleMAUICRUD.Classes;
using SimpleMAUICRUD.pages;

namespace SimpleMAUICRUD;

public partial class MainFlyoutPage : FlyoutPage
{
    private readonly IServiceProvider _serviceProvider;

    public MainFlyoutPage(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        InitializeComponent();

        // Define the list view for the flyout menu
        var listView = new ListView
        {
            ItemsSource = new List<FlyoutPageItem>
            {
                new FlyoutPageItem { Title = "Dashboard", 
                    IconSource = "home.png", TargetType = typeof(DashboardPage) },
                new FlyoutPageItem { Title = "Items List", IconSource = "item.png", TargetType = typeof(ItemListPage) },
                new FlyoutPageItem { Title = "New Item", IconSource = "item_add.png", TargetType = typeof(CreateItemPage) }
            },
            ItemTemplate = new DataTemplate(() =>
            {
                var grid = new Grid { Padding = 10 };
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

                var image = new Image { WidthRequest = 20, HeightRequest = 20 };
                image.SetBinding(Image.SourceProperty, "IconSource");
                var label = new Label { VerticalOptions = LayoutOptions.FillAndExpand };
                label.SetBinding(Label.TextProperty, "Title");

                grid.Children.Add(image); // This is in column 0 by default
                Grid.SetColumn(label, 1); // Set the label in column 1
                grid.Children.Add(label);

                return new ViewCell { View = grid };
            }),
            SeparatorVisibility = SeparatorVisibility.None
        };

        // Event handler for item selection
        listView.ItemSelected += async (sender, e) =>
        {
            if (!(e.SelectedItem is FlyoutPageItem item))
                return;

            // Resolve the page instance using the service provider
            Page page = (Page)_serviceProvider.GetRequiredService(item.TargetType);
            Detail = new NavigationPage(page);
            IsPresented = false; // Close the flyout menu
            ((ListView)sender).SelectedItem = null; // Deselect item
        };


        // Initial content for Detail
        Detail = new NavigationPage(_serviceProvider.GetRequiredService<DashboardPage>());

        // Assign the list view to the Flyout property
        Flyout = new ContentPage
        {
            Title = "Menu",
            Content = new StackLayout
            {
                Children =
                {
                    listView
                }
            },
        };
    }


    public void NavigateToFirstItem()
    {
        Detail = new NavigationPage(_serviceProvider.GetRequiredService<DashboardPage>());
        IsPresented = false; // Optionally close the flyout menu
    }
}