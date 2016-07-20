var React = require('react');
var Base = require('../../components/shared/base');
var axios = require("axios");

var BaseContainer = React.createClass({
  getInitialState: function() {
    return {
      dropdowns: null,
      samaccount:""
    };
  },

  componentDidMount: function(){
  
    //fetch informações do usuario
    var token = localStorage.getItem("token");

    axios.defaults.headers.common['token'] = token;

    // busca no banco esse samaccount
    axios.get('http://10.10.15.113:65122/api/sam/perfil/').then(
        function(response){
          this.setState({
            samaccount: response.data.Usuario.samaccount,
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
                      nome: "Editar",
                      url: "/Funcionario/Edicao/gabriela",
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
                  itemMenu : response.data.Usuario.nome,
                  imagem:response.data.Usuario.foto,
                  id: 8,
                  itens: [
                    {
                      nome: "Perfil",
                      url: "/Perfil/" + response.data.Usuario.samaccount,
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
        }.bind(this),

        function(jqXHR){
          debugger;
        }
    );


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
          $('.pesquisar').addClass('white-text');
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
                nome: "Editar",
                url: "/Funcionario/Edicao/gabriela",
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
            itemMenu : "",
            imagem:"",
            id: 8,
            itens: [
              {
                nome: "Perfil",
                url: "",
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
      return(<Base dropdowns = {this.state.dropdowns} children = {this.props.children} samaccount ={this.state.samaccount}/>)
  }
});

module.exports = BaseContainer;
