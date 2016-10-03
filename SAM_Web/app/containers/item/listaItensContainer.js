'use strict'

var React = require('react');
var axios = require("axios");
var Config = require('Config');

var ListaItens = require('../../components/shared/lista');
var ItemCard = require('../item/itemCardContainer');
var Loading = require('react-loading');

var fezFetch = false;

var ListaItensContainer = React.createClass({
  contextTypes: {
    router: React.PropTypes.object.isRequired
  },

  getInitialState: function() {
    return {
      itens: [],
      consulta: "",
      filtro: "",
      categorias:[]
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
        status = jqXHR.status;
        var rota = '/Erro/' + status;

        //erro 401 - acesso não autorizado
        if(status == "401"){
          this.context.router.push({pathname: rota, state: {mensagem: "Você está tentando acessar uma página que não te pertence, que feio!"}});
        }if(status == "500"){
          this.context.router.push({pathname: rota, state: {mensagem: "O seu acesso expirou, por favor, faça o login novamente."}});
        }else{
          this.context.router.push({pathname: rota, state: {mensagem: "Um erro inesperado aconteceu, por favor, tente mais tarde"}});
        }
      }.bind(this)
    );

    //obtém as categorias
      axios.get(Config.serverUrl+'/api/sam/category/all', config).then(
      function(response){
        var categorias = response.data;
        this.setState({categorias: categorias});
      }.bind(this),
      function(jqXHR){
        status = jqXHR.status;
        var rota = '/Erro/' + status;

        //erro 401 - acesso não autorizado
        if(status == "401"){
          this.context.router.push({pathname: rota, state: {mensagem: "Você está tentando acessar uma página que não te pertence, que feio!"}});
        }if(status == "500"){
          this.context.router.push({pathname: rota, state: {mensagem: "O seu acesso expirou, por favor, faça o login novamente."}});
        }else{
          this.context.router.push({pathname: rota, state: {mensagem: "Um erro inesperado aconteceu, por favor, tente mais tarde"}});
        }
      }.bind(this)
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
    var self = this;
    //configurações para passar o token
    var token = localStorage.getItem("token");

    var config = {
      headers: {'token': token}
    };

    var itensModificado = [];

    swal({
      title: "Atenção!",
      text: "Você tem certeza que deseja deletar o item " + nome + " ?",
      type: "warning",
      showConfirmButton: true,
      confirmButtonText: "Sim",
      showCancelButton: true,
      cancelButtonText: "Cancelar",
      confirmButtonColor: "#550000"
    },function(){
      axios.delete(Config.serverUrl+"/api/sam/item/delete/" + id, config).then(
        function(){
          swal({
            title: "Item excluído com sucesso",
            text: "O item " + nome + " foi excluído!",
            type: "success",
            confirmButtonText: "Ok",
            confirmButtonColor: "#550000"
          }, function(){
            //retira da lista de itens pra não precisar acessar o banco
            for(var i = 0; i < self.state.itens.length; i++) {
              if (self.state.itens[i].id !== id) {
                itensModificado.push(self.state.itens[i]);
              }
            }
            self.setState({
              itens: itensModificado
            });
          });
        },

        function(){
          swal({
            title: "Algum erro ocorreu!",
            text: "O item " + nome + " não foi excluído, tente novamente.",
            type: "error",
            confirmButtonText: "Sim",
            confirmButtonColor: "#550000"
          });
        });
      }
    )
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

    var placeholderOption = "Filtre as Categorias";

    var options = this.state.categorias.map(function(categoria, index){
      return <option key = {index} value={categoria.nome}>{categoria.nome}</option>
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
