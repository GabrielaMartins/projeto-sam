'use strict'

var React = require('react');
var Config = require('Config');
var axios = require("axios");

var SelecaoVoto = require('../../components/votacao/selecaoVoto');

var SelecaoVotoContainer = React.createClass({
  contextTypes: {
    router: React.PropTypes.object.isRequired
  },

  getInitialState: function() {
    return {
      dificuldade:"",
      modificador:"1",
      pontuacaoGerada: 0,
      mensagemErro: "",
      mostraResultado: false
    };
  },

  checaPontuacao: function(){
    var pesoCategoria = this.props.evento.Item.Categoria.peso;
    console.log(pesoCategoria);
    if(this.state.dificuldade != ""){

      //é possível votar o modificador
      if(this.props.tipoVoto.votaModificador === true){

        if( this.state.modificador != ""){
          //pontuação de workshops e palestras é a dificuldade * modificador * 6 (o peso da categoria) <-- obter categoria
          this.setState({pontuacaoGerada: parseInt(this.state.dificuldade) * parseInt(this.state.modificador) * pesoCategoria})
        }
      }else{
        //quando não é possível votar o modificador, apenas calcula os pontos de acordo com a dificuldade e o peso da categoria
        this.setState({pontuacaoGerada: parseInt(this.state.dificuldade) * 1 * pesoCategoria})
      }

    }
  },

  handleChangeDificuldade: function(event){
    this.setState({
      dificuldade:event.target.value,
      mensagemErro:""
    });

    this.checaPontuacao();

  },

  handleChangeModificador: function(event){
    this.setState({
      modificador:event.target.value,
      mensagemErro:""
    });

    this.checaPontuacao();

  },

  handleSubmitPontos: function(){

    var votaModificador = this.props.tipoVoto.votaModificador;
    var tipoModificador = this.props.tipoVoto.tipoModificador;

    if(this.state.dificuldade != ""){
      if(votaModificador === false || (votaModificador === true && this.state.modificador != "")){

        var voto;
        var url;

        //configurações para passar o token
        var token = localStorage.getItem("token");
        var samaccount = localStorage.getItem("samaccount");
        var perfil = localStorage.getItem("perfil");

        var config = {
          headers: {'token': token}
        };

        if(perfil.toUpperCase()==="RH"){

          voto = {
            Evento:this.props.evento.id,
            Modificador:this.state.modificador,
            Dificuldade:this.state.dificuldade
          }

          url = Config.serverUrl+"/api/sam/activity/vote/close"
        }else{
          voto = {
            Usuario:samaccount,
            Evento:this.props.evento.id,
            Modificador:this.state.modificador,
            Dificuldade:this.state.dificuldade
          }

          url = Config.serverUrl+"/api/sam/activity/vote";
        }


        axios.post(url, voto, config).then(
          function(response){
            swal({
              title: "Dados Enviados!",
              text: "Os dados foram salvos com sucesso",
              type: "success",
              confirmButtonText: "Ok",
              confirmButtonColor: "#550000"
            },function(){
              debugger;
              //deleta o alerta
              axios.delete(Config.serverUrl+"/api/sam/pendency/delete/" + this.props.evento.id, config).then(
                function(response){
                  //Retorna para a Dashboard
                  this.context.router.push({pathname: "/dashboard/" + perfil + "/" + samaccount});
                }.bind(this)
              );
            }.bind(this));

          }.bind(this),
          function(jqXHR){
            console.log(jqXHR);
            swal({
              title: "O voto não foi computado!",
              text: "O voto não foi computado, por favor, tente novamente.",
              type: "error",
              confirmButtonText: "Ok",
              confirmButtonColor: "#550000"
            });
          }
        );

      }else{
        if(votaModificador === true && this.state.modificador === ""){
          this.setState({
            mensagemErro: "Insira " + tipoModificador
          });
        }
      }

    }else{
      this.setState({
        mensagemErro: "Insira uma dificuldade"
      });

    }
  },

  componentDidMount: function(){
    $(document).ready(function() {
      $('select').material_select();
    });

    $("#Select1").on('change', this.handleChangeDificuldade);
    $("#Select2").on('change', this.handleChangeModificador);
  },

  votoModificador: function(){
    var tipoVoto;

    if(this.props.tipoVoto.votaModificador === true){
      if(this.props.tipoVoto.tipoModificador === "profundidade"){
        tipoVoto = <div className="input-field col l6 s12 m6">
          <select id="Select2" value={this.state.modificador} onChange={this.handleChangeModificador}>
            <option value="" disabled>Escolha uma opção</option>
            <option value="2">Raso</option>
            <option value="3">Profundo</option>
          </select>
          <label>Profundidade</label>
        </div>
      }else{
        tipoVoto = <div className="input-field col l6 s12 m6">
          <select id="Select2" value={this.state.modificador} onChange={this.handleChangeModificador}>
            <option value="" disabled>Escolha uma opção</option>
            <option value="3">Alinhado</option>
            <option value="1">Não Alinhado</option>
          </select>
          <label>Alinhamento</label>
        </div>
      }
    }else{
      tipoVoto = null;
    }

    return tipoVoto;
  },

  render : function(){

      return(<SelecaoVoto
        pontuacaoGerada = {this.state.pontuacaoGerada}
        changeDificuldade = {this.handleChangeDificuldade}
        dificuldade = {this.state.dificuldade}
        titulo = {this.props.titulo}
        botao = {this.props.botao}
        submit = {this.handleSubmitPontos}
        mensagemErro = {this.state.mensagemErro}
        votoModificador = {this.votoModificador()}
        />)
  }
});

module.exports = SelecaoVotoContainer;
