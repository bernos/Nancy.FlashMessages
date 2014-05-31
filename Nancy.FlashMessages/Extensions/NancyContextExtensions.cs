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
        /// Retrieve the FlashMessages instance from the NancyContext. This will lazily
        /// create a FlashMessages instance on first call
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static FlashMessages GetFlashMessages(this NancyContext context)
        {
            if (!context.Items.ContainsKey(ContextKey))
            {
                context.Items[ContextKey] = new FlashMessages(context.Request.Session);
            }

            return context.Items[ContextKey] as FlashMessages;
        }
    }
}