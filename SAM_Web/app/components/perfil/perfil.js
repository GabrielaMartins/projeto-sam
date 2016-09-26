'use strict'
var React = require('react');
var ReactRouter = require('react-router');
var Avatar = require('./avatar');
var PromocoesHistorico = require('./promocoesHistorico');
var PontuacaoGrafico = require('./pontuacaoGrafico');
var BaseHistorico = require('./BaseHistorico');

var Perfil = function(props){

    return(
      <div style={{marginTop:50}} >
        <div className="full-screen-perfil container" >
          <Avatar usuario = {props.usuario}
            tempoDeCasa = {props.tempoDeCasa}
            progresso ={props.progresso}
          />

          <div className="row wrapper hide-on-small-only">
            <a className="center-block"><i id = "arrow" className="fa fa-angle-down fa-5x" aria-hidden="true" onClick={props.scroll} style={{"color":"#801515"}}></i></a>
          </div>
        </div>
        <div id="historico" className="card-panel">
          <h3 className="card-title colorText-default center-align"><b>Histórico</b></h3>
          <div id="filtro-historico" className="row">
            <span className="center col s12">Selecione uma opção:</span><br/><br/>
            <div className="col s6">
              <a id="btn-tudo" className="waves-effect waves-light btn col l4 s12 right color-default" onClick={props.mostraTudo}>Mostra Tudo</a>
            </div>
            <div className="col s6">
              <a id="btn-ano" className="waves-effect waves-light btn col l4 s12 left color-default" onClick={props.ultimoAno}>Última Promoção</a>
            </div>
          </div>
          <div className="row container" >
            <div className="col l6 m6 s12">
              <BaseHistorico
                placeholder = "Pesquise por atividades realizadas"
                titulo = "Atividades Realizadas"
                consulta = {props.consultaAtividades}
                handlePesquisa = {props.handlePesquisaAtividades}>
                <div className="row" style={{"marginLeft":"5%", "marginRight":"5%"}}>
                  {props.atividades}
                </div>
              </BaseHistorico>
            </div>
            <div className="col l6 m6 s12">
              <BaseHistorico
                placeholder = "Pesquise por cargos alcançados"
                titulo = "Promoções Alcançadas"
                consulta = {props.consultaPromocoes}
                handlePesquisa = {props.handlePesquisaPromocoes}>
                <div className="row" style={{"marginLeft":"5%", "marginRight":"5%"}}>
                  {props.promocoes}
                </div>
              </BaseHistorico>
            </div>
          </div>
          <div className="row container">
            <div className="col s12">
              <PontuacaoGrafico columnChart ={props.columnChart}/>
            </div>
          </div>
        </div>
      </div>
    );
}

module.exports = Perfil;
