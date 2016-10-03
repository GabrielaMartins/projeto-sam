'use strict'
var React = require('react');
var ReactRouter = require('react-router');
var axios = require("axios");
var Config = require('Config');

var Login = require('../../components/login/login');

var LoginContainer = React.createClass({

  getInitialState: function(){
    return{
      usuario:"",
      senha: "",
      msg: "",

    }
  },

  handleUpdateUsuario: function(event){
    if(event.key == 'Enter'){
      var event = new MouseEvent('click');
      this.handleSubmit(event);
      return;
    }

    this.setState({
      usuario: event.target.value,
      msg: ""
    })
  },

  handleUpdateSenha: function(event){
    if(event.key == 'Enter'){
      var event = new MouseEvent('click');
      this.handleSubmit(event);
      return;
    }

    this.setState({
      senha: event.target.value,
      msg: ""
    })
  },

  handleSubmit: function(e){

    e.preventDefault();
    var self = this;
    self.setState({msg: ""});

    if(self.state.usuario === "" || self.state.senha === ""){
      if(self.state.usuario === ""){
        self.setState({msg: "Preencha o campo usuário"});
      }else{
        self.setState({msg: "Preencha o campo senha"});
      }

    }else{
      var data = {User: self.state.usuario, Password: self.state.senha};
      axios.post( Config.serverUrl+"/api/sam/login", data).then(

  	    // sucesso
        function(response){
          var token = response.data.Token;

          if (typeof(Storage) !== "undefined") {
            localStorage.setItem("token", token);
            localStorage.setItem("samaccount", self.state.usuario);
          }
          
          var config = {
            headers: {'token': token}
          };

          //verifica qual é o perfil do usuário para que seja direcionado para a dashboard correta
          axios.get(Config.serverUrl + "/api/sam/user/" + self.state.usuario, config).then(
            //se sucesso
            function(response){
              //guarda o perfil do usuario no localStorage
              localStorage.setItem("perfil", response.data.perfil);

              if(response.data.perfil.toUpperCase() == "FUNCIONARIO"){
                self.context.router.push('/Dashboard/Funcionario/' + self.state.usuario);
              }else{
                self.context.router.push('/Dashboard/RH/' + self.state.usuario);
              }
            });

        },

        // falha
        function(jqXHR){

          var status = jqXHR.status;

          // usuário não autenticado
          if(status === 401){
            self.setState({msg: "Senha ou usuário inválido"});
          // usuário não encontrado no banco de dados ou no samaccount?
        }else if(status === 404){
            var rota = '/Erro/' + status;
            self.context.router.push({pathname: rota, state: {mensagem: "O usuário não foi encontrado no banco de dados! Por favor, entre em contato com o administrador ou funcionário do RH."}});
          }

        });
    }
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
