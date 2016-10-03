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
  aprovaSolicitacao: function(){

    //configurações para passar o token
    var token = localStorage.getItem("token");
    var self = this;

    var config = {
      headers: {'token': token}
    };

    var aprovacao = {
      Evento: self.props.atividade.Evento.id,
      Aprova: true
    }

    axios.post(Config.serverUrl + "/api/sam/activity/schedule/approve/", aprovacao, config).then(
      function(response){
        swal({
          title: "Aprovado!",
          text: "A atividade " + self.props.atividade.Evento.Item.nome + " foi aprovada com sucesso!" /*this.props.atividade.Usuario.nome */ + "." ,
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
  desaprovaSolicitacao: function(){

    //configurações para passar o token
    var token = localStorage.getItem("token");
    var self = this;

    var config = {
      headers: {'token': token}
    };

    var aprovacao = {
      Evento: self.props.atividade.Evento.id,
      Aprova: false
    }

    swal({
      title: "Tem certeza?",
      text: "Tem certeza que não deseja aceitar a solicitação de atividade?",
      type: "warning",
      confirmButtonText: "Sim",
      showCancelButton: true,
      cancelButtonText: "Cancelar",
      confirmButtonColor: "#550000"
    },function(){
      axios.post(Config.serverUrl + "/api/sam/activity/schedule/approve/", aprovacao, config).then(
        function(){
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

  return(
    <div id={this.props.index} className="modal modal-fixed-footer">
      <div className="modal-content scrollbar">
        <h2 className="colorText-default center-align"><b>Solicitação de Agendamento</b></h2>
        <br/>
        <div className="row center-align">
          <h5>O funcionário <b className="colorText-default">{this.props.usuario.nome}</b> solicitou o agendamento da atividade <b>{this.props.atividade.Evento.Item.nome}</b>, marcado para o dia <b>{moment(this.props.atividade.Evento.data).format('L')}</b>.</h5>
          <br/>
          <h5>A atividade tem a seguinte descrição:</h5>
          <p>"{this.props.atividade.Evento.Item.descricao}"</p>
          <br/><br/>
          <h4 className="colorText-default">Deseja aceitar a solicitação?</h4>
        </div>
        <br/>
      </div>
      <div className="modal-footer">
        <a className="modal-action modal-close waves-effect waves-green btn-flat" onClick={this.aprovaSolicitacao}>Aceitar</a>
        <a className="modal-action modal-close waves-effect waves-red btn-flat" onClick={this.desaprovaSolicitacao}>Não Aceitar</a>
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
