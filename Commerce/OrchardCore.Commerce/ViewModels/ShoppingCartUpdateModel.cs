using System.Collections.Generic;

namespace OrchardCore.Commerce.ViewModels
{
    public class ShoppingCartUpdateModel
    {
        public string Email { get; set; }
        public IList<ShoppingCartLineUpdateModel> Lines {get;set;}
    }
}
