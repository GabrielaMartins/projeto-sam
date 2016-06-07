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
        "minHeight": "calc(100vh - 154px) !important",
        "flexDirection": "column !important",
        "flex": "1 0 auto !important",
        "backgroundColor": "#FFAAAA"
    },
    "dropdown-content": {
        "backgroundColor": "#FFFFFF",
        "marginTop": 0,
        "marginRight": 0,
        "marginBottom": 0,
        "marginLeft": -1,
        "display": "none",
        "maxHeight": "auto",
        "overflow": "hidden",
        "opacity": 0,
        "position": "absolute",
        "whiteSpace": "nowrap",
        "zIndex": 4,
        "willChange": "height"
    },
    "dropdown-content li>a": {
        "color": "#550000 !important"
    },
    "dropdown-content li>span": {
        "color": "#550000 !important"
    },
    "nav ul a:hover": {
        "backgroundColor": "rgba(0,0,0,0.3) !important"
    },
    "eventsCard > card-panel:hover": {
        "backgroundColor": "rgba(0,0,0,0.3) !important",
        "cursor": "pointer"
    },
    "pendencias:hover": {
        "backgroundColor": "rgba(0,0,0,0.3) !important",
        "cursor": "pointer"
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
        "maxHeight": 300
    },
    "ranking": {
        "overflowY": "scroll",
        "maxHeight": 300
    },
    "eventos": {
        "overflowY": "scroll",
        "maxHeight": 425
    },
    "promocoes": {
        "overflowY": "scroll",
        "maxHeight": 425
    },
    "votacao": {
        "overflowY": "scroll",
        "overflowX": "hidden",
        "maxHeight": 521
    },
    "progress": {
        "backgroundColor": "#FFAAAA"
    },
    "progress determinate": {
        "backgroundColor": "#550000"
    },
    "intern": {
        "marginTop": 10,
        "marginRight": 10,
        "marginBottom": 10,
        "marginLeft": 10
    }
});