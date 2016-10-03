'use strict'

//libs
var React = require('react');
var ReactRouter = require('react-router');
var Link = ReactRouter.Link;

var Modal = React.createClass({
  render: function(){
    var titulo;
    var mensagem;

    if(this.props.atividade.Estado === false){
      titulo = "Promoção em Andamento!";
      mensagem = <p className="center-align grande">Você atingiu os pontos necessários para ser promovido, aguarde a decisão do RH.</p>
    }else{
      if(this.props.atividade.Evento.estado === true){
        titulo = "Parabéns!!!";
        mensagem = <img className="responsive-img center-block" src="./app/imagens/baloes.png" style={{height:300}}/>;
      }else{
        titulo = "Promoção não Aceita!";
        mensagem = <p className="center-align grande">A sua promoção não foi aceita.</p>
      }
    }

    return(
      <div id={this.props.index} className="modal modal-fixed-footer">
        <div className="modal-content scrollbar">
          <h2 className="colorText-default center-align"><b>{titulo}</b></h2>
          <br/>
          <div className="row center-block">
            {mensagem}
          </div>
          <br/>
        </div>
        <div className="modal-footer">
          <a className="modal-action modal-close waves-effect waves-red btn-flat" onClick={this.props.handleDeleteAlerta.bind(null, this.props.index)}>Fechar</a>
        </div>
      </div>
    );
  }
});

Modal.propTypes = {
  handleDeleteAlerta: React.PropTypes.func.isRequired,
  index: React.PropTypes.number.isRequired
}

module.exports = Modal;
