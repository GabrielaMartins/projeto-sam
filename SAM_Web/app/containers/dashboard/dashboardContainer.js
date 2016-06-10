var React = require('react');
var Dashboard = require('../../components/dashboard/dashboard');


var DashboardContainer = React.createClass({
  getInitialState: function() {
    return {
      cardsDashboard: null,
      BarChart: {
				data: [],
				chartType: "",
				options : {}
			}
    };
  },
  componentDidMount: function(){

  },
  componentWillMount: function(){
    this.forceUpdate();
    //fazer fetch aqui
    this.setState({
      cardsDashboard:[
        {
          cardTitulo: "Últimos Eventos",
          cardConteudo: [{
            imagem : "./app/imagens/fulano.jpg",
            nome: "Gabriela",
            evento: "Workshop"
          },
          {
            imagem : "./app/imagens/fulano.jpg",
            nome: "Jesley",
            evento: "Java I"
          },
          {
            imagem : "./app/imagens/fulano.jpg",
            nome: "Thiago",
            evento: "Android"
          },
          {
            imagem : "./app/imagens/fulano.jpg",
            nome: "Telles",
            evento: "Workshop"
          },
          {
            imagem : "./app/imagens/fulano.jpg",
            nome: "Telles",
            evento: "Workshop"
          },
          {
            imagem : "./app/imagens/fulano.jpg",
            nome: "Telles",
            evento: "Workshop"
          },
          {
            imagem : "./app/imagens/fulano.jpg",
            nome: "Telles",
            evento: "Workshop"
          }]
        },
        {
          cardTitulo: "Pendências",
          cardConteudo: [{
            tipo : "Votação",
            assunto : "Workshop",
            data: "10/05/2016",
            pessoa: "Gabriela",
            status: "Finalizado"
          },
          {
            tipo : "Atribuir Pontos",
            assunto : "Certificado",
            data: "10/05/2016",
            pessoa: "Jesley",
            status: "Aberto"
          },
          {
            tipo : "Atribuir Pontos",
            assunto : "Certificado",
            data: "12/05/2016",
            pessoa: "Murilo",
            status: "Aberto"
          }]
        },
        {
          cardTitulo: "Certificações Mais Procuradas",
          cardConteudo: [{}]
        },
        {
          cardTitulo: "Ranking",
          cardConteudo: [{
            imagem : "./app/imagens/fulano.jpg",
            nome : "Thiago",
            pontos : "300",
          },
          {
            imagem : "./app/imagens/fulano.jpg",
            nome:"Telles",
            pontos:"290"
          },
          {
            imagem : "./app/imagens/fulano.jpg",
            nome:"Gabriela",
            pontos:"100"
          },
          {
            imagem : "./app/imagens/fulano.jpg",
            nome:"Jesley",
            pontos:"100"
          },
          {
            imagem : "./app/imagens/fulano.jpg",
            nome:"Gabriela",
            pontos:"100"
          },
          {
            imagem : "./app/imagens/fulano.jpg",
            nome:"Jesley",
            pontos:"100"
          },
          {
            imagem : "./app/imagens/fulano.jpg",
            nome:"Gabriela",
            pontos:"100"
          },
          {
            imagem : "./app/imagens/fulano.jpg",
            nome:"Jesley",
            pontos:"100"
          },
          {
            imagem : "./app/imagens/fulano.jpg",
            nome:"Gabriela",
            pontos:"100"
          },
          {
            imagem : "./app/imagens/fulano.jpg",
            nome:"Jesley",
            pontos:"100"
          }]
        },
        {
          cardTitulo: "Próximas Promoções",
          cardConteudo:[{
            imagem: "./app/imagens/fulano.jpg",
            nome:"Gabriela",
            pontosFaltantes:"10",
            cargoAtual:"Estagiário",
            proximoCargo:"Júnior 1"
          },
          {
            imagem: "./app/imagens/fulano.jpg",
            nome:"Jesley",
            pontosFaltantes:"10",
            cargoAtual:"Estagiário",
            proximoCargo:"Júnior 1"
          },
          {
            imagem: "./app/imagens/fulano.jpg",
            nome:"Thiago",
            pontosFaltantes:"10",
            cargoAtual:"Sênior 1",
            proximoCargo:"Sênior 2"
          },
          {
            imagem: "./app/imagens/fulano.jpg",
            nome:"Telles",
            pontosFaltantes:"10",
            cargoAtual:"Sênior 1",
            proximoCargo:"Sênior 2"
          },
          {
            imagem: "./app/imagens/fulano.jpg",
            nome:"Telles",
            pontosFaltantes:"10",
            cargoAtual:"Sênior 1",
            proximoCargo:"Sênior 2"
          },
          {
            imagem: "./app/imagens/fulano.jpg",
            nome:"Fulano",
            pontosFaltantes:"10",
            cargoAtual:"Sênior 1",
            proximoCargo:"Sênior 2"
          },
          {
            imagem: "./app/imagens/fulano.jpg",
            nome:"Fulano",
            pontosFaltantes:"10",
            cargoAtual:"Sênior 1",
            proximoCargo:"Sênior 2"
          }]
        }
      ],
      columnChart :  {
        data : [
          ['Certificações', 'Java I', 'Java II', 'SQL Server', 'Android',
         'IOS', 'ASP', { role: 'annotation' } ],
          ['2011', 10, 24, 20, 32, 18, 5, ''],
          ['2012', 16, 22, 23, 30, 16, 9, ''],
          ['2013', 28, 19, 29, 30, 12, 13, ''],
          ['2014', 28, 19, 29, 30, 12, 13, ''],
          ['2015', 28, 19, 29, 30, 12, 13, ''],
          ['2016', 28, 19, 29, 30, 12, 13, '']
      ],
      options : {
        title: "Certificações mais procuradas por ano",
        bar: {groupWidth: "100%"},
        legend: { position: 'right', maxLines: 3 },
        isStacked: true
      },
      chartType: "ColumnChart",
			div_id: "ColumnChart"
    }
  });
},

  render : function(){
      return(<Dashboard cardsDashboard = {this.state.cardsDashboard} columnChart = {this.state.columnChart}/>)
  }
});

module.exports = DashboardContainer;
