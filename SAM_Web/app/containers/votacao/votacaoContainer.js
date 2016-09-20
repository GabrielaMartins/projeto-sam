var React = require('react');
var Votacao = require('../../components/votacao/votacao');
var Config = require('Config');
var axios = require("axios");

var fezFetch = false;

var VotacaoContainer = React.createClass({
  getInitialState: function() {
    return {
      votos: [],
      evento: {},
      dificuldades:undefined,
      profundidades:undefined,
      resultado: false
    };
  },
  componentWillMount: function(){
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

      }
    );

  },
  componentWillUnmount: function(){
    fezFetch = false;
  },
  
  render : function(){
      if(!fezFetch){
        return null;
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
