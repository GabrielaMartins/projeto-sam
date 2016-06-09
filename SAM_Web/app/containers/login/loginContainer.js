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

  /*  var config = {headers: {
     'Content-Type': 'application/x-www-form-urlencoded',
     'Accept': 'application/json, text/plain,  *'
   }};*/

/*var param = $.param({
    User: self.state.usuario,
    Password: self.state.senha
});debugger;*/

    axios.post("http://10.10.15.113:65122/api/sam/login",
    {
        User: self.state.usuario,
        Password: self.state.senha
    }).then(
      // sucesso
      function(response){
        debugger;
        var token = response.data;

        if (typeof(Storage) !== "undefined") {
          localStorage.setItem("token", token);
        } else {
          // Sorry! No Web Storage support..
        }

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
