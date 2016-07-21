'use strict'
var axios = require("axios");
var React = require('react');
var ItemCard = require('../../components/item/itemCard');
var moment = require('moment');
moment.locale('pt-br');

const ItemCardContainer = React.createClass({

  render: function(){
    var data = null;
    var acoes = null;

    //calculo da pontuação de um item
    var pontuacao = this.props.item.dificuldade * this.props.item.modificador * this.props.item.Categoria.peso;
    if(this.props.perfil == true){
      data = <p className="center-align media">{moment(this.props.item.data).format('L')}</p>
      acoes = <div className="col l12 m12 s12"><button data-target={this.props.item.id} className="modal-trigger col l12 m8 s8 offset-m2 offset-s2 waves-effect waves-light yellow darken-3 btn">Ver</button></div>
    }else{
      acoes = <div>
                <div className="col l4 m12 s12"><button data-target={this.props.item.id} className="modal-trigger col l12 m8 s8 offset-m2 offset-s2 waves-effect waves-light yellow darken-3 btn">Ver</button><br/><br/></div>
                <div className="col l4 m12 s12"><button className="col l12 m8 s8 offset-m2 offset-s2 waves-effect waves-light green darken-3 btn">Editar</button><br/><br/></div>
                <div className="col l4 m12 s12"><button className="col l12 m8 s8 offset-m2 offset-s2 waves-effect waves-light red darken-3 btn">Deletar</button><br/><br/></div>
              </div>
    }

    return(<ItemCard data = {data} acoes = {acoes}
      item = {this.props.item}
      pontuacao = {pontuacao}
      usuarios = {this.props.usuarios}/>);
  },

  componentDidMount: function(){
    $(document).ready(function() {
      $('.modal-trigger').leanModal();
    });
  },


});

module.exports = ItemCardContainer;
