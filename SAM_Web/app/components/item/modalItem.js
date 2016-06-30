var React = require('react');
var ReactRouter = require('react-router');
var Link = ReactRouter.Link;

var Modal = React.createClass({
  render: function(){
    var status = null;
    /*var categoriasProfundidade = ["Blog", "Workshop", "Palestra"];
    var categoriasAlinhado = ["Curso", "Certificação", "Repositório"];
    var funcionarios = [];

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

    this.props.usuarios.forEach(function(usuario){
      funcionarios.push(
        <div className="col l4 m4 s6 wrapper">
          <Link to="#">
            <img className="responsive-img circle" src={usuario.foto} style={{height:50}}/>
            <br/>
            <p className="center-align"><b>{usuario.nome}</b></p>
              <br/>
              <br/>
          </Link>
        </div>
      );
    });*/

    return(
      <div id={this.props.item.id} className="modal modal-fixed-footer">
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
                <div className="col l4 m4 s6 wrapper">
                  <Link to={"/Perfil/"+ this.props.usuarios.samaccount}>
                    <img className="responsive-img circle center-block" src={this.props.usuarios.foto} style={{height:50}}/>
                    <p className="center-align"><b>{this.props.usuarios.nome}</b></p>
                      <br/>
                      <br/>
                  </Link>
                </div>
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
