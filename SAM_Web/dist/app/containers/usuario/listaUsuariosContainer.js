var React = require('react');
var ListaUsuarios = require('../../components/shared/lista');


var ListaUsuariosContainer = React.createClass({
  getInitialState: function() {
    return {
      usuarios: null,
    };
  },
  componentDidMount: function(){
    window.sr = ScrollReveal();
    sr.reveal('.scrollreveal');
  },
  componentWillMount: function(){
    //fazer fetch aqui
    this.setState({
      usuarios:[
        {
          nome:"Tancredo",
          imagem:"./app/imagens/fulano.jpg",
          cargo: "Sênior I",
          prox_cargo: "Sênior II",
          pontos:"160",
          pontos_cargo:"180",
          tempo_casa:"5 anos"
        },
        {
          nome:"Gabriela",
          imagem:"./app/imagens/fulano.jpg",
          cargo: "Estagiário",
          prox_cargo: "Júnior I",
          pontos:"50",
          pontos_cargo:"100",
          tempo_casa:"1 ano"
        },
        {
          nome:"Jesley",
          imagem:"./app/imagens/fulano.jpg",
          cargo: "Estagiário",
          prox_cargo: "Júnior I",
          pontos:"60",
          pontos_cargo:"100",
          tempo_casa:"1 ano"
        },
        {
          nome:"Teles",
          imagem:"./app/imagens/fulano.jpg",
          cargo: "Sênior I",
          prox_cargo: "Sênior II",
          pontos:"130",
          pontos_cargo:"180",
          tempo_casa:"4 anos"
        },
        {
          nome:"Daniel",
          imagem:"./app/imagens/fulano.jpg",
          cargo: "Júnior I",
          prox_cargo: "Júnior II",
          pontos:"160",
          pontos_cargo:"180",
          tempo_casa:"2 anos"
        },
        {
          nome:"Thiago",
          imagem:"./app/imagens/fulano.jpg",
          cargo: "Sênior I",
          prox_cargo: "Sênior II",
          pontos:"130",
          pontos_cargo:"180",
          tempo_casa:"4 anos"
        }
      ]
  });
},

  render : function(){
      return(<ListaUsuarios usuarios = {this.state.usuarios}/>)
  }
});

module.exports = ListaUsuariosContainer;
