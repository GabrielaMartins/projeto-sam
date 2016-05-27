'use strict'
var React = require('react');
var Login = require('../components/login');
var ReactRouter = require('react-router');
var helpers = require('../helpers/requestService');

var LoginContainer = React.createClass({
  getInitialState: function(){
    return{
      usuario:"",
      senha: ""
    }
  },

  handleUpdateUsuario: function(event){
    this.setState({
      usuario: event.target.value
    })
  },

  handleUpdateSenha: function(event){
    this.setState({
      senha: event.target.value
    })
  },

  handleSubmit: function(e){
    debugger;
    e.preventDefault();
    var promise = helpers.autenticacao("http://localhost:65120/api/ad/user/authenticate", this.state.usuario, this.state.senha);
    var self = this;
    promise.then(function(response){
      if(response.data == true){
        self.context.router.push('/Dashboard');
      }else{
        alert("Não logou");
      }
    });

    promise.catch(function(response){
      console.log("Erro de conexão.");
    });
  },

  render: function(){
    return(
      <Login
        updateUsuario = {this.handleUpdateUsuario}
        updateSenha = {this.handleUpdateSenha}
        entrar = {this.handleSubmit}
      />
    )
  }
});

LoginContainer.contextTypes = {
  router: React.PropTypes.object.isRequired
};

module.exports = LoginContainer;
