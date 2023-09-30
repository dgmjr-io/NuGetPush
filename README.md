---

authors:

- dgmjr
  title: NuGetPush Readme
  lastmod: 2022-11-27T05:00:50.000Z
  date: 2022-11-27T05:00:49.000Z
  license: MIT
  type: readme
  slug: nugetpush-readme
  project: shared
  description: This is a simple MSBuild task that allows you to push a NuGet package to a NuGet server.

---

# NuGet Push

This is a simple MSBuild task that allows you to push a NuGet package to a NuGet server.

## Getting Started

=== "Using `global.json`"

You can reference the SDK in your `global.json` file:

```json
"msbuild-sdks": {
  "NuGetPush": "<VERSION>"
}
```

=== "Using the `Sdk` project element

You can also refer to the SDK with an `Sdk` project element.

!!! Note "If you select this way, you need to supply it with a version number."

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <Sdk Name="NuGetPush" Version="<VERSION>" />
</Project>
```
