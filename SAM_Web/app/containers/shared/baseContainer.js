var React = require('react');
var Base = require('../../components/shared/base');

var BaseContainer = React.createClass({
  getInitialState: function() {
    return {
      dropdowns: null
    };
  },
  componentDidMount: function(){
    (function($) {
      $(function() {
        $(document).ready(function() {
          $(".button-collapse").sideNav({
            closeOnClick: true
          });
          $('.collapsible').collapsible();
        });
      }); // End Document Ready
    })(jQuery);

    (function($) {
      $(window).scroll(function() {
        var distanceFromTop = $(this).scrollTop();
        if (distanceFromTop > 45) {
          $('#campoBusca').addClass('stick');
          $('#campoBusca > .card').css('background-color', '#801515');
          $('#search').addClass('white-text');
          $('#campoBusca > .card > .material-icons').addClass('white-text');
          return false;
        } else {
          $('#campoBusca').removeClass('stick');
          $('#campoBusca > .card').css('background-color', '#FFF');
          $('#search').removeClass('white-text');
          $('#campoBusca > .card > .material-icons').removeClass('white-text');
          return false;
        }
      });
    })(jQuery);

    //this.forceUpdate();
  },
  componentWillMount: function(){
    this.setState({
      dropdowns : [
          {
            itemMenu : "Funcionarios",
            id: 2,
            itens: [
              {
                nome: "Listar",
                url: "/Funcionario/Listagem",
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
            id: 5,
            itens: [
              {
                nome: "Listar",
                url: "/Item/Listagem",
                id: 6
              },
              {
                nome: "Cadastrar",
                url: "/Item/Cadastro",
                id: 7
              }
            ]
          },
          {
            itemMenu : "Gabriela",
            imagem:"./app/imagens/fulano.jpg",
            id: 8,
            itens: [
              {
                nome: "Perfil",
                url: "#",
                id: 9
              },
              {
                nome: "Configuração",
                url: "#",
                id: 10
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
