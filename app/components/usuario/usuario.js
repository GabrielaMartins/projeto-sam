var React = require('react');
var ReactRouter = require('react-router');

var Usuario = React.createClass({
  render: function(){
    return(
      <div className="card-panel">
        <div className="container">
          <img className="responsive-img circle center-block" src={this.props.conteudo.imagem} style={{height:100}}/>
          <h5 className="center-align colorText-default"><b>{this.props.conteudo.nome}</b></h5>
          <div className="row">
            <div className="col l6 m6 s6">
              <span><b>Level: </b> {this.props.conteudo.cargo}</span>
            </div>
            <div className="col l6 m6 s6">
                <span className="right"><b>Próximo level: </b> {this.props.conteudo.prox_cargo}</span>
            </div>
          </div>
          <div className="row">
            <div className="col l6 m6 s6">
                <span><b>{this.props.conteudo.pontos}</b>/{this.props.conteudo.pontos_cargo}</span>
                <div className="progress">
                  <div className="determinate" style={{width: 70 + "%"}}></div>
                </div>
            </div>
            <div className="col l6 m6 s6">
              <span className="right"><b>Tempo de casa: </b> {this.props.conteudo.tempo_casa}</span>
            </div>
          </div>
        </div>
        </div>
    );
  }
});

module.exports = Usuario;
