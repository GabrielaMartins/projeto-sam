import React, {StyleSheet, Dimensions, PixelRatio} from "react-native";
const {width, height, scale} = Dimensions.get("window"),
    vw = width / 100,
    vh = height / 100,
    vmin = Math.min(vw, vh),
    vmax = Math.max(vw, vh);

export default StyleSheet.create({
    "background-start": {
        "overflow": "hidden",
        "background": "url('../imagens/background.jpg') no-repeat fixed",
        "backgroundSize": "cover",
        "backgroundPositionY": "center"
    },
    "background-404": {
        "overflow": "hidden",
        "background": "url('../imagens/404back.jpg') no-repeat fixed",
        "backgroundSize": "cover",
        "backgroundPositionY": "center"
    },
    "background-erro": {
        "overflow": "hidden",
        "background": "url('../imagens/erroback.jpg') no-repeat fixed",
        "backgroundSize": "cover",
        "backgroundPositionY": "center"
    },
    "transparent-dark": {
        "background": "rgba(0,0,0,0.75)"
    },
    "transparent-white": {
        "background": "rgba(255,255,255,0.75)"
    },
    "color-default": {
        "backgroundColor": "#550000"
    },
    "colorText-default": {
        "color": "#550000"
    },
    "colorText-finalizada": {
        "color": "#0A4800"
    },
    "color-links": {
        "color": "#D46A6A"
    },
    "button-color:hover": {
        "backgroundColor": "#550000"
    },
    "input-field label": {
        "color": "#000"
    },
    "input-field input[type=text] + label": {
        "color": "#000"
    },
    "input-field input[type=text]:focus + label": {
        "color": "#550000"
    },
    "input-field input[type=password]:focus + label": {
        "color": "#550000"
    },
    "input-field input[type=text]:focus": {
        "borderBottom": "1px solid #550000",
        "boxShadow": "0 1px 0 0 #550000"
    },
    "input-field input[type=password]:focus": {
        "borderBottom": "1px solid #550000",
        "boxShadow": "0 1px 0 0 #550000"
    },
    "input-field prefixactive": {
        "color": "#550000"
    },
    "corMensagemErro": {
        "color": "#8F8F8F"
    },
    "full-screen": {
        "height": 100 * vh
    },
    "wrapper": {
        "height": "100%",
        "minHeight": "100%",
        "display": "flex",
        "WebkitAlignItems": "center",
        "alignItems": "center",
        "WebkitJustifyContent": "center",
        "justifyContent": "center"
    },
    "main": {
        "display": "flex !important",
        "minHeight": "calc(100vh - 134px) !important",
        "flexDirection": "column !important",
        "flex": "1 0 auto !important",
        "backgroundColor": "#EEE"
    },
    "dropdown-content": {
        "backgroundColor": "#FFFFFF",
        "marginTop": 0,
        "marginRight": 0,
        "marginBottom": 0,
        "marginLeft": -1,
        "display": "none",
        "maxHeight": "auto",
        "minWidth": 150,
        "overflow": "hidden",
        "opacity": 0,
        "position": "absolute",
        "whiteSpace": "nowrap",
        "zIndex": 4,
        "willChange": "width, height"
    },
    "dropdown-content li>a": {
        "color": "#550000 !important"
    },
    "dropdown-content li>span": {
        "color": "#550000 !important"
    },
    "nav ul a:hover": {
        "backgroundColor": "rgba(0,0,0,0.1) !important"
    },
    "eventsCard > card-panel:hover": {
        "backgroundColor": "rgba(0,0,0,0.1)",
        "cursor": "pointer"
    },
    "pendencia:hover": {
        "cursor": "pointer"
    },
    "finalizada:hover": {
        "backgroundColor": "#e57373 !important"
    },
    "aberta:hover": {
        "backgroundColor": "#81c784 !important"
    },
    "footerpage-footer": {
        "marginTop": "0px !important",
        "backgroundColor": "#550000 !important"
    },
    "btn:hover": {
        "backgroundColor": "#801515"
    },
    "pendencia": {
        "overflowY": "scroll",
        "height": 255
    },
    "ranking": {
        "overflowY": "scroll",
        "height": 255
    },
    "eventos": {
        "overflowY": "scroll",
        "height": 423
    },
    "promocoes": {
        "overflowY": "scroll",
        "height": 423
    },
    "alertas": {
        "overflowY": "scroll",
        "height": 580
    },
    "ultimasAtualizacoes": {
        "overflowY": "scroll",
        "height": 580
    },
    "votacao": {
        "overflowY": "scroll",
        "overflowX": "hidden",
        "height": 540
    },
    "progress": {
        "backgroundColor": "#888"
    },
    "progress determinate": {
        "backgroundColor": "#550000"
    },
    "intern": {
        "marginTop": 10,
        "marginRight": 10,
        "marginBottom": 10,
        "marginLeft": 10
    },
    "inputsearch": {
        "display": "block",
        "fontSize": 16,
        "fontWeight": 300,
        "width": "100%",
        "height": 45,
        "marginTop": 0,
        "marginRight": 0,
        "marginBottom": 0,
        "marginLeft": 0,
        "paddingTop": 0,
        "paddingRight": 45,
        "paddingBottom": 0,
        "paddingLeft": 15,
        "border": 0
    },
    "inputsearch: focus": {
        "outline": "none"
    },
    "inputsearch:not([type]):focus:not([readonly])": {
        "boxShadow": "0 0 0 0"
    },
    "scrollbar::-webkit-scrollbar-corner": {
        "backgroundColor": "#FFFFFF"
    },
    "scrollbar::-webkit-scrollbar-track": {
        "backgroundColor": "none"
    },
    "scrollbar::-webkit-scrollbar": {
        "WebkitAppearance": "none",
        "width": 11
    },
    "scrollbar::-webkit-scrollbar-thumb": {
        "borderRadius": 12,
        "border": "4px solid rgba(255,255,255,0)",
        "backgroundClip": "content-box",
        "backgroundColor": "#550000"
    },
    "[type=\"radio\"]:checked+label:after": {
        "backgroundColor": "#550000 !important",
        "border": "2px solid #550000 !important"
    },
    "[type=\"radio\"]with-gap:checked+label:after": {
        "backgroundColor": "#550000 !important",
        "border": "2px solid #550000 !important"
    },
    "[type=\"radio\"]with-gap:checked+label:before": {
        "border": "2px solid #550000 !important"
    },
    "textarea:focus:not([readonly])": {
        "borderBottom": "1px solid #550000 !important",
        "boxShadow": "0 1px 0 0 #550000 !important"
    },
    "textarea:focus:not([readonly])+label": {
        "color": "#550000 !important"
    },
    "btn": {
        "backgroundColor": "#550000 !important"
    },
    "campoBusca": {
        "transition": "all 0.4s ease"
    },
    "campoBuscastick": {
        "display": "block",
        "position": "fixed",
        "zIndex": 500,
        "width": "100%",
        "marginLeft": "0 !important",
        "marginRight": "0 !important"
    },
    "base-historico": {
        "overflowY": "scroll",
        "overflowX": "hidden",
        "height": 400
    },
    "full-screen-perfil": {
        "minHeight": "calc(100vh - 114px)"
    },
    "picker__weekday-display": {
        "backgroundColor": "#550000"
    },
    "picker__date-display": {
        "backgroundColor": "#550000"
    },
    "picker__day--selected": {
        "backgroundColor": "#550000"
    },
    "picker__day--selected:hover": {
        "backgroundColor": "#550000"
    },
    "picker--focused picker__day--selected": {
        "backgroundColor": "#550000"
    },
    "picker__daypicker__day--today": {
        "color": "#550000"
    },
    "picker__close": {
        "color": "#550000"
    },
    "picker__today": {
        "color": "#550000"
    },
    "arrow": {
        "WebkitAnimation": "animation 3000ms linear infinite both",
        "animation": "animation 3000ms linear infinite both"
    }
});