'use strict'

var React = require('react');
var ReactRouter = require('react-router');
var Config = require('Config');
var axios = require("axios");

//components
var EdicaoFuncionario = require('../../components/edicao_funcionario/edicaoFuncionario');
import AvatarEditor from 'react-avatar-editor';

//momentjs
var moment = require('moment');
moment.locale('pt-br');

const EdicaoFuncionarioContainer = React.createClass({
  contextTypes: {
    router: React.PropTypes.object.isRequired
  },
  getInitialState: function(){
    return {
      nome:"",
      cargo:"",
      cargo_id: 0,
      pontos:0,
      data_inicio:"",
      perfil:"",
      facebook:"",
      linkedin:"",
      github:"",
      descricao:"",
      foto:undefined,
      lista_cargos:[],
      urlImage:"",
    }
  },
  componentDidMount: function(){

    var self = this;
    var token = localStorage.getItem("token");

    var usuario = this.props.params.samaccount;

    axios.defaults.headers.common['token'] = token;

    axios.get(Config.serverUrl + "/api/sam/user/" + usuario).then(
      function(response){
        var userData = response.data;
        axios.get(Config.serverUrl + "/api/sam/role/all").then(
          function(response){

            self.setState({
              nome : userData.nome,
              cargo_nome : userData.Cargo.nome,
              cargo_id : userData.Cargo.id,
              pontos : userData.pontos,
              data_inicio : userData.dataInicio,
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
    });

    $('#cropImage').openModal({
      dismissible: false
    });
  },

  getFile : function() {
    var imagem = ($('#img')[0]).files[0];
    return imagem;
  },

  handleSubmit: function(event){
    event.preventDefault();

    //obtém a imagem do canvas e transforma em data url para enviar para o server
    var imagem = this.refs.editor.getImage().toDataURL();

    var perfilDados = {
      cargo: this.state.cargo_id,
      nome: this.state.nome,
      pontos: this.state.pontos,
      dataInicio: this.state.data_inicio,
      perfil: this.state.perfil,
      facebook: this.state.facebook,
      linkedin: this.state.linkedin,
      github: this.state.github,
      descricao: this.state.descricao,
      foto: imagem
    }

      var self = this;
      var token = localStorage.getItem("token");
      var samaccount = localStorage.getItem("samaccount");

      var rota = "/Perfil/" + samaccount;

      var config = {
        headers: {'token': token}
      };

      axios.put(Config.serverUrl+"/api/sam/user/update/" + samaccount, perfilDados, config).then(
        function(response){
          swal({
            title: "Dados Enviados!",
            text: "Os dados foram salvos com sucesso",
            type: "success",
            confirmButtonText: "Ok",
            confirmButtonColor: "#550000"
          },function(){
            this.context.router.push({pathname: rota});
          });
        },
        function(jqXHR){
          //retorna página de erro
          swal({
            title: "Um Erro Ocorreu!",
            text: "Os dados não puderam ser salvos, tente novamente mais tarde.",
            type: "error",
            confirmButtonText: "Ok",
            confirmButtonColor: "#550000"
          });
        }
      );
    //}

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
    imagemField.val(undefined);

    this.setState({
      facebook : "",
      linkedin : "",
      github: "",
      descricao : "",
      foto:"",
      urlImage: ""
    });
  },

  onClickSave: function() {
    var canvas = this.refs.editor.getImage();

    canvas.toBlob(function(blob) {
      var url = (window.URL ? URL : webkitURL).createObjectURL(blob);

      this.setState({
        urlImage: url
      });
    }.bind(this));

  },

  primeiraParteForms: function(){
    var perfil = localStorage.getItem("perfil");

    var cargos =[];
    this.state.lista_cargos.forEach(function(cargo){
      cargos.push( <option value={cargo.id}>{cargo.nome}</option>);
    });

    if(perfil == "Funcionario"){
      return (
        <div>
          <div className="row">
            <div className="col s12">
              <span><b>Nome:</b> {this.state.nome}<br/><br/></span>
            </div>
            <div className="col s12 m6 l6">
              <span><b>Level (Cargo):</b> {this.state.cargo_nome}<br/><br/></span>
            </div>
            <div className="col s12 m6 l6">
              <span><b>XP (Pontos):</b> {this.state.pontos}<br/><br/></span>
            </div>
            <div className="col s12 m6 l6">
              <span><b>Data de Início:</b> {moment(this.state.data_inicio).format('L')}<br/><br/></span>
            </div>
            <div className="col s12 m6 l6">
              <span><b>Perfil:</b> {this.state.perfil}<br/><br/></span>
            </div>
          </div>
        </div>
      );
    }else{
      return (
        <div>
          <div className="row">
            <div className="input-field col s12">
              <input value = {this.state.nome} id="nome" type="text"/>
              <label htmlFor="nome" className="active">Nome: </label>
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
              <label htmlFor="pontos" className="active">XP (Pontos):</label>
            </div>
          </div>
          <div className="row">
            <div className="input-field col s12 m6 l6">
              <input  value={moment(this.state.data_inicio).format('L')} id="inicio" type="text"/>
              <label htmlFor="inicio" className="active">Data de Início:</label>
            </div>
            <div className="col s6">
              <div className="row">
                <div className="col s12 m3 l3">
                  <span style={{"fontSize" : "0.8rem"}}>Grupo: </span>
                </div>
                <div className="input-field">
                  <div className="col s6 m4 l4">
                    <input name="group1" type="radio" id="rh" disabled="disabled" checked ={this.state.perfil === "RH" ? true : false}/>
                    <label htmlFor="rh">RH</label>
                  </div>
                  <div className="col s6 m4 l4">
                    <input name="group1" type="radio" id="funcionario" disabled="disabled" checked ={this.state.perfil === "Funcionario" ? true : false}/>
                    <label htmlFor="funcionario">Funcionário</label>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      )
    }
  },

  segundaParteForms: function(){
    var perfil = localStorage.getItem("perfil");
    var imagemPreview = <img src= {this.state.urlImage} id="img_url"  className="center-block responsive-img"/>
    if(perfil == "Funcionario"){
      return(
        <div>
          <div className="row">
            <div className="input-field col s12 m4 l4">
              <input id="facebook"
                type="text"
                onChange={this.handleFacebookChanges}
                value={this.state.facebook}/>
              <label htmlFor="facebook" className={this.state.facebook ? "active" : null}><b>Facebook:</b></label>
            </div>
            <div className="input-field col s12 m4 l4">
              <input id="linkedin"
                type="text"
                onChange={this.handleLikedinChanges}
                value={this.state.linkedin}/>
              <label htmlFor="linkedin" className={this.state.linkedin ? "active" : null}><b>Linkedin:</b></label>
            </div>
            <div className="input-field col s12 m4 l4">
              <input id="github"
                type="text"
                onChange={this.handleGithubChanges}
                value={this.state.github}/>
              <label htmlFor="github" className={this.state.github ? "active" : null}><b>GitHub:</b></label>
            </div>
          </div>
          <div className="row">
            <div className="input-field col s12">
              <textarea id="bio"
                className="materialize-textarea"
                onChange={this.handleDescricaoChanges}
                value={this.state.descricao}>
              </textarea>
              <label htmlFor="bio" className={this.state.descricao ? "active" : null}><b>Bio:</b></label>
            </div>
          </div>
          <div className="row">
            <form action="#" className="col l10 m10 s12">
              <div className="file-field input-field">
                <div className="btn">
                  <span>Avatar</span>
                  <input type="file"
                    accept="image/gif, image/jpeg, image/png"
                    id = "img"
                    onChange={this.handleFotoChanges}/>
                </div>
                <div className="file-path-wrapper">
                  <input className="file-path"
                    type="text"
                    accept="image/gif, image/jpeg, image/png"
                    id = "imagem"
                    onChange={this.handleFotoChanges}
                    value={this.state.foto}
                    />
                </div>
              </div>
            </form>
            <div className="col l2 m2 s12">
              {this.state.urlImage ? imagemPreview : null}
            </div>
          </div>
          <div id="cropImage" className="modal modal-fixed-footer">
            <div className="modal-content">
              <h3 className="colorText-default center-align"><b>Editar Imagem</b></h3>
              <div className="row center">
                <div className="hide-on-med-and-down">
                  <AvatarEditor
                    ref="editor"
                    image={this.state.urlImage}
                    width={250}
                    height={250}
                    border={50}
                    color={[255, 255, 255, 0.6]}
                    scale={1} />
                </div>
                <div className="hide-on-large-only">
                  <AvatarEditor
                    ref="editor"
                    image={this.state.urlImage}
                    width={150}
                    height={150}
                    border={50}
                    color={[255, 255, 255, 0.6]}
                    scale={1} />
                </div>
              </div>
            </div>
            <div className="modal-footer">
              <a className="modal-action modal-close waves-effect waves-red btn-flat" onClick={this.onClickSave}>Cortar</a>
            </div>
          </div>
        </div>
      );
    }else{
      return(
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

  },

  render: function(){

    return(
      <EdicaoFuncionario
        nome = {this.state.nome}
        handleSubmit = {this.handleSubmit}
        handleClear = {this.handleClear}>
        <div>
          {this.primeiraParteForms()}
          {this.segundaParteForms()}
        </div>
      </EdicaoFuncionario>
    )


  }

});

module.exports = EdicaoFuncionarioContainer;
