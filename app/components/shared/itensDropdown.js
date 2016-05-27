var React = require('react');
var ReactRouter = require('react-router');
var Link = ReactRouter.Link;


var ItensDrop = React.createClass({
  render : function(){
    var itensDrop = [];

    this.props.itensDrop.forEach(function(item){
      itensDrop.push(<li key={item.id}><Link to={item.url}>{item.nome}</Link></li>);
    });

    return(
      <div>
        <ul id={this.props.itemMenu} className="dropdown-content">
          {itensDrop}
        </ul>
      </div>
    );
  }
});

module.exports = ItensDrop;
