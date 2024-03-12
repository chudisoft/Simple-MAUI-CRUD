namespace SimpleMAUICRUD
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();


            // No shell (Not compactible) when using a flyout page
            // MainPage = new AppShell();
            // Use the service provider to get MainFlyoutPage
            MainPage = serviceProvider.GetRequiredService<MainFlyoutPage>();
        }
    }
}
