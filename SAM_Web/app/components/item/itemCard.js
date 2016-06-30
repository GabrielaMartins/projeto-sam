var React = require('react');
var ReactRouter = require('react-router');
var Modal = require('./modalItem');

var Link = ReactRouter.Link;

var Card = function(props){
    return(
        <div>
          <div className="scrollreveal card">
            <div className="card-content">
              <h5 className="card-title center-align"><b>{props.item.nome}</b></h5>
              <h2 className="center-align"><b>{props.pontuacao}</b></h2>
              <h5 className="center-align">{props.item.Categoria.nome}</h5>
              {props.data}
            </div>
            <div className="card-action">
              <div className="row">
                {props.acoes}
              </div>
            </div>
          </div>
          <Modal item = {props.item}
            usuarios = {props.usuarios}
            pontuacao = {props.pontuacao}/>
        </div>

    );
  }

module.exports = Card;
