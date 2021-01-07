Remove-Item $env:USERPROFILE\.nuget\packages\Tec.identityserver4\ -Recurse -ErrorAction SilentlyContinue 
Remove-Item $env:USERPROFILE\.nuget\packages\Tec.identityserver4.storage\ -Recurse -ErrorAction SilentlyContinue 
Remove-Item $env:USERPROFILE\.nuget\packages\Tec.identityserver4.entityframework\ -Recurse -ErrorAction SilentlyContinue 
Remove-Item $env:USERPROFILE\.nuget\packages\Tec.identityserver4.entityframework.storage\ -Recurse -ErrorAction SilentlyContinue 
Remove-Item $env:USERPROFILE\.nuget\packages\Tec.identityserver4.aspnetidentity\ -Recurse -ErrorAction SilentlyContinue 

Remove-Item $env:USERPROFILE\.nuget\packages\identitymodel\ -Recurse -ErrorAction SilentlyContinue 
Remove-Item $env:USERPROFILE\.nuget\packages\IdentityModel.AspNetCore.OAuth2Introspection\ -Recurse -ErrorAction SilentlyContinue 
Remove-Item $env:USERPROFILE\.nuget\packages\Tec.IdentityServer4.AccessTokenValidation\ -Recurse -ErrorAction SilentlyContinue 