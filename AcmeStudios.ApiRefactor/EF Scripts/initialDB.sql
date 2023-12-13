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

CREATE TABLE [Characters] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [HitPoints] int NOT NULL,
    [Strength] int NOT NULL,
    [Defense] int NOT NULL,
    [Intelligence] int NOT NULL,
    [RpgClass] int NOT NULL,
    CONSTRAINT [PK_Characters] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [StudioItemTypes] (
    [StudioItemTypeId] int NOT NULL IDENTITY,
    [Value] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_StudioItemTypes] PRIMARY KEY ([StudioItemTypeId])
);
GO

CREATE TABLE [StudioItems] (
    [StudioItemId] int NOT NULL IDENTITY,
    [Acquired] datetime2 NOT NULL,
    [Sold] datetime2 NULL,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [SerialNumber] nvarchar(max) NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [SoldFor] decimal(18,2) NULL,
    [Eurorack] bit NOT NULL,
    [StudioItemTypeId] int NOT NULL,
    CONSTRAINT [PK_StudioItems] PRIMARY KEY ([StudioItemId]),
    CONSTRAINT [FK_StudioItems_StudioItemTypes_StudioItemTypeId] FOREIGN KEY ([StudioItemTypeId]) REFERENCES [StudioItemTypes] ([StudioItemTypeId]) ON DELETE CASCADE
);
GO

CREATE TABLE [StudioItemImages] (
    [Id] int NOT NULL IDENTITY,
    [FileTitle] nvarchar(max) NULL,
    [FileData] varbinary(max) NULL,
    [StudioItemId] int NULL,
    CONSTRAINT [PK_StudioItemImages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_StudioItemImages_StudioItems_StudioItemId] FOREIGN KEY ([StudioItemId]) REFERENCES [StudioItems] ([StudioItemId]) ON DELETE NO ACTION
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'StudioItemTypeId', N'Value') AND [object_id] = OBJECT_ID(N'[StudioItemTypes]'))
    SET IDENTITY_INSERT [StudioItemTypes] ON;
INSERT INTO [StudioItemTypes] ([StudioItemTypeId], [Value])
VALUES (1, N'Synthesiser'),
(2, N'Drum Machine'),
(3, N'Effect'),
(4, N'Sequencer'),
(5, N'Mixer'),
(6, N'Oscillator'),
(7, N'Utility');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'StudioItemTypeId', N'Value') AND [object_id] = OBJECT_ID(N'[StudioItemTypes]'))
    SET IDENTITY_INSERT [StudioItemTypes] OFF;
GO

CREATE INDEX [IX_StudioItemImages_StudioItemId] ON [StudioItemImages] ([StudioItemId]);
GO

CREATE INDEX [IX_StudioItems_StudioItemTypeId] ON [StudioItems] ([StudioItemTypeId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200513185727_InitialDbCreation', N'6.0.15');
GO

COMMIT;
GO