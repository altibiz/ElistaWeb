using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using OrchardCore.Commerce.Settings;
using OrchardCore.Navigation;

namespace OrchardCore.Commerce
{
    public class AdminMenu : INavigationProvider
    {
        private readonly IStringLocalizer<AdminMenu> T;

        public AdminMenu(IStringLocalizer<AdminMenu> localizer)
        {
            T = localizer;
        }

        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (!String.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
                return Task.CompletedTask;

            builder
                .Add(T["Commerce"], "0", rootView => rootView
                   .Add(T["Products"], "5", childOne => childOne
                       .Url("~/Admin/Contents/ContentItems/4q26ztbkdgggf75x4dc3g6p5cr/Display"))
                   .Add(T["Orders"], "6", childTwo => childTwo
                       .Action("List", "Admin", new { area = "OrchardCore.Contents", contentTypeId = "Order" }))
                 , new[] { "icon-class-fas", "icon-class-fa-cart-shopping" })
                .Add(T["Configuration"], configuration => configuration
                    .Add(T["Settings"], settings => settings
                       .Add(T["Commerce"], T["Commerce"], entry => entry
                          .Action("Index", "Admin", new
                          {
                              area = "OrchardCore.Settings",
                              groupId = CommerceSettingsDisplayDriver.GroupId
                          })
                          .Permission(Permissions.ManageCommerceSettings)
                          .LocalNav()
                )));

            return Task.CompletedTask;
        }
    }
}
