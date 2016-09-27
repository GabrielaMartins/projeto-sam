'use strict'

//libs
var React =  require('react');
var ReactRouter = require('react-router');
var Link = ReactRouter.Link;

//componente modal do Item
var ModalItem = require('../../components/item/modalItem');

var Atualizacoes = function(props){
  return(
    <div>
      <a className="modal-trigger black-text" data-target={"atualizacao" + props.conteudo.Item.id}>
        <div className="eventsCard row scrollreveal">
        <div className="card-panel z-depth-1 col l12 m12 s12 waves-effect" style={{height:100}}>
          <div className="row wrapper">
            <div className="col s5">
              <div className="left">
                <h5 className="media">{props.conteudo.Item.nome}</h5>
              </div>
            </div>
            <div className="col s3">
              <div className="center">
                <span className="extraGrande"><b>{props.pontuacao}</b></span>
                  <div>
                    <span className="pequena">{props.conteudo.tipo}</span>
                  </div>
              </div>
            </div>
            <div className="col s4">
              <div className="right">
                <span className="media"> pontos</span>
              </div>
            </div>
          </div>
        </div>
      </div>
      </a>
      <ModalItem
        item = {props.conteudo.Item}
        index = {"atualizacao" + props.conteudo.Item.id}
        pontuacao = {props.pontuacao}
        usuarios = {props.usuarios}
      />
    </div>
  );

}

Atualizacoes.propTypes = {
  conteudo: React.PropTypes.object.isRequired,
  pontuacao: React.PropTypes.number.isRequired,
  usuarios: React.PropTypes.arrayOf(React.PropTypes.element).isRequired,
}

module.exports = Atualizacoes;
