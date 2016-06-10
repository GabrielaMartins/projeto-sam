var React = require('react');
var ReactRouter = require('react-router');
var Link = ReactRouter.Link;

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
            <i className="material-icons medium">cake</i>
            <h5 className="extraGrande">
              <b>{this.props.conteudo.assunto}</b>
            </h5>
          </div>
          <div className="card-content center">
            <p className="grande">
              {this.props.conteudo.data}
            </p>
            <span className="media">
              {this.props.conteudo.pessoa}
            </span>
          </div>
        </div>
    );
  }
});
module.exports = CardPendencia;
