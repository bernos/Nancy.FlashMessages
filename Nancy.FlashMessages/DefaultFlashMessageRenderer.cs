using System.Collections.Generic;
using System.Text;

namespace Nancy.FlashMessages
{
    class DefaultFlashMessageRenderer : IFlashMessageRenderer
    {
        private readonly FlashMessagesConfiguration _configuration;

        public DefaultFlashMessageRenderer(FlashMessagesConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Render(string messageType, IEnumerable<string> messages)
        {
            var s = new StringBuilder();

            if (messages == null) return s.ToString();
            
            foreach (var message in messages)
            {
                s.Append(string.Format("<div class=\"alert alert-dismissable alert-{0}\">", messageType));
                s.Append(
                    string.Format(
                        "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button>"));
                s.Append(string.Format("{0}", message));
                s.Append(string.Format("</div>"));
            }

            return s.ToString();
        }
    }
}