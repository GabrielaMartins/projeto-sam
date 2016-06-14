
var React = require('react');
var Avatar = require('../../components/usuario/avatar');

const AvatarContainer = React.createClass({

  render: function(){
    return (
      <Avatar
        url = {this.props.url}
        nome = {this.props.nome}
      />
    );
  }

});

Avatar.propTypes = {
  url: React.PropTypes.string.isRequired,
  nome: React.PropTypes.string.isRequired
}

module.exports = Avatar;
