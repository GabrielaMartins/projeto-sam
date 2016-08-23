var React = require('react');
var DashboardRH = require('../../components/dashboard/dashboardRH');
var axios = require("axios");
var ReactRouter = require('react-router');
var CardEventos = require('../../components/dashboard/cardsEventos');
var Pendencias = require('../../components/dashboard/pendencias');
var moment = require('moment');
moment.locale('pt-br');

var DashboardContainer = React.createClass({
  getInitialState: function() {
    return {
      columnChart: {
				data: [],
        options : {},
        chartType: "",
  			div_id: ""
			},
      dados:{
        UltimosEventos:[{
          Evento:{
            Item:{},
            Usuario:{}
          }
        }],
        ProximasPromocoes:[{
          Usuario:{
            Cargo:{},
            ProximoCargo:[{}]
          },
          PontosFaltantes:0
        }],
        Pendencias:[{}],
        Ranking:[{}]
      },
    };

  },

  handleResize: function(e) {
    this.forceUpdate();
  },
  componentDidMount: function(){
    window.sr = ScrollReveal();
    sr.reveal('.scrollreveal');

    window.addEventListener('resize', this.handleResize);

    var samaccount = this.props.params.samaccount;

    //configurações para passar o token
    var token = localStorage.getItem("token");
    var config = {
      headers: {'token': token}
    };

    //obtém dados
    axios.get("http://10.10.15.113:65122/api/sam/dashboard", config).then(
      function(response){
        debugger;
        this.setState({
          dados: response.data,
          columnChart: {
    				data: response.data.CertificacoesMaisProcuradas,
            options : {
              title: "Certificações mais procuradas por ano",
              bar: {groupWidth: "100%"},
              legend: { position: 'right', maxLines: 3 },
              isStacked: true,
              vAxis: {maxValue: 10, format: '0'}
            },
            chartType: "ColumnChart",
            div_id: "ColumnChart"
    			}
        });
      }.bind(this),
      function(jqXHR){

      }
    );
  },
  render : function(){
    var eventos = [];
    var promocoes = [];
    var ranking = [];

    //cria lista de eventos
    var eventos = this.state.dados.UltimosEventos.map(function(evento, index){
      //if(evento.Usuario.foto == null){
      //  evento.Usuario.foto = "./app/imagens/fulano.jpg"
      //}
      return(
        <CardEventos key={index} usuario = {evento.Usuario} estilo = "card-panel z-depth-1 col l12 m12 s12 waves-effect">
          <div className="right">
              <h5 className="right-align extraGrande">{evento.Item.nome}</h5>
              <p className="right-align pequena">{moment(evento.data).format('L')}</p>
          </div>
        </CardEventos>
      );
    });

    //cria lista de promocoes
    var promocoes = this.state.dados.ProximasPromocoes.map(function(conteudo, index){
      //if(conteudo.Usuario.foto == null){
      //  conteudo.Usuario.foto = "./app/imagens/fulano.jpg"
      //}
      return (
        <CardEventos key={index} usuario = {conteudo.Usuario} estilo = "card-panel z-depth-1 col l12 m12 s12 waves-effect">
          <div className="left">
            <p className="center-align grande">Faltam <b>{conteudo.PontosFaltantes}</b> pontos</p>
            <p className="center-align pequena">{conteudo.Usuario.Cargo.nome} > {conteudo.Usuario.ProximoCargo[0].nome}<br/></p>
          </div>
        </CardEventos>
      );
    });

    //cria lista de ranking
    var ranking = this.state.dados.Ranking.map(function(rankingCard, index){
      //if(rankingCard.foto == null){
      //  rankingCard.foto = "./app/imagens/fulano.jpg";
      //}
      return(
        <CardEventos key={index} usuario = {rankingCard} estilo = "card-panel z-depth-1 col l12 m12 s12 waves-effect">
          <h5 className="right"><b>{rankingCard.pontos} pontos</b></h5>
        </CardEventos>
      );
    });

      return(<DashboardRH
        eventos = {eventos}
        promocoes = {promocoes}
        pendencias = {this.state.dados.Pendencias}
        ranking = {ranking}
        columnChart = {this.state.columnChart}/>)
  }
});

module.exports = DashboardContainer;
