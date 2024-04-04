IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'imagile-local-shared')
BEGIN
    CREATE DATABASE [imagile-local-shared];
END
GO