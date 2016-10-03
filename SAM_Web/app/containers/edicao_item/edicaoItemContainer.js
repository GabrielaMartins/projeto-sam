'use strict'

//libs
var React = require('react');
var axios = require("axios");
var Config = require('Config');

//component
var EdicaoItem = require('../../components/edicao_item/edicaoItem');
var Loading = require('react-loading');

const EdicaoItemContainer = React.createClass({
  contextTypes: {
    router: React.PropTypes.object.isRequired
  },

  render: function(){

    //verifica se já buscou os cargos, enquanto isso, Loading..
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
        id_item: 0,
        item: "",
        descricao: "",
        modificador: "",
        usuarios: [],
        checked: false
      }
  },

  componentDidMount: function(){
    this.getCategory(Config.serverUrl+'/api/sam/category/all');

    //obter dados do item e setar os values dos inputs
    //id é passado por parametro na rota
    var id = this.props.params.id;
    this.getItem(Config.serverUrl+'/api/sam/item/' + id);

  },

  componentDidUpdate: function(prevProps, prevState){
    var self = this;

    //inicializador do select do materialize
    $(document).ready(function() {
      $('select').material_select();
      Materialize.updateTextFields();

      //verificar por que não deixa alterar o radio quando inicializa checado
      //$("input:radio").prop("checked", true);
      //faz bind do select e chama a função para setar o novo estado
      $("#select_categoria").on('change', self.handleCategoryChanges);
      $("#select_dificuldade").on('change', self.handleDificultyChanges);

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

  getItem: function(url){
    var token = localStorage.getItem("token");

    var config = {
      headers: {'token': token}
    };

    // recupera o item
    var self = this;

    axios.get(url, config).then(
      function(response){
        var item = response.data;
        self.setState({
          id_item: item.id,
          item: item.nome,
          categoria: item.Categoria.id + "",
          dificuldade: item.dificuldade,
          descricao: item.descricao,
          modificador: item.modificador,
          usuarios: item.Usuarios
        });
      },
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

    //muda os rótulos do radio de acordo com a categoria
    if(this.state.categoria.toLowerCase() === "apresentacao" || this.state.categoria.toLowerCase() === "blog técnico"){
      this.setState({
        rotulosRadio: ["Raso", "Profundo"],
        checked: true
      });
    }else{
      this.setState({
        rotulosRadio: ["Alinhado", "Não Alinhado"],
        checked: true
      });
    }

  },

  handleCategoryChanges: function(event){

    // obtem a categoria escolhida
    var categoriaId = parseInt(event.target.value);
    var categoria = this.state.categorias.find(function(c){return c.id == categoriaId;});

    // troca o rotulo dos radio
    if(categoria.nome.toLowerCase() === "apresentacao" || categoria.nome.toLowerCase() === "blog técnico"){
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
    var id = this.state.id_item;
    var item = this.state.item;
    var descricao = this.state.descricao;
    var categoria = this.state.categoria;
    var dificuldade = this.state.dificuldade;
    var modificador = this.state.modificador;


    var itemObject = {
      Id: id,
      Nome: item,
      Categoria: categoria,
      Dificuldade: dificuldade,
      Modificador: modificador,
      Descricao: descricao
    };

    // faz post do objeto para o servidor

    var token = localStorage.getItem("token");

    var config = {
      headers: {'token': token}
    };

    var samaccount = localStorage.getItem("samaccount");

    var self  = this;
    var rota = "/Dashboard/RH/" + samaccount;

    axios.put(Config.serverUrl+"/api/sam/item/update/" + this.state.id_item , itemObject, config).then(

      function(){
        //sucesso
        swal({
          title: "Dados Enviados!",
          text: "Os dados foram salvos com sucesso",
          type: "success",
          confirmButtonText: "Ok",
          confirmButtonColor: "#550000"
        },function(){
          self.context.router.push({pathname: rota});
        });
      },

      function(){
      //erro
      swal({
        title: "Algum Erro Ocorreu!",
        text: "Os dados não foram salvos, por favor, tente novamente.",
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
        modificador: "",
        item: "",
        descricao: "",
        checked: false,
        erroDescricao: "",
        erroItem: "",
        erroCategoria: "",
        erroModificador: "",
        erroDificuldade: ""
      });

  }

});

module.exports = EdicaoItemContainer;
