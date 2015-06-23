CREATE TABLE [dbo].[__MigrationHistory2] (
    [MigrationId] [nvarchar](150) NOT NULL,
    [ContextKey] [nvarchar](300) NOT NULL,
    [Model] [varbinary](max) NOT NULL,
    [ProductVersion] [nvarchar](32) NOT NULL,
    CONSTRAINT [PK_dbo.__MigrationHistory2] PRIMARY KEY ([MigrationId], [ContextKey])
)
INSERT INTO [dbo].[__MigrationHistory2]
SELECT LEFT([MigrationId], 150), 'Mem.Data.DataContext', [Model], LEFT([ProductVersion], 32) FROM [dbo].[__MigrationHistory]
DROP TABLE [dbo].[__MigrationHistory]
EXECUTE sp_rename @objname = N'dbo.__MigrationHistory2', @newname = N'__MigrationHistory', @objtype = N'OBJECT'
