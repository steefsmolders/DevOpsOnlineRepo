CREATE TABLE [dbo].[Items] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Title]       NVARCHAR (150) NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [Owner]       NVARCHAR (150) NOT NULL,
    [Avatar]      NVARCHAR (255) NULL,
    [Status]      SMALLINT       NOT NULL,
    [Hours]       INT            NOT NULL,
    [DueDate]     DATETIME       NOT NULL,
    CONSTRAINT [PK_dbo.Items] PRIMARY KEY CLUSTERED ([Id] ASC)
);

