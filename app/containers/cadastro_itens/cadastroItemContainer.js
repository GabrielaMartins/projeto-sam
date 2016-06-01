'use strict'
var React = require('react');
var CadastroItem = require('../../components/cadastro_itens/cadastroItem');
var axios = require("axios");

const CadastroItemContainer = React.createClass({

  render: function(){
    return(
      <CadastroItem
        categorias = {this.state.categorias}
        rotulosRadio = {this.state.rotulosRadio}
        item = {this.state.item}
        descricao = {this.state.descricao}
        categoria = {this.state.categoria}
        dificuldade = {this.state.dificuldade}
        handleCategoryChanges = {this.handleCategoryChanges}
        handleDificultyChanges = {this.handleDificultyChanges}
        handleModifierChanges = {this.handleModifierChanges}
        handleDescriptionChanges = {this.handleDescriptionChanges}
        handleItemChanges = {this.handleItemChanges}
        handleSubmit = {this.handleSubmit}
        handleClear = {this.handleClear}
      />
    )
  },

  getInitialState: function(){
      return {
        categorias: [],
        rotulosRadio: ["Raso", "Profundo"],
        dificuldade: "Selecione a dificuldade",
        categoria: "Selecione a categoria",
        item: "",
        descricao: "",
        modificador: 0
      }
  },

  componentDidMount: function(){

    var self = this;
    $(document).ready(function() {
       $('select').material_select();
    });

    $("#select_categoria").on('change', self.handleCategoryChanges);
    $("#select_dificuldade").on('change', self.handleDificultyChanges);


  //  this.forceUpdate();
  },

  componentWillMount: function(){
    this.getCategory('http://localhost:65120/api/sam/categoria/all');
  },

  getCategory: function(url){

      // recupera as categorias
      var self = this;
      axios.get(url).then(
        function(response){

          var nomeCategorias = response.data.map(c => c.nome);
          self.setState({categorias: nomeCategorias});
        },
        function(reason){

        }
      );
  },

  handleCategoryChanges: function(event){

    var categoria = event.target.value;

    // troca o rotulo dos radio
    if(categoria === "Categoria 1"){
      this.setState({
          rotulosRadio: ["Raso", "Profundo"],
          categoria: categoria
      });
    }else{
      this.setState({
        rotulosRadio: ["Alinhado", "Não Alinhado"],
        categoria: categoria
      });
    }

  },

  handleDificultyChanges: function(event){

    var dificuldade = event.target.value;
    this.setState({dificuldade: dificuldade});
  },

  handleModifierChanges: function(event){
    var modificador = event.target.value;
    var val = 0;
    if(modificador === "X"){
      val = 1;
    }

    this.setState({modificador: val});
  },

  handleDescriptionChanges: function(event){
    var descricao = event.target.value;
    this.setState({descricao: descricao});
  },

  handleItemChanges: function(event){
    var item = event.target.value;
    this.setState({item: item});
  },

  handleSubmit: function(){


  },

  handleClear: function(){
      
      this.setState({
        categoria: "Selecione a categoria",
        dificuldade: "Selecione a dificuldade",
        item: "",
        descricao: ""
      });
  }

});

module.exports = CadastroItemContainer;
