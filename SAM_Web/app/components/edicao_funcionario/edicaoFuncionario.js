var React = require('react');
var ReactRouter = require('react-router');

var EdicaoFuncionario = function(props){
    var imagemPreview = <img src= {props.urlImage} id="img_url"  className="center-block responsive-img"/>
    return(
      <div style={{paddingTop: 30 }}>
        <div className="row wrapper">
          <div className="col l8 m8 s10 center-block">
            <div className="card-panel">
              <h3 className="colorText-default card-title center-align">Editar {props.nome}</h3>
              <div className="card-content container">
                <div className="row">
                  <div className="input-field col s12 tooltipped"
                    data-position="bottom"
                    data-delay="50"
                    data-tooltip="Este campo não é editável">
                    <input disabled value = {props.nome} id="nome" type="text"/>
                    <label for="nome" className="active">Nome: </label>
                  </div>
                </div>
                <div className="row">
                  <div className="input-field col s12 m6 l6 tooltipped"
                    data-position="bottom"
                    data-delay="50"
                    data-tooltip="Este campo não é selecionável">
                    <select disabled value={props.cargo}>
                      {props.lista_cargos}
                    </select>
                    <label>Level (Cargo):</label>
                  </div>
                  <div className="input-field col s12 m6 l6 tooltipped"
                    data-position="bottom"
                    data-delay="50"
                    data-tooltip="Este campo não é editável">
                    <input disabled value={props.pontos} id="pontos" type="text"/>
                    <label for="pontos" className="active">XP (Pontos):</label>
                  </div>
                </div>
                <div className="row">
                  <div className="input-field col s12 m6 l6 tooltipped"
                    data-position="bottom"
                    data-delay="50"
                    data-tooltip="Este campo não é editável">
                    <input disabled value={props.data_inicio} id="inicio" type="text"/>
                    <label for="inicio" className="active">Data de Início:</label>
                  </div>
                  <div className="col s6">
                    <div className="row">
                      <div className="col s12 m3 l3">
                      <span style={{"fontSize" : "0.8rem"}}>Grupo: </span>
                      </div>
                      <div className="input-field">
                          <div className="col s6 m4 l4 tooltipped"
                            data-position="bottom"
                            data-delay="50"
                            data-tooltip="Este campo não é selecionável">
                            <input name="group1" type="radio" id="rh" disabled="disabled" checked ={props.perfil === "RH" ? true : false}/>
                            <label for="rh">RH</label>
                          </div>
                          <div className="col s6 m4 l4 tooltipped"
                            data-position="bottom"
                            data-delay="50"
                            data-tooltip="Este campo não é selecionável">
                            <input name="group1" type="radio" id="funcionario" disabled="disabled" checked ={props.perfil === "Funcionário" ? true : false}/>
                            <label for="funcionario">Funcionário</label>
                          </div>
                      </div>
                    </div>
                  </div>
                </div>
                <div className="row">
                  <div className="input-field col s12 m4 l4">
                    <input id="facebook"
                          type="text"
                          onChange={props.handleFacebook}
                          value={props.facebook}/>
                        <label for="facebook" className={props.facebook ? "active" : null}><b>Facebook:</b></label>
                  </div>
                  <div className="input-field col s12 m4 l4">
                    <input id="linkedin"
                      type="text"
                      onChange={props.handleLikedin}
                      value={props.linkedin}/>
                    <label for="linkedin" className={props.linkedin ? "active" : null}><b>Linkedin:</b></label>
                  </div>
                  <div className="input-field col s12 m4 l4">
                    <input id="github"
                      type="text"
                      onChange={props.handleGithub}
                      value={props.github}/>
                    <label for="github" className={props.github ? "active" : null}><b>GitHub:</b></label>
                  </div>
                </div>
                <div className="row">
                  <div className="input-field col s12">
                    <textarea id="bio"
                      className="materialize-textarea"
                      length="200"
                      onChange={props.handleDescricao}
                      value={props.descricao}>
                    </textarea>
                    <label for="bio" className={props.descricao ? "active" : null}><b>Bio:</b></label>
                  </div>
                </div>
                <div className="row">
                  <form action="#" className="col s10">
                    <div className="file-field input-field">
                      <div className="btn">
                        <span>Avatar</span>
                        <input type="file"
                          accept="image/gif, image/jpeg, image/png"
                          id = "img"
                          onChange={props.handleFoto}/>
                      </div>
                      <div className="file-path-wrapper">
                        <input className="file-path"
                          type="text"
                          accept="image/gif, image/jpeg, image/png"
                          id = "imagem"
                          onChange={props.handleFoto}
                          value={props.foto}
                          />
                      </div>
                    </div>
                  </form>
                  <div className="col s2">
                      {props.urlImage ? imagemPreview : null}
                  </div>
                </div>
                <br/><br/><br/>
                <div className="card-action">
                  <div className="row">
                    <div className="col s6">
                      <a className="waves-effect waves-light btn right" onClick={props.handleClear}>Limpar</a>
                    </div>
                    <div className="col s6">
                      <a className="waves-effect waves-light btn left" onClick = {props.handleSubmit}>Enviar</a>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }


module.exports = EdicaoFuncionario;
