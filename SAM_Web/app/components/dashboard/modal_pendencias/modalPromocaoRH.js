'use strict'

//libs
var React = require('react');
var ReactRouter = require('react-router');
var Config = require('Config');
var Link = ReactRouter.Link;
var axios = require("axios");

//momentjs
var moment = require('moment');
moment.locale('pt-br');

var Modal = React.createClass({

  getInitialState: function() {
    return {
      cargoSelecionado: "-1",
      cargos:[],
      mensagemErro: ""
    };
  },

  componentDidUpdate: function(prevProps, prevState){
    var self = this;

    $(document).ready(function(){
      $('select').material_select();
      $("#select_cargo").on('change', self.handleChangeCargo);

    });
  },

  componentDidMount: function(){
      var self = this;
      axios.get(Config.serverUrl + "/api/sam/role/all/" + this.props.atividade.Evento.Usuario.samaccount).then(
        function(response){
          self.setState({
            cargos: response.data
          });
        }
      );
  },

  handleChangeCargo: function(event){
    this.setState({
      cargoSelecionado : event.target.value
    });
  },

  promove: function(){
    //configurações para passar o token
    var token = localStorage.getItem("token");

    var config = {
      headers: {'token': token}
    };



    var self = this;

    if(this.state.cargoSelecionado != "-1"){

      var promocao = {
        Usuario: this.props.atividade.Evento.Usuario.samaccount,
        Evento: this.props.atividade.Evento.id,
        Cargo: this.state.cargoSelecionado,
        PodePromover: true
      }

      axios.post(Config.serverUrl + "/api/sam/activity/promotion/approve", promocao, config).then(
        function(response){
          swal({
            title: "Promovido!",
            text: "O funcionário " + self.props.atividade.Evento.Usuario.nome + " foi promovido." ,
            type: "success",
            confirmButtonText: "Ok",
            confirmButtonColor: "#550000"
          },function(){
            self.props.handleDeleteAlerta(self.props.index);
          });
        },
        function(){
          swal({
            title: "Algum Erro Ocorreu!",
            text: "A promoção do funcionário " + self.props.atividade.Evento.Usuario.nome + " não pode ser confirmada no momento, tente novamente mais tarde.",
            type: "error",
            confirmButtonText: "Ok",
            confirmButtonColor: "#550000"
          });
        }
      );

      //fecha o modal
      $('#' + this.props.index).closeModal();

    }else{
      this.setState({
        mensagemErro: "Por favor, selecione o cargo antes de clicar em promover!"
      });
    }
  },
  naoPromove: function(){
    //configurações para passar o token
    var token = localStorage.getItem("token");

    var config = {
      headers: {'token': token}
    };

    var promocao = {
      Usuario: this.props.atividade.Evento.Usuario.samaccount,
      Evento: this.props.atividade.Evento.id,
      Cargo: this.state.cargoSelecionado,
      PodePromover: false
    }

    var self = this;

    swal({
      title: "Tem certeza?",
      text: "Tem certeza que não deseja promover o funcionário "  + self.props.atividade.Evento.Usuario.nome + "?",
      type: "warning",
      confirmButtonText: "Sim",
      showCancelButton: true,
      cancelButtonText: "Cancelar",
      confirmButtonColor: "#550000"
    },function(){
      axios.post(Config.serverUrl + "/api/sam/activity/promotion/approve", promocao, config).then(
        function(response){
          self.props.handleDeleteAlerta(self.props.index);
        },
        function(){
          swal({
            title: "Algum Erro Ocorreu!",
            text: "A promoção do funcionário " + self.props.atividade.Evento.Usuario.nome + " não pode ser confirmada no momento, tente novamente mais tarde." ,
            type: "error",
            confirmButtonText: "Ok",
            confirmButtonColor: "#550000"
          });
        }
      );
    });
  },
  render: function(){
    var optionsCargos=[];

    this.state.cargos.forEach(function(cargo, index){
      optionsCargos.push( <option key = {index} value={cargo.id}>{cargo.nome}</option>);
    });

    return(
      <div id={this.props.index} className="modal modal-fixed-footer">
        <div className="modal-content scrollbar">
            <h2 className="colorText-default center-align"><b>Promoção</b></h2>
            <br/>
            <div className="row center-align">
              <h5>O funcionário <b className="colorText-default">{this.props.usuario.nome}</b> atingiu pontos suficientes para a promoção no dia <b>{moment(this.props.atividade.Evento.data).format('L')}</b>.</h5>
              <br/><br/>
              <h4 className="colorText-default">Deseja promover o usuário?</h4>
              <div className="row">
                <div className="input-field col s12 m8 offset-m2 l6 offset-l3">
                  <select id ="select_cargo" value={this.state.cargoSelecionado} onChange={this.handleChangeCargo}>
                    <option value="-1" disabled>Escolha uma opção</option>
                    {optionsCargos}
                  </select>
                  <label>Selecione o Cargo</label>
                  <span className="pequena red-text">{this.state.mensagemErro}</span>
                </div>
              </div>

            </div>
            <br/>
        </div>
        <div className="modal-footer">
          <a className="modal-action waves-effect waves-green btn-flat" onClick={this.promove}>Promover</a>
          <a className="modal-action modal-close waves-effect waves-red btn-flat" onClick={this.naoPromove}>Não Promover</a>
        </div>
    </div>
    );
  }
});

Modal.propTypes = {
  atividade: React.PropTypes.object.isRequired,
  usuario: React.PropTypes.object.isRequired,
  handleDeleteAlerta: React.PropTypes.func.isRequired,
  index: React.PropTypes.number.isRequired
}

module.exports = Modal;
