var React = require('react');
var ReactRouter = require('react-router');
var Link = ReactRouter.Link;

var CardEventos = React.createClass({
  render : function(){
    return(
      <div className="eventsCard row">
        <div className={this.props.estilo} style={{height:100}}>
          <div className="row wrapper">
            <div className="col s5 m4 l5">
              <div className="center">
                <img className="responsive-img circle" src={this.props.usuario.foto} style={{height:50}}/>
                <span className="truncate"><b>{this.props.usuario.nome}</b></span>
              </div>
            </div>
            <div className="col s7 m8 l7">
                {this.props.children}
            </div>
          </div>
        </div>
      </div>
    );
  }
});

module.exports = CardEventos;
