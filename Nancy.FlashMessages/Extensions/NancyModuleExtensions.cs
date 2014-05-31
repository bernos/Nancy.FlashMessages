namespace Nancy.FlashMessages.Extensions
{
    public static class NancyModuleExtensions
    {
        public static void FlashMessage(this NancyModule module, string type, string message)
        {
            module.Context.FlashMessage(type, message);
        }
    }
}