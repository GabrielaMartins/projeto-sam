var React = require('react');
var ReactRouter = require('react-router');
var Link = ReactRouter.Link;


var CardEventos = React.createClass({
  render : function(){
    var infos = null;
    var estilo = "card-panel z-depth-1 col l12 m12 s12"
    //se essa propriedade existir, então é o card de próximas promoções, se não é o card de últimas certificações
    if(this.props.conteudo.pontosFaltantes){
      infos = <div className="row">
                  <div className="left">
                    <span style={{fontSize: 16}}>Faltam <b>{this.props.conteudo.pontosFaltantes}</b> pontos</span><br/>
                    <span style={{fontSize: 10}}>{this.props.conteudo.cargoAtual} > {this.props.conteudo.proximoCargo}</span>
                  </div>
              </div>
    }else if(this.props.conteudo.evento){
      infos = <h5 className="left">{this.props.conteudo.evento}</h5>
    }else if(this.props.conteudo.pontos){
      infos = <h5 className="left">{this.props.conteudo.pontos} pontos</h5>
    }else{
      if(this.props.conteudo.dificuldade && this.props.conteudo.profundidade){
        estilo = "card-panel z-depth-1 green lighten-3 col l12 m12 s12";
        infos = <div className="row">
                    <div className="left">
                      <span style={{fontSize: 16}} className="left"><b>Dificuldade: </b>{this.props.conteudo.dificuldade}</span><br/>
                      <span style={{fontSize: 16}} className="left"><b>Profundidade: </b>{this.props.conteudo.profundidade}</span>
                    </div>
                </div>
      }else{
        estilo = "card-panel z-depth-1 red lighten-3 col l12 m12 s12";
        infos = <span style={{fontSize: 16}} className="left">Ainda não registrou o voto.</span>
      }

    }
    return(
      <div className="eventsCard row">
        <div className={estilo} style={{height:100}}>
          <div className="row wrapper">
            <div className="col s6 m4 l5">
              <div className="right">
                <img className="responsive-img circle" src={this.props.conteudo.imagem} style={{height:50}}/>
                <br/>
                <span><b>{this.props.conteudo.nome}</b></span>
              </div>
            </div>
            <div className="col s6 m8 l7">
                {infos}
            </div>
          </div>
        </div>
      </div>
    );
  }
});

module.exports = CardEventos;
