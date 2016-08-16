var React = require('react');
var moment = require('moment');
moment.locale('pt-br');

var CardPendencia = React.createClass({
  render : function(){
    var classes = null;
    if(this.props.conteudo.status == "Finalizado"){
      classes = "card-panel red lighten-3 col l12 s12 m12 waves-effect finalizada colorText-default";
    }else{
      classes = "card-panel green lighten-3 col l12 s12 m12 waves-effect aberta colorText-finalizada";
    }
    return(
        <div className={classes} id="pendencias">
          <div className="card-title center" style={{marginTop:10}}>
            <i className="material-icons small">cake</i>
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
module.exports = CardPendencia;
