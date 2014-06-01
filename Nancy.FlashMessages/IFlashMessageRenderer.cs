using System.Collections;
using System.Collections.Generic;

namespace Nancy.FlashMessages
{
    public interface IFlashMessageRenderer
    {
        string Render(string messageType, IEnumerable<string> messages);
    }
}