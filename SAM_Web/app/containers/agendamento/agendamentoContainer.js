'use strict'
var React = require('react');
var Agendamento = require('../../components/agendamento/agendamento');
var axios = require("axios");

//configurações para passar o token
var token = localStorage.getItem("token");
var config = {
  headers: {'token': token}
};

const AgendamentoContainer = React.createClass({

  render: function(){
    return(
      <Agendamento
        handleCategoryChanges = {this.handleCategoryChanges}
        handleDescriptionChanges = {this.handleDescriptionChanges}
        handleItemChanges = {this.handleItemChanges}
        handleDateChanges = {this.handleDateChanges}
        handleSubmit = {this.handleSubmit}
        handleClear = {this.handleClear}
        item = {this.state.item}
        itens = {this.state.itens}
        descricao = {this.state.descricao}
        categoria = {this.state.categoria}
        date = {this.state.date}
        categorias = {this.state.categorias.map(function(categoria, index){
          return <option value = {categoria.id} key = {index + 1}>{categoria.nome}</option>
        })}
        itens = {this.state.itens.map(function(item, index){
          return <option value = {item.id} key = {index + 1}>{item.nome}</option>
        })}
      />
    )
  },

  getInitialState: function(){
      return {
		    categorias: [],
        itens:[],
        categoria: "Selecione a categoria",
        item: "Selecione o item",
        descricao: "",
        date:""
      }
  },

  componentDidMount: function(){
    var self = this;

    $(document).ready(function() {
      $('select').material_select();
      self.setupDatepicker();
    });

    this.getCategory('http://sam/api/sam/category/all');
    this.getItem('http://sam/api/sam/item/all');

    $("#select_categoria").on('change', self.handleCategoryChanges);
    $("#select_item").on('change', self.handleItemChanges);

  },

  componentDidUpdate: function(){
    var self = this;
    $(document).ready(function() {
      $('select').material_select();
      self.setupDatepicker();
    });
  },

  getCategory: function(url){
      // recupera as categorias
      var self = this;

      axios.get(url, config).then(
        function(response){
          var categorias = response.data;
          self.setState({categorias: categorias});
        },
        function(reason){
          debugger;
        }
      );
  },

  getItem: function(url){

      var self = this;
      axios.get(url, config).then(
        function(response){
          console.log(response);
          var itens = response.data;
          self.setState({itens: itens});
        },
        function(reason){
          debugger;
        }
      );
  },

  handleCategoryChanges: function(event){

    this.setState({categoria: event.target.value});
  },

  handleItemChanges: function(event){
    this.setState({item: event.target.value});
  },

  handleDescriptionChanges: function(event){
    var descricao = event.target.value;
    this.setState({descricao: descricao});
  },

  handleDateChanges: function(event){
    var date = event.target.value;
    this.setState({date: date});
  },

  handleSubmit: function(event){
    debugger;
    event.preventDefault();

    var item = this.state.item;
    var descricao = this.state.descricao;
    var categoria = this.state.categoria;
    var date = this.state.date;

    //obter funcionário que está logado
    var funcionario = localStorage.getItem("samaccount");

    var itemAgendado = {
      funcionario: funcionario,
      item: item,
      categoria: categoria,
      date: date,
      descricao: descricao
    };

    // fazer post do objeto para o server
    axios.post("http://sam/api/sam/scheduling/create", itemAgendado, config).then(
      function(response){
      },
      function(reason){
        debugger;
      }
    );
    // salvar no banco o item
    console.log("item: " + itemAgendado);
  },

  handleClear: function(){

      this.setState({
        item: "Selecione o item",
        categoria: "Selecione a categoria",
        descricao: "",
        date:""
      });

  },
  setupDatepicker: function() {
    // cache this so we can reference it inside the datepicker
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
        // you can use any of the pickadate options here
        var val = this.get('select', 'dd-mm-yyyy');
        self.handleDateChanges({target: {value: val}});
      }
    });
  }

});

module.exports = AgendamentoContainer;
