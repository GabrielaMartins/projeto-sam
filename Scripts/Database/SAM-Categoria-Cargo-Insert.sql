USE [SAM];
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

SET IDENTITY_INSERT [dbo].[Categorias] ON;

BEGIN TRANSACTION;
INSERT INTO [dbo].[Categorias]([id], [nome], [peso])
SELECT 1, N'Curso', 20 UNION ALL
SELECT 2, N'Blog Técnico', 5 UNION ALL
SELECT 3, N'Comunidade de Software', 1 UNION ALL
SELECT 4, N'Apresentação', 6 UNION ALL
SELECT 5, N'Repositório de Código', 3
COMMIT;
RAISERROR (N'[dbo].[Categorias]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[Categorias] OFF;

SET IDENTITY_INSERT [dbo].[Cargos] ON;

BEGIN TRANSACTION;
INSERT INTO [dbo].[Cargos]([id], [nome], [anterior], [pontuacao])
SELECT 1, N'Estagiário', NULL, 0 UNION ALL
SELECT 2, N'Técnico', 1, 0 UNION ALL
SELECT 3, N'Júnior 1', 2, 60 UNION ALL
SELECT 4, N'Júnior 2', 3, 120 UNION ALL
SELECT 5, N'Júnior 3', 4, 180 UNION ALL
SELECT 6, N'Sênior 1', 5, 780 UNION ALL
SELECT 7, N'Sênior 2', 6, 1020 UNION ALL
SELECT 8, N'Sênior 3', 7, 1260 UNION ALL
SELECT 9, N'Pleno 1', 8, 300 UNION ALL
SELECT 10, N'Pleno 2', 9, 420 UNION ALL
SELECT 11, N'Pleno 3', 10, 540
COMMIT;
RAISERROR (N'[dbo].[Cargos]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[Cargos] OFF;

