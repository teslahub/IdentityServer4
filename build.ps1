$ErrorActionPreference = "Stop";

New-Item -ItemType Directory -Force -Path ./nuget

dotnet tool restore

$version = dotnet minver || throw 'dotnet minver'

$substitution = '${open}' + $version + '${close}'
Write-Host "Update version by expression: $substitution"
$targetFile = [System.IO.Path]::GetFullPath('src/Directory.Build.targets')
$content = [System.IO.File]::ReadAllText($targetFile) -Replace '(?<open><IdentityServerVersion>)[^<]+(?<close></IdentityServerVersion>)', $substitution || throw "Read from $targetFile"
[System.IO.File]::WriteAllText($targetFile, $content) || throw "Write to $targetFile"

Push-Location ./src/Storage
Invoke-Expression "./build.ps1 $args"
Pop-Location

Push-Location ./src/IdentityServer4
Invoke-Expression "./build.ps1 $args"
Pop-Location

Push-Location ./src/EntityFramework.Storage
Invoke-Expression "./build.ps1 $args"
Pop-Location

Push-Location ./src/EntityFramework
Invoke-Expression "./build.ps1 $args"
Pop-Location

Push-Location ./src/AspNetIdentity
Invoke-Expression "./build.ps1 $args"
Pop-Location
