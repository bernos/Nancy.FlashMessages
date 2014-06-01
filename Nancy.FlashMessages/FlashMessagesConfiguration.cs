using System;
using System.Collections.Generic;
using System.Text;

namespace Nancy.FlashMessages
{
    public class FlashMessagesConfiguration
    {
        public Func<IFlashMessageRenderer> GetRenderer { get; set; }
               
        public FlashMessagesConfiguration()
        {
            GetRenderer = () => new DefaultFlashMessageRenderer(this);
        }
    }
}