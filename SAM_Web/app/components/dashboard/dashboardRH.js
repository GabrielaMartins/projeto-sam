var React = require('react');
var ReactRouter = require('react-router');
var Chart = require('react-google-charts').Chart;

var Ranking = require('./ranking');
var Pendencias = require('../../containers/dashboard/pendenciaContainer');

var Link = ReactRouter.Link;

var Dashboard = function(props){

    return(
      <div style={{marginTop:25}}>
        <div className="row">
          <div className="col l4 m6 s12">
            <div className="card-panel scrollreveal" style={{paddingBottom:29}}>
               <h5 className="card-title center-align colorText-default" ><b>Últimos Eventos</b></h5>
               <div className="card-content scrollbar eventos" style={{paddingTop:10}}>
                 {props.eventos}
               </div>
            </div>
            <div className="card-panel scrollreveal" style={{paddingBottom:29}}>
               <h5 className="card-title center-align colorText-default"><b>Próximas Avaliações</b></h5>
                 <div className="card-content scrollbar promocoes" style={{paddingTop:10}}>
                   {props.promocoes}
                 </div>
            </div>
          </div>
          <div className="col l8 m6 s12">
            <div className="row">
              <div className="card-panel">
                <h5 className="card-title center-align colorText-default transparent-white scrollreveal"><b>Alertas</b></h5>
                  <div className="card-content scrollbar pendencia" style={{paddingTop:10}}>
                    <Pendencias pendencias = {props.pendencias} tipoPendencia = "pendencia" handleDeleteAlerta = {props.deletePendencia} />
                  </div>
              </div>
            </div>
            <div className="row">
              <div className="card-panel scrollreveal">
                <h5 className="card-title center-align colorText-default"><b>Certificações Mais Procuradas</b></h5>
                <div className="grafico card-content center" style={{paddingTop:10}}>
                  <Chart chartType={props.columnChart.chartType} width={"100%"} height={"200px"} data={props.columnChart.data} options = {props.columnChart.options} graph_id={props.columnChart.div_id} />
                </div>
              </div>
            </div>
            <div className="row">
              <div className="card-panel scrollreveal">
                <h5 className="card-title center-align colorText-default"><b>Ranking</b></h5>
                  <div className="card-content scrollbar ranking" style={{paddingTop:10}}>
                      <Ranking ranking = {props.ranking}></Ranking>
                    </div>
                </div>
            </div>
          </div>
      </div>
    </div>
    );
  }

module.exports = Dashboard;
