var React = require('react');
var ReactRouter = require('react-router');
var Avatar = require('./avatar');
var PromocoesHistorico = require('./promocoesHistorico');
var PontuacaoGrafico = require('./pontuacaoGrafico');
var BaseHistorico = require('./BaseHistorico');

var Perfil = function(props){

    var promocoes = null;
    var atividades = null;
    console.log(props);
    //foreach aqui
    return(
      <div style={{marginTop:50}} className="container">
        <div className="full-screen-perfil">
          <Avatar usuario = {props.usuario}
            tempoDeCasa = {props.tempoDeCasa}
            progresso ={props.progresso}
          />

          <div className="row wrapper hide-on-small-only">
            <a className="center-block"><i id = "arrow" className="fa fa-angle-down fa-5x" aria-hidden="true" onClick={props.scroll} style={{"color":"#801515"}}></i></a>
          </div>
        </div>
        <div id="historico">
          <div className="row" >
            <div className="col l6 m6 s12">
              <BaseHistorico  placeholder = "Pesquise por atividades realizadas" titulo = "Atividades Realizadas">
                <div className="row" style={{"marginLeft":"5%", "marginRight":"5%"}}>
                  {props.atividades}
                </div>
              </BaseHistorico>
            </div>
            <div className="col l6 m6 s12">
              <BaseHistorico placeholder = "Pesquise por cargos alcançados" titulo = "Promoções Alcançadas">
                {/*<PromocoesHistorico/>*/}
              </BaseHistorico>
            </div>
          </div>
          <div className="row">
            <div className="col s12">
              <PontuacaoGrafico columnChart ={props.columnChart}/>
            </div>
          </div>
        </div>
      </div>
    );
}


module.exports = Perfil;
