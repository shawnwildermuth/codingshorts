BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250130013434_Nickname'
)
BEGIN
    ALTER TABLE [Flights] ADD [Nickname] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250130013434_Nickname'
)
BEGIN
    EXEC(N'UPDATE [Flights] SET [Nickname] = NULL
    WHERE [Id] = 1;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250130013434_Nickname'
)
BEGIN
    EXEC(N'UPDATE [Flights] SET [Nickname] = NULL
    WHERE [Id] = 2;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250130013434_Nickname'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250130013434_Nickname', N'9.0.1');
END;

COMMIT;
GO

