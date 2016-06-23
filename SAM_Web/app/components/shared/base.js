var React = require('react');
var ReactRouter = require('react-router');
var Dropdown = require('./itensDropdown');
var ItensMenu = require('./itensMenu');
var Link = ReactRouter.Link;


var Base = function(props){
    var dropdowns = [];
    var itensMenu = [];

    props.dropdowns.forEach(function(dropdown){
      dropdowns.push(<Dropdown key = {dropdown.id} itensDrop = {dropdown.itens} itemMenu = {dropdown.itemMenu}/>);
      //se for diferente do nome da pessoa, cria menu (como exemplo estou usando o meu nome) se não cria menu do usuário
      if(dropdown.itemMenu != "Gabriela"){
        itensMenu.push(dropdown.itemMenu);
      }else{
         this.menuUsuario = dropdown.itemMenu;
         this.imagemUsuario = dropdown.imagem;
      }
    });

    return(
      <div>
        <header>
          {dropdowns}
          <div className="navbar-fixed">
            <nav className="nav-wrapper color-default row">
              <div className="col s10 offset-s1">
                <Link to="/Dashboard/mario" className="brand-logo"><img src="./app/imagens/logo-sam.png" style={{height:60}} /></Link>
                <a data-activates="slide-out" className="button-collapse"><i className="material-icons">menu</i></a>
                <ul className="right hide-on-med-and-down"><li><a className="dropdown-button" data-beloworigin="true"
                  href="#!" data-activates={this.menuUsuario}>Olá, {this.menuUsuario}<i className="material-icons right">arrow_drop_down</i></a></li></ul>
                <img className="responsive-img right circle hide-on-med-and-down" src={this.imagemUsuario} style={{height:40, marginTop:12, marginLeft:100}}/>
              <ItensMenu itensMenu = {itensMenu} usuario={this.menuUsuario} imagemUsuario={this.imagemUsuario} dropdowns={props.dropdowns}/>
              </div>
            </nav>
          </div>
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
