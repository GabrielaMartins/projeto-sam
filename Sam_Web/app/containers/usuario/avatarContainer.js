
var React = require('react');
var Avatar = require('../../components/usuario/avatar');

const AvatarContainer = React.createClass({

  render: function(){
    return (<Avatar usuario = {this.props.user} />);
  }

});

Avatar.propTypes = {
  usuario: React.PropTypes.object.isRequired
}

module.exports = Avatar;
