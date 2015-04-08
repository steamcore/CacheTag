CacheTag
========

Simple and efficient JavaScript and CSS minification for ASP.NET MVC

**NOTE:** This project has been discontinued, use ASP.NET bundles instead.

What it does
------------

CacheTag helps speed up your page load times in a number of ways:

- Reduces the number of HTTP requests by combining resources
- Makes resources quicker to download thanks to minification
- All resources are hashed, preventing clients from using old resources
- Since any change creates a new hash, resources can be cached aggressively

Compared to the competition
---------------------------

CacheTag provides a number of benefits compared to other similar frameworks.

- It's real easy to use, see the examples below
- It's flexible because it works with generic lists, no custom bundle classes
- It's faster because it only compiles resources when needed, even in debug mode
- Built to be extendable, use the source Luke!


How to get started
------------------

Add project references to **`CacheTag.Core`** and the modules you need, **`CacheTag.Mvc`** is a required module for now.

ASP.NET MVC Razor example
-------------------------

Try something like this in a view:

	:::csharp
	@Html.RenderStyles(new []
	{
		"~/CSS/Reset.css",
		"~/CSS/Site.less"
	})
	@Html.RenderScripts(new []
	{
		"~/Scripts/jquery-1.7.1.js",
		"~/Scripts/app.coffee"
	})

You can also reference scripts or styles mid page like this:

	@Html.ReferenceScript("~/Scripts/flot.js")

And then render all referenced scripts at once at the end of the page:

	@Html.RenderReferencedScripts()

If you want, you can render scripts or styles inline as well, just add Inline to any render method.

Advanced examples
-----------------

Or you can build your scripts and styles manually:

	:::csharp
	var styleTags = new StyleList(new [] { "~/CSS/Reset.css", "~/CSS/Site.less" }).Compile().RenderHtml();
	var scriptTags = new ScriptList(new [] { "~/Scripts/jquery-1.8.0.js", "~/Scripts/app.coffee" }).Compile().RenderHtml();

`ScriptList` and `StyleList` are just there for convenience, they only inherit from `List<IScriptList>` and `List<IStyleList>` respectively and do a bit of magic to resolve the type of resources based on the file name.

Since we are working with lists, it's easy to do what you want with it.

	:::csharp
	var scriptUrls =
		new [] {
			new ScriptFile("~/Scripts/jquery-1.8.0.js"),
			new CoffeeScript("~/Scripts/app.coffee")
		}.Compile().Select(x => x.Url);

The code snippet above will load two scripts, compile them if in release mode, and return a list of their resulting URLs. This might be useful for lazy loading scripts via yepnope or something similar.

Example of resulting HTML in release mode
-----------------------------------------

In release mode resources are combined, and minified if a minification module is referenced, to reduce the number of HTTP requests the client has to make.

	<link rel="stylesheet" type="text/css" href="/_cachetag/e4dddf2cdd91d693e023cdc5dc0b56d8664766db.css" />
	<script src="/_cachetag/80e9e6fc43c6644ea9badec99eb5c6cbec87c4b0.js" type="text/javascript"></script>

Example of resulting HTML in debug mode
---------------------------------------

When Web.config is configured for debug mode, resources ar linked individually with a SHA1-hash to ease debugging and prevent any browser cache issues.

	:::html
	<link rel="stylesheet" type="text/css" href="/CSS/Reset.css" />
	<link rel="stylesheet" type="text/css" href="/CSS/Site.less" />
	<script src="/Scripts/jquery-1.8.0.js" type="text/javascript"></script>
	<script src="/Scripts/app.coffee" type="text/javascript"></script>

**Note**: To serve .less and .coffee-files individually in debug mode, you need to add their respective HTTP handlers manually (or via NuGet) to your web project.

Available modules
-----------------

To use a module, simply make a project reference, it will be configured automatically.

**`CacheTag.Mvc`**  
Registers routes and an MVC controller for serving scripts

**`CacheTag.Modules.AjaxMin`**  
Minifies scripts and stylesheets using AjaxMin, depends on `AjaxMin`

**`CacheTag.Modules.DotLess`**  
Support for .less style sheets files, depends on `dotless.Core`

**`CacheTag.Modules.SassAndCoffee`**  
Provides support for .coffee-scripts for now, depends on `SassAndCoffee.Core` and `SassAndCoffee.JavaScript`

Configuration
-------------

The idea is sane defaults and zero configuration, but there are some things you can configure if you want.

`CacheTagSettings`.**`HashAlgorithm`**  
MD5 is used by default

`CacheTagSettings`.**`RuntimeMode`**  
Default mode is Dynamic, other options are ForceDebug or ForceRelease

`CacheTagSettings`.**`HashFilesInDebugMode`**  
If true, then CacheTag will append hash of files includeded in debug mode to force browsers to reload changed resources. Default is off because this confuses debuggers

`CacheTagSettings`.**`HideFileExtensions`**  
Set to true if you think file extensions are unnecessary

`CacheTagMvcSettings`.**`RouteUrl`**  
Default value is `"_cachetag/{id}"`

Create your own module
----------------------

Implement ICacheTagModule and it will automatically be run on startup.

	:::csharp
	public class ModuleConfiguration : ICacheTagModule
	{
		public void Initialize()
		{
			CacheTag.Core.Configuration.Settings.RuntimeMode = RuntimeMode.ForceRelease;
		}
	}

Extension methods
-----------------

Most of CacheTag is based around a couple of extension methods operating on either `IEnumerable<IScriptResource>` or `IEnumerable<IStyleResource>`, these are explained here.

**`Compile()`**  
When operating in debug mode, Compile() does nothing, it just returns the same list it was passed. When in release mode however, it will compile the list of resources using whatever resource compiler is registered for that type of resource. For instance, when using the AjaxMin module, Compile() will minify the list and return a list of one item.

In release mode Compile() also calls Store(), see below.

**`RenderHtml()`**  
Renders HTML script tags with items linked as external resources.

**`RenderHtmlInline()`**  
Renders HTML script tags with the resource content inlined.

**`RenderMvcHtml()`**  
Calls RenderHtml() but wraps the result in a IHtmlString.

**`RenderMvcHtmlInline()`**  
Calls RenderHtmlInline() but wraps the result in a IHtmlString.

**`Store()`**  
When serving items as external resources they have to be stored somewhere so they can be retrieved when the browser next requests them. Therefore Store() saves the list of resources in a static Dictionary<> in the class ResourceStorage.

When running a site in production, scripts and styles tend to be static, so storing your compiled resources in a static dictionary should not be a problem.
