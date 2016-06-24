var React = require('react');
var ReactRouter = require('react-router');
var Chart = require('react-google-charts').Chart;

var PontuacaoGrafico = React.createClass({
  render: function(){
    return(
      <div className="card-panel">
        <h5 className="card-title center-align colorText-default"><b>Pontuações Alcançadas por Período</b></h5>
        <div className="grafico card-content center" style={{paddingTop:10}}>
          {/*}<Chart chartType={props.columnChart.chartType} width={"100%"} height={"200px"} data={props.columnChart.data} options = {props.columnChart.options} graph_id={props.columnChart.div_id} />*/}
        </div>
      </div>
    );
  }
});

module.exports = PontuacaoGrafico;
