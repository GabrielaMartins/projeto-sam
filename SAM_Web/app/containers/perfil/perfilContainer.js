'use strict'
var axios = require("axios");
var React = require('react');
var Perfil = require('../../components/perfil/perfil');

const PerfilUsuarioContainer = React.createClass({

  render: function(){
    return(<Perfil usuario = {this.state.usuario} />);
  },

  getInitialState: function(){
      return {
            usuario: {}
      };
  },

  componentWillMount: function(){
      var self = this;
      var token = localStorage.getItem("token");
      var usuario = this.props.params.samaccount;

      axios.defaults.headers.common['token'] = token;

      // busca no banco esse samaccount
      axios.get('http://10.10.15.81:65122/api/sam/user/' + usuario).then(

        // sucesso
        function(response){
          var u = response.data;
          self.atualizaEstado(u);
        },

        //falha
        function(jqXHR){
          debugger;
        }

      );
  },

  atualizaEstado: function(novoEstado){
    debugger;
    var usuario = novoEstado;
    var tempoDeCasa = this.calculaTempoDeCasa(usuario.dataInicio);

    usuario.tempoDeCasa = tempoDeCasa;
    delete usuario['dataInicio'];
    this.setState({usuario: usuario});
  },

  calculaTempoDeCasa: function(dataInicio){
    debugger;
    var padrao = /(\d{4})-(\d{1,2})-(\d{1,2})/g;
    var match = padrao.exec(dataInicio);

    var anoInicio = parseInt(match[1]);
    var diaInicio = parseInt(match[2]);
    var mesInicio = parseInt(match[3]);

    var inicio = new Date(anoInicio + '-' + mesInicio + '-' + diaInicio);
    var atual = new Date();

    return new Date(atual - inicio);

  }

});

module.exports = PerfilUsuarioContainer;
