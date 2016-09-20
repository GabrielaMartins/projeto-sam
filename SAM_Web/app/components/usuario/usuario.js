var React = require('react');
var ReactRouter = require('react-router');
var moment = require('moment');
moment.locale('pt-br');

var Usuario = React.createClass({
  render: function(){
    var progresso = 0;
    if(this.props.conteudo.pontos != 0){
          progresso = this.props.conteudo.pontos * 100 / this.props.conteudo.ProximoCargo[0].pontuacao;
    }

    var atual = new Date();
    var tempoDeCasa = moment(this.props.conteudo.dataInicio).year() - moment(atual).year();

    if(tempoDeCasa > 1){
      tempoDeCasa = tempoDeCasa + " anos";
    }else{
      tempoDeCasa = tempoDeCasa + " ano";
    }

    return(
      <div className="card-panel" id="cardUsuario">
          <img className="responsive-img circle center-block scrollreveal" src={this.props.conteudo.foto} style={{height:100}}/>
          <h5 className="center-align colorText-default scrollreveal"><b>{this.props.conteudo.nome}</b></h5>
          <div className="row scrollreveal">
            <div className="col l6 m6 s6">
              <span className="black-text right"><b>Level: </b> {this.props.conteudo.Cargo.nome}</span>
            </div>
            <div className="col l6 m6 s6">
                <span className="black-text left"><b>Próximo level: </b> {this.props.conteudo.ProximoCargo[0].nome}</span>
            </div>
          </div>
          <div className="row scrollreveal">
            <div className="col l6 m6 s6 black-text">
                <span className="right"><b>{this.props.conteudo.pontos}</b>/{this.props.conteudo.ProximoCargo[0].pontuacao}</span>
                <div className="progress">
                  <div className="determinate" style={{width: progresso + "%"}}></div>
                </div>
                <span className="right pequena"><b>Próxima avaliação:</b> {moment(this.props.conteudo.DataUltimaPromocao).add(1, 'y').format('L')}</span>
            </div>
            <div className="col l6 m6 s6">
              <span className="left black-text"><b>Tempo de casa: </b> {tempoDeCasa}</span>
            </div>
          </div>
        </div>
    );
  }
});

module.exports = Usuario;
