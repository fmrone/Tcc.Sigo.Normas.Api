USE [bd-tcc-sigo-normas]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Norma]') AND type in (N'U'))
DROP TABLE [dbo].[Norma]
GO

CREATE TABLE [dbo].[Norma]
(
    Id UNIQUEIDENTIFIER NOT NULL,
    Codigo VARCHAR(6) NOT NULL, -- NR-99, NR-125
    Descricao VARCHAR(100) NOT NULL,
    Area TINYINT NOT NULL,
    [Status] BIT NOT NULL,
    CadastradoEm DATETIME NOT NULL,
    EmVigorDesde DATETIME NOT NULL,
    EmVigorAte DATETIME NULL,
    OrgaoLegal VARCHAR(100) NOT NULL
)
GO

ALTER TABLE [dbo].[Norma]
    ADD CONSTRAINT [PK_Norma] PRIMARY KEY CLUSTERED ([Id])
GO

