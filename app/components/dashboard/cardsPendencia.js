var React = require('react');
var ReactRouter = require('react-router');
var Link = ReactRouter.Link;

var CardPendencia = React.createClass({
  render : function(){
    var classes = null;
    if(this.props.conteudo.status == "Finalizado"){
      classes = "card-panel red lighten-3 col l12 s12 m12";
    }else{
      classes = "card-panel green lighten-3 col l12 s12 m12";
    }
    return(
        <div className={classes} id="pendencias">
          <div className="card-title center-align">
            <i className="material-icons medium">cake</i>
            <h5>
              <b>{this.props.conteudo.assunto}</b>
            </h5>
          </div>
          <div className="card-content center-align">
            <span>
              {this.props.conteudo.data}
            </span><br/>
            <span style={{fontSize:10}}>
              {this.props.conteudo.pessoa}
            </span>
          </div>
        </div>
    );
  }
});
module.exports = CardPendencia;
