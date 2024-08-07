$nupkgRegex = "^(?:.*/)?(?<PackageId>.+)\.(?<Version>(?<Major>\d+)\.(?<Minor>\d+)\.(?<Build>\d+)(?:-(?<Prerelease>[\w.-]+))?)\.nupkg$"

function Get-PackageId {
  [CmdletBinding()]
  param (
    [string]$PackagePath
  )
  process {
    if ($PackagePath -match $nupkgRegex) {
      return $matches["PackageId"]
    }
    else {
      Write-Error "The package path does not match the expected pattern."
    }
  }
}

function Get-PackageVersion {
  [CmdletBinding()]
  param (
    [string]$PackagePath
  )
  process {
    if ($PackagePath -match $nupkgRegex) {
      return $matches["Version"]
    }
    else {
      Write-Error "The package path does not match the expected pattern."
    }
  }
}

function Push-Package {
  [CmdletBinding()]
  param(
    [Parameter(Mandatory = $true)]
    [string]$PackagePath,
    [Parameter(Mandatory = $false)]
    [string]$Source = "nuget.org",
    [Parameter(Mandatory = $false)]
    [string]$ApiKey
  )
  process {
    if (-not $ApiKey) {
      $ApiKey = $env:API_KEY
    }
    if (-not $ApiKey) {
      throw "ApiKey is required"
    }
    yes | dotnet nuget delete Get-PackageId $PackagePath Get-PackageVersion $PackagePath --source $Source --api-key $ApiKey -y
    dotnet nuget push $PackagePath --api-key $ApiKey --source $Source
  }
}

Push-Package -PackagePath $args[0] -Source $args[1] -ApiKey $args[2]
