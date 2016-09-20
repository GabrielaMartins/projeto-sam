'use strict'

//libs
var React = require('react');
var ReactRouter = require('react-router');
var Link = ReactRouter.Link;

//momentjs
var moment = require('moment');
moment.locale('pt-br');

var Avatar = React.createClass({
  render: function(){
    console.log(this.props.usuario.DataUltimaPromocao);
    return(
      <div>
        <div className="center-block">
          <img className="responsive-img circle center-block scrollreveal" src={this.props.usuario.foto} style={{height:200}}/>
        </div>
        <br/>
      <h4 className="center-align colorText-default scrollreveal"><b>{this.props.usuario.nome}</b></h4><br/>
        <div className="row scrollreveal">
          <div className="col l6 m6 s6">
            <span className="right"><b>Level: </b> {this.props.usuario.Cargo.nome}</span>
          </div>
          <div className="col l6 m6 s6">
            <span className="left"><b>Próximo level: </b> {this.props.usuario.ProximoCargo[0].nome}</span>
          </div><br/><br/>
          <div className="col l6 m6 s6">
              <span className="right"><b>{this.props.usuario.pontos}</b>/{this.props.usuario.ProximoCargo[0].pontuacao}</span><br/>
              <div className="progress col s10 m8 l5 right">
                <div className="determinate" style={{width: this.props.progresso + "%"}}></div>
              </div>
              <div className="col s12">
                <span className="right pequena"><b>Próxima avaliação:</b> {moment(this.props.usuario.DataUltimaPromocao).add(1, 'y').format('L')}</span>
              </div>
          </div>
          <div className="col l6 m6 s6">
            <br/>
            <span className="left"><b>Tempo de casa: </b> {this.props.tempoDeCasa}</span>
          </div>

        </div>
        <br/>
        <div className="row scrollreveal">
          <p className="center-align container"><b>Bio: </b>{this.props.usuario.descricao}</p>
        </div>
        <br/>
        <div className="row center-align scrollreveal">
          {this.props.usuario.facebook != undefined ? <a href = {"http://" + this.props.usuario.facebook}><i className="fa fa-facebook-square fa-3x colorText-default"></i></a> : null}
          {this.props.usuario.linkedin != undefined ? <a href = {"http://" + this.props.usuario.linkedin}><i className="fa fa-linkedin-square fa-3x colorText-default"></i></a> : null}
          {this.props.usuario.github != undefined ?<a href = {"http://" + this.props.usuario.github}><i className="fa fa-github-square fa-3x colorText-default"></i></a> : null}
        </div>
      </div>
    );
  }
});

Avatar.propTypes = {
  usuario: React.PropTypes.object.isRequired,
  progresso: React.PropTypes.number.isRequired
}

module.exports = Avatar;
