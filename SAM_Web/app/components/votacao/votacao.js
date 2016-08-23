var React = require('react');
var ReactRouter = require('react-router');
var CardEventos = require('../dashboard/cardsEventos');
var SelecaoVoto = require('../../containers/votacao/SelecaoVotoContainer');
var Usuario = require('../usuario/usuario');

var moment = require('moment');
moment.locale('pt-br');

var Link = ReactRouter.Link;

var Votacao = function(props){
    var votos = [];
    console.log(props.resultado);
    props.votos.forEach(function(voto){
      if(voto.Dificuldade && voto.Profundidade){
        votos.push(<CardEventos usuario = {voto.Usuario} estilo = "aberta card-panel z-depth-1 col l12 m12 s12 green lighten-3 waves-effect">
                      <div className="left">
                        <p className="media"><b>Dificuldade: </b>{voto.Dificuldade}</p>
                        <p className="media"><b>Profundidade: </b>{voto.Profundidade}</p>
                      </div>
                  </CardEventos>);
        }else{
          votos.push(<CardEventos usuario = {voto.Usuario} estilo = "finalizada card-panel z-depth-1 col l12 m12 s12 red lighten-3 waves-effect">
                        <div className="media left"><p><b>Ainda não registrou o voto.</b></p></div>
                    </CardEventos>);
        }
    });

    if(props.perfil == "rh"){
      this.painelEsquerdo = <div className="card-panel" style={{paddingBottom:29}}>
                              <h5 className="card-title center-align colorText-default" ><b>Votação</b></h5>
                              <div className="votacao scrollbar card-content" style={{paddingTop:10}}>
                                {votos}
                              </div>
                            </div>;

      this.painelDireito =  <div>
                              <Usuario conteudo = {props.evento.Usuario}/>
                              <SelecaoVoto titulo = "Definir Pontuação" botao = "Atribuir" />
                            </div>;
    }else{
      this.painelEsquerdo = <Usuario conteudo = {props.evento.Usuario}/>;
      if(props.resultado == false){
        this.painelDireito = <SelecaoVoto
                              titulo = "Votação"
                              botao = "Votar"
                              evento = {props.evento.id}
                              mostraResultado = {props.mostraResultado.bind(null)}
                              />
      }else{
        this.painelDireito = null;//faz mostrar resultado da votação (gráfico)
      }
    }

    return(
      <div style={{paddingTop: 30 }}>
        <div className="container">
          <div className="card-panel">
            <h1 className="colorText-default center">{props.evento.Item.nome}</h1>
            <div className="row">
              <div className="col l6 m6 s6"><span className="right"><b>{props.evento.Item.Categoria.nome}</b></span></div>
              <div className="col l6 m6 s6"><span className="left"><b>{moment(props.evento.data).format('L')}</b></span></div>
            </div>
            <p className="center-align" style={{color:"#801515", fontSize: 16}}><b>{props.evento.Item.descricao}</b></p>
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
