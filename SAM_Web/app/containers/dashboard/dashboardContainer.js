var React = require('react');
var Dashboard = require('../../components/dashboard/dashboard');
var axios = require("axios");

var DashboardContainer = React.createClass({
  getInitialState: function() {
    return {
      ultimosEventos: {
        cardTitulo: "",
        cardConteudo: []
      },
      proximasPromocoes:{
        cardTitulo: "",
        cardConteudo: []
      },
      pendencias:{
        cardTitulo: "Pendências",
        cardConteudo: []
      },
      certificacoesProcuradas:{
        cardTitulo: "Certificações mais Procuradas",
      },
      ranking:{
        cardTitulo: "Ranking",
        cardConteudo: []
      },
      columnChart: {
				data: [],
        options : {},
        chartType: "",
  			div_id: ""
			}
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

    //Busca no banco as pendências e o perfil do usuário
    //axios.post("")

    //Ultimos Eventos
    axios.get("http://localhost:65120/api/sam/Dashboard/ultimosEventos").then(
      function(response){
        this.setState({
          ultimosEventos: {
            cardTitulo : "Últimos Eventos",
            cardConteudo : response.data
          }
        })
      }.bind(this));

      //Próximas Promoções
      axios.get("http://localhost:65120/api/sam/Dashboard/proximasPromocoes").then(
        function(response){
          this.setState({
            proximasPromocoes: {
              cardTitulo : "Próximas Promoções",
              cardConteudo : response.data
            }
          })
        }.bind(this));

        //Ranking
        axios.get("http://localhost:65120/api/sam/Dashboard/ranking").then(
          function(response){
            this.setState({
              ranking: {
                cardTitulo : "Ranking",
                cardConteudo : response.data
              }
            })
          }.bind(this));

          //Gráfico: certificações mais procuradas
          axios.get("http://localhost:65120/api/sam/Dashboard/certificacoesProcuradas").then(

            function(response){
              debugger;
              this.setState({
                columnChart: {
                  data: response.data,
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
              })
            }.bind(this));

  },
  render : function(){
      return(<Dashboard
        ultimosEventos = {this.state.ultimosEventos}
        proximasPromocoes = {this.state.proximasPromocoes}
        pendencias = {this.state.pendencias}
        certificacoesProcuradas = {this.state.certificacoesProcuradas}
        ranking = {this.state.ranking}
        columnChart = {this.state.columnChart}/>)
  }
});

module.exports = DashboardContainer;
