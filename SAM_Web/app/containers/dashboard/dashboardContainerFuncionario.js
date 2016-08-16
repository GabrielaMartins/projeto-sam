var React = require('react');
var axios = require("axios");
var ReactRouter = require('react-router');
var CardEventos = require('../../components/dashboard/cardsEventos');
var Pendencias = require('../../components/dashboard/pendencias');
var DashboardFuncionario = require('../../components/dashboard/dashboardFuncionario');
var moment = require('moment');
moment.locale('pt-br');

var fezFetch = false;

var DashboardContainerFuncionario = React.createClass({
  getInitialState: function() {
    return {
      columnChart: {
        data: [],
        options : {},
        chartType: "",
        div_id: ""
      },
      dados:{
        ResultadoVotacoes:[{}],
        Alertas:[{}],
        Usuario:[{}],
        Atualizacoes:[{}]
      }
    };

  },

  handleResize: function(e) {
    if(this._mounted == true){
      this.forceUpdate();
    }

  },
  componentDidMount: function(){
    this._mounted = true;
    window.addEventListener('resize', this.handleResize);

    var samaccount = this.props.params.samaccount;

    //configurações para passar o token
    var token = localStorage.getItem("token");
    var config = {
      headers: {'token': token}
    };

    //obtém dados
    axios.get("http://sam/api/sam/dashboard", config).then(
      function(response){
        fezFetch = true;
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
        status = jqXHR.status;
        var rota = '/Erro/' + status;
        self.props.history.push({pathname: rota, state: {mensagem: "Um erro inesperado aconteceu, por favor, tente mais tarde"}});
      }
    );
  },
  componentWillUnmount: function(){
    this._mounted = false;
    fezFetch = false;
  },

  render : function(){
    if(!fezFetch){
      return false;
    }
    var atualizacoes = [];
    //cria lista de ultimas atualizações do funcionário
    var atualizacoes = this.state.dados.Atualizacoes.map(function(conteudo, index){
      if(conteudo.tipo != "agendamento"){
        var pontuacao = conteudo.Item.dificuldade * conteudo.Item.modificador * conteudo.Item.Categoria.peso;
        return(<div  key={index} className="eventsCard row">
          <div className="card-panel z-depth-1 col l12 m12 s12 waves-effect" style={{height:100}}>
            <div className="row wrapper">
              <div className="col s5">
                <div className="left">
                  <h5 className="media">{conteudo.Item.nome}</h5>
                </div>
              </div>
              <div className="col s3">
                <div className="center">
                  <span className="extraGrande"><b>{pontuacao}</b></span>
                    <div>
                      <span className="pequena">{conteudo.tipo}</span>
                    </div>
                </div>

              </div>
              <div className="col s4">
                <div className="right">
                  <span className="media"> pontos</span>
                </div>
              </div>
            </div>
          </div>
        </div>);
      }
    });

    return(<DashboardFuncionario
      alertas = {this.state.dados.Alertas}
      usuario = {this.state.dados.Usuario}
      resultadoVotacao = {this.state.dados.ResultadoVotacoes}
      atualizacoes = {atualizacoes}
      columnChart = {this.state.columnChart}/>);

    }
  });

  module.exports = DashboardContainerFuncionario;
