Provides a simple API for displaying "flash" messages. Messages are stored in the current ISession, and so will persist between redirects and so forth. Currently only the Razor view engine is supported, but support for other view engines can easily be added by refering to the source code for Nancy.FlashMessages.Razor

## Installation

Via nuget. Install the Nancy.FlashMessages.Razor package. This will pull down the Nancy.FlashMessages core package as a dependency.

```
Install-Package Nancy.FlashMessages.Razor
```

## Usage

### Enabling Nancy.FlashMessages

Enable Nancy.FlashMessages in the ApplicationStartup method of your bootstrapper

```csharp
protected override void ApplicationStartup(Nancy.TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
{
	base.ApplicationStartup(container, pipelines);
    
	FlashMessages.Enable(pipelines);
}
```

You can also provide a custom configuration, by sending your own implementation of FlashMessagesConfiguration.

```csharp
protected override void ApplicationStartup(Nancy.TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
{
	base.ApplicationStartup(container, pipelines);
    
	FlashMessages.Enable(pipelines, CustomFlashMessagesConfiguration());
}
```

See below for an example of how to use a custom configuration class to alter the message template used when rendering messages in the browser.

### Adding messages

Extension methods are added to both the NancyContext and NancyModule classes to make it easy to set messages. From within a NancyModule:

```csharp
public class MyModule : NancyModule {
	public MyModule() {
		Get["/"] = _ => {
			this.FlashMessage(FlashMessages.Info, "Hello world");
		};
	}
}
```

Or, from anywhere that you have access to the current NancyContext:

```csharp
context.FlashMessage(FlashMessages.Info, "Hello world");
```

The first argument to `FlashMessage()` is the message type. This is simply a string used to group messages. Normally this value will be used as a css class name in our message template. The standard message types are exposed as static values on the FlashMessages class, but you can use any values you like.

### Displaying messages

Currently, extension methods for the Razor engine HtmlHelpers class are provided, in order to render flash messages in your view.

```csharp
@Html.FlashMessages("info")
@Html.FlashMessages("success")
```

The single argument is the message type to render. The most common practice is to place this code in a layout or partial view that is shared across your entire application. Messages are popped off the message buffer when they are rendered, so will only be rendered once per session.

### Customising the message template

The default message template that ships is designed to work with twitter bootstrap 3 css. If you'd like to change the template markup you can easily do this by providing a custom FlashMessagesConfiguration that uses your own implementation of the IFlashMessageRenderer interface. For example:

```csharp
public class CustomFlashMessagesConfiguration : FlashMessagesConfiguration
{
	public CustomFlashMessagesConfiguration() : base()
	{
    		GetRenderer = () => new CustomFlashMessageRenderer();
	}

	public class CustomFlashMessageRenderer : IFlashMessageRenderer
	{
    		public string Render(string messageType, IEnumerable<string> messages)
    		{
        		var s = new StringBuilder();

		        s.Append("<ul>");

        		foreach (var message in messages)
        		{
            			s.Append(string.Format("<li>{0}: {1}</li>", messageType, message));
			}

        		s.Append("</ul>");

		        return s.ToString();
    		}
	}
}
```
