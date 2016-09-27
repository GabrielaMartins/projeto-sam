'use strict'

//libs
var React = require('react');
var ReactRouter = require('react-router');
var Config = require('Config');
var Link = ReactRouter.Link;
var axios = require("axios");

//momentjs
var moment = require('moment');
moment.locale('pt-br');

var Modal = React.createClass({
  atribuiPontos: function(){

    //configurações para passar o token
    var token = localStorage.getItem("token");

    var config = {
      headers: {'token': token}
    };

    var atribuicao = {
      Usuario: this.props.atividade.Evento.Usuario.samaccount,
      Evento: this.props.atividade.Evento.id,
      ReceberPontos: true
    }

    var pontos = this.props.atividade.Evento.Item.Categoria.peso * this.props.atividade.Evento.Item.dificuldade * this.props.atividade.Evento.Item.modificador;
    var self = this;

    axios.post(Config.serverUrl + "/api/sam/activity/assignment/", atribuicao, config).then(
      function(response){
        swal({
          title: "Aprovado!",
          text: pontos + " pontos foram atribuídos para o funcionário " + self.props.atividade.Evento.Usuario.nome + "." ,
          type: "success",
          confirmButtonText: "Ok",
          confirmButtonColor: "#550000"
        },function(){
          self.props.handleDeleteAlerta(self.props.index);
        });
      },
      function(){
        swal({
          title: "Algum Erro Ocorreu!",
          text: "A atividade " + self.props.atividade.Evento.Item.nome + " não pode ser confirmada no momento, tente novamente mais tarde." /*this.props.atividade.Usuario.nome */ + "." ,
          type: "error",
          confirmButtonText: "Ok",
          confirmButtonColor: "#550000"
        });
      }
    )
  },
  naoAtribuiPontos: function(){
    //configurações para passar o token
    var token = localStorage.getItem("token");

    var config = {
      headers: {'token': token}
    };

    var atribuicao = {
      Usuario: this.props.atividade.Evento.Usuario.samaccount,
      Evento: this.props.atividade.Evento.id,
      ReceberPontos: false
    }
    var self = this;

    swal({
      title: "Tem certeza?",
      text: "Tem certeza que não deseja atribuir os pontos para o funcionário "  + /*this.props.atividade.Usuario.nome*/ + "",
      type: "warning",
      confirmButtonText: "Sim",
      showCancelButton: true,
      cancelButtonText: "Cancelar",
      confirmButtonColor: "#550000"
    },function(){
      axios.post(Config.serverUrl + "/api/sam/activity/assignment/", atribuicao, config).then(
        function(response){
          self.props.handleDeleteAlerta(self.props.index);
        },
        function(){
          swal({
            title: "Algum Erro Ocorreu!",
            text: "A atividade " + self.props.atividade.Evento.Item.nome + " não pode ser confirmada no momento, tente novamente mais tarde." /*this.props.atividade.Usuario.nome */ + "." ,
            type: "error",
            confirmButtonText: "Ok",
            confirmButtonColor: "#550000"
          });
        }
      );
    });
  },
  render: function(){
    var pontos = this.props.atividade.Evento.Item.Categoria.peso * this.props.atividade.Evento.Item.dificuldade * this.props.atividade.Evento.Item.modificador;

    return(
      <div id={this.props.index} className="modal modal-fixed-footer">
        <div className="modal-content scrollbar">
            <h2 className="colorText-default center-align"><b>Atribuição de Pontos</b></h2>
            <br/>
            <div className="row center-align">
              <h5>O funcionário <b className="colorText-default">{this.props.usuario.nome}</b> realizou a atividade <b>{this.props.atividade.Evento.Item.nome}</b>, no dia <b>{moment(this.props.atividade.Evento.data).format('L')}</b>.</h5>
              <br/>
              <h5>A atividade gerou: <b>{pontos}</b> pontos.</h5>
              <br/><br/>
              <h4 className="colorText-default">Deseja atribuir os pontos?</h4>
            </div>
            <br/>
        </div>
        <div className="modal-footer">
          <a className="modal-action modal-close waves-effect waves-green btn-flat" onClick={this.atribuiPontos}>Atribuir</a>
          <a className="modal-action modal-close waves-effect waves-red btn-flat" onClick={this.naoAtribuiPontos}>Não Atribuir</a>
        </div>
    </div>
    );
  }
});

Modal.propTypes = {
  atividade: React.PropTypes.object.isRequired,
  usuario: React.PropTypes.object.isRequired,
  handleDeleteAlerta: React.PropTypes.func.isRequired,
  index: React.PropTypes.number.isRequired
}

module.exports = Modal;
