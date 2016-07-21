var React = require('react');
var ReactRouter = require('react-router');
var moment = require('moment');
moment.locale('pt-br');

var PromocoesHistorico = React.createClass({
  render: function(){
    return(
      <div className="scrollreveal card">
        <div className="card-content">
          <h5 className="card-title center-align">
            <b>{this.props.promocao.CargoAnterior.nome} > {this.props.promocao.CargoAdquirido.nome}</b>
          </h5><br/>
        <h2 className="center-align"><b>{this.props.promocao.Usuario.pontos}</b></h2><br/>
          <p className="center-align media"> Alcançada em: <b>{moment(this.props.promocao.Data).format('L')}</b></p>
        </div>
      </div>
    );
  }
});

module.exports = PromocoesHistorico;
