'use strict'

var React = require('react');
var EdicaoItem = require('../../components/edicao_itens/edicaoItem');
var axios = require("axios");
var Config = require('Config');

const CadastroItemContainer = React.createClass({

  render: function(){

    //opções do select para categorias
    var categorias = this.state.categorias.map(function(categoria, index){
      return <option value = {categoria.id} key = {index + 1}>{categoria.nome}</option>
    });

    return(
      <EdicaoItem
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

    //obter dados do item e setar os values dos inputs
    //id é passado por parametro na rota
    var id = this.props.location.query.id;

    this.getItem(Config.serverUrl+'/api/sam/item/update/' + id);


  },

  componentDidUpdate: function(prevProps, prevState){

    //inicializador do select do materialize
    $(document).ready(function() {
      $('select').material_select();
    });

  },

  getCategory: function(url){

      // recupera as categorias
      var self = this;
      axios.get(url).then(
        function(response){
          var categorias = response.data;
          self.setState({categorias: categorias});
        },
        function(reason){
        }
      );
  },

  getItem: function(url){

    // recupera as categorias
    var self = this;
    axios.get(url).then(
      function(response){
        self.setState({item: response.data});
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

    // faz post do objeto para o servidor

    // salva no banco o item
    console.log("item: " + itemObject);
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
