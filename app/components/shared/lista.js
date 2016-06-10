var React = require('react');
var ReactRouter = require('react-router');
var UsuarioCard = require('../usuario/usuarioCard');
var ItemCard = require('../item/itemCard');;

var Lista = function(props){
    var lista = [];
    var placeholder = "";

  if(props.usuarios){
    props.usuarios.forEach(function(usuario){
      lista.push(<div className="col l4 m6 s12"><UsuarioCard conteudo = {usuario}/></div>)
    });
    placeholder = "Procure por Funcionários";
  }else{
    props.itens.forEach(function(item){
      lista.push(<div className="col l4 m6 s12"><ItemCard conteudo = {item}/></div>)
    });
    placeholder = "Procure por Itens e Categorias";
  }

  return(
    <div className="row" style={{"marginLeft":"5%", "marginRight":"5%"}}>
      <div className="card col l12 m12 s12 wrapper">
        <input id="search" placeholder={placeholder} className="colorText-default"/><i className="material-icons colorText-default">search</i>
      </div>
        {lista}
    </div>
  );
}

module.exports = Lista;
