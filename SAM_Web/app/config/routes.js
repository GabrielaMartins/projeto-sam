var React = require('react');
var ReactRouter = require('react-router');
var Router = ReactRouter.Router;
var Route = ReactRouter.Route;
var IndexRoute = ReactRouter.IndexRoute;
var HashHistory = ReactRouter.hashHistory;
var Main = require('../components/main');
var Login = require('../containers/login/loginContainer');
var Base = require('../containers/shared/baseContainer');
var Dashboard = require('../containers/dashboard/dashboardContainer');
var Votacao = require('../containers/votacao/votacaoContainer');
var CadastroItem = require('../containers/cadastro_itens/cadastroItemContainer');
var ListaUsuarios = require('../containers/usuario/listaUsuariosContainer');
var ListaItens = require('../containers/item/listaItensContainer');
var Perfil = require('../containers/perfil/perfilUsuarioContainer');

var Routes = (
  <Router history={HashHistory}>
    <Route path='/' component={Login}/>
      <Route component={Base}>
        <Route path='/Dashboard/:samaccount' component={Dashboard}/>
        <Route path="/Item/Cadastro" component={CadastroItem}/>
        <Route path='/Votacao/:id' component={Votacao}/>
        <Route path='/Funcionario/Listagem' component={ListaUsuarios}/>
        <Route path='/Item/Listagem' component={ListaItens}/>
        <Route path='/Perfil/:samaccount' component={Perfil}/>
      </Route>
  </Router>
)

module.exports = Routes;
