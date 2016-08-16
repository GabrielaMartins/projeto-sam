var React = require('react');
var ReactRouter = require('react-router');
var Link = ReactRouter.Link;
var CardPendencia = require('./cardsPendencia');

var Pendencias = React.createClass({
  render : function(){
    var cards = [];
    var self = this;
    self.props.pendencias.forEach(function(conteudoCard, index){
        if(conteudoCard.Evento.tipo == "Votação" || self.props.tipoPendencia == "resultadoVotacao"){
          cards.push(<div key={index} className={self.props.tipoPendencia == "alerta" ? "col l12 m12 s12" : "col l6 m12 s12"}>
                        <Link to={{ pathname: '/Votacao/' + conteudoCard.Evento.id}}>
                          <CardPendencia conteudo = {conteudoCard}/>
                        </Link>
                      </div>);
        }else{
          cards.push(<div key={index} classname = {self.props.tipoPendencia == "alerta" ? "col l12 m12 s12" : "col l6 m12 s12"}>
                        <CardPendencia conteudo = {conteudoCard}/>
                      </div>);
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
