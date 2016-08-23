var React = require('react');
var ReactRouter = require('react-router');
var Link = ReactRouter.Link;

var Modal = React.createClass({
  render: function(){
    var status = null;
    var categoriasProfundidade = ["Blog", "Workshop", "Palestra"];
    var categoriasAlinhado = ["Curso", "Certificação", "Repositório"];

    if(categoriasProfundidade.indexOf(this.props.item.Categoria.nome)){
      if(this.props.item.status == true){
        status = <p className="center col s12 l4"><b>Profundidade: </b> Profundo</p>
      }else{
        status = <p className="center col s12 l4"><b>Profundidade: </b> Raso</p>
      }
    }else if(categoriasAlinhado.indexOf(this.props.item.Categoria.nome)){
      if(this.props.item.status == true){
        status = <p className="center col s12 l4"><b>Alinhado: </b> Sim</p>
      }else{
        status = <p className="center col s12 l4"><b>Alinhado: </b> Não</p>
      }
    }

    return(
      <div id={this.props.index} className="modal modal-fixed-footer">
        <div className="modal-content scrollbar">
            <h3 className="colorText-default center-align"><b>{this.props.item.nome}</b></h3>
            <br/>
            <div className="row">
                <p className="center col s12 l3 offset-l3"><b>Categoria: </b> {this.props.item.Categoria.nome}</p>
                <p className="center col s6 l4"><b>Pontuação: </b> {this.props.pontuacao}</p>
                <p className="center col s6 l3 offset-l3"><b>Dificuldade: </b> {this.props.item.dificuldade}</p>
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

module.exports = Modal;
