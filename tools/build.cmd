@echo off

REM This script builds and packs the artifacts. Use when you have VS installed.
set releaseconfig=%1
if "%releaseconfig%" == "" set releaseconfig=Release

:download
echo Downloading packages...
"%ProgramFiles%\dotnet\dotnet.exe" restore "..\ServiceCity.sln" -p:Configuration=%releaseconfig%
if %errorlevel% == 0 goto :build
echo There was an error trying to download packages (%errorlevel%).
goto :finished

:build
echo Building ServiceCity...
"%ProgramFiles%\dotnet\dotnet.exe" build "..\ServiceCity.sln" -p:Configuration=%releaseconfig%
if %errorlevel% == 0 goto :success
echo There was an error trying to build (%errorlevel%).
goto :finished

:success
echo Build successful.
:finished
