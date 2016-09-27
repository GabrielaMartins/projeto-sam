'use strict'

//libs
var React = require('react');

var Modal = React.createClass({
  render: function(){
    var texto_atribuicao;
    if(this.props.atividade.Estado === false){
      texto_atribuicao = "Seus pontos referentes à atividade " + this.props.atividade.Evento.Item.nome + " ainda não foram atribuídos. Aguarde o posicionamento do RH"
    }else{
      if(this.props.atividade.Evento.estado == true){
        texto_atribuicao = "Seus pontos referentes à atividade " + this.props.atividade.Evento.Item.nome + " foram atribuídos pelo RH. Verifique sua nova pontuação!"
      }else{
        texto_atribuicao = "Seus pontos referentes à atividade " + this.props.atividade.Evento.Item.nome + " não foram aceitos pelo RH."
      }
    }

    return(
      <div id={this.props.index} className="modal modal-fixed-footer">
        <div className="modal-content scrollbar">
            <h2 className="colorText-default center-align"><b>Atribuição de Pontos</b></h2>
            <br/>
            <div className="row">
              <p className="center-align grande">{texto_atribuicao}</p>
            </div>
            <br/>
        </div>
        <div className="modal-footer">
          <a className="modal-action modal-close waves-effect waves-red btn-flat" onClick={this.props.atividade.Estado === true ? this.props.handleDeleteAlerta.bind(null, this.props.index) : null}>Ok</a>
        </div>
    </div>
    );
  }
});

Modal.propTypes = {
  atividade: React.PropTypes.object.isRequired,
  handleDeleteAlerta: React.PropTypes.func.isRequired,
  index: React.PropTypes.number.isRequired
}

module.exports = Modal;
