'use strict'

//libs
var React = require('react');
var ReactRouter = require('react-router');

var EdicaoFuncionario = function(props){
  return(
    <div style={{paddingTop: 30 }}>
      <div className="row wrapper">
        <div className="col l8 m10 s12 center-block">
          <div className="card-panel">
            <h3 className="colorText-default card-title center-align">Editar {props.nome}</h3>
            <br/>
            <div className="card-content">
              {props.children}
              <br/><br/>
              <div className="card-action">
                <div className="row wrapper">
                  <div className="col l12 m12 s12">
                    <div className="row">
                      <div className="col s6">
                        <a
                          className="color-default waves-effect waves-light btn right"
                          onClick = {props.handleClear}
                          name="btn_limpar">Limpar
                          <i className="fa fa-eraser right"></i>
                        </a>
                      </div>
                      <div className="col s6">
                        <button
                          className="color-default btn waves-effect waves-light left"
                          type="submit"
                          name="btn_enviar"
                          onClick = {props.handleSubmit}>Salvar
                          <i className="fa fa-floppy-o right"></i>
                        </button>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

EdicaoFuncionario.propTypes = {
  nome: React.PropTypes.string.isRequired,
  handleClear: React.PropTypes.func.isRequired,
  handleSubmit: React.PropTypes.func.isRequired,
  children: React.PropTypes.element.isRequired,
}

module.exports = EdicaoFuncionario;
