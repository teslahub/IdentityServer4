Remove-Item $env:USERPROFILE\.nuget\packages\Tec.identityserver4\ -Recurse -Force -Confirm:$false -ErrorAction SilentlyContinue -Verbose
Remove-Item $env:USERPROFILE\.nuget\packages\Tec.identityserver4.storage\ -Recurse -Force -Confirm:$false -ErrorAction SilentlyContinue -Verbose
Remove-Item $env:USERPROFILE\.nuget\packages\Tec.identityserver4.entityframework\ -Recurse -Force -Confirm:$false -ErrorAction SilentlyContinue -Verbose
Remove-Item $env:USERPROFILE\.nuget\packages\Tec.identityserver4.entityframework.storage\ -Recurse -Force -Confirm:$false -ErrorAction SilentlyContinue -Verbose
Remove-Item $env:USERPROFILE\.nuget\packages\Tec.identityserver4.aspnetidentity\ -Recurse -Force -Confirm:$false -ErrorAction SilentlyContinue -Verbose
Remove-Item $env:USERPROFILE\.nuget\packages\Tec.IdentityServer4.AccessTokenValidation\ -Recurse -Force -Confirm:$false -ErrorAction SilentlyContinue -Verbose

Remove-Item $env:USERPROFILE\.nuget\packages\identitymodel\ -Recurse -Force -Confirm:$false -ErrorAction SilentlyContinue -Verbose
Remove-Item $env:USERPROFILE\.nuget\packages\IdentityModel.AspNetCore.OAuth2Introspection\ -Recurse -Force -Confirm:$false -ErrorAction SilentlyContinue -Verbose
