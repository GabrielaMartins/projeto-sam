var React = require('react');
var Base = require('../../components/shared/base');
var axios = require("axios");
var Config = require('Config');

var BaseContainer = React.createClass({
  contextTypes: {
    router: React.PropTypes.object.isRequired
  },

  getInitialState: function() {
    return {
      dropdowns: [
        {
          itemMenu : "Funcionarios",
          id: 1,
          itens: []
        },
        {
          itemMenu : "Itens",
          id: 2,
          itens: []
        },
        {
          itemMenu : "Eventos",
          id: 3,
          itens: []
        }
      ],
      samaccount:"",
      perfil:""
    };
  },

  componentDidMount: function(){

    //fetch informações do usuario
    var token = localStorage.getItem("token");
    var samaccount = localStorage.getItem("samaccount");
    var self = this;

    axios.defaults.headers.common['token'] = token;
    // busca no banco esse samaccount
    axios.get(Config.serverUrl + '/api/sam/user/' + samaccount).then(
      function(response){
        if(response.data.perfil == "RH"){

          self.setState({
            perfil: response.data.perfil,
            samaccount: response.data.samaccount,
            dropdowns : [
              {
                itemMenu : "Funcionarios",
                id: 1,
                itens: [
                  {
                    nome: "Listar",
                    url: "/Funcionario/Listagem",
                    id: 11
                  },
                  {
                    nome: "Editar Meu Perfil",
                    url: "/Funcionario/Edicao/" + samaccount,
                    id: 12
                  },
                ]
              },
              {
                itemMenu : "Itens",
                id: 2,
                itens: [
                  {
                    nome: "Listar",
                    url: "/Item/Listagem",
                    id: 21
                  },
                  {
                    nome: "Cadastrar",
                    url: "/Item/Cadastro",
                    id: 22
                  }
                ]
              },
              {
                itemMenu : response.data.nome,
                imagem:response.data.foto,
                id: 4,
                itens: [
                  {
                    nome: "Perfil",
                    url: "/Perfil/" + response.data.samaccount,
                    id: 41
                  }
                ]
              }
            ]
          });
        }else{
          self.setState({
            perfil: response.data.perfil,
            samaccount: response.data.samaccount,
            dropdowns : [
              {
                itemMenu : "Funcionario",
                id: 1,
                itens: [
                  {
                    nome: "Editar Meu Perfil",
                    url: "/Funcionario/Edicao/" + samaccount,
                    id: 12
                  }
                ]
              },
              {
                itemMenu : "Itens",
                id: 2,
                itens: [
                  {
                    nome: "Listar",
                    url: "/Item/Listagem",
                    id: 21
                  }
                ]
              },
              {
                itemMenu : "Eventos",
                id: 3,
                itens: [
                  {
                    nome: "Agendamento",
                    url: "/Item/Agendamento",
                    id: 31
                  },
                  /*{
                    nome: "Lista de Eventos",
                    url: "/Item/Agendamento",
                    id: 32
                  }*/
                ]
              },
              {
                itemMenu : response.data.nome,
                imagem:response.data.foto,
                id: 4,
                itens: [
                  {
                    nome: "Perfil",
                    url: "/Perfil/" + response.data.samaccount,
                    id: 41
                  }
                ]
              }
            ]
          });
        }
      },

      function(jqXHR){
        //direcionar para a página de erro
        status = jqXHR.status;
        var rota = '/Erro/' + status;
        var mensagem = ""
        if(status == "500"){
          mensagem = "O seu acesso expirou, por favor, faça o login novamente."
        }else{
          mensagem = "Um erro inesperado aconteceu, por favor, tente mais tarde";
        }
        self.props.history.push({pathname: rota, state: {mensagem: mensagem}});
      }
    );

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

      (function($) {
        $(function() {
          $('.dropdown-button').dropdown({
            belowOrigin: true,
            alignment: 'left',
            inDuration: 200,
            outDuration: 150,
            constrain_width: true,
            hover: true,
            gutter: 1
          });
        }); // End Document Ready
      })(jQuery); // End of jQuery name space

      $(document).ready(function() {
        $(".button-collapse").sideNav({
          closeOnClick: true
        });
        $('.collapsible').collapsible();
      });

    })(jQuery);

    window.sr = ScrollReveal();
  },
  cleanupLocalStorage: function(){
    localStorage.clear();
    this.context.router.push('/');
  },
  render : function(){
    return(
      <Base
        logout = {this.cleanupLocalStorage}
        dropdowns = {this.state.dropdowns}
        children = {this.props.children}
        samaccount ={this.state.samaccount}
        perfil = {this.state.perfil}/>
    );
  }
});

module.exports = BaseContainer;
