'use strict'
var React =  require('react');
var ReactRouter = require('react-router');
var Link = ReactRouter.Link;

var CadastroItem = function(props){
  console.log(props.categorias);
  var options = [];

  props.categorias.forEach(function(categoria){
    debugger;
    options.push(<option value = {categoria} key = {categoria}>{categoria}</option>);
  });

  //var categorias = props.categorias.map( (opt, index) => <option value = {opt} key = {index}>{opt}</option>);

  return(
      <main className="main">
        <form>
          <div className="row wrapper">
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
                  <div className="input-field col l6 m6 s12">
                    <select
                      id ="select_categoria"
                      value = {props.categoria}
                      onChange = {props.handleCategoryChanges}>
                      <option
                        value = {props.categoria}
                        key = {props.categoria}
                        disabled>{props.categoria}
                      </option>
                      {options}
                   </select>
                    <label>Categoria</label>
                  </div>
                  <div className="input-field col l6 m6 s12">
                    <select
                      id = "select_dificuldade"
                      value = {props.dificuldade}
                      onChange = {props.handleDificultyChanges}>
                        <option
                          value = {props.dificuldade}
                          key = {props.dificuldade}
                          disabled>{props.dificuldade}
                        </option>
                        {["Fácil", "Médio", "Difícil"].map(opt => <option value = {opt} key = {opt}>{opt}</option>)}
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
                    <div className="col l6 m6 s6">
                      <input
                        id="modificador1"
                        name="rdgroup"
                        type="radio"
                        onClick = {props.handleModifierChanges}
                      />
                      <label for="modificador1">{props.rotulosRadio[0]}</label>
                    </div>
                    <div className="col l6 m6 s6">
                      <input
                        id = "modificador2"
                        name = "rdgroup"
                        type = "radio"
                        onClick = {props.handleModifierChanges}
                        />
                      <label for="modificador2">{props.rotulosRadio[1]}</label>
                    </div>
                  </div>
                </div>

                {/* aqui fica os botoes */}
                <div className="row wrapper">
                  <div className="col l12 m12 s12">
                    <div className="row">
                      <div className="col l6 m6 s6">
                        <a
                          className="waves-effect waves-light btn"
                          onClick = {props.handleClear}
                          name="btn_limpar">Limpar
                          <i className="material-icons right">send</i>
                        </a>
                      </div>
                      <div className="col l6 m6 s6">
                        <button
                          className="btn waves-effect waves-light"
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
      </main>
    );

}

CadastroItem.propTypes = {
   categorias: React.PropTypes.arrayOf(React.PropTypes.string).isRequired,
   rotulosRadio: React.PropTypes.arrayOf(React.PropTypes.string).isRequired,
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
