var React = require('react');
var ReactRouter = require('react-router');
var Router = ReactRouter.Router;
var Route = ReactRouter.Route;
var IndexRoute = ReactRouter.IndexRoute;
var HashHistory = ReactRouter.hashHistory;
var Main = require('../components/main');
var Login = require('../containers/login/loginContainer');
var Base = require('../containers/shared/baseContainer');
var DashboardRH = require('../containers/dashboard/dashboardContainerRH');
var DashboardFuncionario = require('../containers/dashboard/dashboardContainerFuncionario');
var Votacao = require('../containers/votacao/votacaoContainer');
var CadastroItem = require('../containers/cadastro_itens/cadastroItemContainer');
var ListaUsuarios = require('../containers/usuario/listaUsuariosContainer');
var ListaItens = require('../containers/item/listaItensContainer');
var Perfil = require('../containers/perfil/perfilContainer');
var Atividade = require('../containers/atividade/atividadeContainer');
var Erro = require('../components/shared/PaginaErro');
var Erro404 = require('../components/shared/pagina404');
var EdicaoFuncionario = require('../containers/edicao_funcionario/edicaoFuncionarioContainer');
var EdicaoItem = require('../containers/edicao_item/edicaoItemContainer');
var Pontuacao = require('../components/pontuacao/pontuacao');

var Routes = (
  <Router history={HashHistory}>
      <Route path='/' component={Login}/>
      <Route component={Base}>
        <Route path='/Dashboard/RH/:samaccount' component={DashboardRH}/>
        <Route path='/Dashboard/Funcionario/:samaccount' component={DashboardFuncionario}/>
        <Route path='Item/Atividade' component={Atividade}/>
        <Route path="/Item/Cadastro" component={CadastroItem}/>
        <Route path="/Item/Edicao/:id" component={EdicaoItem}/>
        <Route path='/Votacao/:id' component={Votacao}/>
        <Route path='/Funcionario/Listagem' component={ListaUsuarios}/>
        <Route path='/Funcionario/Edicao/:samaccount' component={EdicaoFuncionario}/>
        <Route path='/Item/Listagem' component={ListaItens}/>
        <Route path='/Perfil/:samaccount(/:historico)' component={Perfil}/>
        <Route path='/Pontuacao' component={Pontuacao}/>
      </Route>
      <Route path='/Erro/:status' component={Erro}/>
      <Route path='*' component={Erro404}/>
  </Router>
)

module.exports = Routes;
