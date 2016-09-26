var React = require('react');
var axios = require("axios");
var ListaItens = require('../../components/shared/lista');
var ItemCard = require('../item/itemCardContainer');
var Loading = require('react-loading');
var Config = require('Config');
var fezFetch = false;

var ListaItensContainer = React.createClass({
  getInitialState: function() {
    return {
      itens: [],
      consulta: "",
      filtro: ""
    };
  },
  componentDidMount: function(){

    var token = localStorage.getItem("token");
    var config = {
      headers: {'token': token}
    };

    //obtém dados
    axios.get(Config.serverUrl+"/api/sam/item/all", config).then(
      function(response){
        fezFetch = true;
        this.setState({
          itens: response.data
        });
        sr.reveal('.scrollreveal');
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

  handleFiltro: function(event){
    this.setState({
      filtro: event.target.value
    });
  },


  handleDeletarItem: function(id, nome){
    swal({
      title: "Atenção!",
      text: "Você tem certeza que deseja deletar o item " + nome + " ?",
      type: "warning",
      confirmButtonText: "Sim",
      showCancelButton: true,
      cancelButtonText: "Cancelar",
      confirmButtonColor: "#550000"
    },function(){

    });
  },

  render : function(){

    if(!fezFetch){
      return (
        <div className="full-screen-less-nav">
          <div className="row wrapper">
            <Loading type='bubbles' color='#550000' height={150} width={150}/>
          </div>
        </div>
      );
    }

    var lista = [];
    var placeholder = "Procure por Itens";
    var self = this;
    self.state.itens.forEach(function(item, index){
      if(item.nome.toLowerCase().indexOf(self.state.consulta.toLowerCase())!=-1 &&
        item.Categoria.nome.toLowerCase().indexOf(self.state.filtro.toLowerCase())!=-1){
            lista.push(<div key={index} className="col l4 m6 s12"><ItemCard item = {item} deletarItem = {self.handleDeletarItem}/></div>)
      }
    });

    var categorias = ["Apresentação", "Blog Técnico", "Comunidade de Software", "Curso", "Repositório de Código"];
    var placeholderOption = "Filtre as Categorias";

    var options = categorias.map(function(categoria, index){
      return <option key = {index} value={categoria}>{categoria}</option>
    });

    return(
      <ListaItens
        placeholder = {placeholder}
        consulta = {this.state.consulta}
        filtro = {this.state.filtro}
        handlePesquisa = {this.handlePesquisa}
        handleFiltro = {this.handleFiltro}
        optionFiltro = {options}
        placeholderOption = {placeholderOption}>
        {lista}
      </ListaItens>
    );
  }
});

module.exports = ListaItensContainer;
