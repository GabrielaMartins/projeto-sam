var React = require('react');
var ReactRouter = require('react-router');

var SelecaoVoto = React.createClass({
  componentDidMount: function(){
    $(document).ready(function() {
    $('select').material_select();
  });
  },
  render: function(){
    return(
      <div className="card-panel">
        <h5 className="card-title center-align colorText-default" ><b>Definir Pontuação</b></h5>
        <div className="row">
          <div className="input-field col l6 s12 m6">
            <select>
              <option value="" disabled selected>Escolha uma opção</option>
              <option value="1">Fácil</option>
              <option value="2">Médio</option>
              <option value="3">Difícil</option>
            </select>
            <label>Dificuldade</label>
          </div>
          <div className="input-field col l6 s12 m6">
            <select>
              <option value="" disabled selected>Escolha uma opção</option>
              <option value="1">Raso</option>
              <option value="2">Profundo</option>
            </select>
            <label>Profundidade</label>
          </div>
        </div>
        <div className="row">
            <h5 className="center-align">Pontuação final: <b className="colorText-default">24</b></h5><br/>
          <div className="col l6 m6 s6">
              <a className="waves-effect color-default waves-light btn right col l12 m12 s12">Cancelar</a>
          </div>
          <div className="col l6 m6 s6">
            <a className="waves-effect color-default waves-light btn left col l12 m12 s12">Atribuir</a>
          </div>
        </div>
      </div>
    );
  }
});

module.exports = SelecaoVoto;
