using System;
using System.Collections.Generic;
using System.Text;

namespace Nancy.FlashMessages
{
    public class FlashMessagesConfiguration
    {
        /// <summary>
        /// Message renderer function. Accepts a string representing the message type, and
        /// an IEnumerable<string> containing all the message to render. The default implementation
        /// renders html compatible with the twitter bootstrap "alert" component css
        /// 
        /// Override this in your own configuration if you would like to customise the flash
        /// messages template
        /// </summary>
        public Func<string, IEnumerable<string>, string> MessagesRenderer { get; set; }

        public FlashMessagesConfiguration()
        {
            MessagesRenderer = (messageType, messages) =>
            {
                var s = new StringBuilder();

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

                return s.ToString();
            };
        }
    }
}