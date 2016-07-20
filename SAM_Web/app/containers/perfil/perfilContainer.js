'use strict'
var axios = require("axios");
var React = require('react');
var Perfil = require('../../components/perfil/perfil');
var AtividadesHistorico = require('../../containers/item/itemCardContainer');
var moment = require('moment');
moment.locale('pt-br');

const PerfilUsuarioContainer = React.createClass({

  render: function(){
    debugger;
    var atividades = this.state.atividades.map(function(atividade, index){
      return(
        <div className="col l12 m12 s12" key={index}>
          <AtividadesHistorico
            item = {atividade.Item}
            usuarios = {atividade.Usuario}
            pontuacao = {atividade.Item.dificuldade * atividade.Item.modificador * atividade.Item.Categoria.peso}
            perfil = {true}
            />
        </div>
      );
    });

    return(<Perfil usuario = {this.state.usuario}
      tempoDeCasa = {this.state.tempoDeCasa}
      progresso = {this.state.progresso}
      atividades = {atividades}
      promocoes = {this.state.promocoes}
      columnChart = {this.state.columnChart}
      scroll = {this.scrollParaHistorico}/>);
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
      			}
      };
  },

  componentWillMount: function(){
      var self = this;
      var usuario = this.props.params.samaccount;

      axios.defaults.headers.common['token'] = localStorage.getItem("token");

      // busca no banco esse samaccount
      axios.get('http://10.10.15.113:65122/api/sam/perfil/').then(

        // sucesso
        function(response){
          debugger;
          var estado = self.montaEstado(response.data.Usuario);
          self.setState({
            usuario: estado.usuario,
            tempoDeCasa: estado.tempoDeCasa,
            progresso: estado.progresso,
            atividades : response.data.Atividades,
            promocoes : response.data.PromocoesAdquiridas,
            columnChart: {
      				data: response.data.DadosDoGraficoDoFuncionario,
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
        },

        //falha
        function(jqXHR){
          self.setState(self.getInitialState());
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
    if(usuario.Cargo.pontuacao != 0){
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

  }

});

module.exports = PerfilUsuarioContainer;
