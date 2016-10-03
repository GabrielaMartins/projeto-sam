--CREATE DATABASE SAM;
USE SAM;
GO

-- CRIAÇÃO DA PROCEDURE
-- cria a procedure que roda como job
CREATE PROCEDURE dbo.geraPromocoes AS

    -- desativa as mensagens para o cliente
	SET NOCOUNT ON;  
    
		-- variavel temporaria
	DECLARE @temp TABLE
	(
	  usuario INT, 
	  --cargo_atual INT,
	  --proximo_cargo INT,
	  ultimo_item INT,
	  data DATE
	);

	-- popula a variavel
	INSERT INTO @temp
		SELECT
			TUsuarios.*,
			TItem.item as ultimo_item,
			TItem.data --(nao precisa)
		FROM 

		-- recupera usuários aptos a promoção
		(SELECT DISTINCT
			--(SELECT c3.nome FROM Cargos c3 WHERE c3.id = u.cargo) as cargo_atual,
			--c.nome as proximo_cargo,
			--c.pontuacao as pontuacao_proximo_cargo,
			u.id  as usuario
				FROM
					Cargos c,
					(SELECT
						id, cargo, pontos
					 FROM
						Usuarios u
					WHERE
						u.perfil <> 'rh' AND
						u.dataAvaliacao = Convert(date, getdate())
					) as u
				WHERE
					-- filtra cargos com pontuação igual ou maior do que o cargo atual do usuário
					c.pontuacao >= (SELECT pontuacao from Cargos c2 WHERE c2.id = u.cargo) AND
					
					-- filtra cargos diferente do cargo atual do usuário
					c.id <> (SELECT id from Cargos c3 WHERE c3.id = u.cargo) AND

					-- filtra usuários com pontuação maior ou igual a requerida pelo cargo
					u.pontos >= c.pontuacao

				--ORDER BY c.pontuacao DESC

		) AS TUsuarios LEFT JOIN

		-- recupera ultimo item de cada usuario apto a promoção
		(SELECT
				u.id AS usuario,
				i.id AS item,
				e.data AS data
			FROM
				Usuarios u, Eventos e, Itens i
			WHERE
				e.tipo = 'atividade' AND
				e.processado = 1 AND
				e.estado = 1 AND
				u.id = e.usuario AND
				i.id = e.item AND
				e.id = (SELECT MAX(id) FROM Eventos WHERE usuario = e.usuario and tipo = 'atividade')
		
		) AS TItem ON TUsuarios.usuario = TItem.usuario;

		-- para cada usuario apto a ser promovido, cria um evento de promocao
		INSERT INTO Eventos (item, usuario, data, estado, tipo)
			SELECT
				t.ultimo_item as item,
				t.usuario as usuario,
				null as data, -- diz que a data da promocao ainda nao foi atribuida
				0 as estado,
				'promocao' as tipo
			FROM
				@temp t
			WHERE
				NOT EXISTS (
					SELECT
						usuario
						item,
						data
					FROM 
						Eventos e
					WHERE
						e.tipo = 'promocao' AND
						t.usuario = e.usuario AND
						t.ultimo_item = e.item AND
						(t.data = e.data OR t.data IS NULL AND e.data IS NULL)
												
				);

		-- cria a pendencia para cada evento de promocao e associa ao funcionario no evento
		INSERT INTO Pendencias(usuario, evento, estado)
			SELECT
				p.usuario as usuario,
				p.id as evento,
				0 as estado
			FROM
				(SELECT usuario, id FROM Eventos e WHERE e.tipo = 'promocao') as p
			
		-- cria as pendencias para cada evento de promocao e associa ao RH
		INSERT INTO Pendencias(usuario, evento, estado)
			SELECT
				u.usuario as usuario,
				p.id as evento,
				0 as estado
			FROM
				(SELECT usuario, id FROM Eventos e WHERE e.tipo = 'promocao') as p,
				(SELECT id as usuario FROM Usuarios usr WHERE usr.perfil = 'rh') as u
GO

CREATE TABLE Cargos
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	nome VARCHAR(50) UNIQUE NOT NULL,
	anterior INT REFERENCES Cargos(id) ON DELETE NO ACTION,
	pontuacao INT NOT NULL
);

CREATE TABLE Usuarios
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	cargo INT FOREIGN KEY REFERENCES Cargos(id) ON DELETE NO ACTION,
	pontos INT NOT NULL DEFAULT 0,
	samaccount VARCHAR(50) UNIQUE NOT NULL,
	nome VARCHAR(50) NOT NULL,
	descricao VARCHAR(200),
	github VARCHAR(100),
	facebook VARCHAR(100),
	linkedin VARCHAR(100),
	perfil VARCHAR(20) NOT NULL DEFAULT 'funcionario' CHECK(perfil IN ('funcionario','rh')),
	dataInicio DATE NOT NULL,
	dataAvaliacao DATE NOT NULL,
	foto VARCHAR(300),
	ativo BIT NOT NULL DEFAULT 1
);

CREATE TABLE Categorias
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	nome VARCHAR(50) UNIQUE NOT NULL,
	peso INT NOT NULL CHECK(peso IN (1, 3, 5, 6, 20))
);

CREATE TABLE Tags
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	descricao VARCHAR(100) UNIQUE NOT NULL
);

CREATE TABLE Itens
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	nome VARCHAR(50) NOT NULL,
	descricao VARCHAR(200),
	votado BIT NOT NULL DEFAULT 0,
	dificuldade INT NOT NULL CHECK(dificuldade IN (1,3,8)),
	modificador INT NOT NULL CHECK(modificador IN (1,2,3,8)),
	categoria INT FOREIGN KEY REFERENCES Categorias(id) ON DELETE NO ACTION NOT NULL
);

CREATE TABLE ItensTagged
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	item INT FOREIGN KEY REFERENCES Itens(id) ON DELETE CASCADE,
	tag  INT FOREIGN KEY REFERENCES Tags(id) ON DELETE CASCADE
);

CREATE TABLE Eventos
(
	id INT IDENTITY(1,1) PRIMARY KEY,

	-- assunto do evento
	item INT FOREIGN KEY REFERENCES Itens(id) ON DELETE NO ACTION,

	-- usuário que gerou o evento (possivelmente quem precisa ganhar pontos)
	usuario INT FOREIGN KEY REFERENCES Usuarios(id) ON DELETE NO ACTION,

	data DATE,
	estado BIT NOT NULL DEFAULT 0,
	processado BIT NOT NULL DEFAULT 0,
	tipo VARCHAR(12) NOT NULL CHECK(tipo IN ('atividade', 'votacao','atribuicao','promocao','agendamento')),
);

CREATE TABLE Pendencias
(
	id INT IDENTITY(1,1) PRIMARY KEY,

	-- usuário a qual a pendência foi atribuída
	usuario INT FOREIGN KEY REFERENCES Usuarios(id) ON DELETE CASCADE,

	-- evento do qual se trata a pendência
	evento INT FOREIGN KEY REFERENCES Eventos(id) ON DELETE CASCADE,

	-- diz se essa pendência foi ou nao resolvida
	estado BIT NOT NULL DEFAULT 1,
);


CREATE TABLE ResultadoVotacoes
(
	id INT IDENTITY(1,1) PRIMARY KEY,

	-- evento sendo votado
	evento INT FOREIGN KEY REFERENCES Eventos(id) ON DELETE NO ACTION,

	-- usuário que votou
	usuario INT FOREIGN KEY REFERENCES Usuarios(id) ON DELETE NO ACTION,

	-- valor que foi votado
	dificuldade INT NOT NULL CHECK(dificuldade IN (1,3,8)),
	
	-- modificador que foi votado
	modificador INT NOT NULL CHECK(modificador IN (1,2,3,8))
);

CREATE TABLE Promocoes
(
	id INT IDENTITY(1,1) PRIMARY KEY,

	-- usuário a qual a promocao foi atribuída
	usuario INT FOREIGN KEY REFERENCES Usuarios(id) ON DELETE CASCADE,

	cargoanterior INT FOREIGN KEY REFERENCES Cargos(id),

	cargoadquirido INT FOREIGN KEY REFERENCES Cargos(id),

	data DATE NOT NULL
);

-- CRIAÇÃO DE DADOS
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
SELECT 3, N'Júnior 1', 2, 0 UNION ALL
SELECT 4, N'Júnior 2', 3, 60 UNION ALL
SELECT 5, N'Júnior 3', 4, 120 UNION ALL
SELECT 6, N'Sênior 1', 5, 720 UNION ALL
SELECT 7, N'Sênior 2', 6, 960 UNION ALL
SELECT 8, N'Sênior 3', 7, 1200 UNION ALL
SELECT 9, N'Pleno 1', 8, 240 UNION ALL
SELECT 10, N'Pleno 2', 9, 360 UNION ALL
SELECT 11, N'Pleno 3', 10, 480
COMMIT;
RAISERROR (N'[dbo].[Cargos]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[Cargos] OFF;

INSERT INTO Itens VALUES ('Java Programmer', 'Oracle Certified Professional, Java SE 7 Programmer', 8, 3, 1);
INSERT INTO Itens VALUES ('AngularJS', 'Curso AngularJS para WebApps RESTful', 3, 3, 2);
INSERT INTO Itens VALUES ('AWS', 'Amazon Web Services, também conhecido como AWS, é uma coleção de serviços de computação em nuvem ou serviços web, que formam uma plataforma de computação na nuvem oferecida por Amazon.com.', 8, 3, 7);