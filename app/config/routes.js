var React = require('react');
var ReactRouter = require('react-router');
var Router = ReactRouter.Router;
var Route = ReactRouter.Route;
var IndexRoute = ReactRouter.IndexRoute;
var HashHistory = ReactRouter.hashHistory;
var Main = require('../components/main');
var Login = require('../containers/loginContainer');
var Base = require('../containers/shared/baseContainer');
var Dashboard = require('../containers/dashboard/dashboardContainer');
var Votacao = require('../containers/votacao/votacaoContainer');


var Routes = (
  <Router history={HashHistory}>
    <Route path='/' component={Login}/>
      <Route component={Base}>
        <Route path='/Dashboard' component={Dashboard}/>
        <Route path="Votacao/:id" component={Votacao}/>
      </Route>
  </Router>
)

module.exports = Routes;
