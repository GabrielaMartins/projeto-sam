'use strict'

var React = require('react');
var CadastroItem = require('../../components/cadastro_itens/cadastroItem');
var axios = require("axios");
var Config = require('Config');

const CadastroItemContainer = React.createClass({

  render: function(){

    //opções do select para categorias
    var categorias = this.state.categorias.map(function(categoria, index){
      return <option value = {categoria.id} key = {index + 1}>{categoria.nome}</option>
    });

    return(
      <CadastroItem
        handleCategoryChanges = {this.handleCategoryChanges}
        handleDificultyChanges = {this.handleDificultyChanges}
        handleModifierChanges = {this.handleModifierChanges}
        handleDescriptionChanges = {this.handleDescriptionChanges}
        handleItemChanges = {this.handleItemChanges}
        handleSubmit = {this.handleSubmit}
        handleClear = {this.handleClear}
        rotulosRadio = {this.state.rotulosRadio}
        item = {this.state.item}
        descricao = {this.state.descricao}
        categoria = {this.state.categoria}
        dificuldade = {this.state.dificuldade}
        categorias = {categorias}
      />
    )
  },

  //estado inicial das variáveis
  getInitialState: function(){
      return {
		    categorias: [],
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
    this.getCategory(Config.serverUrl+'/api/sam/category/all');

    //faz bind do select e chama a função para setar o novo estado
    $("#select_categoria").on('change', self.handleCategoryChanges);
    $("#select_dificuldade").on('change', self.handleDificultyChanges);

  },

  componentDidUpdate: function(prevProps, prevState){

    //inicializador do select do materialize
    $(document).ready(function() {
      $('select').material_select();
    });

  },

  getCategory: function(url){
    var token = localStorage.getItem("token");

    var config = {
      headers: {'token': token}
    };
    // recupera as categorias
    var self = this;
    axios.get(url, config).then(
      function(response){
        var categorias = response.data;
        self.setState({categorias: categorias});
      },
      function(reason){
      }
    );
  },

  handleCategoryChanges: function(event){

    // obtem a categoria escolhida
    var categoriaId = parseInt(event.target.value);
    var categoria = this.state.categorias.find(function(c){return c.id == categoriaId;});

    // troca o rotulo dos radio
    if(categoria.nome.toLowerCase() === "workshop"){
      this.setState({
          rotulosRadio: ["Raso", "Profundo"],
          categoria: event.target.value
      });
    }else{
      this.setState({
        rotulosRadio: ["Alinhado", "Não Alinhado"],
        categoria: event.target.value
      });
    }

  },

  handleDificultyChanges: function(event){

    var dificuldade = event.target.value;
    this.setState({dificuldade: dificuldade});
  },

  handleModifierChanges: function(event){

    var modificador = event.target.value;
    var val;
    if(modificador === "Raso"){
      val = 2;
    }else if(modificador === "Profundo"){
      val = 3;
    }else if(modificador === "Alinhado"){
      val = 1;
    }else if(modificador === "Não Alinhado"){
      val = 3;
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

  handleSubmit: function(event){
    event.preventDefault();

    var item = this.state.item;
    var descricao = this.state.descricao;
    var categoria = this.state.categoria;
    var dificuldade = this.state.dificuldade;
    var modificador = this.state.modificador;

    var itemObject = {
      Nome: item,
      Categoria: categoria,
      Dificuldade: dificuldade,
      Modificador: modificador,
      Descricao: descricao
    };

    console.log(itemObject);

    var token = localStorage.getItem("token");

    var config = {
      headers: {'token': token}
    };

    // faz post do objeto para o servidor
    axios.post(Config.serverUrl + "/api/sam/item/save", itemObject, config).then(
      function(response){

      },

      function(){

      }
    );

  },

  //limpa os dados do formulário
  handleClear: function(){

      $("input:radio").prop("checked", false);

      this.setState({
        rotulosRadio: ["Raso", "Profundo"],
        dificuldade: "Selecione a dificuldade",
        categoria: "Selecione a categoria",
        item: "",
        descricao: "",
        checked: false
      });

  }

});

module.exports = CadastroItemContainer;
