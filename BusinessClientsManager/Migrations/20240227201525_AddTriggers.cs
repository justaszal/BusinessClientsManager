using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessClientsManager.Migrations
{
    /// <inheritdoc />
    public partial class AddTriggers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
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
            ");

            migrationBuilder.Sql(@"
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
            ");

            migrationBuilder.Sql(@"
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
            ");

            migrationBuilder.Sql(@"
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
            ");

            migrationBuilder.Sql(@"
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
            ");

            migrationBuilder.Sql(@"
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
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER BusinessClient_Insert");
            migrationBuilder.Sql("DROP TRIGGER BusinessClient_Update");
            migrationBuilder.Sql("DROP TRIGGER BusinessClient_Delete");

            migrationBuilder.Sql("DROP TRIGGER Postcode_Insert");
            migrationBuilder.Sql("DROP TRIGGER Postcode_Update");
            migrationBuilder.Sql("DROP TRIGGER Postcode_Delete");

        }
    }
}
