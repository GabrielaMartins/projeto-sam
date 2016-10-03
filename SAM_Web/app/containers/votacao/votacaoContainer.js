'use strict'

var React = require('react');
var Config = require('Config');
var axios = require("axios");

var Votacao = require('../../components/votacao/votacao');
var Loading = require('react-loading');

var fezFetch = false;

var VotacaoContainer = React.createClass({
  contextTypes: {
    router: React.PropTypes.object.isRequired
  },

  getInitialState: function() {
    return {
      votos: [],
      evento: {},
      dificuldades:undefined,
      profundidades:undefined,
      resultado: false
    };
  },
  handleMostraResultado: function(resposta){
    this.setState({
      resultado: resposta
    })
  },
  componentDidMount: function(){

    //configurações para passar o token
    var token = localStorage.getItem("token");
    var samaccount = localStorage.getItem("samaccount");

    var config = {
      headers: {'token': token}
    };

    var id = this.props.params.id;
    axios.get(Config.serverUrl + "/api/sam/vote/" + id, config).then(
      function(response){
        fezFetch = true;
        this.setState({
          evento:response.data.Evento,
          votos:response.data.Votos,
        });

        for(var i=0; i<this.state.votos.length; i++){
          if(this.state.votos[i].Usuario.samaccount == samaccount){
            this.handleMostraResultado(true);
            break;
          }
        }

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
  componentWillUnmount: function(){
    fezFetch = false;
  },

  render : function(){

      //se não fez fetch, loading
      if(!fezFetch){
        return (
          <div className="full-screen-less-nav">
            <div className="row wrapper">
              <Loading type='bubbles' color='#550000' height={150} width={150}/>
            </div>
          </div>
        );
      }

      var perfil = localStorage.getItem("perfil");
      return(<Votacao votos = {this.state.votos}
                      evento = {this.state.evento}
                      perfil = {perfil}
                      mostraResultado = {this.handleMostraResultado}
                      resultado = {this.state.resultado}/>)
  }
});

module.exports = VotacaoContainer;
