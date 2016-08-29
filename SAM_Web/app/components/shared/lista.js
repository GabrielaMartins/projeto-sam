'use strict'

//libs
var React = require('react');
var ReactRouter = require('react-router');

var Lista = function(props){
  return(
    <div id="lista">
      <div className="row" id="campoBusca" style={{"marginLeft":"5%", "marginRight":"5%", "marginBottom":"0px"}}>
          <div className="card wrapper">
            <i className="material-icons colorText-default right" >search</i>
            <input
              id="search"
              placeholder={props.placeholder}
              value= {props.consulta}
              className="colorText-default pesquisar"
              onChange = {props.handlePesquisa}
            />
          </div>
        </div>
        <div className="row" style={{"marginLeft":"5%", "marginRight":"5%"}}>
          {props.children}
      </div>
    </div>
  );
}

module.exports = Lista;
