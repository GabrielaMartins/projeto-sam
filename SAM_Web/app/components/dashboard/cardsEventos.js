var React = require('react');
var ReactRouter = require('react-router');
var Link = ReactRouter.Link;


var CardEventos = React.createClass({
  render : function(){
    var infos = null;
    var estilo = "card-panel z-depth-1 col l12 m12 s12 waves-effect";
    //se essa propriedade existir, então é o card de próximas promoções, se não é o card de últimas certificações
    if(this.props.conteudo.pontosFaltantes){
      infos = <div className="left">
                  <p className="center-align grande">Faltam <b>{this.props.conteudo.pontosFaltantes}</b> pontos</p>
                  <p className="center-align pequena">{this.props.conteudo.cargoAtual} > {this.props.conteudo.proximoCargo}</p>
              </div>
    }else if(this.props.conteudo.evento){
      infos = <h5 className="left extraGrande">{this.props.conteudo.evento}</h5>
    }else if(this.props.conteudo.pontos){
      infos = <h5 className="right"><b>{this.props.conteudo.pontos} pontos</b></h5>
    }else{
      if(this.props.conteudo.dificuldade && this.props.conteudo.profundidade){
        estilo = "aberta card-panel z-depth-1 col l12 m12 s12 green lighten-3 waves-effect";
        infos =<div className="left">
                  <p className="media"><b>Dificuldade: </b>{this.props.conteudo.dificuldade}</p>
                  <p className="media"><b>Profundidade: </b>{this.props.conteudo.profundidade}</p>
              </div>
      }else{
        estilo = "finalizada card-panel z-depth-1 col l12 m12 s12 red lighten-3 waves-effect";
        infos = <div className="left grande"><p >Ainda não registrou o voto.</p></div>
      }

    }
    return(
      <div className="eventsCard row">
        <div className={estilo} style={{height:100}}>
          <div className="row wrapper">
            <div className="col s5 m4 l4">
              <div className="center">
                <img className="responsive-img circle" src={this.props.conteudo.imagem} style={{height:50}}/>
                <br/>
                <span><b>{this.props.conteudo.nome}</b></span>
              </div>
            </div>
            <div className="col s7 m8 l8">
                {infos}
            </div>
          </div>
        </div>
      </div>
    );
  }
});

module.exports = CardEventos;
