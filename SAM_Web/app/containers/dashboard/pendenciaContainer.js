'use strict'

//libs
var React = require('react');
var ReactRouter = require('react-router');
var Link = ReactRouter.Link;

//componentes
var CardPendencia = require('../../components/dashboard/cardsPendencia');
var Modal = require('../../components/dashboard/modal_pendencias/modal_promocao');

var Pendencias = React.createClass({
  //se for do tipo votação, cria link para a página de votação
  tipoPendenciaVotacao: function(tipo){

    if(tipo == "votacao" || tipo == "resultadoVotacao"){
      return true;
    }else{
      return false;
    }
  },

  //seta as diferentes classes para cada tipo de pendência
  classes: function(tipo){
    if(tipo == "alerta"){
      return "col l12 m12 s12";
    }else{
      return "col l6 m12 s12";
    }
  },

  //retorna o modal apropriado para cada tipo de pendência
  tipoModal: function(conteudo){
    if(conteudo.Evento.tipo == "promocao"){
      return (
        <Modal index = {conteudo.id} handleDeleteAlerta = {this.props.handleDeleteAlerta}/>
      );
    }else{
      return null;
    }
  },

  //seleciona o icone de cada tipo de pendencia
  icone : function(tipo){
    if(tipo == "votacao"){
      return "fa fa-lg fa-gavel";
    }else if(tipo == "promocao"){
      return "fa fa-lg fa-trophy";
    }else if(tipo == "atividade"){
      return "fa fa-lg fa-calendar";
    }
  },

  render: function(){
    var self = this;

    var pendencias = this.props.pendencias.map(function(conteudo, index){
      
      return(
        <div key={index} className = {self.classes(self.props.tipoPendencia)}>
          {
            self.tipoPendenciaVotacao(conteudo.Evento.tipo) == true ?
                //se for uma pendencia do tipo votação, retorna-se link para a página de votação
                <Link className="scrollreveal" to={{ pathname: '/Votacao/' + conteudo.Evento.id}}>
                  <CardPendencia conteudo = {conteudo} icone= {self.icone(conteudo.Evento.tipo)}/>
                </Link>
            :
                //se não retorna um modal quando houver clique na pendência
                <div>
                  <a className="modal-trigger scrollreveal" data-target={conteudo.id}>
                    <CardPendencia conteudo = {conteudo} icone= {self.icone(conteudo.Evento.tipo)}/>
                  </a>
                  {
                    //retorna o modal apropriado dependendo do tipo de pendência
                    self.tipoModal(conteudo)
                  }
                </div>
            }
        </div>
      );
    });

    return (
      <div className="row">
        {pendencias}
      </div>
    );
  }

});

Pendencias.propTypes = {
  pendencias: React.PropTypes.arrayOf(React.PropTypes.object).isRequired,
  tipoPendencia: React.PropTypes.string.isRequired,
}

module.exports = Pendencias;
