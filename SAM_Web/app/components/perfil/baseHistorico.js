var React = require('react');
var ReactRouter = require('react-router');


var BaseHistorico = React.createClass({
  render: function(){
    return(
      <div className="card-panel">
        <h4 className="card-title extraGrande colorText-default center-align"><b>Atividades</b></h4>
        <div className="card wrapper">
          <i className="material-icons colorText-default right" >search</i>
          <input id="search" placeholder={this.props.placeholder} className="colorText-default" />
        </div>
        <div className="row">
          {this.props.children}
        </div>
      </div>
    );
  }
});

module.exports = BaseHistorico;
