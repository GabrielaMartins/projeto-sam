USE SAM;
GO

-- ESSA PROCEDURE AINDA TEM UM PROBLEMA:
-- SE POR EXEMPLO ELA EXECUTAR DIARIAMENTE E
-- O USUARIO X ESTÁ	PRESTES A SER PROMOVIDO, ENTÃO
-- A PROCEDURE IRÁ GERAR NO BANCO VÁRIOS EVENTOS DE PROMOÇÃO
-- REFERENTE AO MESMO EVENTO, POIS SÓ LEVA EM CONTA SE ALGUEM ESTÁ PARA SER PROMOVIDO
-- PRECISAMOS ARRUMAR ISSO

-- cria a procedure que roda como job
CREATE PROCEDURE dbo.geraPromocoes AS

    -- desativa as mensagens para o cliente
	SET NOCOUNT ON;  
    
	-- variavel temporaria
	DECLARE @temp TABLE
	(
	  usuario INT, 
	  cargo_atual INT,
	  proximo_cargo INT,
	  ultimo_item INT,
	  data DATETIME
	);

	-- popula a variavel
	INSERT INTO @temp
		SELECT
			TUsuarios.*,
			TItem.item as ultimo_item, 
			TItem.data
		FROM 
		(SELECT 
			u.id  as usuario, 
			u.cargo as cargo_atual,
			c.id as proximo_cargo
				FROM Cargos c, Usuarios u
				WHERE
				c.id > u.cargo and
				u.pontos >= c.pontuacao and
				u.perfil <> 'rh'

		) AS TUsuarios
		JOIN
		(SELECT
				u.id AS usuario,
				i.id AS item,
				e.data AS data
			FROM
				Usuarios u, Eventos e, Itens i
			WHERE
				e.tipo = 'atividade' AND
				u.id = e.usuario AND
				i.id = e.item AND
				e.id = (SELECT MAX(id) FROM Eventos WHERE usuario = e.usuario and tipo = 'atividade')
		) AS TItem
		ON 
		TUsuarios.usuario = TItem.usuario;

		-- insere um novo evento baseado na tabela temporaria
		INSERT INTO Eventos (item, usuario, data, estado, tipo)
			SELECT
				ultimo_item as item,
				usuario as usuario,
				data as data,
				0 as estado,
				'promocao' as tipo
			FROM @temp

		-- obtem o id desse novo evento
		DECLARE @evento INT;
		SET @evento = (SELECT IDENT_CURRENT('Eventos'));
	
		-- cria a pendencia para o evento de promocao, associada ao funcionario
		INSERT INTO Pendencias(usuario, evento, estado)
			SELECT
				usuario as usuario,
				@evento as evento,
				0 as estado
			FROM @temp

		-- cria as pendencias para o evento de promocao, associadas ao RH
		INSERT INTO Pendencias(usuario, evento, estado)
			SELECT
				u.id as usuario,
				@evento as evento,
				0 as estado
			FROM 
				Usuarios u
			WHERE
				u.perfil = 'rh'
GO 