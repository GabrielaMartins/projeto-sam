var React = require('react');
var ReactRouter = require('react-router');
var Avatar = require('./avatar');
var AtividadesHistorico = require('../item/itemCard');
var PromocoesHistorico = require('./promocoesHistorico');
var PontuacaoGrafico = require('./pontuacaoGrafico');
var BaseHistorico = require('./BaseHistorico');

var Perfil = React.createClass({
  render: function(){

    var promocoes = null;
    var atividades = null;

    //foreach aqui
    return(
      <div style={{marginTop:50}} className="container">
        <Avatar
          foto = "./app/imagens/fulano.jpg"
          nome = "Gabriela"
          cargo = "Estagiário"
          prox_cargo = "Junior I"
          pontos = "60"
          pontos_cargo = "120"
          tempo_casa = "5 anos"
          descricao = "Eiiitaaa Mainhaaa!! Esse Lorem ipsum é só na sacanageeem!!
          E que abundância meu irmão viuu!! Assim você vai matar o papai.
          Só digo uma coisa, Domingo ela não vai! Danadaa!! Vem minha odalisca, agora faz essa cobra coral subir!!!
          Pau que nasce torto, Nunca se endireita. Tchannn!! Tchannn!! Tu du du pááá! Eu gostchu muitchu, heinn! danadinha!
          Mainhaa! Agora use meu lorem ipsum ordinária!!! Olha o quibeee! rema, rema, ordinária!.
          Você usa o Lorem Ipsum tradicional? Sabe de nada inocente!! Conheça meu lorem que é Tchan, Tchan, Tchannn!! Txu Txu Tu Paaaaa!!
          Vem, vem ordinária!! Venha provar do meu dendê que você não vai se arrepender.
          Só na sacanageeem!! Eu gostchu muitchu, heinn! Eitchaaa template cheio de abundância danadaaa!!
          Assim você mata o papai hein!? Etâaaa Mainhaaaaa...me abusa nesse seu layout, me gera, me geraaaa ordinária!!!
          Só na sacanagem!!!! Venha provar do meu dendê Tu du du pááá!.
          "/>
        <div className="row">
          <div className="col l7 m6 s12">
            <BaseHistorico placeholder = "Pesquise por atividades realizadas" titulo = "Atividades Realizadas">
              {/*<AtividadesHistorico/>*/}
            </BaseHistorico>
          </div>
          <div className="col l5 m6 s12">
            <BaseHistorico placeholder = "Pesquise por cargos alcançados" titulo = "Promoções Alcançadas">
              {/*<PromocoesHistorico/>*/}
            </BaseHistorico>
          </div>
        </div>
        <div className="row">
          <PontuacaoGrafico/>
        </div>
      </div>
    );
  }
});

module.exports = Perfil;
