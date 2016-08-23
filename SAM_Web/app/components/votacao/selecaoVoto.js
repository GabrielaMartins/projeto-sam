var React = require('react');
var ReactRouter = require('react-router');

var SelecaoVoto = function(props){
    var options = [];
    console.log(props.mensagemErro);
    return(
      <div className="card-panel" id="cardVotacao">
        <h5 className="card-title center-align colorText-default" ><b>{props.titulo}</b></h5>
        <div className="row">
          <div className="input-field col l6 s12 m6">
            <select id="Select1" value={props.dificuldade} onChange={props.changeDificuldade}>
              <option value="" disabled>Escolha uma opção</option>
              <option value="1">Fácil</option>
              <option value="3">Médio</option>
              <option value="8">Difícil</option>
            </select>
            <label>Dificuldade</label>
          </div>
          <div className="input-field col l6 s12 m6">
            <select id="Select2" value={props.profundidade} onChange={props.changeProfundidade}>
              <option value="" disabled>Escolha uma opção</option>
              <option value="2">Raso</option>
              <option value="3">Profundo</option>
            </select>
            <label>Profundidade</label>
          </div>
          <p className="pequena red-text center">{props.mensagemErro}</p>
        </div>
        <div className="row">
          <h5 className="center-align">Pontuação final: <b className="colorText-default">{props.pontuacaoGerada}</b></h5><br/>
          <div className="col s8 offset-s2">
            <a className="waves-effect color-default waves-light btn left col l12 m12 s12" onClick={props.submit}>{props.botao}</a>
          </div>
        </div>
      </div>
    );
}

module.exports = SelecaoVoto;
