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

    props.ultimosEventos.cardConteudo.forEach(function(conteudo){
      if(conteudo.imagem == null){
        conteudo.imagem = "./app/imagens/fulano.jpg"
      }
      eventos.push(<CardEventos conteudo = {conteudo}></CardEventos>);
    });

    props.proximasPromocoes.cardConteudo.forEach(function(conteudo){
      if(conteudo.imagem == null){
        conteudo.imagem = "./app/imagens/fulano.jpg"
      }
      promocoes.push(<CardEventos conteudo = {conteudo}></CardEventos>);
    });

    return(
      <div style={{marginTop:25}}>
        <div className="row">
          <div className="col l4 m6 s12">
            <div className="card-panel scrollreveal" style={{paddingBottom:29}}>
               <h5 className="card-title center-align colorText-default" ><b>{props.ultimosEventos.cardTitulo}</b></h5>
               <div className="card-content scrollbar eventos" style={{paddingTop:10}}>
                 {eventos}
               </div>
            </div>
            <div className="card-panel scrollreveal" style={{paddingBottom:29}}>
               <h5 className="card-title center-align colorText-default"><b>{props.proximasPromocoes.cardTitulo}</b></h5>
                 <div className="card-content scrollbar promocoes" style={{paddingTop:10}}>
                   {promocoes}
                 </div>
            </div>
          </div>
          <div className="col l8 m6 s12">
            <div className="row">
              <div className="card-panel scrollreveal">
                <h5 className="card-title center-align colorText-default transparent-white"><b>{props.pendencias.cardTitulo}</b></h5>
                  <div className="card-content scrollbar pendencia" style={{paddingTop:10}}>
                      <Pendencias pendencias = {props.pendencias.cardConteudo} />
                  </div>
              </div>
            </div>
            <div className="row">
              <div className="card-panel scrollreveal">
                <h5 className="card-title center-align colorText-default"><b>{props.certificacoesProcuradas.cardTitulo}</b></h5>
                <div className="grafico card-content center" style={{paddingTop:10}}>
                  <Chart chartType={props.columnChart.chartType} width={"100%"} height={"200px"} data={props.columnChart.data} options = {props.columnChart.options} graph_id={props.columnChart.div_id} />
                </div>
              </div>
            </div>
            <div className="row">
              <div className="card-panel scrollreveal">
                <h5 className="card-title center-align colorText-default"><b>{props.ranking.cardTitulo}</b></h5>
                  <div className="card-content scrollbar ranking" style={{paddingTop:10}}>
                      <Ranking ranking = {props.ranking.cardConteudo}></Ranking>
                    </div>
                </div>

            </div>
          </div>
      </div>
    </div>
    );
  }

module.exports = Dashboard;
