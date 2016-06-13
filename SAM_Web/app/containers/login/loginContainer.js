'use strict'
var React = require('react');
var Login = require('../../components/login/login');
var ReactRouter = require('react-router');
var axios = require("axios");

var LoginContainer = React.createClass({

  getInitialState: function(){
    return{
      usuario:"",
      senha: "",
      msg: ""
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

    var self = this;
    e.preventDefault();

    self.setState({msg: ""});
    
    var data = {User: self.state.usuario, Password: self.state.senha};
    axios.post("http://10.10.15.113:65122/api/sam/login", data).then(

	    // sucesso
      function(response){
        debugger;
        var token = response.data.token;

        if (typeof(Storage) !== "undefined") {
          localStorage.setItem("token", token);
        } else {
          // Sorry! No Web Storage support..
        }

        self.context.router.push('/Dashboard');
      },

      // falha
      function(jqXHR){
        debugger;

		var status = jqXHR.status;

		// usuário não autenticado
		if(status === 401){
			console.log("Avisar que errou a senha ou usuario");
      self.setState({msg: "Invalid username or password"});
		// usuário não encontrado no banco de dados
		}else if(status === 404){
			console.log("Avisar que não está cadastrado");
		}
      });
  },

  render: function(){
    return(
      <Login
        msg = {this.state.msg}
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
