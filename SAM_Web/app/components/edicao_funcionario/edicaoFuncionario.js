var React = require('react');
var ReactRouter = require('react-router');

var EdicaoFuncionario = function(props){
    return(
      <div style={{paddingTop: 30 }}>
        <div className="row wrapper">
          <div className="col l8 m8 s10 center-block">
            <div className="card-panel">
              <h3 className="colorText-default card-title center-align">Editar {props.nome}</h3>
              <br/>
              <div className="card-content container">
                {props.children}
                <br/><br/>
                <div className="card-action">
                  <div className="row">
                    <div className="col s6">
                      <a className="waves-effect waves-light btn right" onClick={props.handleClear}>Limpar <i className="fa fa-eraser right"></i></a>
                    </div>
                    <div className="col s6">
                      <a className="waves-effect waves-light btn left" onClick = {props.handleSubmit}>Salvar <i className="fa fa-floppy-o right"></i></a>
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


module.exports = EdicaoFuncionario;
