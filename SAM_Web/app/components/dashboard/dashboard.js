var React = require('react');
var ReactRouter = require('react-router');
var CardEventos = require('./cardsEventos');
var Ranking = require('./ranking');
var Pendencias = require('./pendencias');
var moment = require('moment');
moment.locale('pt-br');
var Chart = require('react-google-charts').Chart;

var Link = ReactRouter.Link;

var Dashboard = function(props){
    var eventos = [];
    var promocoes = [];
    
    props.ultimosEventos.forEach(function(conteudo){
      if(conteudo.Evento.Usuario.foto == null){
        conteudo.Evento.Usuario.foto = "./app/imagens/fulano.jpg"
      }
      eventos.push(<CardEventos usuario = {conteudo.Evento.Usuario} estilo = "card-panel z-depth-1 col l12 m12 s12 waves-effect">
                      <div className="right">
                          <h5 className="right-align extraGrande">{conteudo.Evento.Item.nome}</h5>
                          <p className="right-align pequena">{moment(conteudo.Evento.data).format('L')}</p>
                      </div>
                  </CardEventos>);
    });

    props.proximasPromocoes.forEach(function(conteudo){
      if(conteudo.Usuario.foto == null){
        conteudo.Usuario.foto = "./app/imagens/fulano.jpg"
      }
      promocoes.push(<CardEventos usuario = {conteudo.Usuario} estilo = "card-panel z-depth-1 col l12 m12 s12 waves-effect">
                        <div className="left">
                          <p className="center-align grande">Faltam <b>{conteudo.PontosFaltantes}</b> pontos</p>
                          <p className="center-align pequena">{conteudo.Usuario.Cargo.nome} > {conteudo.Usuario.ProximoCargo[0].nome}<br/></p>
                        </div>
                      </CardEventos>);
    });

    return(
      <div style={{marginTop:25}}>
        <div className="row">
          <div className="col l4 m6 s12">
            <div className="card-panel scrollreveal" style={{paddingBottom:29}}>
               <h5 className="card-title center-align colorText-default" ><b>Últimos Eventos</b></h5>
               <div className="card-content scrollbar eventos" style={{paddingTop:10}}>
                 {eventos}
               </div>
            </div>
            <div className="card-panel scrollreveal" style={{paddingBottom:29}}>
               <h5 className="card-title center-align colorText-default"><b>Próximas Promoções</b></h5>
                 <div className="card-content scrollbar promocoes" style={{paddingTop:10}}>
                   {promocoes}
                 </div>
            </div>
          </div>
          <div className="col l8 m6 s12">
            <div className="row">
              <div className="card-panel scrollreveal">
                <h5 className="card-title center-align colorText-default transparent-white"><b>Pendências</b></h5>
                  <div className="card-content scrollbar pendencia" style={{paddingTop:10}}>
                      {/*}<Pendencias pendencias = {props.pendencias.cardConteudo} />*/}
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
