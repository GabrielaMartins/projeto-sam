var React = require('react');
var ReactRouter = require('react-router');
var Link = ReactRouter.Link;
var CardPendencia = require('./cardsPendencia');

var Pendencias = React.createClass({
  render : function(){
    var cards = [];

    this.props.pendencias.forEach(function(conteudoCard){
        if(conteudoCard.tipo == "Votação"){
          cards.push(<div className="col l4 m12 s12"><Link to={{ pathname: '/Votacao/1'}}><CardPendencia conteudo = {conteudoCard}/></Link></div>);
        }else{
          cards.push(<div className="col l4 m12 s12"><CardPendencia conteudo = {conteudoCard}/></div>);
        }
    });

    return(
      <div className="row">
        {cards}
      </div>
    );
  }
});

module.exports = Pendencias;
