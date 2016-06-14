'use strict'
var axios = require("axios");
var React = require('react');
var Perfil = require('../../components/usuario/perfil');


const PerfilUsuarioContainer = React.createClass({

  render: function(){
    return(
      <div>
        <Perfil
          nome = {this.state.nome}
          url = {this.state.url}
        />
      </div>
    )
  },

  getInitialState: function(){

      return {
            nome: '',
            url: '',
            descricao: ''
      };
  },

  componentWillMount: function(){

      var self = this;
      var token = localStorage.getItem("token");
      var user = this.props.params.samaccount;
      //var config = {
      //  headers: {
      //      token: token
      //  }
      //};
      axios.defaults.headers.common['token'] = token;

      // busca no banco esse samaccount
      axios.get('http://10.10.15.113:65122/api/sam/user/' + user).then(

        // sucesso
        function(response){
          debugger;
          var data = response.data;
          self.setState({nome: data.nome, descricao: data.descricao});
        },

        //falha
        function(jqXHR){
          debugger;
        }

      );

  }

});

module.exports = PerfilUsuarioContainer;
