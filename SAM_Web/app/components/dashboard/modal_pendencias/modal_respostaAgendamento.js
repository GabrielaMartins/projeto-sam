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

    var config = {
      headers: {'token': token}
    };

    axios.get(Config.serverUrl + "/api/sam/scheduling/approve/" + this.props.atividade.id, config).then(
      function(response){
        //colocar que foi aprovado etc swal
        this.props.handleDeleteAlerta(this.props.index);
      },
      function(){

      }
    )
  },
  desaprovaSolicitacao: function(){
    this.props.handleDeleteAlerta(this.props.index);
  },
  render: function(){
    return(
      <div id={this.props.index} className="modal modal-fixed-footer">
        <div className="modal-content scrollbar">
            <h2 className="colorText-default center-align"><b>Solicitação de Agendamento</b></h2>
            <br/>
            <div className="row center-align">
              <h5>O funcionário <b className="colorText-default">{this.props.usuario.nome}</b> solicitou o agendamento da atividade <b>{this.props.atividade.Item.nome}</b>, marcado para o dia <b>{moment(this.props.atividade.data).format('L')}</b>.</h5>
              <br/>
              <h5>A atividade tem a seguinte descrição:</h5>
              <p>"{this.props.atividade.Item.descricao}"</p>
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

module.exports = Modal;
