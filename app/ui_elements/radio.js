'use strict'
var React = require('react');

const Radio = React.createClass({

  render: function(){
    return(
      <div>
        <input
          id = {this.props.id}
          name = {this.props.name}
          type = "radio"
          value = {this.state.value}
          onChange = {this.props.onChange}/>

        <label
          for = {this.props.id}
          onClick = {this.handleLabelClick}>
            {this.state.label}
        </label>
      </div>
    );
  },

  getInitialState: function(){
    var label = this.props.label;
    var value = this.props.value;
    return {
            label: label,
            value: value
          };
  },

  componentWillReceiveProps: function(nextProps) {
    this.setState({
      label: nextProps.label,
      value: nextProps.value
    });
  },

  handleLabelClick: function(){
    var e = document.getElementById(this.props.id);
    e.click();
  }

});

Radio.propTypes = {
   id: React.PropTypes.string.isRequired,
   name: React.PropTypes.string.isRequired,
   label: React.PropTypes.string.isRequired,
   value: React.PropTypes.string.isRequired
};

module.exports = Radio;
