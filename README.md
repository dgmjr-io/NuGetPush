---
author: David G. Moore, Jr.
author_email: david@dgmjr.io
title: NuGetPush Readme
modified: 2022-11-27-05:00:50
created: 2022-11-27-05:00:49
license: MIT
---

# NuGet Push

This is a simple MSBuild task that allows you to push a NuGet package to a NuGet server.

## Usage

First, you need to insert a reference to the SDK into your project like this:

```xml
<Sdk Name="NuGetPush" />
```

Then, you can use the task like this:

```shellscript title="Pushing to Azure Artifacts"
dotnet pack MyProject.csproj --target:PushAzure
```

```shellscript title="Pushing to GitHub Packages"
dotnet pack MyProject.csproj --target:PushGitHub
```
