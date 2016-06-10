var React = require('react');
var ReactRouter = require('react-router');
var Dropdown = require('./itensDropdown');


var ItemMenu = React.createClass({
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
    var itensMenuMobile = [];

    //navbar
    this.props.itensMenu.forEach(function(item){
      itensMenu.push(<li key={item}><a className="dropdown-button" data-activates={item}>{item}<i className="material-icons right">arrow_drop_down</i></a></li>);
    });

    //mobile sidebar
    this.props.dropdowns.forEach(function(dropdown){
      if(dropdown.itemMenu != "Gabriela"){

        itensMenuMobile.push(<li>
                                <a className="collapsible-header">{dropdown.itemMenu}<i className="material-icons right">expand_more</i></a>
                                  <div className="collapsible-body">
                                    <Dropdown key = {dropdown.id} itensDrop = {dropdown.itens} itemMenu = {dropdown.itemMenu} isMobile={true}/>
                                  </div>
                              </li>);
      }

    });

    return(
      <div>
        <ul className="right hide-on-med-and-down">
          {itensMenu}
        </ul>
        <div id="slide-out" className="side-nav">
          <div className="row">
            <div className="col s8 offset-s2">
              <img className="center circle" src={this.props.imagemUsuario} style={{height:150, marginTop:12}}/>
            </div>
          </div>
            <a className="center"><b>Olá, {this.props.usuario}</b></a>
            <ul>
              <li className="no-padding">
                <ul className="collapsible" data-collapsible="accordion">
                  {itensMenuMobile}
                </ul>
              </li>
            </ul>
        </div>
      </div>
    );

  }
});

module.exports = ItemMenu;
