using Microsoft.AspNetCore.Mvc;
using OrchardCore.Admin;
using OrchardCore.Commerce.Indexes;
using OrchardCore.ContentManagement;
using OrchardCore.Lists.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesSql;

namespace OrchardCore.Commerce.Controllers
{
    [Admin]
    public class DataMigrationController : Controller
    {
        private IContentManager _cm;
        private ISession _sess;

        public DataMigrationController(IContentManager cm, ISession session)
        {
            _cm = cm;
            _sess = session;
        }

        [HttpGet]
        public async Task<ActionResult> MigrateToProductList()
        {
            var products = await _sess.Query<ContentItem, ProductPartIndex>(x => x.ContentItemId != null).ListAsync();
            foreach (var product in products)
            {
                product.Apply(new ContainedPart
                {
                    ListContentItemId = "4q26ztbkdgggf75x4dc3g6p5cr"
                });
                await _cm.UpdateValidateAndCreateAsync(product, VersionOptions.Published);
            }
            return Ok("Done");
        }
    }
}
