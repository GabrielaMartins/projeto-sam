'use strict'
var React =  require('react');
var AvatarContainer = require('../../containers/usuario/avatarContainer');

var Perfil = function(props){

  return(
      <div>
          <AvatarContainer
            nome = {props.nome}
            url = {props.url}
          />
      </div>
    );

}

Perfil.propTypes = {
  nome: React.PropTypes.string.isRequired,
  url: React.PropTypes.string.isRequired
}

module.exports = Perfil;
