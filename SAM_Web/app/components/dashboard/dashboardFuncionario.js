var React = require('react');
var ReactRouter = require('react-router');
var Chart = require('react-google-charts').Chart;
var Ranking = require('./ranking');
var CardUsuario = require('../usuario/usuario');
var Pendencias = require('../../components/dashboard/pendencias');
var Link = ReactRouter.Link;

var Dashboard = function(props){
  return(
    <div style={{marginTop:25}}>
      <div className="row">
        <div className="col l3 m12 s12">
          <div className="card-panel scrollreveal" style={{paddingBottom:29}}>
            <h5 className="card-title center-align colorText-default" ><b>Alertas</b></h5>
            <div className="card-content scrollbar alertas" style={{paddingTop:10}}>
              <Pendencias pendencias = {props.alertas} tipoPendencia = "alerta"/>
            </div>
          </div>
        </div>
        <div className="col l6 m12 s12">
          <div className="row">
            <div className="scrollreveal">
              <Link to={{ pathname: '/Perfil/' + props.usuario.samaccount}}>
                <CardUsuario conteudo = {props.usuario}/>
              </Link>
            </div>
          </div>
          <div className="row">
            <div className="card-panel scrollreveal">
              <h5 className="card-title center-align colorText-default transparent-white"><b>Resultado das Últimas Votações</b></h5>
              <div className="card-content scrollbar pendencia" style={{paddingTop:10}}>
                <Pendencias pendencias = {props.resultadoVotacao} tipoPendencia = "resultadoVotacao"/>
              </div>
            </div>
          </div>
        </div>
        <div className="col l3 m12 s12">
          <div className="card-panel scrollreveal" style={{paddingBottom:29}}>
            <h5 className="card-title center-align colorText-default"><b>Últimas Atualizações</b></h5>
            <div className="card-content scrollbar ultimasAtualizacoes" style={{paddingTop:10}}>
              {props.atualizacoes}
            </div>
          </div>
        </div>
        <div className="col s12">
          <div className="card-panel scrollreveal">
            <h5 className="card-title center-align colorText-default"><b>Certificações Mais Procuradas</b></h5>
            <div className="grafico card-content center" style={{paddingTop:10}}>
              <Chart chartType={props.columnChart.chartType} width={"100%"} height={"200px"} data={props.columnChart.data} options = {props.columnChart.options} graph_id={props.columnChart.div_id} />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

module.exports = Dashboard;
