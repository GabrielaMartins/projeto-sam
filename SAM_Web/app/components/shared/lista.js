var React = require('react');
var ReactRouter = require('react-router');
var UsuarioCard = require('../usuario/usuarioCard');
var ItemCard = require('../../containers/item/itemCardContainer');

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
      lista.push(<div className="col l4 m6 s12"><ItemCard item = {item}/></div>)
    });
    placeholder = "Procure por Itens e Categorias";
  }

  return(
    <div id="lista">
      <div className="row" id="campoBusca" style={{"marginLeft":"5%", "marginRight":"5%", "marginBottom":"0px"}}>
          <div className="card wrapper">
            <i className="material-icons colorText-default right" >search</i>
            <input id="search" placeholder={placeholder} className="colorText-default pesquisar" />
          </div>
        </div>
        <div className="row" style={{"marginLeft":"5%", "marginRight":"5%"}}>
          {lista}
      </div>
    </div>
  );
}

module.exports = Lista;
