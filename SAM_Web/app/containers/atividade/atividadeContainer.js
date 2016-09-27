'use strict'

//libs
var React = require('react');
var axios = require("axios");

//acesso às configurações de url para requisição ao server
var Config = require('Config');

//componente de Agendamento e loading
var Atividade = require('../../components/atividade/atividade');
var Loading = require('react-loading');

//momentjs
var moment = require('moment');
moment.locale('pt-br');


const AtividadeContainer = React.createClass({
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

    /*categorias a serem exibidas no select*/
    var categorias = this.state.categorias.map(function(categoria, index){
      return <option value = {categoria.id} key = {index + 1}>{categoria.nome}</option>
    });

    /*Verifica se a categoria selecionada pertence a um determinado item, afim de filtrar os itens a serem exibidos no select*/
    var itens = this.state.itens.map(function(item, index){
      if(item.Categoria.id == this.state.categoria || this.state.categoria == "0" || this.state.categoria == "Selecione a categoria"){
        return <option value = {item.id} key = {index + 1}>{item.nome}</option>
      }
    }.bind(this));

    return(

      <Atividade
        handleCategoryChanges = {this.handleCategoryChanges}
        handleDescriptionChanges = {this.handleDescriptionChanges}
        handleItemChanges = {this.handleItemChanges}
        handleDataChanges = {this.handleDataChanges}
        handleSubmit = {this.handleSubmit}
        handleClear = {this.handleClear}
        item = {this.state.item}
        itens = {this.state.itens}
        descricao = {this.state.descricao}
        categoria = {this.state.categoria}
        data = {this.state.data}
        erroItem = {this.state.erroItem}
        erroCategoria= {this.state.erroCategoria}
        erroData= {this.state.erroData}
        erroDescricao= {this.state.erroDescricao}
        categorias = {categorias}
        itens = {itens}
        mostraAlerta = {this.state.mostraAlerta}
      />
    )
  },

  //estado inicial das variáveis
  getInitialState: function(){
      return {
		    categorias: [],
        itens:[],
        categoria: "Selecione a categoria",
        item: "Selecione o item",
        descricao: "",
        data:"",
        erroItem: undefined,
        erroCategoria: undefined,
        erroData: undefined,
        erroDescricao: undefined,
        mostraAlerta: false
      }
  },

  componentDidMount: function(){
    var self = this;

    //funções para obter categorias e itens
    this.getCategory(Config.serverUrl+'/api/sam/category/all');
    this.getItem(Config.serverUrl+'/api/sam/item/all');


    this.forceUpdate();

  },

  componentDidUpdate: function(){
    var self = this;

    //inicializador do select do materialize
    $(document).ready(function() {
      $('select').material_select();
      self.setupDatepicker();
    });

    //faz bind do select e chama a função para setar o novo estado
    $("#select_categoria").on('change', self.handleCategoryChanges);
    $("#select_item").on('change', self.handleItemChanges);

  },

  getCategory: function(url){
      // recupera as categorias
      var self = this;

      //configurações para passar o token
      var token = localStorage.getItem("token");

      var config = {
        headers: {'token': token}
      };

      axios.get(url, config).then(
        function(response){
          var categorias = response.data;
          self.setState({categorias: categorias});
        },

        //erro
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

      var self = this;

      //configurações para passar o token
      var token = localStorage.getItem("token");

      var config = {
        headers: {'token': token}
      };

      axios.get(url, config).then(
        function(response){
          var itens = response.data;
          self.setState({itens: itens, mostraAlerta: true});
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

  handleCategoryChanges: function(event){
    var categoria = event.target.value;

    //se a categoria é diferente de 0 (alguma categoria foi selecionada), não retornará mensagem de erro
    if( categoria != "0"){
      this.setState({
        erroCategoria: undefined
      });
    }

    //seta a catogoria selecionada
    this.setState({categoria: categoria});
  },

  handleItemChanges: function(event){
    var item = event.target.value;
    if( item != "0"){
      this.setState({
        erroItem: undefined
      });
    }
    this.setState({
      item: item
    });

  },

  handleDescriptionChanges: function(event){
    var descricao = event.target.value;
    if( descricao != ""){
      this.setState({
        erroDescricao: undefined
      });
    }
    this.setState({descricao: descricao});
  },

  handleDataChanges: function(event){
    var data = event.target.value;
    if( data != ""){
      this.setState({
        erroData: undefined
      });
    }
    this.setState({data: data});
  },

  //faz validação dos campos de agendamento
  validacao: function(){
    var valido = true;

    var hoje = moment().format('L');

    //se nenhum item foi selecionado, então retornará mensagem de erro e tora o formulário inválido
    if(this.state.item === "Selecione o item" || this.state.item === "0"){
      this.setState({
        erroItem: "Por favor, selecione uma atividade"
      });

      valido = false;
    }

    if(this.state.categoria === "Selecione a categoria" || this.state.categoria === "0"){
      this.setState({
        erroCategoria: "Por favor, selecione uma categoria"
      });
      valido = false;
    }

    if(this.state.data === ""){
      this.setState({
        erroData: "Por favor, selecione uma data"
      });
      valido = false;
    }

    //validação de data
    var data_atividade = moment(this.state.data, 'DD-MM-YYYY').format('L');
    if( data_atividade < hoje){
      this.setState({
        erroData: "Por favor, escolha uma data maior que o dia de hoje."
      });
      valido = false;
    }

    if(this.state.descricao === ""){
      this.setState({
        erroDescricao: "Por favor, adicione uma descrição"
      });
      valido = false;
    }

    return valido;

  },

  handleSubmit: function(event){
    event.preventDefault();

    //se o formulário não está válido, não envia os dados para o servidor
    if (this.validacao() == false){
      return;
    }

    var item = this.state.item;
    var descricao = this.state.descricao;
    var categoria = this.state.categoria;
    var data = this.state.data;

    //obter funcionário que está logado
    var funcionario = localStorage.getItem("samaccount");
    var perfil = localStorage.getItem("perfil");

    var itemAgendado = {
      funcionario: funcionario,
      item: item,
      categoria: categoria,
      data: data,
      descricao: descricao
    };

    var self = this;
    //configurações para passar o token
    var token = localStorage.getItem("token");

    var config = {
      headers: {'token': token}
    };

    // faz post do objeto para o servidor
    axios.post(Config.serverUrl+"/api/sam/activity/schedule", itemAgendado, config).then(
      function(response){
        //retorna um alert confirmando o envio dos dados e limpa formulário
        swal({
          title: "Dados Enviados!",
          text: "Os dados foram salvos com sucesso",
          type: "success",
          confirmButtonText: "Ok",
          confirmButtonColor: "#550000"
        },function(){
          self.handleClear();
          self.context.router.push({pathname: "/Dashboard/" + perfil +"/"+ funcionario});
        });
      },
      function(jqXHR){
        var status = jqXHR.status;
        var titulo;
        var mensagem;

        if(status == 403){
          titulo = "Este evento já existe!";
          mensagem = "O evento agendado já existe na data definida."
        }else{
          titulo = "Um Erro Ocorreu!";
          mensagem = "Os dados não puderam ser salvos, tente novamente mais tarde.";
        }
        //retorna alerta de erro
        swal({
          title: titulo,
          text: mensagem,
          type: "error",
          confirmButtonText: "Ok",
          confirmButtonColor: "#550000"
        });

      }
    );

  },

  //limpa os dados do formulário
  handleClear: function(){

      this.setState({
        item: "Selecione o item",
        categoria: "Selecione a categoria",
        descricao: "",
        data:"",
        erroItem: undefined,
        erroCategoria: undefined,
        erroData: undefined,
        erroDescricao: undefined
      });

  },

  //inicializador do datepicker
  setupDatepicker: function() {

    var self = this;

    $('.datepicker').pickadate({
      monthsFull: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
      weekdaysShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab'],
      today: 'hoje',
      clear: 'limpar',
      close: 'fechar',
      format: 'dd-mm-yyyy',
      formatSubmit: 'dd-mm-yyyy',
      selectMonths: true,
      selectYears: 5,
      closeOnSelect: true,
      onSet: function(e) {
        var val = this.get('select', 'dd-mm-yyyy');
        self.handleDataChanges({target: {value: val}});
      }
    });
  }

});

module.exports = AtividadeContainer;
