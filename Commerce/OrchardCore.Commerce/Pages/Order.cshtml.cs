using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Notify;
using YesSql;

namespace OrchardCore.Commerce.Pages
{
    public class OrderModel : PageModel
    {
        private readonly IHtmlLocalizer H;
        private readonly INotifier _notifier;
        private readonly ISession _session;
        private readonly IContentManager _contentManager;
        private readonly IContentItemDisplayManager _contentItemDisplayManager;
        private readonly IUpdateModelAccessor _updateModelAccessor;
        public dynamic Shape { get; set; }

        public OrderModel(ISession session, IHtmlLocalizer<OrderModel> htmlLocalizer,
            IContentManager contentManager, INotifier notifier,
            IUpdateModelAccessor uma, IContentItemDisplayManager contentItemDisplayManager)
        {
            _notifier = notifier;
            H = htmlLocalizer;
            _session = session;
            _contentItemDisplayManager = contentItemDisplayManager;
            _updateModelAccessor = uma;
            _contentManager = contentManager;
        }
        public async Task<IActionResult> OnGetAsync(string contentItemId = null)
        {
            if (contentItemId == null)
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
            var order = await _contentManager.GetAsync(contentItemId, VersionOptions.Latest);

            if (order == null)
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
            else
            {
                Shape = await _contentItemDisplayManager.BuildEditorAsync(order, null, true);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string contentItemId)
        {
            var offer = await _contentManager.GetAsync(contentItemId, VersionOptions.Latest);
            Shape = await _contentItemDisplayManager.UpdateEditorAsync(offer, _updateModelAccessor.ModelUpdater, true);

            if (ModelState.IsValid)
            {
                await _contentManager.UpdateAsync(offer);
                var result = await _contentManager.ValidateAsync(offer);

                if (result.Succeeded)
                {
                    await _contentManager.PublishAsync(offer);
                    await _notifier.SuccessAsync(H["Offer updated successful"]);
                }
            }
            return Page();
        }

    }
}
