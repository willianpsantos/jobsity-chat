# jobsity-chat

# Start the project

[ ] Configure the database connection string on appsettings.json, in Jobsity.Chat.API project.

[ ] Run .NET identity migrations

navigate to Jobsity.Chat.Identity project folder via CMD or PowerShell
dotnet ef database update --context Jobsity.Chat.DB.IdentityDataContext -s ..\Jobsity.Chat.API\Jobsity.Chat.API.csproj

[ ] Run de Application migrations

navigate to Jobsity.Chat.DB project folder via CMD or PowerShell
dotnet ef database update --context Jobsity.Chat.DB.JobsityChatDataContext -s ..\Jobsity.Chat.API\Jobsity.Chat.API.csproj

[ ] Include some users using Swagger interface

[ ] Include some rooms using Swagger interface

[ ] Add some users to to the rooms as participantes using Swagger interface

[ ] Start the websocket server

navigate to Jobsity.Chat.Services\Third\SocketServer folder via CMD or PowerShell

if is the first time running the project, please install the npm dependencies running the command npm install

Run the command node .\server.js to start the websocket server (Socket.IO)

[ ] Configure the websockets options on appsettings.json file in Jobsity.Chat.API

[ ] Install the front end dependencies.

navigate to Jobsity.Chat.UI\ClientApp folder

if is the first time running the project, please install the npm dependencies running the command npm install

[ ] Run the application