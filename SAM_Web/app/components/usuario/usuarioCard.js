var React = require('react');
var ReactRouter = require('react-router');
var Usuario = require('../usuario/usuario');

var Card = React.createClass({
  render: function(){
    return(
      <div className="scrollreveal card hoverable">
        <div className="card-image waves-effect waves-block waves-light">
          <img className="activator" src={this.props.conteudo.imagem} style={{height:300}}/>
        </div>
        <div className="card-content center">
          <span className="card-title activator colorText-default"><b>{this.props.conteudo.nome}</b><i className="material-icons right">more_vert</i></span>
        </div>
        <div className="card-action">
          <div className="row">
            <div className="col l4 m12 s12"><a className="col l12 m8 s8 offset-m2 offset-s2 waves-effect waves-light yellow darken-3 btn">Perfil</a><br/><br/></div>
            <div className="col l4 m12 s12"><a className="col l12 m8 s8 offset-m2 offset-s2 waves-effect waves-light green darken-3 btn">Editar</a><br/><br/></div>
            <div className="col l4 m12 s12"><a className="col l12 m8 s8 offset-m2 offset-s2 waves-effect waves-light red darken-3 btn">Desativar</a><br/><br/></div>
          </div>
        </div>
        <div className="card-reveal">
          <div className="row">
            <span className="card-title colorText-default"><i className="material-icons right">close</i></span>
          </div>
          <div className="row">
            <Usuario conteudo = {this.props.conteudo}/>
          </div>
        </div>
    </div>
    );
  }
});

module.exports = Card;
