var React = require('react');
var Base = require('../../components/shared/base');

var BaseContainer = React.createClass({
  getInitialState: function() {
    return {
      dropdowns: null
    };
  },
  componentWillMount: function(){
    this.setState({
      dropdowns : [
          {
            itemMenu : "Funcionários",
            id: 1,
            itens: [
              {
                nome: "Listar",
                url: "#",
                id: 3
              },
              {
                nome: "Cadastrar",
                url: "#",
                id: 4
              }
            ]
          },
          {
            itemMenu : "Itens",
            id: 2,
            itens: [
              {
                nome: "Listar",
                url: "#",
                id: 1
              },
              {
                nome: "Cadastrar",
                url: "#",
                id: 2
              }
            ]
          },
          {
            itemMenu : "Gabriela",
            id: 5,
            itens: [
              {
                nome: "Perfil",
                url: "#",
                id: 1
              },
              {
                nome: "Configuração",
                url: "#",
                id: 2
              }
            ]
          }
        ]
    });
  },
  render : function(){
      return(<Base dropdowns = {this.state.dropdowns} children = {this.props.children} />)
  }
});

module.exports = BaseContainer;
