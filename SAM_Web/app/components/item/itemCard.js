var React = require('react');
var ReactRouter = require('react-router');
var Modal = require('./modalItem');
var moment = require('moment');
moment.locale('pt-br');
var Link = ReactRouter.Link;

var Card = React.createClass({
  componentDidMount: function(){
    $(document).ready(function() {
      $('.modal-trigger').leanModal();
    });
  },

  render: function(){
    var data = null;
    var acoes = null;

    if(this.props.perfil == true){
      data = <p className="center-align pequena">{moment(this.props.conteudo.data).format('L')}</p>
      acoes = <div className="col l4 m12 s12 center-block"><button data-target={this.props.conteudo.id} className="modal-trigger col l12 m8 s8 offset-m2 offset-s2 waves-effect waves-light yellow darken-3 btn">Ver</button></div>
    }else{
      acoes = <div>
                <div className="col l4 m12 s12"><button data-target={this.props.conteudo.id} className="modal-trigger col l12 m8 s8 offset-m2 offset-s2 waves-effect waves-light yellow darken-3 btn">Ver</button><br/><br/></div>
                <div className="col l4 m12 s12"><button className="col l12 m8 s8 offset-m2 offset-s2 waves-effect waves-light green darken-3 btn">Editar</button><br/><br/></div>
                <div className="col l4 m12 s12"><button className="col l12 m8 s8 offset-m2 offset-s2 waves-effect waves-light red darken-3 btn">Deletar</button><br/><br/></div>
              </div>
    }
    return(
        <div>
          <div className="scrollreveal card">
            <div className="card-content">
              <h5 className="card-title center-align"><b>{this.props.conteudo.nome}</b></h5><br/>
              <h2 className="center-align"><b>{this.props.conteudo.pontos}</b></h2><br/>
              <h5 className="center-align">{this.props.conteudo.categoria}</h5>
              {data}
            </div>
            <div class="card-action">
              <div className="row">
                {acoes}
              </div>
            </div>
          </div>
          <Modal conteudo = {this.props.conteudo}/>
        </div>

    );
  }
});

module.exports = Card;
