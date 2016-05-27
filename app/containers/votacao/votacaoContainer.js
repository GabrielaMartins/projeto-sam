var React = require('react');
var Votacao = require('../../components/votacao/votacao');


var VotacaoContainer = React.createClass({
  getInitialState: function() {
    return {
      votos: null,
      evento: null,
      dificuldades:null,
      profundidades:null
    };
  },
  componentWillMount: function(){
    //fazer fetch aqui
    this.setState({
      votos:[
        {
          imagem:"./app/imagens/fulano.jpg",
          nome:"Gabriela",
          dificuldade:"médio",
          profundidade:"profundo"
        },
        {
          imagem:"./app/imagens/fulano.jpg",
          nome:"Telles",
          dificuldade:"fácil",
          profundidade:"profundo"
        },
        {
          imagem:"./app/imagens/fulano.jpg",
          nome:"Thiago",
          dificuldade:"médio",
          profundidade:"profundo"
        },
        {
          imagem:"./app/imagens/fulano.jpg",
          nome:"Jesley",
          dificuldade:"fácil",
          profundidade:"raso"
        },
        {
          imagem:"./app/imagens/fulano.jpg",
          nome:"Vitor",
        },
        {
          imagem:"./app/imagens/fulano.jpg",
          nome:"Caio",
        }
      ],
      evento:{
        nome_item:"AWS",
        descricao:"Amazon Web Services, também conhecido como AWS, é uma coleção de serviços de computação em nuvem ou serviços web, que formam uma plataforma de computação na nuvem oferecida por Amazon.com.",
        categoria_item:"Workshop",
        data_evento:"24/05/2016",
        funcionario:{
          nome:"Tancredo",
          imagem:"./app/imagens/fulano.jpg",
          cargo: "Sênior I",
          prox_cargo: "Sênior II",
          pontos:"160",
          pontos_cargo:"180",
          tempo_casa:"5 anos"
        }
      }
  });
},

  render : function(){
      return(<Votacao votos = {this.state.votos} evento = {this.state.evento}/>)
  }
});

module.exports = VotacaoContainer;
