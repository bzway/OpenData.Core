dir
ECHO OFF
Set PATH=C:\Windows\Microsoft.NET\Framework\v4.0.30319;%PATH:C:\Windows\Microsoft.NET\Framework\v4.0.30319;=%
ECHO delete old files
rd /s /q Publish
MSBuild "%~dp0Website\WebSite.WebApp\OpenData.WebSite.WebApp.csproj"  /p:DeployOnBuild=true /p:PublishProfile=%~dp0\PublishProfiles.pubxml
pause