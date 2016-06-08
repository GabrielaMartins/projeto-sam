var React = require('react');
var ReactRouter = require('react-router');
var Link = ReactRouter.Link;

var Modal = React.createClass({
  render: function(){
    var status = null;
    var categoriasProfundidade = ["Blog", "Workshop", "Palestra"];
    var categoriasAlinhado = ["Curso", "Certificação", "Repositório"];
    var funcionarios = [];

    if(categoriasProfundidade.indexOf(this.props.conteudo.categoria)){
      if(this.props.conteudo.status == true){
        status = <p className="center col s12 l4"><b>Profundidade: </b> Profundo</p>
      }else{
        status = <p className="center col s12 l4"><b>Profundidade: </b> Raso</p>
      }
    }else if(categoriasAlinhado.indexOf(this.props.conteudo.categoria)){
      if(this.props.conteudo.status == true){
        status = <p className="center col s12 l4"><b>Alinhado: </b> Sim</p>
      }else{
        status = <p className="center col s12 l4"><b>Alinhado: </b> Não</p>
      }
    }

    this.props.conteudo.feitoPor.forEach(function(funcionario){
      funcionarios.push(
        <div className="col l4 m4 s6 wrapper">
          <Link to="#">
            <img className="responsive-img circle" src={funcionario.imagem} style={{height:50}}/>
            <br/>
            <p className="center-align"><b>{funcionario.nome}</b></p>
              <br/>
              <br/>
          </Link>
        </div>
      );
    });

    return(
      <div id={this.props.conteudo.id} className="modal modal-fixed-footer">
        <div className="modal-content">
          <h3 className="colorText-default center-align"><b>{this.props.conteudo.nome}</b></h3><br/>
            <div className="row">
                <p className="center col s12 l3 offset-l3"><b>Categoria: </b> {this.props.conteudo.categoria}</p>
                <p className="center col s12 l4"><b>Pontuação: </b> {this.props.conteudo.pontos}</p>
                <p className="center col s12 l3 offset-l3"><b>Dificuldade: </b> {this.props.conteudo.dificuldade}</p>
                {status}
            </div>
            <br/>
            <div className="row center-block">
              <h5 className="center-align colorText-default"><b>Quem já fez:</b></h5><br/><br/>
              {funcionarios}
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
