var React = require('react');
var ReactRouter = require('react-router');
var Dropdown = require('./itensDropdown');
var ItensMenu = require('./itensMenu');
var Link = ReactRouter.Link;

var Base = function(props){

  var dropdowns = [];
  var itensMenu = [];
  var urlDashboard = '/dashboard/' + props.perfil + '/' + props.samaccount;
  props.dropdowns.forEach(function(dropdown){
    dropdowns.push(<Dropdown key = {dropdown.id} itensDrop = {dropdown.itens} itemMenu = {dropdown.itemMenu} />);
    //se for diferente do nome da pessoa, cria menu, se não cria menu do usuário
    if(dropdown.itemMenu == "Itens" || dropdown.itemMenu == "Funcionarios"){
      itensMenu.push(dropdown.itemMenu);
    }else{
      if(dropdown.itemMenu != undefined){
        this.menuUsuario = dropdown.itemMenu;
        this.imagemUsuario = dropdown.imagem;
        this.url = dropdown.itens[0].url;
      }
    }
  });

  return(
    <div>
      <header>
        {dropdowns}
        <div className="navbar-fixed">
          <nav className="nav-wrapper color-default row">
            <div className="col s6">
              <ItensMenu itensMenu = {itensMenu} usuario={this.menuUsuario} imagemUsuario={this.imagemUsuario} urlUsuario = {this.url} dropdowns={props.dropdowns} logout = {props.logout}/>
              <Link to={urlDashboard} className="brand-logo center"><img src="./app/imagens/logo-sam.png" style={{height:60}} /></Link>
              <a data-activates="slide-out" className="button-collapse"><i className="material-icons">menu</i></a>
            </div>
            <div className="col s6">
              <a style={{'cursor':'pointer'}} onClick={props.logout} className="right hide-on-med-and-down">Logout <i className="fa fa-sign-out"></i></a>
              <ul className="hide-on-med-and-down right" style={{marginRight:100}}>
                <li className="right">
                  <Link to={this.url}>Olá, {this.menuUsuario}</Link>
                </li>
                <img className="responsive-img  circle hide-on-med-and-down" src={this.imagemUsuario} style={{height:40, marginTop:12}}/>
              </ul>
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
