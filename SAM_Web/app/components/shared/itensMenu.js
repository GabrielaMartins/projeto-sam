'use strict'

//libs
var React = require('react');
var ReactRouter = require('react-router');
var Link = ReactRouter.Link;

//componente
var Dropdown = require('./itensDropdown');

var ItemMenu = React.createClass({
  itensMenu: function(){
    //navbar
    return(
      this.props.itensMenu.map(function(item, index){
        return(
          <li key={index}>
            <a className="dropdown-button" data-activates={item}>
              {item}<i className="material-icons right">arrow_drop_down</i>
            </a>
          </li>
        );
      })
    );
  },
  itensMenuMobile: function(){
    //mobile sidebar
    var self = this;
    return(
      this.props.dropdowns.map(function(dropdown, index){
        if(dropdown.itemMenu != self.props.usuario){
          return(<li key={index}>
            <a className="collapsible-header">{dropdown.itemMenu}<i className="material-icons right">expand_more</i></a>
            <div className="collapsible-body">
              <Dropdown key = {dropdown.id} itensDrop = {dropdown.itens} itemMenu = {dropdown.itemMenu} isMobile={true}/>
            </div>
          </li>);
        }
      })
    );
  },

  render : function(){
    return(
      <div>
        <ul className="left hide-on-med-and-down">
          {this.itensMenu()}
        </ul>
        <div id="slide-out" className="side-nav">
          <div className="row">
            <div className="col s8 offset-s2">
              <img className="center circle" src={this.props.imagemUsuario} style={{height:150, marginTop:12}}/>
            </div>
          </div>
            <Link to ={this.props.urlUsuario} className="center"><b>Olá, {this.props.usuario}</b></Link>
            <ul>
              <li className="no-padding">
                <ul className="collapsible" data-collapsible="accordion">
                  {this.itensMenuMobile()}
                </ul>
              </li>
              <li>
                <a className="collapsible-header" onClick={this.props.logout}>Logout<i className="fa fa-sign-out right" aria-hidden="true" ></i></a>
              </li>
            </ul>
        </div>
      </div>
    );
  }
});

module.exports = ItemMenu;
