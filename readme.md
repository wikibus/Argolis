![graph icon](https://raw.githubusercontent.com/wikibus/Argolis/master/assets/logo.png)

# Argolis [![Build status][av-badge]][build] [![NuGet version][nuget-badge]][nuget-link] [![codecov.io][cov-badge]][cov-link] [![codefactor][codefactor-badge]][codefactor-link]

[Hydra hypermedia controls][hydra] for .NET Web applications written in [Nancy][nancy].

## Introduction

### Hydra

From Hydra hompage

> Hydra simplifies the development of interoperable, hypermedia-driven Web APIs

Hydra does it by defining (*still in production though*) a wide variety of hypermedia controls, which allow the implementation
of resilient API clients. This resilience is achieved by relying on runtime API documentation and not out-of-band information
and human-readable API descriptions.

For more information please see it's [specification page](http://www.hydra-cg.com/spec/latest/core/).

### Argolis

Argolis is a [Super-Duper-Happy-Path][sdhp] towards documenting .NET web applications with Hydra. It struggles to be as simple as
possible and have the least impact on your actual application logic:

* Provides out-of-the-box defaults and conventions
* Any default behaviour can be modified (thank you [Strategy Pattern][sp])
* Doesn't hijack responsibilities by using [JsonLD.Entities][JsonLD.Entities] and [Nancy.Rdf][Nancy.Rdf] packages

#### The name

The name Argolis comes from the [ancient](https://en.wikipedia.org/wiki/Regions_of_ancient_Greece#Argolis) (and 
[modern](https://en.wikipedia.org/wiki/Argolis)) Greek province sometimes called *The Argolid*. Argolis is where the mythological
monster [Hydra](https://en.wikipedia.org/wiki/Lernaean_Hydra) lived. The monster's full name is Lernaean Hydra after the lake
of Lerna, where it had it's lair.

## Getting started with Argolis

*TL;DR;* To install add the Nuget package.

```
PM> Install-Package Argolis.Nancy
```

It is a meta-package, which pulls all components from their respective NuGet packages.

Currently only Nancy is supported, but the core library doesn't have a dependency and Web API
or ServiceStack are definitely a possibility.

### Basic usage

There are three steps requried to start using Agolis(.Nancy)

#### Implement `IHydraDocumentationSettings`

... and [wire it up with Nancy][Registrations].

``` c#
public class HydraDocumentationSettings : IHydraDocumentationSettings
{
    public string DocumentationPath
    {
        get { return "api"; }
    }

    public IriRef EntryPoint
    {
        get { return (IriRef)"http://localhost:61186/entrypoint"; }
    }
}

public class ArgolisRegistrations : Registrations
{
    public ArgolisRegistrations(ITypeCatalog tc) : base(tc)
    {
        Register<IHydraDocumentationSettings>(new HydraDocumentationSettings());
    }
}
```

This will set up the [`hydra:entryPoint`](http://www.hydra-cg.com/spec/latest/core/#hydra:entrypoint) link and the 
route used to serve API Documentation.

#### Implement `IDocumentedTypeSelector`

This interface is used to discover `Type`s, which should be used to produce [SupportedClasses][sc] exposed by the API.
There is an abstract class `AssemblyAnnotatedTypeSelector`, which will look for types annotated with `[SupportedClass]`
in given assemblies.

``` c#
public class AssembliesToScanForSupportedTypes : AssemblyAnnotatedTypeSelector
{
    protected override IEnumerable<Assembly> Assemblies
    {
        get { yield return typeof (Issue).Assembly; }
    }
}
```

#### Define your models

``` c#
[SupportedClass("http://example.api/o#Issue")]
[Description("An issue reported by our users")]
public class Issue
{
    public string Id { get; set; }
        
    [JsonProperty("titel")]
    public string Title { get; set; }
        
    public string Content { get; set; }
        
    [Description("The number of people who liked this issue")]
    public int LikesCount { get; private set; }

    public bool IsResolved { get; set; }

    public User Submitter { get; set; }

    [Range("http://example.api/o#project")]
    public string ProjectId { get; set; }
    
    private JObject Context => new AutoContext<Issue>();
}
```

Mandatory parts are the `[SupportedClass]` attribute and a `@context`. Here it is defined directly in the class
but it can also be provided by implementing `IContextProvider` and registering it with the container.

Please see the documentation of [JsonLd.Entities](https://github.com/wikibus/JsonLD.Entities/tree/master/src/Documentation/ResolvingContext)
for more details of how the JSON-LD context can be created.

#### Serve your ApiDocumentation

Requesting the configured route will return the `hydra:ApiDocumentation` (excerpt).

``` bash
curl http://localhost:61186/api -H Accept:application/ld+json
```

``` json
{
  "@context": {
	// ...
  },
  "@id": "http://localhost:61186/api",
  "@type": "ApiDocumentation",
  "entrypoint": "http://localhost:61186/entrypoint",
  "supportedClass": [
    {
      "@id": "http://example.api/o#Issue",
      "@type": "Class",
      "description": "An issue reported by our users",
      "supportedProperty": [
        {
          "@type": "SupportedProperty",
          "description": "The title property",
          "property": {
            "@id": "http://example.api/o#Issue/titel",
            "@type": "rdf:Property",
            "range": "xsd:string"
          },
          "readable": true,
          "required": false,
          "title": "title",
          "writeable": true
        },
        {
          "@type": "SupportedProperty",
          "description": "The number of people who liked this issue",
          "property": {
            "@id": "http://example.api/o#Issue/likesCount",
            "@type": "rdf:Property",
            "range": "xsd:int"
          },
          "readable": true,
          "required": false,
          "title": "likesCount",
          "writeable": false
        },
        {
          "@type": "SupportedProperty",
          "description": "The dateCreated property",
          "property": {
            "@id": "http://example.api/o#Issue/dateCreated",
            "@type": "rdf:Property",
            "range": "xsd:dateTime"
          },
          "readable": true,
          "required": false,
          "title": "dateCreated",
          "writeable": false
        }
      ],
      "title": "Issue"
    }    
  ]
}
```

And any requested resource will include a [`Link` header to the Api Documentation](http://www.hydra-cg.com/spec/latest/core/#discovering-a-hydra-powered-web-api)

``` http
HTTP/1.1 200 OK
Link: <http://localhost:61186/api>; rel="http://www.w3.org/ns/hydra/core#apiDocumentation"
```

[nancy]: https://github.com/NancyFx/Nancy/
[av-badge]: https://ci.appveyor.com/api/projects/status/1lm9rpx89w10ik6j?svg=true
[build]: https://ci.appveyor.com/project/tpluscode78631/argolis
[core-badge]: https://badge.fury.io/nu/argolis.svg
[core-link]: https://badge.fury.io/nu/argolis
[nuget-badge]: https://badge.fury.io/nu/argolis.nancy.svg
[nuget-link]: https://badge.fury.io/nu/argolis.nancy
[hydra]: http://hydra-cg.com
[sdhp]: https://github.com/NancyFx/Nancy/wiki/Introduction#the-super-duper-happy-path
[sp]: https://en.wikipedia.org/wiki/Strategy_pattern
[JsonLD.Entities]: https://github.com/wikibus/JsonLD.Entities
[Nancy.Rdf]: https://github.com/wikibus/Nancy.Rdf
[sc]: http://www.hydra-cg.com/spec/latest/core/#documenting-a-web-api
[Registrations]: #
[cov-badge]: https://codecov.io/github/wikibus/Argolis/coverage.svg?branch=master
[cov-link]: https://codecov.io/github/wikibus/Argolis?branch=master
[codefactor-badge]: https://www.codefactor.io/repository/github/wikibus/Argolis/badge/master
[codefactor-link]: https://www.codefactor.io/repository/github/wikibus/Argolis/overview/master
