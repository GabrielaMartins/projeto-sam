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
    var config = {
      headers: {'token': 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwOi8vb3B1cy5zYW0uY29tIiwiaWF0IjoiXC9EYXRlKDE0NjY3MDkyMjI4OTgpXC8iLCJzdWIiOiJHYWJyaWVsYSBNYXJ0aW5zIiwiY29udGV4dCI6eyJ1c2VyIjp7ImlkIjoyLCJzYW1hY2NvdW50IjoiZ2FicmllbGEifSwicGVyZmlsIjoiRnVuY2lvbsOhcmlvIn19.nXiBQb5npG9QFFv7OYlb2QylCx0tdgloNYwo96uLp8M'}
    };

    //obtém dados
    axios.get("http://10.10.15.113:65122/api/sam/dashboard", config).then(
      function(response){
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
    this.state.dados.UltimosEventos.forEach(function(conteudo){
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

    //cria lista de promocoes
    this.state.dados.ProximasPromocoes.forEach(function(conteudo){
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

    //cria lista de ranking
    this.state.dados.Ranking.forEach(function(rankingCard){
      if(rankingCard.foto == null){
        rankingCard.foto = "./app/imagens/fulano.jpg";
      }
      ranking.push(<CardEventos usuario = {rankingCard} estilo = "card-panel z-depth-1 col l12 m12 s12 waves-effect">
                      <h5 className="right"><b>{rankingCard.pontos} pontos</b></h5>
                    </CardEventos>)
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
