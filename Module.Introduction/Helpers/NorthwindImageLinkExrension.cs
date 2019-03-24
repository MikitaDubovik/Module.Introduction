using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Module.Introduction.Helpers
{
    public static class NorthwindImageLinkExrension
    {
        public static IHtmlContent NorthwindImageLink(this IHtmlHelper helper, string id)
        {
            return new HtmlString($"<a href=\"/Categories/GetImage/{id}\">Id of image - {id}</a>");
        }
    }
}
