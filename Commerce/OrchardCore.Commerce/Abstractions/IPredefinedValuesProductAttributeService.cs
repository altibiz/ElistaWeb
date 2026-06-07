using System.Collections.Generic;
using System.Threading.Tasks;
using OrchardCore.ContentManagement;

namespace OrchardCore.Commerce.Abstractions
{
    public interface IPredefinedValuesProductAttributeService
    {
        Task<IEnumerable<ProductAttributeDescription>> GetProductAttributesRestrictedToPredefinedValuesAsync(ContentItem product);
        Task<IEnumerable<IEnumerable<object>>> GetProductAttributesPredefinedValuesAsync(ContentItem product);
        Task<IEnumerable<string>> GetProductAttributesCombinationsAsync(ContentItem product);
    }
}
