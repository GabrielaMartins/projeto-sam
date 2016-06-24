var React = require('react');
var Dashboard = require('../../components/dashboard/dashboard');
var axios = require("axios");

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
      return(<Dashboard
        ultimosEventos = {this.state.dados.UltimosEventos}
        proximasPromocoes = {this.state.dados.ProximasPromocoes}
        pendencias = {this.state.dados.Pendencias}
        ranking = {this.state.dados.Ranking}
        columnChart = {this.state.columnChart}/>)
  }
});

module.exports = DashboardContainer;
