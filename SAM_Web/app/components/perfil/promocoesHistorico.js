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
            <b>{this.props.promocao.CargoAnterior.nome}</b> <i className="fa fa-chevron-right fa-1x"></i> <b>{this.props.promocao.CargoAdquirido.nome}</b>
          </h5>
          <h2 className="center-align colorText-default"><b>{this.props.promocao.Usuario.pontos}</b></h2>
          <p className="center-align media"> Alcançada em: <b>{moment(this.props.promocao.Data).format('L')}</b></p>
          <br/>
        </div>
      </div>
    );
  }
});

module.exports = PromocoesHistorico;
