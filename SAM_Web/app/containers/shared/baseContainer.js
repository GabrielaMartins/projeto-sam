'use strict'

//libs
var React = require('react');
var axios = require("axios");
var Config = require('Config');

//component
var Base = require('../../components/shared/base');

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

    axios.get(Config.serverUrl + '/api/sam/user/' + samaccount).then(
      function(response){
        if(response.data.perfil.toUpperCase() == "RH"){

          self.setState({
            perfil: response.data.perfil,
            samaccount: response.data.samaccount,
            dropdowns : [
              {
                itemMenu : "Funcionarios",
                id: 1,
                itens: [
                  {
                    nome: "Listar Todos",
                    url: "/Funcionario/Listagem",
                    id: 11
                  },
                  {
                    nome: "Editar Meu Perfil",
                    url: "/Funcionario/Edicao/" + samaccount,
                    id: 12
                  },
                  {
                    nome: "Meu Perfil",
                    url: "/Perfil/" + response.data.samaccount,
                    id: 13
                  },
                  {
                    nome: "Meu Histórico",
                    url: "/Perfil/" + response.data.samaccount + "/historico/",
                    id: 14
                  }
                ]
              },
              {
                itemMenu : "Itens",
                id: 2,
                itens: [
                  {
                    nome: "Listar Todos",
                    url: "/Item/Listagem",
                    id: 21
                  },
                  {
                    nome: "Cadastrar",
                    url: "/Item/Cadastro",
                    id: 22
                  },
                  {
                    nome: "Como Pontuar?",
                    url: "/Pontuacao",
                    id: 23
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
                  },
                  {
                    nome: "Ver Meu Perfil",
                    url: "/Perfil/" + response.data.samaccount,
                    id: 13
                  },
                  {
                    nome: "Ver Meu Histórico",
                    url: "/Perfil/" + response.data.samaccount + "/historico/",
                    id: 14
                  }
                ]
              },
              {
                itemMenu : "Itens",
                id: 2,
                itens: [
                  {
                    nome: "Listar Todos",
                    url: "/Item/Listagem",
                    id: 21
                  },
                  {
                    nome: "Como Pontuar?",
                    url: "/Pontuacao",
                    id: 22
                  }
                ]
              },
              {
                itemMenu : "Eventos",
                id: 3,
                itens: [
                  {
                    nome: "Realizar Atividade",
                    url: "/Item/Atividade",
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
          mensagem = "O seu acesso expirou, por favor, faça o login novamente.";
        }else if(status == "401"){
          mensagem = "Você está tentando acessar uma página que não te pertence, que feio!";
        }else{
          mensagem = "Um erro inesperado aconteceu, por favor, tente mais tarde";
        }
        self.context.router.push({pathname: rota, state: {mensagem: mensagem}});
      }
    );

    //mantem estática a barra de pesquisa de itens e funcionário
    (function($) {
      $(window).scroll(function() {
        var distanceFromTop = $(this).scrollTop();
        if (distanceFromTop > 45) {
          $('#campoBusca').addClass('stick');
          return false;
        } else {
          $('#campoBusca').removeClass('stick');
          return false;
        }
      });

      //inicializador do dropdown dos itens da barra de navegação
      $('.dropdown-button').dropdown({
        belowOrigin: true,
        alignment: 'left',
        inDuration: 200,
        outDuration: 150,
        constrain_width: true,
        hover: true,
        gutter: 1
      });


      //inicializador do botão colapse (barra de navegação mobile)
      $(document).ready(function() {
        $(".button-collapse").sideNav({
          closeOnClick: true
        });
        $('.collapsible').collapsible();
      });
    })(jQuery);

    //inicializador do scrollreveal (efeito de aparecimento dos cards)
    window.sr = ScrollReveal();

  },

  //logout
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
