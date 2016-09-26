'use strict'

//libs
var React = require('react');
var ReactRouter = require('react-router');

//componente
var Chart = require('react-google-charts').Chart;

var PontuacaoGrafico = React.createClass({
  render: function(){
    return(
      <div className="card-panel card-historico-color">
        <h5 className="card-title center-align colorText-default scrollreveal"><b>Pontuações Alcançadas por Período</b></h5>
        <div className="grafico card-content center scrollreveal" style={{paddingTop:10}}>
          {this.props.columnChart.data.length > 1 ?
            <Chart chartType={this.props.columnChart.chartType}
              width={"100%"}
              height={"200px"}
              data={this.props.columnChart.data}
              options = {this.props.columnChart.options}
              graph_id={this.props.columnChart.div_id} />
            :
            <div className="wrapper">
              <p className="center-align media">Não há dados suficientes para apresentar o gráfico.</p>
            </div>
          }

        </div>
      </div>
    );
  }
});

PontuacaoGrafico.propTypes = {
  columnChart: React.PropTypes.object.isRequired
}

module.exports = PontuacaoGrafico;
