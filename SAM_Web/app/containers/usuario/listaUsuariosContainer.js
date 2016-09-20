"use strict"

var React = require('react');
var ListaUsuarios = require('../../components/shared/lista');
var UsuarioCard = require('../../components/usuario/usuarioCard');
var Config = require('Config');
var Loading = require('react-loading');
var axios = require("axios");

var fezFetch = false;

var ListaUsuariosContainer = React.createClass({
  getInitialState: function() {
    return {
      usuarios: [],
      consulta: "",
      filtro: ""
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
        //sr.reveal('.scrollreveal');
      }.bind(this),
      function(jqXHR){

      }
    );
  },
  handlePesquisa: function(event){
    this.setState({
      consulta: event.target.value
    });
  },

  handleFiltro: function(event){
    debugger;
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

    var cargos = ["Certificação","Curso", "Palestra", "Workshop"];
    var placeholderOption = "Filtre os Cargos";

    var options = cargos.map(function(cargo, index){
      return <option key = {index} value={cargo}>{cargo}</option>
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
