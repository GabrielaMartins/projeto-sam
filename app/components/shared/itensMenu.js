var React = require('react');
var ReactRouter = require('react-router');
var ItensDrop = require('./itensDropdown');


var ItemMenu = React.createClass({
  
  componentWillMount: function() {
       this.forceUpdate();
   },
  render : function(){

  (function($) {
      $(function() {
        $('.dropdown-button').dropdown({
          belowOrigin: true,
          alignment: 'left',
          inDuration: 200,
          outDuration: 150,
          constrain_width: true,
          hover: true,
          gutter: 1
      });
    }); // End Document Ready
  })(jQuery); // End of jQuery name space

    var itensMenu = [];
    this.props.itensMenu.forEach(function(item){
      itensMenu.push(<li key={item}><a className="dropdown-button"
      data-activates={item}
      >{item}<i className="material-icons right">arrow_drop_down</i></a></li>);
    });

    return(
      <div>
        <ul className="right hide-on-med-and-down">
          {itensMenu}
        </ul>
      </div>
    );

  }
});

module.exports = ItemMenu;
