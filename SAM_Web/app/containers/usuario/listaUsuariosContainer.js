"use strict"

var React = require('react');
var Config = require('Config');
var axios = require("axios");

var ListaUsuarios = require('../../components/shared/lista');
var UsuarioCard = require('../../components/usuario/usuarioCard');
var Loading = require('react-loading');

var fezFetch = false;

var ListaUsuariosContainer = React.createClass({
  contextTypes: {
    router: React.PropTypes.object.isRequired
  },

  getInitialState: function() {
    return {
      usuarios: [],
      consulta: "",
      filtro: "",
      cargos: []
    };
  },
  componentDidMount: function(){
    var token = localStorage.getItem("token");
    var config = {
      headers: {'token': token}
    };

    axios.get(Config.serverUrl+"/api/sam/user/all", config).then(
      function(response){
        fezFetch = true;
        this.setState({
          usuarios: response.data
        });
      }.bind(this),
      function(jqXHR){
        status = jqXHR.status;
        var rota = '/Erro/' + status;

        //erro 401 - acesso não autorizado
        if(status == "401"){
          this.context.router.push({pathname: rota, state: {mensagem: "Você está tentando acessar uma página que não te pertence, que feio!"}});
        }if(status == "500"){
          this.context.router.push({pathname: rota, state: {mensagem: "O seu acesso expirou, por favor, faça o login novamente."}});
        }else{
          this.context.router.push({pathname: rota, state: {mensagem: "Um erro inesperado aconteceu, por favor, tente mais tarde"}});
        }
      }.bind(this)
    );

    //obtém os cargos
    axios.get(Config.serverUrl + "/api/sam/role/all").then(
      function(response){
        this.setState({
          cargos:response.data
        });
      }.bind(this),
      function(jqXHR){
        status = jqXHR.status;
        var rota = '/Erro/' + status;

        //erro 401 - acesso não autorizado
        if(status == "401"){
          this.context.router.push({pathname: rota, state: {mensagem: "Você está tentando acessar uma página que não te pertence, que feio!"}});
        }if(status == "500"){
          this.context.router.push({pathname: rota, state: {mensagem: "O seu acesso expirou, por favor, faça o login novamente."}});
        }else{
          this.context.router.push({pathname: rota, state: {mensagem: "Um erro inesperado aconteceu, por favor, tente mais tarde"}});
        }
      }.bind(this)
    );

  },
  handlePesquisa: function(event){
    this.setState({
      consulta: event.target.value
    });
  },

  handleFiltro: function(event){
    this.setState({
      filtro: event.target.value
    });
  },

  handleDesativarUsuario: function(id, nome){
    swal({
      title: "Atenção!",
      text: "Você tem certeza que deseja desativar o usuário " + nome + " ?",
      type: "warning",
      confirmButtonText: "Sim",
      showCancelButton: true,
      cancelButtonText: "Cancelar",
      confirmButtonColor: "#550000"
    },function(){

    });
  },

  render : function(){

    if(!fezFetch){
      return (
        <div className="full-screen-less-nav">
          <div className="row wrapper">
            <Loading type='bubbles' color='#550000' height={150} width={150}/>
          </div>
        </div>
      );
    }
     var placeholder = "Procure por Funcionários";
     var lista = [];
     var self = this;

    this.state.usuarios.forEach(function(usuario, index){
      if(usuario.nome.toLowerCase().indexOf(self.state.consulta.toLowerCase())!=-1 &&
        usuario.Cargo.nome.toLowerCase().indexOf(self.state.filtro.toLowerCase())!=-1){
          lista.push(<div className="col l4 m6 s12" key={index}><UsuarioCard conteudo = {usuario} desativarUsuario = {this.handleDesativarUsuario}/></div>);
        }
    }.bind(this));

    var placeholderOption = "Filtre os Cargos";

    var options = this.state.cargos.map(function(cargo, index){
      return <option key = {index} value={cargo.nome}>{cargo.nome}</option>
    });


    return(
      <ListaUsuarios
        placeholder = {placeholder}
        consulta = {this.state.consulta}
        filtro = {this.state.filtro}
        handlePesquisa = {this.handlePesquisa}
        handleFiltro = {this.handleFiltro}
        optionFiltro = {options}
        placeholderOption = {placeholderOption}>
        {lista}
      </ListaUsuarios>
    );
  }
});

module.exports = ListaUsuariosContainer;
