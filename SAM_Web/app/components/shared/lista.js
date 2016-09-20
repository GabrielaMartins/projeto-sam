'use strict'

//libs
var React = require('react');
var ReactRouter = require('react-router');

var Lista = function(props){
  return(
    <div id="lista">
      <div className="card" id="campoBusca" style={{"marginTop":"0px", "backgroundColor":"#801515"}}>
        <div className="row" style={{"marginBottom":"0px"}}>
            <br/>
            <div className="row" style={{"marginLeft":"5%", "marginRight":"5%"}}>
              <div className="col l10 s8">
                <div className="card wrapper" >
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
              <div className="col l2 s4">
                <div className="right " style={{"marginTop":"10px"}}>
                  <select className="browser-default"
                    style={{"backgroundColor":"white"}}
                    onChange = {props.handleFiltro}
                    value = {props.filtro}>
                    <option value="">Mostrar Todos</option>
                    {props.optionFiltro}
                  </select>
                </div>
              </div>
            </div>
        </div>
      </div>
      <div className="row" style={{"marginLeft":"5%", "marginRight":"5%"}}>
          {props.children}
      </div>
    </div>
  );
}

module.exports = Lista;
