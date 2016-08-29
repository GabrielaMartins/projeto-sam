'use strict'

//libs
var React = require('react');
var ReactRouter = require('react-router');
var Link = ReactRouter.Link;

//componentes
var Modal = require('./modalItem');

var Card = function(props){
    return(
        <div>
          <div className="scrollreveal card">
            <div className="card-content">
              <h5 className="card-title center-align"><b>{props.item.nome}</b></h5>
              <h2 className="center-align colorText-default"><b>{props.pontuacao}</b></h2>
              <h5 className="center-align">{props.item.Categoria.nome}</h5>
              {props.date}
            </div>
            <div className="card-action">
              <div className="row">
                {props.acoes}
              </div>
            </div>
          </div>
          <Modal
            index = {props.item.id}
            item = {props.item}
            usuarios = {props.usuarios}
            pontuacao = {props.pontuacao}/>
        </div>

    );
  }

Card.propTypes = {
  item: React.PropTypes.object.isRequired,
  pontuacao: React.PropTypes.number.isRequired,
  acoes: React.PropTypes.element.isRequired,
  usuarios: React.PropTypes.arrayOf(React.PropTypes.element).isRequired
}

module.exports = Card;
