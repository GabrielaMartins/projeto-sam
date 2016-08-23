var React = require('react');
var ListaUsuarios = require('../../components/shared/lista');
var UsuarioCard = require('../../components/usuario/usuarioCard');
var Config = require('Config');

var ListaUsuariosContainer = React.createClass({
  getInitialState: function() {
    return {
      usuarios: null,
      consulta: ""
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
handlePesquisa: function(event){

  this.setState({
    consulta: event.target.value
  });

},

  render : function(){
     var placeholder = "Procure por Funcionários ou Cargos";
     var lista = [];

    this.state.usuarios.forEach(function(usuario){
      if(usuario.nome.toLowerCase().indexOf(self.state.consulta.toLowerCase())!=-1 ||
        item.Cargo.nome.toLowerCase().indexOf(self.state.consulta.toLowerCase())!=-1){
          lista.push(<div className="col l4 m6 s12"><UsuarioCard conteudo = {usuario}/></div>);
        }
    });

    <ListaUsuarios
      placeholder = {placeholder}
      consulta = {this.state.consulta}
      handlePesquisa = {this.handlePesquisa}>
      {lista}
    </ListaUsuarios>
  }
});

module.exports = ListaUsuariosContainer;
