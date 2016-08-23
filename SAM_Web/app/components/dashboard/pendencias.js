var React = require('react');
var ReactRouter = require('react-router');
var Link = ReactRouter.Link;
var CardPendencia = require('./cardsPendencia');
var Modal = require('./modal_pendencias/modal_promocao');

var Pendencias = React.createClass({
  tipoPendencia: function(){
    var cards = [];
    var self = this;

    self.props.pendencias.forEach(function(conteudoCard, index){
        if(conteudoCard.Evento.tipo == "votacao" || self.props.tipoPendencia == "resultadoVotacao"){
          cards.push(<div key={index} className={self.props.tipoPendencia == "alerta" ? "col l12 m12 s12" : "col l6 m12 s12"}>
                        <Link className="scrollreveal" to={{ pathname: '/Votacao/' + conteudoCard.Evento.id}}>
                          <CardPendencia conteudo = {conteudoCard}/>
                        </Link>
                      </div>);
        }else{
          cards.push(<div key={index} className = {self.props.tipoPendencia == "alerta" ? "col l12 m12 s12" : "col l6 m12 s12"}>
                        <a className="modal-trigger scrollreveal" data-target={conteudoCard.Evento.id}>
                          <CardPendencia conteudo = {conteudoCard}/>
                        </a>
                        { self.props.tipoPendencia == "alerta" && conteudoCard.Evento.tipo == "promocao" ?
                          <Modal index = {conteudoCard.Evento.id} handleDeleteAlerta = {self.props.handleDeleteAlerta.bind(null)}/> : null
                        }
                      </div>);
        }
    });

    return cards;
  },

  render : function(){
    return(
      <div className="row">
        {this.tipoPendencia()}
      </div>
    );
  }
});

Pendencias.propTypes = {
  pendencias: React.PropTypes.arrayOf(React.PropTypes.object).isRequired,
  tipoPendencia: React.PropTypes.string.isRequired,
}

module.exports = Pendencias;
