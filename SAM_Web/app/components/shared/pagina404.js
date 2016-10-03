'use strict'

var React = require('react');
var ReactRouter = require('react-router');
var Link = ReactRouter.Link;

var PaginaErro = React.createClass({
  render : function(){
    var samaccount = localStorage.getItem("samaccount");
    var perfil = localStorage.getItem("perfil");
    return(
      <div className="background-404">
        <div className="full-screen transparent-dark">
          <div className="row wrapper">
            <div className="col s12">
              <h1 className="corMensagemErro center-align gigante">404</h1>
              <h3 className="corMensagemErro center-align">Você está perdido!</h3>
              <h5 className="corMensagemErro center-align">Este lugar é perigoso para crianças, <Link className="colorText-default" to={{ pathname: '/dashboard/' + perfil + '/' + samaccount}}><b>vá para casa</b>.</Link></h5>
            </div>
          </div>
        </div>
      </div>
    )
  }
});

module.exports = PaginaErro;
