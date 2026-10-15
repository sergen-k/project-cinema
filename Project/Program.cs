using Microsoft.Data.Sqlite;

// See https://aka.ms/new-console-template for more information

SqliteConnection connection = new SqliteConnection("Data Source=DataSources/project.db");
UserAccess userAccess = new UserAccess(connection);
UserLogic userLogic = new UserLogic(userAccess);

Register.Initialize(userLogic);

Menu.Start();