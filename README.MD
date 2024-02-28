# Run the project

**Start the MSSQL Server**
Set the ConnectionStrings in the appsettings.json file.

**Start the .NET Core project**
In the root folder run
`dotnet build` 
`dotnet run .\bin\Debug\net8.0\BusinessClientsManager.dll`

**Run the migrations**
In the BusinessClientsManager folder run
`dotnet ef database update`

**Run the frontend application**
In business-clients-ui folder run
`npm run start`

## Features
- Frontend app renders the business clients list and has a button to update postcodes.
- Swagger website can be used to call all APIs, including the APi to import the clients JSON file.
**Swagger website**: `{baseurl}/swagger/index.html`

