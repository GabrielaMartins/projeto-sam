'use strict'

var React = require('react');
var ReactRouter = require('react-router');

var ResultadoVotacao = function(props){

    return(
      <div className="card-panel">
        <h5 className="card-title center-align colorText-default" ><b>Resultado Parcial da Votação</b></h5>
        <Chart chartType={props.typeChart} width={"100%"} height={"200px"} data={props.data} options = {props.options} graph_id={props.div_id} />
      </div>
    );
}

module.exports = SelecaoVoto;
