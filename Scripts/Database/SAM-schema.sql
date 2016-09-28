CREATE DATABASE SAM;
USE SAM;
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
	foto VARCHAR(300),
	ativo BIT NOT NULL DEFAULT 1
);

CREATE TABLE Categorias
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	nome VARCHAR(50) UNIQUE NOT NULL,
	peso INT NOT NULL CHECK(peso IN (3, 5, 6, 20))
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

	data DATETIME UNIQUE,
	estado BIT NOT NULL DEFAULT 0,
	processado BIT NOT NULL DEFAULT 0,
	tipo VARCHAR(12) UNIQUE NOT NULL CHECK(tipo IN ('votacao','atribuicao','promocao','agendamento')),
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