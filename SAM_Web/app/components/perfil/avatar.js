var React = require('react');
var ReactRouter = require('react-router');

var Avatar = React.createClass({
  render: function(){
    return(
      <div>
        <div className="center-block">
          <img className="responsive-img circle center-block" src={this.props.foto} style={{height:200}}/>
        </div><br/>
      <h4 className="center-align grande colorText-default"><b>{this.props.nome}</b></h4><br/><br/>
        <div className="row">
          <div className="col l6 m6 s6">
            <span className="right"><b>Level: </b> {this.props.cargo}</span>
          </div>
          <div className="col l6 m6 s6">
              <span className="left"><b>Próximo level: </b> {this.props.prox_cargo}</span>
          </div>
        </div>
        <div className="row">
          <div className="col l6 m6 s6">
              <span className="col l4 right"><b>{this.props.pontos}</b>/{this.props.pontos_cargo}</span><br/>
              <div className="progress col l4 right">
                <div className="determinate" style={{width: 70 + "%"}}></div>
              </div>
          </div>
          <div className="col l6 m6 s6">
            <br/>
            <span className="left"><b>Tempo de casa: </b> {this.props.tempo_casa}</span>
          </div>
        </div>
        <br/>
        <div className="row">
          <p className="center-align container">{this.props.descricao}</p>
        </div>
        <br/>
        <div className="row center-align">
          <a><i className="material-icons colorText-default" style={{fontSize:50}}>movie</i>  </a>
          <a><i className="material-icons colorText-default" style={{fontSize:50}}>movie</i>  </a>
          <a><i className="material-icons colorText-default" style={{fontSize:50}}>movie</i>  </a>
        </div>
      </div>
    );
  }
});

module.exports = Avatar;
