BEGIN TRANSACTION;
DECLARE @var sysname;
SELECT @var = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Flights]') AND [c].[name] = N'Number');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [Flights] DROP CONSTRAINT [' + @var + '];');
ALTER TABLE [Flights] ALTER COLUMN [Number] nvarchar(450) NOT NULL;

ALTER TABLE [Flights] ADD [Gate] nvarchar(max) NULL;

UPDATE [Flights] SET [Gate] = NULL
WHERE [Id] = 1;
SELECT @@ROWCOUNT;


UPDATE [Flights] SET [Gate] = NULL
WHERE [Id] = 2;
SELECT @@ROWCOUNT;


CREATE INDEX [IDX_FLIGHT_NUMBER] ON [Flights] ([Number]) WITH (FILLFACTOR = 80);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250116194933_Update1', N'9.0.1');

COMMIT;
GO

