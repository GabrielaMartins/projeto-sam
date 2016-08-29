'use strict'
//libs
var React =  require('react');
var ReactRouter = require('react-router');
var Link = ReactRouter.Link;

//componente para Radio buttom
var Radio = require('../../ui_elements/radio');

var Agendamento = function(props){
  return(
    <form>
      <div style={{paddingTop: 30 }}>
        <div className="row wrapper">
          <div className="col l8 m10 s12 center-block">
            <div className="card z-depth-6">
              <div className="card-content">
                <h3 className="colorText-default card-title center-align"><b>Agendamento</b></h3>
                <div className="row">
                  <div className="input-field col l6 m6 s12">
                    <select
                      id ="select_categoria"
                      value = {props.categoria}
                      onChange = {props.handleCategoryChanges}>
                      <option
                        value = {0}
                        key = {0}
                        >Selecione a categoria
                      </option>
                      {props.categorias}
                    </select>
                    <label>Categoria</label>
                    <span className="pequena red-text">{props.erroCategoria}</span>
                  </div>
                  <div className="input-field col l6 m6 s12">
                    <select
                      id = "select_item"
                      value = {props.item}
                      onChange = {props.handleItemChanges}>
                      <option
                        value = {0}
                        key = {0}
                        >Selecione a atividade
                      </option>
                      {props.itens}
                    </select>
                    <label>Atividade</label>
                    <span className="pequena red-text">{props.erroItem}</span>
                  </div>
                </div>
                <div>
                  <div className="row">
                    <div className="input-field col l12 m12 s12">
                      <input
                        type="date"
                        id="data_atividade"
                        className="datepicker"
                        onChange = {props.handleDataChanges}
                        value = {props.data}
                        />
                      <label htmlFor="data_atividade">Data da Atividade</label>
                      <span className="pequena red-text">{props.erroData}</span>
                    </div>
                  </div>
                  <div className="row">
                    <div className="input-field col l12 m12 s12">
                      <textarea
                        id = "descricao_item"
                        className = "materialize-textarea"
                        onChange = {props.handleDescriptionChanges}
                        value = {props.descricao}
                        maxLength = "150"
                        />
                      <label htmlFor="descricao_item">Descrição</label>
                      <span className="pequena red-text">{props.erroDescricao}</span>
                    </div>
                  </div>
                </div>
              </div>
              <div className="card-action">
                <div className="row wrapper">
                  <div className="col l12 m12 s12">
                    <div className="row">
                      <div className="col s6">
                        <a
                          className="color-default waves-effect waves-light btn right"
                          onClick = {props.handleClear}
                          name="btn_limpar">Limpar
                          <i className="fa fa-eraser right"></i>
                        </a>
                      </div>
                      <div className="col s6">
                        <button
                          className="color-default btn waves-effect waves-light left"
                          type="submit"
                          name="btn_enviar"
                          onClick = {props.handleSubmit}>Salvar
                          <i className="fa fa-floppy-o right"></i>
                        </button>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </form>
  );

}

Agendamento.propTypes = {
  categorias: React.PropTypes.arrayOf(React.PropTypes.object).isRequired,
  itens: React.PropTypes.arrayOf(React.PropTypes.object).isRequired,
  item: React.PropTypes.string.isRequired,
  descricao: React.PropTypes.string.isRequired,
  categoria: React.PropTypes.string.isRequired,
  data: React.PropTypes.string.isRequired,
  handleCategoryChanges: React.PropTypes.func.isRequired,
  handleDescriptionChanges: React.PropTypes.func.isRequired,
  handleItemChanges: React.PropTypes.func.isRequired,
  handleDataChanges: React.PropTypes.func.isRequired,
  handleSubmit: React.PropTypes.func.isRequired,
  handleClear: React.PropTypes.func.isRequired
}

module.exports = Agendamento;
