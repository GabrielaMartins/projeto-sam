var React = require('react');
var ReactRouter = require('react-router');
var CardEventos = require('./cardsEventos');
var Link = ReactRouter.Link;

var Ranking = React.createClass({
  render : function(){
    var ranking = [];
    this.props.ranking.forEach(function(rankingCard){
      ranking.push(<CardEventos conteudo={rankingCard}></CardEventos>)
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
