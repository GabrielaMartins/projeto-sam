--Próximo cargo
SELECT u.nome, u.cargo, u.foto, u.pontos, c.nome, c.pontuacao FROM CARGOS c, USUARIOS u WHERE c.anterior = u.cargo;