using System.Collections.Generic;
using System.Threading.Tasks;
using OrchardCore.ContentManagement;

namespace OrchardCore.Commerce.Abstractions
{
    public interface IProductAttributeService
    {
        Task<IEnumerable<ProductAttributeDescription>> GetProductAttributeFieldsAsync(ContentItem product);
    }
}
