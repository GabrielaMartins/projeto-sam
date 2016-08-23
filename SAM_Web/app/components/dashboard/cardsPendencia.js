//lib
var React = require('react');

//mommentjs
var moment = require('moment');
moment.locale('pt-br');

var CardPendencia = React.createClass({
  icone : function(){
    if(this.props.conteudo.Evento.tipo == "votacao"){
      return "fa fa-lg fa-gavel";
    }else if(this.props.conteudo.Evento.tipo == "promocao"){
      return "fa fa-lg fa-trophy";
    }else if(this.props.conteudo.Evento.tipo == "atividade"){
      return "fa fa-lg fa-calendar";
    }
  },
  render : function(){
    return(
        <div
          id="pendencias"
          className={this.props.conteudo.status == "Finalizado" ? "card-panel red lighten-3 col l12 s12 m12 waves-effect finalizada colorText-default" : "card-panel green lighten-3 col l12 s12 m12 waves-effect aberta colorText-finalizada"}>
          <div className="card-title center" style={{marginTop:10}}>
              <i className={this.icone()}></i>
            <h5 className="grande">
              <b>{this.props.conteudo.Evento.Item.nome}</b>
            </h5>
          </div>
          <div className="card-content center">
            <p className="media">
              {moment(this.props.conteudo.Evento.data).format('L')}
            </p>
            <p className="media">
              <b>{this.props.conteudo.Evento.tipo}</b> - {this.props.conteudo.Usuario.nome}
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
