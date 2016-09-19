'use strict'
var React = require('react');
var axios = require("axios");
var ReactRouter = require('react-router');
var Router = ReactRouter.Router;
var Link = ReactRouter.Link;

var Atualizacoes = require('../../components/dashboard/atualizacoes');
var DashboardFuncionario = require('../../components/dashboard/dashboardFuncionario');
var Loading = require('react-loading');

var Config = require('Config');

//variável que indica se um componente já possui os dados para renderizar
var fezFetch = false;

var DashboardContainerFuncionario = React.createClass({
  contextTypes: {
    router: React.PropTypes.object.isRequired
  },

  //estado inicial dos objetos
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
        Usuario:[{}],
        Atualizacoes:[{}]
      },
      alertas:[]
    };

  },

  //força a re-renderização do gráfico quando a tela muda de tamanho
  handleResize: function(e) {
    if(this._mounted == true){
      this.forceUpdate();
    }
  },

  handleDeleteAlerta: function(id){
    var index = -1;
    var alertas = this.state.alertas;
    var alertasModificado = [];

    //configurações para passar o token
    var token = localStorage.getItem("token");

    var config = {
      headers: {'token': token}
    };

    //remove do banco
    axios.delete(Config.serverUrl+"/api/sam/pendency/delete/" + id, config).then(
      function(response){

        //atualiza o estado
        for(var i = 0; i < alertas.length; i++) {
          if (alertas[i].id !== id) {
            alertasModificado.push(alertas[i]);
          }
        }
        this.setState({
          alertas: alertasModificado
        });
      }.bind(this),
      function(jqXHR){

      }
    );
  },

  componentDidMount: function(){
    this._mounted = true;

    //verifica se a janela mudou de tamanho e força o update
    window.addEventListener('resize', this.handleResize);

    //samaccount passado pela url
    var samaccount = this.props.params.samaccount;

    //configurações para passar o token
    var token = localStorage.getItem("token");

    var config = {
      headers: {'token': token}
    };

    //obtém dados
    axios.get(Config.serverUrl+"/api/sam/dashboard/", config).then(
      function(response){
        debugger;
        fezFetch = true;
        this.setState({
          dados: response.data,
          alertas: response.data.Alertas,
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

        //inicializa o efeito para aparece os elementos quando faz scroll
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

  componentWillUnmount: function(){
    this._mounted = false;
    fezFetch = false;
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
    var atualizacoes = [];

    //cria lista de ultimas atualizações do funcionário
    var atualizacoes = this.state.dados.Atualizacoes.map(function(conteudo, index){

      //lista de usuarios que fez uma atividade para inserir no modal de cada item
      var usuarios = null;
      usuarios = conteudo.Item.Usuarios.map(function(usuario, index){
        return(
          <div key={index} className="col l4 m4 s6 wrapper">
            {/*<Link to={"/Perfil/"+ usuario.samaccount}>*/}
            <div>
              <img className="responsive-img circle center-block" src={usuario.foto} style={{height:50}}/>
              <p className="center-align colorText-default"><b>{usuario.nome}</b></p>
                <br/>
                <br/>
            </div>
            {/*</Link>*/}
          </div>
        );
      });

      //agendamento não é um evento realizado. Aqui deve aparecer apenas os elementos realizados
      if(conteudo.tipo != "agendamento"){
        var pontuacao = conteudo.Item.dificuldade * conteudo.Item.modificador * conteudo.Item.Categoria.peso;
        return(
          <Atualizacoes key={index} pontuacao = {pontuacao} conteudo = {conteudo} usuarios = {usuarios}/>
          );
      }
    });

    return atualizacoes;
  },



  render : function(){

    //se a tela não possui dados para renderizar, então não renderiza (mudar posteriormente para loading)
    if(fezFetch == false){
      return (
        <div className="full-screen-less-nav">
          <div className="row wrapper">
            <Loading type='bubbles' color='#550000' height={150} width={150}/>
          </div>
        </div>
      );
    }

    var atualizacoes = this.criaElementoAtualizacoes();

    return(
      <DashboardFuncionario
        alertas = {this.state.alertas}
        usuario = {this.state.dados.Usuario}
        resultadoVotacao = {this.state.dados.ResultadoVotacoes}
        atualizacoes = {atualizacoes}
        columnChart = {this.state.columnChart}
        handleDeleteAlerta = {this.handleDeleteAlerta}/>
    );

    }
  });

  module.exports = DashboardContainerFuncionario;
