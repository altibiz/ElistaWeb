using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;

namespace ElistaTheme
{
    public class ResourceManagementOptionsConfiguration : IConfigureOptions<ResourceManagementOptions>
    {
        private static ResourceManifest _manifest;

        static ResourceManagementOptionsConfiguration()
        {
            _manifest = new ResourceManifest();

            //_manifest
            //    .DefineStyle("ElistaTheme-bootstrap")
            //    .SetUrl("~/ElistaTheme/vendor/css/bootstrap.min.css", "~/ElistaTheme/vendor/css/bootstrap.css")
            //    .SetVersion("1.0.0");
            //// fonts / stylesheet.css
            _manifest
                .DefineStyle("ElistaTheme-FontStyle")
                .SetUrl("~/ElistaTheme/fonts/stylesheet.css")
                .SetVersion("1.0.0");
            _manifest
                .DefineStyle("ElistaTheme-Swiper")
                .SetUrl("~/ElistaTheme/vendor/swiper/swiper-bundle.min.css", "~/ElistaTheme/vendor/swiper/swiper-bundle.css")
                .SetVersion("1.0.0");

            _manifest
                .DefineStyle("ElistaTheme-Aos")
                .SetUrl("~/ElistaTheme/vendor/aos/aos.css")
                .SetVersion("1.0.0");


            _manifest
                .DefineStyle("ElistaTheme-MalihuScrollbar")
                .SetUrl("~/ElistaTheme/vendor/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.css")
                .SetVersion("1.0.0");


            _manifest
                .DefineStyle("ElistaTheme-Default")
                .SetUrl("~/ElistaTheme/css/style.default.css")
                .SetVersion("1.0.0");


            _manifest
                .DefineStyle("ElistaTheme-Custom")
                .SetUrl("~/ElistaTheme/css/custom.css")
                .SetVersion("1.0.0");


            _manifest
                .DefineStyle("ElistaTheme-FontAwesome")
                .SetUrl("https://use.fontawesome.com/releases/v5.10.0/css/all.css")
                .SetVersion("1.0.0");


            _manifest
                .DefineScript("ElistaTheme-Bootstrap")
                .SetCdn("~/ElistaTheme/vendor/bootstrap/js/bootstrap.bundle.min.js", "~/ElistaTheme/vendor/bootstrap/js/bootstrap.bundle.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("ElistaTheme-Jquery")
                .SetCdn("~/ElistaTheme/vendor/jquery/jquery.min.js", "~/ElistaTheme/vendor/jquery/jquery.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("ElistaTheme-Swiper")
                .SetCdn("~/ElistaTheme/vendor/swiper/swiper-bundle.min.js", "~/ElistaTheme/vendor/swiper/swiper-bundle.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("ElistaTheme-BootstrapSelect")
                .SetCdn("~/ElistaTheme/vendor/bootstrap-select/js/bootstrap-select.min.js", "~/ElistaTheme/vendor/bootstrap-select/js/bootstrap-select.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("ElistaTheme-Aos")
                .SetCdn("~/ElistaTheme/vendor/aos/aos.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("ElistaTheme-mCustomScrollbar")
                .SetCdn("~/ElistaTheme/vendor/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.concat.min.js", "~/ElistaTheme/vendor/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("ElistaTheme-CustomScrollbar")
                .SetCdn("~/ElistaTheme/js/custom-scrollbar-init.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("ElistaTheme-PolyFills")
                .SetCdn("~/ElistaTheme/vendor/smooth-scroll/smooth-scroll.polyfills.min.js", "~/ElistaTheme/vendor/smooth-scroll/smooth-scroll.polyfills.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("ElistaTheme-Ofi")
                .SetCdn("~/ElistaTheme/vendor/object-fit-images/ofi.min.js", "~/ElistaTheme/vendor/object-fit-images/ofi.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("ElistaTheme-Countdown")
                .SetCdn("~/ElistaTheme/js/countdown.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("ElistaTheme-Sliders")
                .SetDependencies("ElistaTheme-Swiper")
                .SetCdn("~/ElistaTheme/js/sliders-init.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("ElistaTheme-Jquery.zoom")
                .SetDependencies("ElistaTheme-Jquery")
                .SetCdn("~/ElistaTheme/vendor/jquery-zoom/jquery.zoom.min.js", "~/ElistaTheme/vendor/jquery-zoom/jquery.zoom.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("ElistaTheme-photoswipe")
                .SetCdn("~/ElistaTheme/vendor/photoswipe/photoswipe.min.js", "~/ElistaTheme/vendor/photoswipe/photoswipe.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("ElistaTheme-photoswipe-ui")
                .SetDependencies("ElistaTheme-photoswipe")
                .SetCdn("~/ElistaTheme/vendor/photoswipe/photoswipe-ui-default.min.js", "~/ElistaTheme/vendor/photoswipe/photoswipe-ui-default.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("ElistaTheme-photoswipe-init")
                .SetDependencies("ElistaTheme-photoswipe-ui", "ElistaTheme-Jquery")
                .SetCdn("~/ElistaTheme/js/photoswipe-init.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("ElistaTheme-masonry")
                .SetUrl("~/ElistaTheme/vendor/masonry-layout/masonry.pkgd.js")
                .SetDependencies("ElistaTheme-Jquery")
                .SetVersion("1.0.0");
            _manifest
                .DefineScript("ElistaTheme-imagesloaded")
                .SetUrl("~/ElistaTheme/vendor/imagesloaded/imagesloaded.js")
                .SetDependencies("ElistaTheme-Jquery")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("ElistaTheme-masonry-init")
                .SetDependencies("ElistaTheme-masonry", "ElistaTheme-imagesloaded")
                .SetCdn("~/ElistaTheme/js/masonry-init.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("ElistaTheme-Theme")
                .SetDependencies("ElistaTheme-Jquery.zoom")
                .SetCdn("~/ElistaTheme/js/theme.js")
                .SetVersion("1.0.0");
        }

        public void Configure(ResourceManagementOptions options)
        {
            options.ResourceManifests.Add(_manifest);
        }
    }
}
