using OrchardCore.Commerce.Fields;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;

namespace OrchardCore.Commerce.Models
{
    public class Order : ContentPart
    {
        public TextField OrderId { get; set; }
        public AddressField ShippingAddress { get; set; }
        public AddressField BillingAddress { get; set; }
        public TextField Email { get; set; }
        public TextField Phone { get; set; }
    }

}
