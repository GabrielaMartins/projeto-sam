'use strict'

//libs
var React = require('react');
var axios = require("axios");
var ReactRouter = require('react-router');
var Link = ReactRouter.Link;

//components
var DashboardRH = require('../../components/dashboard/dashboardRH');
var CardEventos = require('../../components/dashboard/cardsEventos');
var Pendencias = require('../../containers/dashboard/pendenciaContainer');
var Config = require('Config');
var Loading = require('react-loading');

//variável que indica se um componente já possui os dados para renderizar
var fezFetch = false;

var moment = require('moment');
moment.locale('pt-br');

var DashboardContainerRH = React.createClass({
  getInitialState: function() {
    return {
      columnChart: {
        data: [],
        options : {},
        chartType: "",
        div_id: ""
      },
      dados:{
        Atividades:[],
        CertificacoesMaisProcuradas:[],
        ProximasPromocoes:[],
        Ranking:[]
      },
      Pendencias:[]
    };

  },

  handleResize: function(e) {
    if(this._mounted == true){
      this.forceUpdate();
    }
  },
  handleDeletePendencia: function(id){
    var index = -1;
    var pendencias = this.state.Pendencias;
    var pendenciasModificado = [];

    //configurações para passar o token
    var token = localStorage.getItem("token");

    var config = {
      headers: {'token': token}
    };

    //remove do banco
    axios.delete(Config.serverUrl+"/api/sam/pendency/delete/" + id, config).then(
      function(response){

        //atualiza o estado
        for(var i = 0; i < pendencias.length; i++) {
          if (pendencias[i].Id !== id) {
            pendenciasModificado.push(pendencias[i]);
          }
        }
        this.setState({
          Pendencias: pendenciasModificado
        });
      }.bind(this)
    );
  },
  componentDidMount: function(){
    window.addEventListener('resize', this.handleResize);

    var samaccount = this.props.params.samaccount;

    //configurações para passar o token
    var token = localStorage.getItem("token");
    var config = {
      headers: {'token': token}
    };

    //obtém dados
    axios.get(Config.serverUrl + "/api/sam/dashboard", config).then(
      function(response){
        fezFetch = true;
        this.setState({
          dados: response.data,
          Pendencias: response.data.Pendencias,
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
        sr.reveal('.scrollreveal');
      }.bind(this),

      function(jqXHR){
        status = jqXHR.status;
        var rota = '/Erro/' + status;

        //erro 401 - acesso não autorizado
        if(status == "401"){
          this.context.router.push({pathname: rota, state: {mensagem: "Você está tentando acessar uma página que não te pertence, que feio!"}});
        }if(status == "500"){
          this.context.router.push({pathname: rota, state: {mensagem: "O seu acesso expirou, por favor, faça o login novamente."}});
        }else{
          this.context.router.push({pathname: rota, state: {mensagem: "Um erro inesperado aconteceu, por favor, tente mais tarde"}});
        }
      }.bind(this)
    );
  },
  componentDidUpdate: function(){
    //inicializa o modal
    $(window).ready(function() {
      $('.modal-trigger').leanModal({
        dismissible: false
      });
    });
  },
  criaElementoAtualizacoes: function(){

  },
  render : function(){
    if(!fezFetch){
      return (
        <div className="full-screen-less-nav">
          <div className="row wrapper">
            <Loading type='bubbles' color='#550000' height={150} width={150}/>
          </div>
        </div>
      );
    }

    var eventos = [];
    var promocoes = [];
    var ranking = [];
    var atualizacoes = [];

    //cria lista de últimos eventos
    var eventos = this.state.dados.Atividades.map(function(conteudo, index){
      return(
        <Link to={{ pathname: '/Perfil/' + conteudo.Usuario.samaccount}} key={index}>
          <CardEventos key={index} usuario = {conteudo.Usuario} estilo = "card-panel black-text z-depth-1 col l12 m12 s12 waves-effect">
            <div className="right black-text">
              <h5 className="right-align grande">{conteudo.Item.nome}</h5>
              <p className="right-align pequena">{moment(conteudo.data).format('L')}</p>
            </div>
          </CardEventos>
        </Link>
      );
    });

    //cria lista de próximas atualizações
    var promocoes = this.state.dados.ProximasPromocoes.map(function(conteudo, index){
      return (
        <Link to={{ pathname: '/Perfil/' + conteudo.Usuario.samaccount}} key={index}>
          <CardEventos usuario = {conteudo.Usuario} estilo = "card-panel black-text z-depth-1 col l12 m12 s12 waves-effect">
            <div className="left black-text">
              <p className="center-align grande" style={{"lineHeight":"1"}}>Faltam <b>{conteudo.PontosFaltantes}</b> pontos</p>
              <p className="center-align pequena" style={{"lineHeight":"0"}}>{conteudo.Usuario.Cargo.nome} > {conteudo.Usuario.ProximoCargo[0].nome}</p>
              <p className="center-align pequena">Próxima avaliação: <b>{moment(conteudo.DataUltimaPromocao).add(1, 'y').format('L')}</b></p>
            </div>
          </CardEventos>
        </Link>
      );
    });

    //cria lista de ranking
    var ranking = this.state.dados.Ranking.map(function(rankingCard, index){
      return(
        <Link to={{ pathname: '/Perfil/' + rankingCard.samaccount}} key={index}>
          <CardEventos usuario = {rankingCard} estilo = "card-panel black-text z-depth-1 col l12 m12 s12 waves-effect black-text">
            <h5 className="right black-text"><b>{rankingCard.pontos} pontos</b></h5>
          </CardEventos>
        </Link>
      );
    });

    return(<DashboardRH
      eventos = {eventos}
      promocoes = {promocoes}
      pendencias = {this.state.Pendencias}
      ranking = {ranking}
      columnChart = {this.state.columnChart}
      deletePendencia = {this.handleDeletePendencia}/>);

    }
  });

  module.exports = DashboardContainerRH;
