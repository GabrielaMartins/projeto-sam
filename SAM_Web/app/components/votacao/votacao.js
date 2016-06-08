var React = require('react');
var ReactRouter = require('react-router');
var CardEventos = require('../dashboard/cardsEventos');
var SelecaoVoto = require('../../containers/votacao/SelecaoVotoContainer');
var Usuario = require('../usuario/usuario');

var Link = ReactRouter.Link;

var Votacao = function(props){
    var votos = [];

    props.votos.forEach(function(voto){
      votos.push(<CardEventos conteudo = {voto}></CardEventos>);
    });

    if(props.perfil == "rh"){
      this.painelEsquerdo = <div className="card-panel" style={{paddingBottom:29}}>
                              <h5 className="card-title center-align colorText-default" ><b>Votação</b></h5>
                              <div className="votacao scrollbar card-content" style={{paddingTop:10}}>
                                {votos}
                              </div>
                            </div>;

      this.painelDireito =  <div>
                              <Usuario conteudo = {props.evento.funcionario}/>
                              <SelecaoVoto titulo = "Definir Pontuação" botao = "Atribuir" />
                            </div>;
    }else{
      this.painelEsquerdo = <Usuario conteudo = {props.evento.funcionario}/>;
      this.painelDireito = <SelecaoVoto titulo = "Votação" botao = "Votar"/>
    }

    return(
      <div className="container">
          <div style={{marginTop:25}}>
            <div className="card-panel">
              <h1 className="colorText-default center">{props.evento.nome_item}</h1>
              <div className="row">
                <div className="col l6 m6 s6"><span className="right"><b>{props.evento.categoria_item}</b></span></div>
                <div className="col l6 m6 s6"><span className="left"><b>{props.evento.data_evento}</b></span></div>
              </div>
              <p className="center-align" style={{color:"#801515", fontSize: 16}}><b>{props.evento.descricao}</b></p>
            </div>
            <div className="row">
              <div className="col l6 m12 s12">
                {this.painelEsquerdo}
              </div>
              <div className="col l6 m12 s12">
                {this.painelDireito}
              </div>
          </div>
        </div>
      </div>
    );
  }

module.exports = Votacao;