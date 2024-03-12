using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific;
using SimpleMAUICRUD.Classes;

namespace SimpleMAUICRUD.pages;

public partial class DashboardPage : Microsoft.Maui.Controls.TabbedPage
{
	public DashboardPage(AppDb appDb)
	{
        NavigationPage.SetHasBackButton(this, false);

        On<Microsoft.Maui.Controls.PlatformConfiguration.Windows>().SetHeaderIconsEnabled(true);
        On<Microsoft.Maui.Controls.PlatformConfiguration.Windows>().SetHeaderIconsSize(new Size(24, 24));
        On<Microsoft.Maui.Controls.PlatformConfiguration.Windows>()
            .SetToolbarPlacement(Microsoft.Maui.Controls.PlatformConfiguration
            .WindowsSpecific.ToolbarPlacement.Bottom);
        On<Microsoft.Maui.Controls.PlatformConfiguration.Android>()
            .SetToolbarPlacement(Microsoft.Maui.Controls.PlatformConfiguration
            .AndroidSpecific.ToolbarPlacement.Bottom);

        InitializeComponent();
    }
}