using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrchardCore.Commerce.Abstractions;
using OrchardCore.ContentManagement;

namespace OrchardCore.Commerce.Services
{
    public class PredefinedValuesProductAttributeService : IPredefinedValuesProductAttributeService
    {
        private readonly IProductAttributeService _productAttributeService;

        public PredefinedValuesProductAttributeService(IProductAttributeService productAttributeService)
        {
            _productAttributeService = productAttributeService;
        }

        public async Task<IEnumerable<IEnumerable<object>>> GetProductAttributesPredefinedValuesAsync(ContentItem product)
            => (await GetProductAttributesRestrictedToPredefinedValuesAsync(product))
                .Select(x => (x.Settings as IPredefinedValuesProductAttributeFieldSettings).PredefinedValues.ToList())
                .ToList();

        public async Task<IEnumerable<string>> GetProductAttributesCombinationsAsync(ContentItem product)
            => CartesianProduct(await GetProductAttributesPredefinedValuesAsync(product))
                .Select(x => String.Join("-", x));

        private IEnumerable<IEnumerable<T>> CartesianProduct<T>(IEnumerable<IEnumerable<T>> sequences)
        {
            IEnumerable<IEnumerable<T>> emptyProduct = new[] { Enumerable.Empty<T>() };
            return sequences.Aggregate(
                emptyProduct,
                (accumulator, sequence) =>
                    from accseq in accumulator
                    from item in sequence
                    select accseq.Concat(new[] { item })
                );
        }

        public async Task<IEnumerable<ProductAttributeDescription>> GetProductAttributesRestrictedToPredefinedValuesAsync(ContentItem product)
            => (await _productAttributeService
                .GetProductAttributeFieldsAsync(product))
                .Where(x => x.Settings is IPredefinedValuesProductAttributeFieldSettings textSettings && textSettings.RestrictToPredefinedValues)
                .OrderBy(x => x.PartName)
                .ThenBy(x => x.Name);
    }
}
