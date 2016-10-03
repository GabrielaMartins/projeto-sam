'use strict'

//lib
var React = require('react');

//momentjs
var moment = require('moment');
moment.locale('pt-br');

var CardPendencia = React.createClass({
  render : function(){
    console.log(this.props.conteudo.Evento.Item);
    return(
        <div
          id="pendencias"
          className={this.props.conteudo.Estado == true ? "card-panel red lighten-3 col l12 s12 m12 waves-effect finalizada colorText-default" : "card-panel green lighten-3 col l12 s12 m12 waves-effect aberta colorText-finalizada"}>
          <div className="card-title center" style={{marginTop:10}}>
              <i className={this.props.icone}></i>
            <h5 className="grande">
              {this.props.conteudo.Evento.Item != null?
                <b>{this.props.conteudo.Evento.Item.nome}</b>
                :
                null
              }

            </h5>
          </div>
          <div className="card-content center">
            <p className="media">
              {moment(this.props.conteudo.Evento.data).format('L')}
            </p>
            <p className="media">
              <b>{this.props.conteudo.Evento.tipo}</b> - {this.props.conteudo.Evento.Usuario.nome}
            </p>
          </div>
        </div>
    );
  }
});

CardPendencia.propTypes = {
  conteudo: React.PropTypes.object.isRequired,
}

module.exports = CardPendencia;
