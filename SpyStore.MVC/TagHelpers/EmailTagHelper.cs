﻿using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SpyStore.MVC.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {
        public string EmailName { get; set; }
        public string EmailDomain { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            output.TagName = "a";   // replaces <email> tag with <a> tag
            var address = EmailName + "@" + EmailDomain;
            output.Attributes.SetAttribute("href", "mailto:" + address);
            output.Content.SetContent(address);
        }
    }
}
