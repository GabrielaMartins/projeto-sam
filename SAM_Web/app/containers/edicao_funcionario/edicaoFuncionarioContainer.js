'use strict'
var React = require('react');
var ReactRouter = require('react-router');
var EdicaoFuncionario = require('../../components/edicao_funcionario/edicaoFuncionario');
var axios = require("axios");
var moment = require('moment');
moment.locale('pt-br');

const EdicaoFuncionarioContainer = React.createClass({
  getInitialState: function(){
      return {
        nome:"..",
        cargo:"",
        cargo_id: null,
        pontos:0,
        data_inicio:"",
        perfil:"",
		    facebook:"",
        linkedin:"",
        github:"",
        descricao:"",
        foto:null,
        lista_cargos:[],
        urlImage:""
      }
  },
  componentDidMount: function(){
    var self = this;

    var token = localStorage.getItem("token");

    var usuario = this.props.params.samaccount;

    axios.defaults.headers.common['token'] = token;

    //fetch aqui
    axios.get("http://10.10.15.113:65122/api/sam/user/" + usuario).then(
      function(response){
        var userData = response.data;
        axios.get("http://10.10.15.113:65122/api/sam/role/all").then(
          function(response){
            debugger;
            self.setState({
              nome : userData.nome,
              cargo_id : userData.Cargo.id,
              pontos : userData.pontos,
              data_inicio : userData.DataInicio,
              perfil : userData.perfil,
              facebook : userData.facebook,
              linkedin : userData.linkedin,
              github: userData.github,
              descricao : userData.descricao,
              urlImage: userData.foto,
              lista_cargos:response.data
            });
          },
          function(jqXHR){
            debugger;
          }
        );
      },
      function(jqXHR){
        debugger;
      }
    );

  },

  componentDidUpdate: function(prevProps, prevState){

    $(document).ready(function(){
      $('select').material_select();
      $('.tooltipped').tooltip({delay: 50});
    });
  },

  handleFacebookChanges: function(event){
    this.setState({facebook: event.target.value})
  },

  handleLikedinChanges: function(event){
    this.setState({linkedin: event.target.value})
  },

  handleGithubChanges: function(event){
    this.setState({github: event.target.value})
  },

  handleDescricaoChanges: function(event){
    this.setState({descricao: event.target.value})
  },

  handleFotoChanges: function(event){
    this.setState({
      foto: event.target.value,
      urlImage: (window.URL ? URL : webkitURL).createObjectURL(event.target.files[0])
    })
  },

  getFile : function() {
    var imagem = ($('#img')[0]).files[0];
    return imagem;
  },

  handleSubmit: function(event){
    event.preventDefault();

    var perfilDados = {
       Cargo: this.state.cargo_id,
       Nome: this.state.nome,
       Pontos: this.state.pontos,
       DataInicio: this.state.data_inicio,
       Perfil: this.state.perfil,
       Facebook: this.state.facebook,
       Linkedin: this.state.linkedin,
       Github: this.state.github,
       Descricao: this.state.descricao,
       Foto: ""
     }

    var imagem = this.getFile();

    var reader = new FileReader();

    reader.onloadend = function(evento){
      debugger;
      perfilDados.Foto = evento.target.result;

      var config = {
        headers: {'token': 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwOi8vb3B1cy5zYW0uY29tIiwiaWF0IjoiXC9EYXRlKDE0NjY3MDkyMjI4OTgpXC8iLCJzdWIiOiJHYWJyaWVsYSBNYXJ0aW5zIiwiY29udGV4dCI6eyJ1c2VyIjp7ImlkIjoyLCJzYW1hY2NvdW50IjoiZ2FicmllbGEifSwicGVyZmlsIjoiRnVuY2lvbsOhcmlvIn19.nXiBQb5npG9QFFv7OYlb2QylCx0tdgloNYwo96uLp8M'}
      };

      axios.put("http://10.10.15.113:65122/api/sam/user/update", perfilDados, config).then(
        function(response){
          Materialize.toast('Seus dados foram atualizados com sucesso', 4000);
        },
        function(jqXHR){
          console.log(jqXHR);
        }
      );
    }

    reader.readAsDataURL(imagem);

    //this.context.router.push('/Perfil/gabriela');

  },

  handleClear: function(){
      var facebookField = $('#facebook');
      facebookField.val('');

      var linkedinField = $('#linkedin');
      linkedinField.val('');

      var githubField = $('#github');
      githubField.val('');

      var bioField = $('#bio');
      bioField.val('');

      var imagemField = $('#imagem');
      imagemField.val('');

      imagemField = $('#img');
      imagemField.val(null);

      this.setState({
        facebook : "",
        linkedin : "",
        github: "",
        descricao : "",
        foto:"",
        urlImage: ""
      });
  },

  render: function(){
    var cargos =[];
    this.state.lista_cargos.forEach(function(cargo){
      cargos.push( <option value={cargo.id}>{cargo.nome}</option>);
    });
    return(
      <EdicaoFuncionario
        nome = {this.state.nome}
        cargo = {this.state.cargo_id}
        pontos = {this.state.pontos}
        data_inicio = {moment(this.state.data_inicio).format('L')}
        perfil = {this.state.perfil}
        facebook = {this.state.facebook}
        linkedin = {this.state.linkedin}
        github = {this.state.github}
        descricao = {this.state.descricao}
        foto = {this.state.foto}
        lista_cargos = {cargos}
        urlImage = {this.state.urlImage}
        handleFacebook = {this.handleFacebookChanges}
        handleGithub = {this.handleGithubChanges}
        handleLikedin = {this.handleLikedinChanges}
        handleDescricao = {this.handleDescricaoChanges}
        handleFoto = {this.handleFotoChanges}
        handleSubmit = {this.handleSubmit}
        handleClear = {this.handleClear}
      />
    )


  }

});

module.exports = EdicaoFuncionarioContainer;
