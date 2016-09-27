'use strict'

//libs
var React = require('react');
var axios = require("axios");
var Config = require('Config');

//component
var CadastroItem = require('../../components/cadastro_itens/cadastroItem');
var Loading = require('react-loading');


const CadastroItemContainer = React.createClass({
  contextTypes: {
    router: React.PropTypes.object.isRequired
  },

  render: function(){

    //verifica se já buscou as categorias, enquanto isso, Loading..
    if(this.state.categorias.length === 0){
      return (
        <div className="full-screen-less-nav">
          <div className="row wrapper">
            <Loading type='bubbles' color='#550000' height={150} width={150}/>
          </div>
        </div>
      );
    }

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
      Materialize.updateTextFields();
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

    var token = localStorage.getItem("token");
    var samaccount = localStorage.getItem("samaccount");

    var rota = "/Perfil/" + samaccount;

    var config = {
      headers: {'token': token}
    };

    // faz post do objeto para o servidor
    axios.post(Config.serverUrl + "/api/sam/item/save", itemObject, config).then(
      function(response){
        swal({
          title: "Dados Enviados!",
          text: "Os dados foram salvos com sucesso",
          type: "success",
          confirmButtonText: "Ok",
          confirmButtonColor: "#550000"
        },function(){
          this.context.router.push({pathname: rota});
        }.bind(this));
      },

      function(){
        swal({
          title: "Algum Erro Ocorreu!",
          text: "Os dados foram salvos, por favor, tente novamente.",
          type: "error",
          confirmButtonText: "Ok",
          confirmButtonColor: "#550000"
        });
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
