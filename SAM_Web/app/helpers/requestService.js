var axios = require("axios");
var Router = require("react-router");
var transitionTo = Router.transitionTo;

function login(url, usuario, senha){
  return axios.post(url, {
    user: usuario,
    password: senha
  });
}

var helpers = {
  autenticacao : login
}

module.exports = helpers;
