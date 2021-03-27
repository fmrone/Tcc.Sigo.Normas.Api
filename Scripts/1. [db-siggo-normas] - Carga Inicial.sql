USE [bd-tcc-sigo-normas]
GO

DELETE Norma

INSERT Norma (Id, Codigo, Descricao, Area, [Status], CadastradoEm, EmVigorDesde, OrgaoLegal)
SELECT NEWID(), 'NR-5', 'NR-5 – Comissão Interna de Prevenção de Acidentes', 1, 1, GETDATE(), GETDATE(), 'Ministério do Trabalho' UNION ALL
SELECT NEWID(), 'NR-6', 'NR-6 – Equipamento de Proteção Individual (EPI)', 1, 1, GETDATE(), GETDATE(), 'Ministério do Trabalho' UNION ALL
SELECT NEWID(), 'NR-10', 'NR-10 – Segurança em Instalações e Serviços em Eletricidade', 1, 1, GETDATE(), GETDATE(), 'Ministério do Trabalho' UNION ALL
SELECT NEWID(), 'NR-12', 'NR-12 – Segurança no Trabalho em Máquinas e Equipamentos', 1, 1, GETDATE(), GETDATE(), 'Ministério do Trabalho' UNION ALL
SELECT NEWID(), 'NR-15', 'Tratamento do Descarte de Tecidos', 2, 1, GETDATE(), GETDATE(), 'Ministério do Trabalho'


select * from Norma