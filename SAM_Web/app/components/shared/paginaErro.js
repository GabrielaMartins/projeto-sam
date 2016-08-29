var React = require('react');
var ReactRouter = require('react-router');
var Link = ReactRouter.Link;

var PaginaErro = React.createClass({

  render : function(){
    return(
      <div className="background-erro">
        <div className="full-screen transparent-dark">
          <div className="row wrapper">
            <div className="col s12">
              <h1 className="corMensagemErro center-align gigante">Há algo errado!</h1>
              <h5 className="corMensagemErro center-align">{this.props.location.state.mensagem}</h5>
              <br/>
              <Link to={{ pathname: '/'}} ><h5 className="colorText-default center-align"><i className="fa fa-chevron-left fa-1x" aria-hidden="true"></i>
                <b> Voltar ao início</b></h5>
              </Link>
            </div>
          </div>
        </div>
      </div>
    )
  }
});

module.exports = PaginaErro;
