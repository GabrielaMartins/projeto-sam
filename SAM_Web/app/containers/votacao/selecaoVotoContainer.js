var React = require('react');
var SelecaoVoto = require('../../components/votacao/selecaoVoto');


var SelecaoVotoContainer = React.createClass({
  getInitialState: function() {
    return {
      dificuldade:"",
      profundidade:"",
      pontuacaoGerada: 0
    };
  },

  handleChangeDificuldade: function(event){
    this.setState({dificuldade:event.target.value});
    if(this.state.dificuldade != "" && this.state.profundidade != ""){
      //pontuação de workshops e palestras é a dificuldade * profundidade * 6 (o peso da categoria)
      this.setState({pontuacaoGerada: parseInt(this.state.dificuldade) * parseInt(this.state.profundidade) * 6})
    }
  },

  handleChangeProfundidade: function(event){
    this.setState({profundidade:event.target.value});
    if(this.state.dificuldade != "" && this.state.profundidade != ""){
      //pontuação de workshops e palestras é a dificuldade * profundidade * 6 (o peso da categoria)
      this.setState({pontuacaoGerada: parseInt(this.state.dificuldade) * parseInt(this.state.profundidade) * 6})
    }
  },

  componentDidMount: function(){
    $(document).ready(function() {
      $('select').material_select();
    });

    $("#Select1").on('change', this.handleChangeDificuldade);
    $("#Select2").on('change', this.handleChangeProfundidade);
  },

  render : function(){
      return(<SelecaoVoto
        pontuacaoGerada = {this.state.pontuacaoGerada}
        changeDificuldade = {this.handleChangeDificuldade}
        changeProfundidade = {this.handleChangeProfundidade}
        dificuldade = {this.state.dificuldade}
        profundidade = {this.state.profundidade}
        titulo = {this.props.titulo}
        botao = {this.props.botao}
        />)
  }
});

module.exports = SelecaoVotoContainer;
