'use strict'
var React =  require('react');
var AvatarContainer = require('../../containers/usuario/avatarContainer');

var Perfil = function(props){

  return (<AvatarContainer usuario = {props.usuario} />);

}

Perfil.propTypes = {
  usuario: React.PropTypes.object.isRequired
}

module.exports = Perfil;
