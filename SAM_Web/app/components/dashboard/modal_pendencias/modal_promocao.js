var React = require('react');
var ReactRouter = require('react-router');
var Link = ReactRouter.Link;

var Modal = React.createClass({
  render: function(){

    return(
      <div id={this.props.item.id} className="modal modal-fixed-footer">
        <div className="modal-content scrollbar">
            <h2 className="colorText-default center-align"><b>Parabéns!!!</b></h2>
            <br/>
            <div className="row center-block">
              <img className="responsive-img center-block" src="./app/imagens/baloes.png" style={{height:200}}/>
            </div>
            <br/>
        </div>
        <div className="modal-footer">
          <a className="modal-action modal-close waves-effect waves-red btn-flat">Fechar</a>
        </div>
    </div>
    );
  }
});

module.exports = Modal;
