var React = require('react');
var axios = require("axios");
var ListaItens = require('../../components/shared/lista');
var ItemCard = require('../item/itemCardContainer');
var fezFetch = false;

var ListaItensContainer = React.createClass({
  getInitialState: function() {
    return {
      itens: [],
      consulta: ""
    };
  },
  componentDidMount: function(){

    var token = localStorage.getItem("token");
    var config = {
      headers: {'token': token}
    };

    //obtém dados
    axios.get("http://sam/api/sam/item/all", config).then(
      function(response){
        fezFetch = true;
        this.setState({
          itens: response.data
        });
      }.bind(this),
      function(jqXHR){

      }
    );
  },

  componentWillUnmount: function(){
    fezFetch = false;
  },

  handlePesquisa: function(event){

    this.setState({
      consulta: event.target.value
    });

  },

  render : function(){

    if(!fezFetch){
      return null;
    }

    var lista = [];
    var placeholder = "Procure por Itens e Categorias";;
    var self = this;

    self.state.itens.forEach(function(item, index){
      if(item.nome.toLowerCase().indexOf(self.state.consulta.toLowerCase())!=-1 ||
        item.Categoria.nome.toLowerCase().indexOf(self.state.consulta.toLowerCase())!=-1){
            lista.push(<div key={index} className="col l4 m6 s12"><ItemCard item = {item}/></div>)
      }
    });

    return(
      <ListaItens
        placeholder = {placeholder}
        consulta = {this.state.consulta}
        handlePesquisa = {this.handlePesquisa}>
        {lista}
      </ListaItens>
    );
  }
});

module.exports = ListaItensContainer;
