IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Log] (
    [Id] int NOT NULL IDENTITY,
    [EventType] nvarchar(max) NOT NULL,
    [ObjectName] nvarchar(max) NOT NULL,
    [ObjectId] nvarchar(max) NOT NULL,
    [EventDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Log] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Postcode] (
    [Name] nvarchar(10) NOT NULL,
    [City] nvarchar(80) NOT NULL,
    CONSTRAINT [PK_Postcode] PRIMARY KEY ([Name])
);
GO

CREATE TABLE [BusinessClient] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Address] nvarchar(200) NOT NULL,
    [PostcodeName] nvarchar(10) NOT NULL,
    CONSTRAINT [PK_BusinessClient] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BusinessClient_Postcode_PostcodeName] FOREIGN KEY ([PostcodeName]) REFERENCES [Postcode] ([Name]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_BusinessClient_PostcodeName] ON [BusinessClient] ([PostcodeName]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240225212735_InitializeClientDB', N'8.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[BusinessClient]') AND [c].[name] = N'Name');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [BusinessClient] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [BusinessClient] ALTER COLUMN [Name] nvarchar(450) NOT NULL;
GO

CREATE UNIQUE INDEX [IX_BusinessClient_Address] ON [BusinessClient] ([Address]);
GO

CREATE UNIQUE INDEX [IX_BusinessClient_Name] ON [BusinessClient] ([Name]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240225221236_AddIndicesToClient', N'8.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [BusinessClient] DROP CONSTRAINT [FK_BusinessClient_Postcode_PostcodeName];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[BusinessClient]') AND [c].[name] = N'PostcodeName');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [BusinessClient] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [BusinessClient] ALTER COLUMN [PostcodeName] nvarchar(10) NULL;
GO

ALTER TABLE [BusinessClient] ADD CONSTRAINT [FK_BusinessClient_Postcode_PostcodeName] FOREIGN KEY ([PostcodeName]) REFERENCES [Postcode] ([Name]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240225222456_ClientPostCodeNotReq', N'8.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240227180350_AddBusinessClientForeignKey', N'8.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Log] ADD [NextObject] nvarchar(max) NULL;
GO

ALTER TABLE [Log] ADD [PrevObject] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240227195617_AddLogObjectColumns', N'8.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

                CREATE TRIGGER BusinessClient_Insert
                ON [dbo].[BusinessClient]
                AFTER INSERT
                AS
                BEGIN
                    INSERT INTO [dbo].Log(
                        ObjectId,
		                ObjectName,
                        EventDate,
		                EventType,
		                PrevObject,
		                NextObject
                    )
                    VALUES(
                        (SELECT TOP 1 id FROM Inserted),
		                'BusinessClient',
                        CURRENT_TIMESTAMP,
		                'INSERT',
		                null,
		                (SELECT * FROM Inserted FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
                    );
                END
GO

                CREATE TRIGGER BusinessClient_Update
                ON [dbo].[BusinessClient]
                AFTER UPDATE
                AS
                BEGIN
                    INSERT INTO [dbo].Log(
                        ObjectId,
		                ObjectName,
                        EventDate,
		                EventType,
		                PrevObject,
		                NextObject
                    )
                    VALUES(
                        (SELECT TOP 1 id FROM Inserted),
		                'BusinessClient',
                        CURRENT_TIMESTAMP,
		                'UPDATE',
		                (SELECT * FROM Deleted FOR JSON PATH, WITHOUT_ARRAY_WRAPPER),
		                (SELECT * FROM Inserted FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
                    );
                END
GO

                CREATE TRIGGER BusinessClient_Delete
                ON [dbo].[BusinessClient]
                AFTER DELETE
                AS
                BEGIN
                    INSERT INTO [dbo].Log(
                        ObjectId,
		                ObjectName,
                        EventDate,
		                EventType,
		                PrevObject,
		                NextObject
                    )
                    VALUES(
                        (SELECT TOP 1 id FROM Deleted),
		                'BusinessClient',
                        CURRENT_TIMESTAMP,
		                'DELETE',
		                (SELECT * FROM Deleted FOR JSON PATH, WITHOUT_ARRAY_WRAPPER),
		                null
                    );
                END
GO

                CREATE TRIGGER Postcode_Insert
                ON [dbo].[Postcode]
                AFTER INSERT
                AS
                BEGIN
                    INSERT INTO [dbo].Log(
                        ObjectId,
		                ObjectName,
                        EventDate,
		                EventType,
		                PrevObject,
		                NextObject
                    )
                    VALUES(
                        (SELECT TOP 1 name FROM Inserted),
		                'Postcode',
                        CURRENT_TIMESTAMP,
		                'INSERT',
		                null,
		                (SELECT * FROM Inserted FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
                    );
                END
GO

                CREATE TRIGGER Postcode_Update
                ON [dbo].[Postcode]
                AFTER UPDATE
                AS
                BEGIN
                    INSERT INTO [dbo].Log(
                        ObjectId,
		                ObjectName,
                        EventDate,
		                EventType,
		                PrevObject,
		                NextObject
                    )
                    VALUES(
                        (SELECT TOP 1 name FROM Inserted),
		                'Postcode',
                        CURRENT_TIMESTAMP,
		                'UPDATE',
		                (SELECT * FROM Deleted FOR JSON PATH, WITHOUT_ARRAY_WRAPPER),
		                (SELECT * FROM Inserted FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
                    );
                END
GO

                CREATE TRIGGER Postcode_Delete
                ON [dbo].[Postcode]
                AFTER DELETE
                AS
                BEGIN
                    INSERT INTO [dbo].Log(
                        ObjectId,
		                ObjectName,
                        EventDate,
		                EventType,
		                PrevObject,
		                NextObject
                    )
                    VALUES(
                        (SELECT TOP 1 name FROM Deleted),
		                'Postcode',
                        CURRENT_TIMESTAMP,
		                'DELETE',
		                (SELECT * FROM Deleted FOR JSON PATH, WITHOUT_ARRAY_WRAPPER),
		                null
                    );
                END
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240227201525_AddTriggers', N'8.0.2');
GO

COMMIT;
GO

