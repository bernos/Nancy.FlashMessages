using System;

namespace Nancy.FlashMessages.Extensions
{
    /// <summary>
    /// NancyContext extension methods for adding and accessing FlashMessages
    /// </summary>
    public static class NancyContextExtensions
    {
        private const string ContextKey = "__am";

        /// <summary>
        /// Add a new flash message of the specified type
        /// </summary>
        /// <param name="context"></param>
        /// <param name="type"></param>
        /// <param name="message"></param>
        public static void FlashMessage(this NancyContext context, string type, string message)
        {
            var messages = GetFlashMessages(context);

            messages.AddMessage(type, message);
        }

        /// <summary>
        /// Retrieve the FlashMessages instance from the NancyContext. 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static FlashMessages GetFlashMessages(this NancyContext context)
        {
            if (!context.Items.ContainsKey(ContextKey))
            {
                throw new Exception("FlashMessages not initialised. Ensure that you have called FlashMessages.Enable() in your Bootstrappers ApplicationStartup method.");
            }

            return context.Items[ContextKey] as FlashMessages;
        }

        /// <summary>
        /// Set up flash messages for the nancy context. This will be called by
        /// the before request handler that is set up by FlashMessages.Enable
        /// </summary>
        /// <param name="context"></param>
        /// <param name="flashMessages"></param>
        internal static void SetFlashMessages(this NancyContext context, FlashMessages flashMessages)
        {
            context.Items[ContextKey] = flashMessages;
        }
    }
}