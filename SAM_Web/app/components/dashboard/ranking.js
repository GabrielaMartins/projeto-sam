var React = require('react');
var ReactRouter = require('react-router');
var CardEventos = require('./cardsEventos');
var Link = ReactRouter.Link;

var Ranking = React.createClass({
  render : function(){
    return(
        <div className="row">
          <div className="l4 m12 s12">
            {this.props.ranking}
          </div>
        </div>
    );
  }
});
module.exports = Ranking;
