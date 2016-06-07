'use strict'
var React = require('react');
var CadastroItem = require('../../components/cadastro_itens/cadastroItem');
var axios = require("axios");

const CadastroItemContainer = React.createClass({

  render: function(){
    return(
      <CadastroItem
        categorias = {this.categorias.map(c => c.nome)}
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
        rotulosRadio: ["Raso", "Profundo"],
        dificuldade: "Selecione a dificuldade",
        categoria: "Selecione a categoria",
        item: "",
        descricao: "",
        checked: false
      }
  },

  componentDidMount: function(){

    var self = this;
    $(document).ready(function() {
       $('select').material_select();
    });

    $("#select_categoria").on('change', self.handleCategoryChanges);
    $("#select_dificuldade").on('change', self.handleDificultyChanges);

  },

  componentWillMount: function(){
    this.getCategory('http://localhost:65120/api/sam/categoria/all');
  },

  getCategory: function(url){

      // recupera as categorias
      var self = this;
      axios.get(url).then(
        function(response){
          //debugger;
          //var categorias = response.data;
          //self.categorias = categorias;
        },
        function(reason){

        }
      );

      // teste: remover depois
      debugger;
      var categorias = [{nome:"Categoria 1"},{nome:"Categoria 2"}];
      self.categorias = categorias;
  },

  handleCategoryChanges: function(event){
    debugger;
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
    debugger;
    var dificuldade = event.target.value;
    this.setState({dificuldade: dificuldade});
  },

  handleModifierChanges: function(event){
    debugger;
    var modificador = event.target.value;
    if(modificador === "Raso"){
      this.modificador = 2;
    }else if(modificador === "Profundo"){
      this.modificador = 3;
    }else if(modificador === "Alinhado"){
      this.modificador = 1;
    }else if(modificador === "Não Alinhado"){
      this.modificador = 3;
    }

    //this.setState({modificador: val});
  },

  handleDescriptionChanges: function(event){
    var descricao = event.target.value;
    this.setState({descricao: descricao});
  },

  handleItemChanges: function(event){
    var item = event.target.value;
    this.setState({item: item});
  },

  handleSubmit: function(event){
    event.preventDefault();

    var item = this.state.item;
    var descricao = this.state.descricao;
    var categoria = this.categoria;
    var dificuldade = this.dificuldade;
    var modificador = this.modificador;

    var itemObject = {
      item: item,
      categoria: categoria,
      dificuldade: dificuldade,
      modificador: modificador,
      descricao: descricao
    };

    // fazer post do objeto para o server

    // salvar no banco o item
    console.log("item: " + itemObject);
  },

  handleClear: function(){

      $("input:radio").prop("checked", false);

      debugger;
      var cmbCategoria = $('#select_categoria');
      cmbCategoria.val('Selecione a categoria');
      this.setState(this.getInitialState());

  }

});

module.exports = CadastroItemContainer;
