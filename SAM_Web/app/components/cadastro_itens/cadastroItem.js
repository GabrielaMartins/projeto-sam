'use strict'
var React =  require('react');
var ReactRouter = require('react-router');
var Radio = require('../../ui_elements/radio');
var Link = ReactRouter.Link;

var CadastroItem = function(props){

  return(
      <div>
        <form>
          <div className="row valign-wrapper" style={{paddingTop: '5%' }}>
            <div className="col l6 m6 s12 center-block">
              <div className="card-panel z-depth-6 transparent-white">
                <div className="row">
                  <div className="input-field col l12 m12 s12">
                    <input
                      id="nome_item"
                      type="text"
                      className="validate"
                      onChange = {props.handleItemChanges}
                      value = {props.item}
                    />
                    <label for="nome_item">Nome do Item:</label>
                  </div>
                </div>
                <div className="row">
                  <div className=" col l6 m6 s12">
                    <select
                      id ="select_categoria"
                      value = {props.categoria}
                      onChange = {props.handleCategoryChanges}>
                      <option
                        value = {0}
                        key = {0}
                        >Selecione a categoria
                      </option>
                      {props.categorias.map( function(opt, index){ return <option value = {opt} key = {index + 1}>{opt}</option>})}
                   </select>
                    <label>Categoria</label>
                  </div>
                  <div className="input-field col l6 m6 s12">
                    <select
                      id = "select_dificuldade"
                      value = {props.dificuldade}
                      onChange = {props.handleDificultyChanges}>
                        <option
                          value = {0}
                          key = {0}
                          >Selecione a dificuldade
                        </option>
                        <option value = "1" key = "1">Fácil</option>
                        <option value = "3" key = "2">Médio</option>
                        <option value = "8" key = "3">Difícil</option>
                   </select>
                    <label>Dificuldade</label>
                  </div>
                </div>
                { /* essa div deve ser escondida e mostrada apenas quando o item é novo */}
                <div>
                  <div className="row">
                    <div className="input-field col l12 m12 s12">
                      <textarea
                        id = "descricao_item"
                        className = "materialize-textarea"
                        onChange = {props.handleDescriptionChanges}
                        value = {props.descricao}
                      />
                      <label for="descricao_item">Descrição</label>
                    </div>
                  </div>
                  <div className="row">
                    <div className="col s6">
                      <div className="right">
                        <Radio
                          className="right"
                          id = "radio1"
                          name = "rdGroup"
                          label = {props.rotulosRadio[0]}
                          value = {props.rotulosRadio[0]}
                          onChange = {props.handleModifierChanges}
                        />
                      </div>
                    </div>
                    <div className="col s6">
                      <div className="left">
                        <Radio
                          name = "rdGroup"
                          id = "radio2"
                          label = {props.rotulosRadio[1]}
                          value = {props.rotulosRadio[1]}
                          onChange = {props.handleModifierChanges}
                        />
                      </div>
                    </div>
                  </div>
                </div>

                {/* aqui fica os botoes */}
                <div className="row wrapper">
                  <div className="col l12 m12 s12">
                    <div className="row">
                      <div className="col s6">
                        <a
                          className="color-default waves-effect waves-light btn right"
                          onClick = {props.handleClear}
                          name="btn_limpar">Limpar
                          <i className="material-icons right">send</i>
                        </a>
                      </div>
                      <div className="col s6">
                        <button
                          className="color-default btn waves-effect waves-light left"
                          type="submit"
                          name="btn_enviar"
                          onClick = {props.handleSubmit}>Enviar
                          <i className="material-icons right">send</i>
                        </button>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </form>
      </div>
    );

}

CadastroItem.propTypes = {
   categorias: React.PropTypes.arrayOf(React.PropTypes.string).isRequired,
   rotulosRadio: React.PropTypes.arrayOf(React.PropTypes.string).isRequired,
   //checked: React.PropTypes.bool.isRequired,
   item: React.PropTypes.string.isRequired,
   descricao: React.PropTypes.string.isRequired,
   categoria: React.PropTypes.string.isRequired,
   handleCategoryChanges: React.PropTypes.func.isRequired,
   handleDificultyChanges: React.PropTypes.func.isRequired,
   handleModifierChanges: React.PropTypes.func.isRequired,
   handleDescriptionChanges: React.PropTypes.func.isRequired,
   handleItemChanges: React.PropTypes.func.isRequired,
   handleSubmit: React.PropTypes.func.isRequired,
   handleClear: React.PropTypes.func.isRequired
}

module.exports = CadastroItem;
