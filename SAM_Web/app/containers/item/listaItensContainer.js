var React = require('react');
var ListaItens = require('../../components/shared/lista');


var ListaItensContainer = React.createClass({
  getInitialState: function() {
    return {
      itens: null,
    };
  },
  componentWillMount: function(){
    //fazer fetch aqui
    this.setState({
      itens:[
        {
          nome:"AWS",
          categoria: "Workshop",
          dificuldade: "Difícil",
          status:true,
          pontos: 40,
          id:1,
          feitoPor:[
            {
              nome:"Tancredo",
              imagem:"./app/imagens/fulano.jpg",
              id:1
            },
            {
              nome:"Vinicius",
              imagem:"./app/imagens/fulano.jpg",
              id:2
            },
            {
              nome:"Reginaldo",
              imagem:"./app/imagens/fulano.jpg",
              id:3
            }
          ]
        },
        {
          nome:"Java 1",
          categoria: "Certificação",
          dificuldade: "Difícil",
          status:true,
          pontos: 40,
          id:2,
          feitoPor:[
            {
              nome:"Gabriel",
              imagem:"./app/imagens/fulano.jpg",
              id:4
            },
            {
              nome:"Daniel",
              imagem:"./app/imagens/fulano.jpg",
              id:5
            },
            {
              nome:"Bruno",
              imagem:"./app/imagens/fulano.jpg",
              id:6
            }
          ]
        },
        {
          nome:"ReactJs",
          categoria: "Workshop",
          dificuldade: "Médio",
          status:false,
          pontos: 20,
          id:3,
          feitoPor:[
            {
              nome:"Thiago",
              imagem:"./app/imagens/fulano.jpg",
              id:7
            },
            {
              nome:"Gabriel",
              imagem:"./app/imagens/fulano.jpg",
              id:8
            }
          ]
        },
        {
          nome:"Angular",
          categoria: "Workshop",
          dificuldade: "Médio",
          status:true,
          pontos: 60,
          id:4,
          feitoPor:[
            {
              nome:"Gabriela",
              imagem:"./app/imagens/fulano.jpg",
              id:9
            },
            {
              nome:"Jesley",
              imagem:"./app/imagens/fulano.jpg",
              id:10
            }
          ]
        },
        {
          nome:"SQL Server 1",
          categoria: "Certificação",
          dificuldade: "Difícil",
          status:true,
          pontos: 80,
          id:5,
          feitoPor:[
            {
              nome:"Márcio",
              imagem:"./app/imagens/fulano.jpg",
              id:11
            },
            {
              nome:"Marcos",
              imagem:"./app/imagens/fulano.jpg",
              id:12
            },
            {
              nome:"Maurício",
              imagem:"./app/imagens/fulano.jpg",
              id:13
            }
          ]
        }]
  });
},

  render : function(){
      return(<ListaItens itens = {this.state.itens}/>)
  }
});

module.exports = ListaItensContainer;
