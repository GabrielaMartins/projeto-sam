var React = require('react');
var ReactRouter = require('react-router');
var Dropdown = require('./itensDropdown');
var ItensMenu = require('./itensMenu');
var Link = ReactRouter.Link;


var Base = function(props){
    //console.log(props.children);
    var dropdowns = [];
    var itensMenu = [];
    var menuUsuario = null;
    props.dropdowns.forEach(function(dropdown){
      dropdowns.push(<Dropdown key = {dropdown.id} itensDrop = {dropdown.itens} itemMenu = {dropdown.itemMenu}/>);
      //se for diferente do nome da pessoa, cria menu (como exemplo estou usando o meu nome) se não cria menu do usuário
      if(dropdown.itemMenu != "Gabriela"){
        itensMenu.push(dropdown.itemMenu);
      }else{
         menuUsuario = dropdown.itemMenu;
      }
    });

    return(
      <div>
        <header>
          {dropdowns}
          <nav className="nav-wrapper color-default z-depth-2">
            <div className="col s12">
              <Link to="/Dashboard" className="brand-logo"><img src="./app/imagens/logo-sam.png" style={{height:60}} /></Link>
                <ul className="right hide-on-med-and-down"><li><a className="dropdown-button" data-beloworigin="true"
                 href="#!" data-activates={menuUsuario}>Olá, {menuUsuario}<i className="material-icons right">arrow_drop_down</i></a></li></ul>
              <img className="responsive-img right circle hide-on-med-and-down" src="./app/imagens/fulano.jpg" style={{height:40, marginTop:12, marginLeft:100}}/>
            <ItensMenu itensMenu = {itensMenu}/>
            </div>
          </nav>
        </header>
        <main>
          {/*renderiza o conteúdo das rotas dentro da base*/}
          {props.children}
        </main>
        <footer className="page-footer">
          <div className="footer-copyright">
            <div className="container">
              © 2016 SAM - Opus Software
              <span className="right">Feito com <i className="material-icons red-text" style={{fontSize:12}}>favorite</i>, <a className="color-links" href="https://facebook.github.io/react/">React</a> e <a className="color-links" href="http://materializecss.com/">MaterializeCSS</a></span>
            </div>
          </div>
        </footer>
      </div>
    );
}

module.exports = Base;
