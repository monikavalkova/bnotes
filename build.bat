@echo off
echo Building and testing bnotes project...
echo.

echo Building .NET API...
dotnet restore
if %ERRORLEVEL% neq 0 exit /b %ERRORLEVEL%

dotnet build --no-restore --configuration Release
if %ERRORLEVEL% neq 0 exit /b %ERRORLEVEL%

echo.
echo Testing .NET API...
dotnet test --no-restore --configuration Release --verbosity normal
if %ERRORLEVEL% neq 0 exit /b %ERRORLEVEL%

echo.
echo Building React UI...
cd bnotes-ui
call npm ci
if %ERRORLEVEL% neq 0 exit /b %ERRORLEVEL%

call npm test -- --watchAll=false --passWithNoTests
if %ERRORLEVEL% neq 0 exit /b %ERRORLEVEL%

call npm run build
if %ERRORLEVEL% neq 0 exit /b %ERRORLEVEL%

cd ..

echo.
echo All builds and tests completed successfully!
