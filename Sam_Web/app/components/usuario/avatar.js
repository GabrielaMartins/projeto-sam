'use strict'
var React = require('react');

var Avatar = function(props){
  return (
    <div>
      <img src={props.url}/>
      <label>{props.nome}</label>
    </div>
  );
}

module.exports = Avatar;
