

//        02/10/2012 Ricardo Almeida
//        Inclusão da tag jQuery.noConflict();, para nao ter conflito com o scriptaculous.js
//        Substituição do $ por jQuery
jQuery.noConflict();
jQuery(document).ready(function () {
    var label = jQuery('#<%=hdfLabel.ClientID%>').val();


    if (label == "Peticao") {
        goToByScroll('Peticoes');
    }

    if (label == "Peticao2") {
        goToByScroll('CkEditor');
    }

    if (label == "AnexoPeticao") {
        goToByScroll('AnexoPeticao');
    }

    if (label == "AnexoProcesso") {
        goToByScroll('AnexoProcesso');
    }

    if (label == "Interessado") {
        goToByScroll('Interessado');
    }

    if (label == "Diligencias") {
        goToByScroll('Diligencias');
    }

    if (label == "Oficio") {
        goToByScroll('Oficio');
    }

    if (label == "TextoOficio") {
        goToByScroll('TextoOficio');
    }

    if (label == "Decisoes") {
        goToByScroll('Decisoes');
    }

    if (label == "Observacoes") {
        goToByScroll('Observacoes');
    }

    if (label == "Audiencia") {
        goToByScroll('Audiencia');
    }

    if (label == "AudienciaFutura") {
        goToByScroll('AudienciaFutura');
    }

    if (label == "AterarAssunto") {
        goToByScroll('AterarAssunto');
    }

    if (label == "NovaClasse") {
        goToByScroll('NovaClasse');
    }

    if (label == "DadosSentenca") {
        goToByScroll('Sentenca');
    }

    if (label == "DadosCondenacao") {
        goToByScroll('Condenacao');
    }

    if (label == "ReceberProcesso") {
        goToByScroll('RecebProcesso');
    }
    if (label == "Tj") {
        goToByScroll('divTJ');
    }

    if (label == "VisitasCriminal") {
        goToByScroll('VisitasCriminal');
    }

    if (label == "ConvenioCriminal") {
        goToByScroll('ConvenioCriminal');
    }

    if (label == "VisitaSocioEducativa") {
        goToByScroll('VisitaSocioEducativa');
    }

    if (label == "VisitaAcolhimento") {
        goToByScroll('VisitaAcolhimento');
    }



});

function goToByScroll(id) {
    // Reove "link" from the ID
    //        alert(id);
    id = id.replace("link", "");
    // Scroll
    jQuery('html,body').animate({
        scrollTop: jQuery("#" + id).offset().top
    },
            'slow');
}
    
     