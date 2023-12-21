set Server=%1
set DBName=%2
set User=%3
set Password=%4
set NetCoreVersion=%5
dotnet ef dbcontext scaffold --project DatabaseModel\DatabaseModel.csproj --startup-project DatabaseModel\DatabaseModel.csproj --configuration Release --no-build --framework %NetCoreVersion% server=%Server%;database=%DBName%;uid=%User%;pwd=%Password%; Pomelo.EntityFrameworkCore.MySql --context %DBName%DatabaseContext --context-dir Context --force --output-dir Models
dotnet build --configuration Release --no-restore DatabaseModel\DatabaseModel.csproj --framework %NetCoreVersion%
dotnet build --configuration Release --no-restore DatabaseModel\DatabaseModel.csproj --framework %NetCoreVersion%
pause
IF %ERRORLEVEL% NEQ 0 (
    exit /b 1
)

exit /b 0