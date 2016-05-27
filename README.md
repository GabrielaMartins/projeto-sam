## Projeto SAM ##
O SAM (Sistema de Acompanhamento de Méritos) visa por meio de uma plataforma Web e outra mobile, permitir um total acompanhamento do processo de crescimento técnico do funcionário.  Composto por categorias e seus itens, o SAM é baseado num conjunto de matrizes que quantifica através de pontos cada novo item realizado pelo funcionário.

## Configuração ##

 1. Instale o Node.js : [Installing Node.js and updating npm](https://docs.npmjs.com/getting-started/installing-node)
 2.  Na linha de comando, vá até o diretório do repositório e dê o seguinte comando: `npm init`
 3. Dê enter em todas as opções que aparecerem (se for necessário, mude o nome.
 4. A seguir, entre o seguinte comando `npm install --save react react-dom react-router@2.0.0-rc5 && npm install --save-dev html-webpack-plugin webpack webpack-dev-server babel-{core,loader} babel-preset-react`
 5. Verifique se foi criado um arquivo **package.json**  e uma pasta **node_modules** com todas as dependências.
 6. Caso necessário, baixe uma dependência por vez!
 7. Em **package.json** troque a linha dentro de "scripts" : `test": "echo \"Error: no test specified\"&& exit 1"` por `"production": "webpack -p", "start" : "webpack-dev-server"`
 8. Para inicializar o server e compilar o JSX para JS basta estar dentro do diretório do projeto e entrar o seguinte comando: `npm run start`

Acesse o site em http://localhost:8080/


## Tutoriais de ReacJS ##
 - [ReactJS Fundamentals](http://courses.reactjsprogram.com/courses/reactjsfundamentals)
 - [React Router](https://egghead.io/series/getting-started-with-react-router)
 - [First App](https://egghead.io/series/react-testing-cookbook)
 - [Documentação](https://facebook.github.io/react/)
 - [Adicionais](https://css-tricks.com/search-results/?q=reactjs)

