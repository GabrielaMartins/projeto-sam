var React = require('react');
var ReactRouter = require('react-router');
var CardEventos = require('./cardsEventos');
var Link = ReactRouter.Link;

var Ranking = React.createClass({
  render : function(){
    var ranking = [];
    this.props.ranking.forEach(function(rankingCard){
      if(rankingCard.foto == null){
        rankingCard.foto = "./app/imagens/fulano.jpg";
      }
      ranking.push(<CardEventos usuario = {rankingCard} estilo = "card-panel z-depth-1 col l12 m12 s12 waves-effect">
                      <h5 className="right"><b>{rankingCard.pontos} pontos</b></h5>
                    </CardEventos>)
    });
    return(
        <div className="row">
          <div className="l4 m12 s12">
            {ranking}
          </div>
        </div>
    );
  }
});
module.exports = Ranking;
