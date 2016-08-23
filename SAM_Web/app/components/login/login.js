'use strict'
var React =  require('react');
var ReactRouter = require('react-router');
var Link = ReactRouter.Link;

var Login = function(props){
    return(
      <main className="background-start">
        <div className="full-screen transparent-dark">
          <div className="row wrapper">
            <div className="col l3 m6 s10 center-block">
              <div className="card-panel z-depth-6 transparent-white">
                  <img className="center-block" src="./app/imagens/logo-sam.png" style={{height:150}}/>
                  <div className="row">
                    <div className="input-field">
                      <i className="material-icons prefix input-color">account_circle</i>
                      <input id="usuario"
                        type="text"
                        onChange = {props.updateUsuario}
                      />
                    <label htmlFor="nome">Usuário</label>
                    </div>
                    <div className="input-field">
                      <i className="material-icons prefix">lock</i>
                      <input id="senha"
                        type="password"
                        onChange = {props.updateSenha}
                      />
                    <label htmlFor="senha">Senha</label>
                    </div>
                  </div>
                  <div className="row">
                    <p className="center-align media red-text">{props.msg}</p>
                  </div>
                  <div className="row wrapper">
                    <button className="col s6 color-default center-block waves-effect waves-light btn" onClick={props.entrar}>Entrar</button>
                  </div>
              </div>
            </div>
          </div>
        </div>
      </main>
    );
}

Login.propTypes = {
   msg: React.PropTypes.string.isRequired,
   updateUsuario: React.PropTypes.func.isRequired,
   updateSenha: React.PropTypes.func.isRequired,
   entrar: React.PropTypes.func.isRequired
}

module.exports = Login;
