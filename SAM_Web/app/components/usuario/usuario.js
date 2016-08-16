var React = require('react');
var ReactRouter = require('react-router');
var moment = require('moment');
moment.locale('pt-br');

var Usuario = React.createClass({
  render: function(){
    var progresso = 0;
    if(this.props.conteudo.pontuacao != 0){
          progresso = this.props.conteudo.pontos * 100 / this.props.conteudo.ProximoCargo[0].pontuacao;
    }

    var atual = new Date();
    var tempoDeCasa = moment(this.props.conteudo.dataInicio).year() - moment(atual).year();

    return(
      <div className="card-panel">
        <div className="container">
          <img className="responsive-img circle center-block" src={this.props.conteudo.foto} style={{height:100}}/>
          <h5 className="center-align colorText-default"><b>{this.props.conteudo.nome}</b></h5>
          <div className="row">
            <div className="col l6 m6 s6">
              <span className="black-text"><b>Level: </b> {this.props.conteudo.Cargo.nome}</span>
            </div>
            <div className="col l6 m6 s6">
                <span className="right black-text"><b>Próximo level: </b> {this.props.conteudo.ProximoCargo[0].nome}</span>
            </div>
          </div>
          <div className="row">
            <div className="col l6 m6 s6 black-text">
                <span><b>{this.props.conteudo.pontos}</b>/{this.props.conteudo.ProximoCargo[0].pontuacao}</span>
                <div className="progress">
                  <div className="determinate" style={{width: progresso + "%"}}></div>
                </div>
            </div>
            <div className="col l6 m6 s6">
              <span className="right black-text"><b>Tempo de casa: </b> {tempoDeCasa}</span>
            </div>
          </div>
        </div>
        </div>
    );
  }
});

module.exports = Usuario;
