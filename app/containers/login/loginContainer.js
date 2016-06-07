'use strict'
var React = require('react');
var Login = require('../../components/login/login');
var ReactRouter = require('react-router');
var axios = require("axios");

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
    var self = this;
    e.preventDefault();

    axios.post("localhost:65120/api/sam/login",
    {
        user: self.state.usuario,
        password: self.state.senha
    }).then(
      // sucesso
      function(response){
        debugger;
        self.context.router.push('/Dashboard');
      },
      // falha
      function(jqXHR, textStatus, errorThrown){
        debugger;
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
