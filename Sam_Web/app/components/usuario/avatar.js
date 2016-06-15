'use strict'
var React = require('react');

var Avatar = function(props){
  debugger;

  var cargo_pontuacao;
  if(props.usuario.Cargo){
    cargo_pontuacao = props.usuario.Cargo.pontuacao
  }else{
    cargo_pontuacao = 0;
  }

  return (
    <div className="wrapper" style={{paddingTop: '5%' }}>
      <div className = "row card">
        {/* aqui vai a foto */}
        <div className = "col l12 m12 s12">
          <div className = "wrapper">
            <img className = "responsive-img right circle hide-on-med-and-down"
                 src={props.usuario.foto}
                 style={{height:300}}
            />
          </div>
        </div>
        {/* aqui vai o nome */}
        <div className = "col l12 m12 s12">
          <div className = "wrapper">
            <label>{props.usuario.nome}</label>
          </div>
        </div>
        {/* aqui vai a barra de progresso */}
        <div className = "col l2 m12 s12">
          <div className = "row">
            <div className = "col l6 m6 s6">
              <div className = "wrapper">
                <span><b>{props.usuario.pontos}</b>/{cargo_pontuacao}</span>
                <div className="progress">
                  <div className="determinate" style={{width: 70 + "%"}}></div>
                </div>
              </div>
            </div>
            <div className = "col l6 m6 s6">
              <div className = "wrapper">
                <label>Tempo de casa: {props.usuario.dataInicio}</label>
              </div>
            </div>
          </div>
        </div>
        {/* aqui vai a descricao */}
        <div className = "col l12 m12 s1">
          <label>{props.descricao}</label>
        </div>
      </div>
    </div>
  );
}

module.exports = Avatar;
