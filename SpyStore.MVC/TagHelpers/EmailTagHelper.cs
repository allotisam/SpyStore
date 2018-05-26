using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SpyStore.MVC.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {
        public string EmailName { get; set; }
        public string EmailDomain { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            var address = EmailName + "@" + EmailDomain;
            output.TagName = "a";
            output.Attributes.SetAttribute("href", "mailto:" + address);
            output.Content.SetContent(address);
        }
    }
}
