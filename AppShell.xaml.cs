using SimpleMAUICRUD.pages;

namespace SimpleMAUICRUD
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(DashboardPage), typeof(DashboardPage));
            Routing.RegisterRoute(nameof(ItemListPage), typeof(ItemListPage));
            Routing.RegisterRoute(nameof(CreateItemPage), typeof(CreateItemPage));
        }
    }
}
