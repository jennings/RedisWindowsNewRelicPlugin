@echo off
setlocal

set HERE=%~dp0
set FAKE=%HERE%\packages\FAKE\tools\fake.exe

if not exist "%FAKE%" (
    "%HERE%\tools\nuget.exe" install FAKE              -OutputDirectory "%HERE%\packages" -ExcludeVersion
)

"%FAKE%" "%HERE%\build.fsx" %*
