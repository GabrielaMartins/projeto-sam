'use strict'
var axios = require("axios");
var React = require('react');
var Perfil = require('../../components/perfil/perfil');
var AtividadesHistorico = require('../../containers/item/itemCardContainer');
var PromocoesHistorico = require('../../components/perfil/promocoesHistorico');
var Loading = require('react-loading');
var Config = require('Config');

var moment = require('moment');
moment.locale('pt-br');

const PerfilUsuarioContainer = React.createClass({

  render: function(){
    if(this.state.usuario.nome == null){
      return(<div className="full-screen-less-nav">
        <div className="row wrapper">
          <Loading type='bubbles' color='#550000' height={150} width={150}/>
        </div>
      </div>);
    }


    var self = this;
    var atividades = this.state.atividades.map(function(atividade, index){
      if(atividade.tipo !== "agendamento"){
        if((atividade.Item.nome.toLowerCase().indexOf(self.state.consultaAtividades.toLowerCase())!=-1 ||
          atividade.Item.Categoria.nome.toLowerCase().indexOf(self.state.consultaAtividades.toLowerCase())!=-1) && self.verificaAno(moment(atividade.data).year())){
          return(
            <div className="col l12 m12 s12" key={index}>
              <AtividadesHistorico
                item = {atividade.Item}
                pontuacao = {atividade.Item.dificuldade * atividade.Item.modificador * atividade.Item.Categoria.peso}
                perfil = {true}
                />
            </div>
          );
        }
      }
    });

    var promocoes = this.state.promocoes.map(function(promocao, index){
      if((promocao.CargoAnterior.nome.toLowerCase().indexOf(self.state.consultaPromocoes.toLowerCase())!=-1 ||
        promocao.CargoAdquirido.nome.toLowerCase().indexOf(self.state.consultaPromocoes.toLowerCase())!=-1) && self.verificaAno(moment(promocao.Data).year())){
          return(
            <div className="col l12 m12 s12" key={index}>
              <PromocoesHistorico promocao = {promocao} />
            </div>
          );
        }
    });

    return(
      <Perfil
        usuario = {this.state.usuario}
        tempoDeCasa = {this.state.tempoDeCasa}
        progresso = {this.state.progresso}
        atividades = {atividades}
        promocoes = {promocoes}
        columnChart = {this.state.columnChart}
        scroll = {this.scrollParaHistorico}
        consultaAtividades = {this.state.consultaAtividades}
        handlePesquisaAtividades = {this.handlePesquisaAtividades}
        consultaPromocoes = {this.state.consultaPromocoes}
        handlePesquisaPromocoes = {this.handlePesquisaPromocoes}
        mostraTudo = {this.handleMostraTudo}
        ultimoAno = {this.handleUltimoAno}
      />
    );
  },

  getInitialState: function(){
      return {
            usuario: {
              foto: null,
              nome: null,
              Cargo:{
                nome: null
              },
              ProximoCargo:[
                {
                  nome: null,
                  pontuacao: 0
                }
              ]
            },
            tempoDeCasa:null,
            atividades: [],
            promocoes:[],
            progresso: 0,
            columnChart: {
      				data: [],
              options : {},
              chartType: "",
        			div_id: ""
      			},
            consultaAtividades: "",
            consultaPromocoes: "",
            mostraTudo : true
      };
  },

  componentDidUpdate: function(){

    if(this.props.params.historico){
      var event = new MouseEvent('click');
      this.scrollParaHistorico(event);
    }
  },
  componentWillMount: function(){
      var self = this;
      var usuario = this.props.params.samaccount;

      axios.defaults.headers.common['token'] = localStorage.getItem("token");

      // busca no banco esse samaccount
      axios.get(Config.serverUrl+'/api/sam/perfil/'+ usuario).then(

        // sucesso
        function(response){
          var estado = self.montaEstado(response.data.Usuario);
          self.setState({
            usuario: estado.usuario,
            tempoDeCasa: estado.tempoDeCasa,
            progresso: estado.progresso,
            atividades : response.data.Atividades,
            promocoes : response.data.PromocoesAdquiridas,
            columnChart: {
      				data: [],//response.data.DadosDoGraficoDoFuncionario,
              options : {
                title: "Pontuações Atingidas por Ano",
                bar: {groupWidth: "100%"},
                legend: { position: 'right', maxLines: 3 },
                isStacked: true,
                vAxis: {maxValue: 500, format: '0'}
              },
              chartType: "ColumnChart",
              div_id: "ColumnChart"
      			}
          })
          sr.reveal('.scrollreveal');
        },

        //falha
        function(jqXHR){
          self.setState(self.getInitialState());
          status = jqXHR.status;
          var rota = '/Erro/' + status;

          if(status == "401"){
            this.props.history.push({pathname: rota, state: {mensagem: "Você está tentando acessar uma página que não te pertence, que feio!"}});
          }else{
            this.props.history.push({pathname: rota, state: {mensagem: "Um erro inesperado aconteceu, por favor, tente mais tarde"}});
          }
        }

      );
  },

  montaEstado: function(estado){
    var usuario = estado;
    var tempoDeCasa = this.calculaTempoDeCasa(usuario.DataInicio);

    if(tempoDeCasa > 1){
      tempoDeCasa = tempoDeCasa + " anos";
    }else{
      tempoDeCasa = tempoDeCasa + " ano";
    }

    var progresso = 0;
    if(usuario.pontos != 0){
          progresso = usuario.pontos * 100 / usuario.ProximoCargo[0].pontuacao;
    }

    return {usuario: usuario, progresso: progresso, tempoDeCasa: tempoDeCasa};
  },

  calculaTempoDeCasa: function(dataInicio){
    var atual = new Date();
    var tempoDeCasa = moment(dataInicio).year() - moment(atual).year();

    return tempoDeCasa;

  },

  scrollParaHistorico: function(event){
    event.preventDefault();

    $('html, body').animate({
      scrollTop: $("#historico").offset().top - 50
    }, 2000);

  },

  handlePesquisaAtividades: function(event){
    this.setState({
      consultaAtividades: event.target.value
    });
  },

  handlePesquisaPromocoes: function(event){
    this.setState({
      consultaPromocoes: event.target.value
    });
  },

  handleMostraTudo: function(){
    this.setState({
      mostraTudo : true
    });

    //muda cor dos botoes
    $("#btn-tudo").addClass("btn-pressed").removeClass("color-default");
    $("#btn-ano").removeClass("btn-pressed").addClass("color-default");
  },

  handleUltimoAno: function(){
    this.setState({
      mostraTudo : false
    });

    //muda cor dos botoes
    $("#btn-ano").addClass("btn-pressed").removeClass("color-default");
    $("#btn-tudo").removeClass("btn-pressed").addClass("color-default");


  },

  verificaAno : function(anoAtividade){

    if(this.state.mostraTudo === false){
      //obtém o ano da última promoção (está em ordem decrescente)
      var ano = moment(this.state.promocoes[0].Data).year();
      return (anoAtividade >= ano && anoAtividade <= ano + 1);
    }else {
      return anoAtividade > 0;
    }
  }

});

module.exports = PerfilUsuarioContainer;
