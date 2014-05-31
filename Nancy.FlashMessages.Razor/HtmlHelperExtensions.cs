using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy.FlashMessages.Extensions;
using Nancy.ViewEngines.Razor;

namespace Nancy.FlashMessages
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlString FlashMessages<T>(this HtmlHelpers<T> helpers, string messageType)
        {
            var s = new StringBuilder();
            var alertMessages = helpers.RenderContext.Context.GetFlashMessages();
            var messages = alertMessages.PopMessages(messageType);

            if (messages != null)
            {
                foreach (var message in messages)
                {
                    s.Append(string.Format("<div class=\"alert alert-dismissable alert-{0}\">", messageType));
                    s.Append(
                        string.Format(
                            "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button>"));
                    s.Append(string.Format("{0}", message));
                    s.Append(string.Format("</div>"));
                }
            }

            return new NonEncodedHtmlString(s.ToString());
        }
    }
}
