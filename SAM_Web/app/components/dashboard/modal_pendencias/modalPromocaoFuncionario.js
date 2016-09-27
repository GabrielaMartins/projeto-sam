'use strict'

//libs
var React = require('react');
var ReactRouter = require('react-router');
var Link = ReactRouter.Link;

var Modal = React.createClass({
  render: function(){
    return(
      <div id={this.props.index} className="modal modal-fixed-footer">
        <div className="modal-content scrollbar">
            <h2 className="colorText-default center-align"><b>Parabéns!!!</b></h2>
            <br/>
            <div className="row center-block">
              <img className="responsive-img center-block" src="./app/imagens/baloes.png" style={{height:300}}/>
            </div>
            <br/>
        </div>
        <div className="modal-footer">
          <a className="modal-action modal-close waves-effect waves-red btn-flat" onClick={this.props.handleDeleteAlerta.bind(null, this.props.index)}>Fechar</a>
        </div>
    </div>
    );
  }
});

Modal.propTypes = {
  handleDeleteAlerta: React.PropTypes.func.isRequired,
  index: React.PropTypes.number.isRequired
}

module.exports = Modal;
