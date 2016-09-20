'use strict'

//libs
var React = require('react');
var ReactRouter = require('react-router');
var Link = ReactRouter.Link;

//plugin para google charts
var Chart = require('react-google-charts').Chart;

//componentes
var Ranking = require('./ranking');
var CardUsuario = require('../usuario/usuario');
var Pendencias = require('../../containers/dashboard/pendenciaContainer');

var Dashboard = function(props){
  return(
    <div style={{marginTop:25}}>
      <div className="row">
        <div className="col l3 m12 s12">
          <div className="card-panel" style={{paddingBottom:29}}>
            <h5 className="card-title center-align colorText-default scrollreveal" ><b>Alertas</b></h5>
            <div className="card-content scrollbar alertas" style={{paddingTop:10}}>
              <Pendencias pendencias = {props.alertas} tipoPendencia = "alerta" handleDeleteAlerta = {props.handleDeleteAlerta}/>
            </div>
          </div>
        </div>
        <div className="col l6 m12 s12">
          <div className="row">
            <div>
              <Link to={{ pathname: '/Perfil/' + props.usuario.samaccount}}>
                <CardUsuario conteudo = {props.usuario}/>
              </Link>
            </div>
          </div>
          <div className="row">
            <div className="card-panel">
              <h5 className="card-title center-align colorText-default transparent-white scrollreveal"><b>Resultado das Últimas Votações</b></h5>
              <div className="card-content scrollbar pendencia" style={{paddingTop:10}}>
                <Pendencias pendencias = {props.resultadoVotacao} tipoPendencia = "resultadoVotacao"/>
              </div>
            </div>
          </div>
        </div>
        <div className="col l3 m12 s12">
          <div className="card-panel" style={{paddingBottom:29}}>
            <h5 className="card-title center-align colorText-default scrollreveal"><b>Últimos Eventos</b></h5>
            <div className="card-content scrollbar ultimasAtualizacoes" style={{paddingTop:10}}>
              {props.atualizacoes}
            </div>
          </div>
        </div>
        <div className="col s12">
          <div className="card-panel">
            <h5 className="card-title center-align colorText-default scrollreveal"><b>Certificações Mais Procuradas</b></h5>
            <div className="grafico card-content center scrollreveal" style={{paddingTop:10}}>
              <Chart chartType={props.columnChart.chartType} width={"100%"} height={"200px"} data={props.columnChart.data} options = {props.columnChart.options} graph_id={props.columnChart.div_id} />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

Dashboard.propTypes = {
  alertas: React.PropTypes.arrayOf(React.PropTypes.object).isRequired,
  usuario: React.PropTypes.object.isRequired,
  resultadoVotacao: React.PropTypes.arrayOf(React.PropTypes.object).isRequired,
  atualizacoes: React.PropTypes.arrayOf(React.PropTypes.element).isRequired,
}

module.exports = Dashboard;
