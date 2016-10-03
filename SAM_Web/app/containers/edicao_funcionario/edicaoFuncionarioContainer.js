'use strict'

//libs
var React = require('react');
var ReactRouter = require('react-router');
var Config = require('Config');
var axios = require("axios");

//components
var EdicaoFuncionario = require('../../components/edicao_funcionario/edicaoFuncionario');
var Loading = require('react-loading');
import AvatarEditor from 'react-avatar-editor';
var Radio = require('../../ui_elements/radio');


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
      cargo_id: "0",
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
      grupo:"",
      rotulosRadio: ["Funcionário", "RH"],
    }
  },
  componentDidMount: function(){

    var self = this;
    var token = localStorage.getItem("token");

    //obtem samaccount do funcionário passado por parâmetro
    var usuario = this.props.params.samaccount;

    //config
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
          });

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

  componentDidUpdate: function(prevProps, prevState){
    var self = this;
    $(document).ready(function(){
      $('select').material_select();
      self.setupDatepicker();
      //verificar por que não deixa alterar o radio quando inicializa checado
      //$("input:radio").prop("checked", true);
    });

  },

  handleDataChanges: function(event){
    this.setState({data: event.target.value});
  },

  handleChangeCargo: function(event){
    this.setState({cargo_id: event.target.value});
  },

  handleChangePontos: function(event){
    this.setState({pontos: event.target.value});
  },

  handleFacebookChanges: function(event){
    this.setState({facebook: event.target.value});
  },

  handleLikedinChanges: function(event){
    this.setState({linkedin: event.target.value});
  },

  handleGithubChanges: function(event){
    this.setState({github: event.target.value});
  },

  handleDescricaoChanges: function(event){
    this.setState({descricao: event.target.value});
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

  handleGrupoChanges: function(event){
    this.setState({
      grupo : event.target.value
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
      dataInicio: moment(this.state.data_inicio).format('L'),
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
            self.context.router.push({pathname: rota});
            window.location.reload();
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
    //obtem perfil do usuário
    var perfil = localStorage.getItem("perfil");

    //obtem samaccount do usuario
    var samaccount = localStorage.getItem("samaccount");

    //obtem samaccount passado na rota
    var usuario = this.props.params.samaccount;

    if(perfil.toUpperCase() == "FUNCIONARIO"){
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
    }else{
     $('select').val(1);
     $('#pontos').val('');
     $('#inicio').val('');
     $("input:radio").prop("checked", false);

     this.setState({
       cargo_nome : "",
       cargo_id : 0,
       pontos : 0,
       data_inicio : "",
       grupo:"",
       checked: false
     });
    }

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
      },
      onStart: function() {
        this.set('select', moment(self.state.data_inicio).format('L'), {format:'dd-mm-yyyy'});
      }
    })
  },

  primeiraParteForms: function(){
    var perfil = localStorage.getItem("perfil");

    var cargos =[];
    this.state.lista_cargos.forEach(function(cargo, index){
      cargos.push( <option key = {index} value={cargo.id}>{cargo.nome}</option>);
    });

    if(perfil.toUpperCase() == "FUNCIONARIO"){
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
            <div className="col s12">
                <span><b>Nome:</b> {this.state.nome}<br/><br/></span>
            </div>
          </div>
          <div className="row">
            <div className="input-field col s12 m6 l6">
              <select value={this.state.cargo_id} onChange={this.handleChangeCargo}>
                {cargos}
              </select>
              <label>Level (Cargo):</label>
            </div>
            <div className="input-field col s12 m6 l6">
              <input value={this.state.pontos} onChange={this.handleChangePontos} id="pontos" type="text"/>
              <label htmlFor="pontos" className="active">XP (Pontos):</label>
            </div>
          </div>
          <div className="row">
            <div className="input-field col l6 m6 s12">
              <input
                type="date"
                id="inicio"
                className="datepicker"
                />
              <label htmlFor="inicio" className="active">Data de Início:</label>
            </div>
            <div className="col s12 l6">
              <div className="row">
                <div className="col s12 m2 l2">
                  <span style={{"fontSize" : "0.8rem"}}>Grupo: </span>
                </div>
                <div className="row">
                  <div className="col s6 m5 l5">
                    <Radio
                      name = "rdGroup"
                      id = "radio1"
                      label = {this.state.rotulosRadio[0]}
                      value = {this.state.rotulosRadio[0]}
                      onChange = {this.handleGrupoChanges}/>
                  </div>
                  <div className="col s6 m5 l5">
                    <Radio
                      name = "rdGroup"
                      id = "radio2"
                      label = {this.state.rotulosRadio[1]}
                      value = {this.state.rotulosRadio[1]}
                      onChange = {this.handleGrupoChanges}/>
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

    //obtem perfil do usuário
    var perfil = localStorage.getItem("perfil");

    //obtem samaccount do usuario
    var samaccount = localStorage.getItem("samaccount");

    //obtem samaccount passado na rota
    var usuario = this.props.params.samaccount;

    //cria visualização de imagem
    var imagemPreview = <img src= {this.state.urlImage} id="img_url"  className="center-block responsive-img" style={{"height": "100px"}}/>

    //se for funcionário ou os dados do próprio funcionário de rh, ele pode editar dados como facebook, avatar, etc
  if(perfil.toUpperCase() == "FUNCIONARIO" || (perfil.toUpperCase() == "RH" && samaccount == usuario )){

      return(
        <div>
          <div className="row">
            <div className="input-field col s12 m4 l4">
              <input id="facebook"
                type="text"
                onChange={this.handleFacebookChanges}
                value={this.state.facebook == null ? "" : this.state.facebook}/>
              <label htmlFor="facebook" className={this.state.facebook ? "active" : undefined}><b>Facebook:</b></label>
            </div>
            <div className="input-field col s12 m4 l4">
              <input id="linkedin"
                type="text"
                onChange={this.handleLikedinChanges}
                value={this.state.linkedin == null ? "" : this.state.linkedin}/>
              <label htmlFor="linkedin" className={this.state.linkedin ? "active" : undefined}><b>Linkedin:</b></label>
            </div>
            <div className="input-field col s12 m4 l4">
              <input id="github"
                type="text"
                onChange={this.handleGithubChanges}
                value={this.state.github == null ? "" : this.state.github}/>
              <label htmlFor="github" className={this.state.github  ? "active" : undefined}><b>GitHub:</b></label>
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
                <div className="btn color-default">
                  <span>Avatar</span>
                  <input type="file"
                    accept="image/gif, image/jpeg, image/png"
                    id = "img"
                    value = {this.state.foto}
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
              <p><b>Bio: </b> {this.state.descricao}</p>
            </div>
          </div>
          <div className="row">
            <div className="col s12">
              <span><b>Avatar: </b></span><br/><br/>
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
    //verifica se já buscou os cargos, enquanto isso, Loading..
    if(this.state.lista_cargos.length === 0){
      return (
        <div className="full-screen-less-nav">
          <div className="row wrapper">
            <Loading type='bubbles' color='#550000' height={150} width={150}/>
          </div>
        </div>
      );
    }

    //obtem perfil do usuário
    var perfil = localStorage.getItem("perfil");

    //obtem samaccount do usuario
    var samaccount = localStorage.getItem("samaccount");

    //usuario via parametro
    var usuario = this.props.params.samaccount;

    var id;

    if (perfil.toUpperCase() == "RH" && samaccount == usuario){
      id = "RH"
    }

    return(
      <EdicaoFuncionario
        nome = {this.state.nome}
        handleSubmit = {this.handleSubmit}
        handleClear = {this.handleClear}
        perfil = {id}>
        <div>
          {this.primeiraParteForms()}
          {this.segundaParteForms()}
        </div>
      </EdicaoFuncionario>
    );

  }

});

module.exports = EdicaoFuncionarioContainer;
