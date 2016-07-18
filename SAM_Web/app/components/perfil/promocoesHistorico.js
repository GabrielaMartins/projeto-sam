var React = require('react');
var ReactRouter = require('react-router');
var moment = require('moment');
moment.locale('pt-br');

var PromocoesHistorico = React.createClass({
  render: function(){
    return(
      <div className="scrollreveal card">
        <div className="card-content">
          <h5 className="card-title center-align"><b>{this.props.conteudo.cargo_atual} > {this.props.conteudo.prox_cargo}</b></h5><br/>
          <h2 className="center-align"><b>{this.props.conteudo.pontos}</b></h2><br/>
          <p className="center-align media"> Alcançada em: <b>{moment(this.props.conteudo.data).format('L')}</b></p>
        </div>
      </div>
    );
  }
});

module.exports = PromocoesHistorico;
