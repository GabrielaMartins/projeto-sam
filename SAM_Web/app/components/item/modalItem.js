'use strict'

//libs
var React = require('react');
var ReactRouter = require('react-router');
var Link = ReactRouter.Link;

var Modal = React.createClass({
  render: function(){
    var status = null;
    var categoriasProfundidade = ["Blog Técnico", "Apresentação"];

    //determina o nome do status que aparecerá na tela
    if(categoriasProfundidade.indexOf(this.props.item.Categoria.nome) != -1){
      if(this.props.item.status == true){
        status = <p className="left-align col l6"><b>Profundidade: </b> Profundo</p>
      }else{
        status = <p className="left-align col l6"><b>Profundidade: </b> Raso</p>
      }
    }else{
      if(this.props.item.status == true){
        status = <p className="left-align col l6"><b>Alinhado: </b> Sim</p>
      }else{
        status = <p className="left-align col l6"><b>Alinhado: </b> Não</p>
      }
    }

    return(
      <div id={this.props.index} className="modal modal-fixed-footer">
        <div className="modal-content scrollbar">
            <h3 className="colorText-default center-align"><b>{this.props.item.nome}</b></h3>
            <br/>
            <div className="row">
                <p className="right-align col l6"><b>Categoria: </b> {this.props.item.Categoria.nome}</p>
                <p className="left-align col l6"><b>Pontuação: </b> {this.props.pontuacao}</p>
                <p className="right-align col l6"><b>Dificuldade: </b> {this.props.item.dificuldade}</p>
                {status}
            </div>
            <br/>
            <div className="row center-block">
              <h5 className="center-align colorText-default"><b>Quem já fez:</b></h5><br/><br/>
                {this.props.usuarios}
            </div>
        </div>
        <div className="modal-footer">
          <a className="modal-action modal-close waves-effect waves-red btn-flat ">Fechar</a>
        </div>
    </div>
    );
  }
});

Modal.propTypes = {
  item: React.PropTypes.object.isRequired,
  pontuacao: React.PropTypes.number.isRequired,
  usuarios: React.PropTypes.arrayOf(React.PropTypes.element).isRequired
}


module.exports = Modal;
