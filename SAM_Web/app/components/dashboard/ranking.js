'use strict'

var React = require('react');

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

Ranking.propTypes = {
  ranking: React.PropTypes.arrayOf(React.PropTypes.element).isRequired
}

module.exports = Ranking;
