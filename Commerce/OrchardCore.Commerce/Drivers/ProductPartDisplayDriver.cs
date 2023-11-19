using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using OrchardCore.Commerce.Abstractions;
using OrchardCore.Commerce.Indexes;
using OrchardCore.Commerce.Models;
using OrchardCore.Commerce.ViewModels;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Mvc.ModelBinding;
using YesSql;

namespace OrchardCore.Commerce.Drivers
{
    public class ProductPartDisplayDriver : ContentPartDisplayDriver<ProductPart>
    {
        private readonly IProductAttributeService _productAttributeService;
        private readonly ISession _session;

        public ProductPartDisplayDriver(IProductAttributeService productAttributeService, ISession session)
        {
            _productAttributeService = productAttributeService;
            _session = session;
        }

        public override IDisplayResult Display(ProductPart productPart, BuildPartDisplayContext context)
        {
            return Initialize<ProductPartViewModel>(GetDisplayShapeType(context), m =>
            {
                BuildViewModel(m, productPart);
            })
                .Location("Detail", "Gallery:20")
                .Location("Summary", "Meta:5");
        }

        public override IDisplayResult Edit(ProductPart productPart, BuildPartEditorContext context)
        {
            return Initialize<ProductPartViewModel>(GetEditorShapeType(context), m => BuildViewModel(m, productPart));
        }

        public override async Task<IDisplayResult> UpdateAsync(ProductPart model, IUpdateModel updater, UpdatePartEditorContext context)
        {
            await updater.TryUpdateModelAsync(model, Prefix, t => t.Sku);
            var cId = model.ContentItem.ContentItemId;
            var hasSku = 0 < (await _session.QueryIndex<ProductPartIndex>(x => x.Sku == model.Sku && x.ContentItemId != cId).CountAsync());
            if (hasSku)
            {
                updater.ModelState.BindValidationResult(Prefix, new ValidationResult("Existing SKU choose another", new[] { nameof(model.Sku) }));
            }

            return Edit(model, context);
        }

        private Task BuildViewModel(ProductPartViewModel model, ProductPart part)
        {
            model.ContentItem = part.ContentItem;
            model.Sku = part.Sku;
            model.ProductPart = part;

            model.Attributes = _productAttributeService.GetProductAttributeFields(part.ContentItem);

            // TODO: filter out of inventory products here as well when we have inventory management
            // model.CanBeBought = ...;

            return Task.CompletedTask;
        }
    }
}
