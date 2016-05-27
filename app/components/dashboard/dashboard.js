var React = require('react');
var ReactRouter = require('react-router');
var CardEventos = require('./cardsEventos');
var Ranking = require('./ranking');
var Pendencias = require('./pendencias');
var Chart = require('react-google-charts').Chart;

var Link = ReactRouter.Link;

var Dashboard = function(props){
    var eventos = [];
    var promocoes = [];

    props.cardsDashboard[0].cardConteudo.forEach(function(conteudo){
      eventos.push(<CardEventos conteudo = {conteudo}></CardEventos>);
    });

    props.cardsDashboard[4].cardConteudo.forEach(function(conteudo){
      promocoes.push(<CardEventos conteudo = {conteudo}></CardEventos>);
    });

    return(
      <div style={{marginTop:25}}>
        <div className="row">
          <div className="col l4 m6 s12">
            <div className="card-panel" style={{paddingBottom:29}}>
               <h5 className="card-title center-align colorText-default" ><b>{props.cardsDashboard[0].cardTitulo}</b></h5>
               <div className="eventos card-content" style={{paddingTop:10}}>
                 {eventos}
               </div>
            </div>
            <div className="card-panel" style={{paddingBottom:29}}>
               <h5 className="card-title center-align colorText-default"><b>{props.cardsDashboard[4].cardTitulo}</b></h5>
                 <div className="promocoes card-content" style={{paddingTop:10}}>
                   {promocoes}
                 </div>
            </div>
          </div>
          <div className="col l8 m6 s12">
            <div className="row">
              <div className="card-panel">
                <h5 className="card-title center-align colorText-default"><b>{props.cardsDashboard[1].cardTitulo}</b></h5>
                  <div className="pendencia card-content" style={{paddingTop:10}}>
                    <Pendencias pendencias = {props.cardsDashboard[1].cardConteudo} />
                  </div>
              </div>
            </div>
            <div className="row">
              <div className="card-panel">
                <h5 className="card-title center-align colorText-default"><b>{props.cardsDashboard[2].cardTitulo}</b></h5>
                <div className="card-content center" style={{paddingTop:10}}>
                  <Chart chartType={props.columnChart.chartType} width={"100%"} height={"100%"} data={props.columnChart.data} options = {props.columnChart.options} graph_id={props.columnChart.div_id} />
                </div>
              </div>
            </div>
            <div className="row">
              <div className="card-panel">
                <h5 className="card-title center-align colorText-default"><b>{props.cardsDashboard[3].cardTitulo}</b></h5>
                  <div className="ranking card-content" style={{paddingTop:10}}>
                    <Ranking ranking = {props.cardsDashboard[3].cardConteudo}></Ranking>
                  </div>
              </div>
            </div>
          </div>
      </div>
    </div>
    );
  }

module.exports = Dashboard;
