USE SAM;
GO

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
	  data DATE
	);

	-- popula a variavel
	INSERT INTO @temp
		SELECT
			TUsuarios.*,
			TItem.item as ultimo_item, 
			TItem.data
		FROM 

		-- recupera usuários aptos a promoção
		(SELECT 
			u.id  as usuario, 
			u.cargo as cargo_atual,
			c.id as proximo_cargo
				FROM Cargos c, Usuarios u
				 
				WHERE
				c.pontuacao > (SELECT pontuacao from Cargos c2 WHERE c2.id = u.cargo) and
				u.pontos >= c.pontuacao and
				u.perfil <> 'rh'
		) AS TUsuarios
		JOIN

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
		) AS TItem
		ON 
		TUsuarios.usuario = TItem.usuario;
	
		-- insere um novo evento baseado na tabela temporaria
		INSERT INTO Eventos (item, usuario, data, estado, tipo)
			SELECT
				t.ultimo_item as item,
				t.usuario as usuario,
				t.data as data,
				0 as estado,
				'promocao' as tipo
			FROM
				@temp t
			WHERE
				NOT EXISTS (
					SELECT *
					FROM 
						Eventos e
					WHERE
						t.ultimo_item = e.item AND
						t.data = e.data AND 
						t.usuario = e.usuario AND
						e.tipo = 'promocao'
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