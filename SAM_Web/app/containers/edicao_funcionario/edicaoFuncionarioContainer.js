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
    axios.get("http://sam/api/sam/user/" + usuario).then(
      function(response){
        var userData = response.data;
        axios.get("http://sam/api/sam/role/all").then(
          function(response){
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
            /*console.log(self.context);
            var rota = '/Erro/' + jqXHR.status;
            self.context.router.push({pathname: rota, state: {mensagem: "Um erro inesperado ocorreu no servidor. Por favor, tente novamente mais tarde."}});
            */
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
      perfilDados.Foto = evento.target.result;

      var token = localStorage.getItem("token");
      var config = {
        headers: {'token': token}
      };

      axios.put("http://sam/api/sam/user/update", perfilDados, config).then(
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
    var editavelRH = undefined;
    var editavelFuncionario = undefined;

    var perfil = localStorage.getItem("perfil");

    var imagemPreview = <img src= {this.state.urlImage} id="img_url"  className="center-block responsive-img"/>

    var cargos =[];
    this.state.lista_cargos.forEach(function(cargo){
      cargos.push( <option value={cargo.id}>{cargo.nome}</option>);
    });

    if(perfil == "Funcionario"){
      editavelRH = (
        <div>
          <div className="row">
            <div className="col s12">
              <span><b>Nome:</b> {this.state.nome}</span>
            </div>
          </div>
          <div className="row">
            <div className="col s12 m6 l6">
              <span><b>Level (Cargo):</b> {this.state.nome}</span>
            </div>
            <div className="col s12 m6 l6">
              <span><b>XP (Pontos):</b> {this.state.pontos}</span>
            </div>
            <div className="col s12 m6 l6">
              <span><b>Data de Início:</b> {moment(this.state.data_inicio).format('L')}</span>
            </div>
            <div className="col s12 m6 l6">
              <span><b>Perfil:</b> {this.state.perfil}</span>
            </div>
          </div>
        </div>
      );

      editavelFuncionario = (
        <div>
          <div className="row">
            <div className="input-field col s12 m4 l4">
              <input id="facebook"
                    type="text"
                    onChange={this.handleFacebook}
                    value={this.state.facebook}/>
                  <label for="facebook" className={this.state.facebook ? "active" : null}><b>Facebook:</b></label>
            </div>
            <div className="input-field col s12 m4 l4">
              <input id="linkedin"
                type="text"
                onChange={this.handleLikedinChanges}
                value={this.state.linkedin}/>
              <label for="linkedin" className={this.state.linkedin ? "active" : null}><b>Linkedin:</b></label>
            </div>
            <div className="input-field col s12 m4 l4">
              <input id="github"
                type="text"
                onChange={this.handleGithub}
                value={this.state.github}/>
              <label for="github" className={this.state.github ? "active" : null}><b>GitHub:</b></label>
            </div>
          </div>
          <div className="row">
            <div className="input-field col s12">
              <textarea id="bio"
                className="materialize-textarea"
                length="200"
                onChange={this.handleDescricao}
                value={this.state.descricao}>
              </textarea>
              <label for="bio" className={this.state.descricao ? "active" : null}><b>Bio:</b></label>
            </div>
          </div>
          <div className="row">
            <form action="#" className="col s10">
              <div className="file-field input-field">
                <div className="btn">
                  <span>Avatar</span>
                  <input type="file"
                    accept="image/gif, image/jpeg, image/png"
                    id = "img"
                    onChange={this.handleFoto}/>
                </div>
                <div className="file-path-wrapper">
                  <input className="file-path"
                    type="text"
                    accept="image/gif, image/jpeg, image/png"
                    id = "imagem"
                    onChange={this.handleFoto}
                    value={this.state.foto}
                    />
                </div>
              </div>
            </form>
            <div className="col s2">
                {this.state.urlImage ? imagemPreview : null}
            </div>
          </div>
        </div>
      );

    }else{
      editavelRH = (
        <div>
          <div className="row">
            <div className="input-field col s12">
              <input value = {this.state.nome} id="nome" type="text"/>
              <label for="nome" className="active">Nome: </label>
            </div>
          </div>
          <div className="row">
            <div className="input-field col s12 m6 l6">
              <select value={this.state.cargo_id}>
                {cargos}
              </select>
              <label>Level (Cargo):</label>
            </div>
            <div className="input-field col s12 m6 l6">
              <input value={this.state.pontos} id="pontos" type="text"/>
              <label for="pontos" className="active">XP (Pontos):</label>
            </div>
          </div>
          <div className="row">
            <div className="input-field col s12 m6 l6">
              <input disabled value={moment(this.state.data_inicio).format('L')} id="inicio" type="text"/>
              <label for="inicio" className="active">Data de Início:</label>
            </div>
            <div className="col s6">
              <div className="row">
                <div className="col s12 m3 l3">
                  <span style={{"fontSize" : "0.8rem"}}>Grupo: </span>
                </div>
                <div className="input-field">
                  <div className="col s6 m4 l4">
                    <input name="group1" type="radio" id="rh" disabled="disabled" checked ={this.state.perfil === "RH" ? true : false}/>
                    <label for="rh">RH</label>
                  </div>
                  <div className="col s6 m4 l4">
                    <input name="group1" type="radio" id="funcionario" disabled="disabled" checked ={this.state.perfil === "Funcionario" ? true : false}/>
                    <label for="funcionario">Funcionário</label>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      );

      editavelFuncionario = (
        <div>
          <div className="row">
            <div className="col s12 m4 l4">
              <span><b>Facebook: </b>{this.state.facebook}</span>
            </div>
            <div className="col s12 m4 l4">
              <span><b>Linkedin: </b>{this.state.linkedin}</span>
            </div>
            <div className="col s12 m4 l4">
              <span><b>GitHub: </b>{this.state.github}</span>
            </div>
          </div>
          <div className="row">
            <div className="col s12">
              <p><b>Bio: </b></p>
              <p>{this.state.descricao}</p>
            </div>
          </div>
          <div className="row">
            <div className="col s12">
              <span><b>Avatar: </b></span>
                <div className="col s2">
                    {this.state.urlImage ? imagemPreview : null}
                </div>
            </div>
          </div>
        </div>

      );

    }

    return(
      <EdicaoFuncionario
        nome = {this.state.nome}
        handleSubmit = {this.handleSubmit}
        handleClear = {this.handleClear}>
          <div>
            {editavelRH}
            {editavelFuncionario}
          </div>
      </EdicaoFuncionario>
    )


  }

});

module.exports = EdicaoFuncionarioContainer;
