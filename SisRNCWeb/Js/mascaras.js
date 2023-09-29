function mascaraData(e) {
    // Identifica evento, código da tecla pressionada e controle que disparou evento
    var e = e || event;
    var k = e.keyCode || e.which;
    var targ = e.target || e.srcElement;
    var tamanho = targ.value.length;
    var bRetorno;

    // Verifica se o tamanho máximo já foi atingido
    if ((k != 8) && (tamanho == 10))
        return false;

    // Se tamanho máximo ainda não foi atingido, verifica se tecla pressionada é número
    if (((k >= 48) && (k <= 57)) || (k == 8)) {
        // Verifica se tecla pressionada corresponde a número não permitido na posição atual
        bRetorno = verificaCaractereData(k, tamanho, targ.value);

        // Se tecla pressionada for número e valor permitido para a posição, insere a barra caso necessário
        if (((tamanho == 2) || (tamanho == 5)) && (k != 8))
            targ.value += '/';

        return bRetorno;
    }
    else
        return false;
}

function mascaraHora(e) {
    // Identifica evento, código da tecla pressionada e controle que disparou evento
    var e = e || event;
    var k = e.keyCode || e.which;
    var targ = e.target || e.srcElement;
    var tamanho = targ.value.length;
    var bRetorno;

    // Verifica se o tamanho máximo já foi atingido
    if ((k != 8) && (tamanho == 5))
        return false;

    // Se tamanho máximo ainda não foi atingido, verifica se tecla pressionada é número
    if (((k >= 48) && (k <= 57)) || (k == 8)) {

        // Se tecla pressionada for número e valor permitido para a posição, insere a barra caso necessário
        if ((tamanho == 2) && (k != 8))
            targ.value += ':';

        return true;
    }
    else
        return false;
}


function verificaCaractereData(key, size, fieldValue) {
    var valor01 = fieldValue.substr(0, 1);
    var valor11 = fieldValue.substr(1, 1);
    var valor31 = fieldValue.substr(3, 1);
    var valor41 = fieldValue.substr(4, 1);
    var valor63 = fieldValue.substr(6, 3);
    if ((size == 0) && (key > 51)) return false; // dia tem q começar com 0, 1, 2 ou 3
    if ((size == 1) && (valor01 == '0') && (key == 48)) return false; // não existe dia 00
    if ((size == 1) && (valor01 == '3') && (key > 49)) return false; // se primeiro dígito do dia for 3, segundo só pode ser 0 ou 1.
    if (((size == 2) || (size == 3)) && (key > 49)) return false; // não existe vigésimo mês.
    if ((size == 4) && (valor31 == '1') && (key > 50)) return false; // o último mês é 12
    if ((size == 4) && (valor31 == '0') && (key == 48)) return false; // não existe o mês 00
    if ((size == 4) && (valor01 == '3') && (valor31 == '0') && (key == 50)) return false; // não existe 30/02 ou 31/02
    if ((size == 9) && (valor01 == '2') && (valor11 == '9') && (valor31 == '0') && (valor41 == '2') && (parseInt(valor63 + String.fromCharCode(key)) % 4 != 0)) return false; // só existe 29/02 em anos bissextos
    if ((size == 4) && (valor01 == '3') && (valor11 == '1') && (valor31 == '0') && ((key == 52) || (key == 54) || (key == 57))) return false; // não existe 31/04, 31/06 ou 31/09
    if ((size == 4) && (valor01 == '3') && (valor11 == '1') && (valor31 == '1') && (key == 49)) return false; // não existe 31/11
    return true;
}

function mascaraCPF(e) {
    // Identifica evento, código da tecla pressionada e controle que disparou evento
    var e = e || event;
    var k = e.keyCode || e.which;
    var targ = e.target || e.srcElement;
    var tamanho = targ.value.length;

    // Verifica se o tamanho máximo já foi atingido
    if ((k != 8) && (tamanho == 14))
        return false;

    // Se tamanho máximo ainda não foi atingido, verifica se tecla pressionada é número
    if (((k >= 48) && (k <= 57)) || (k == 8)) {
        // Se tecla pressionada for número e valor permitido para a posição, insere a ponto/traço caso necessário
        if (((tamanho == 3) || (tamanho == 7)) && (k != 8))
            targ.value += '.';
        else if ((tamanho == 11) && (k != 8))
            targ.value += '-';
        return true;
    }
    else
        return false;
}

function completaCPF(e) {
    // Identifica evento, código da tecla pressionada e controle que disparou evento
    var e = e || event;
    var k = e.keyCode || e.which;
    var targ = e.target || e.srcElement;
    var tamanho = targ.value.length;

    if (tamanho < 14) {
        targ.value = targ.value.replace('.', '');
        targ.value = targ.value.replace('.', '');
        targ.value = targ.value.replace('-', '');
        for (i = targ.value.length; i < 11; i++) {
            targ.value = '0' + targ.value;
        }

        var s1 = targ.value.substr(0, 3);
        var s2 = targ.value.substr(3, 3);
        var s3 = targ.value.substr(6, 3);
        var s4 = targ.value.substr(9, 2);

        targ.value = s1 + '.' + s2 + '.' + s3 + '-' + s4;
    }
}

function mascaraPE(e) {
    // Identifica evento, código da tecla pressionada e controle que disparou evento
    var e = e || event;
    var k = e.keyCode || e.which;
    var targ = e.target || e.srcElement;
    var tamanho = targ.value.length;

    // Verifica se o tamanho máximo já foi atingido
    if ((k != 8) && (tamanho == 19))
        return false;

    // Se tamanho máximo ainda não foi atingido, verifica se tecla pressionada é número
    if (((k >= 48) && (k <= 57)) || (k == 8)) {
        // Se tecla pressionada for número e valor permitido para a posição, insere a ponto/traço caso necessário
        if (((tamanho == 3) || (tamanho == 8) || (tamanho == 10)) && (k != 8))
            targ.value += '.';
        else if ((tamanho == 17) && (k != 8))
            targ.value += '-';
        return true;
    }
    else
        return false;
}

function completaPE(e) {
    // Identifica evento, código da tecla pressionada e controle que disparou evento
    var e = e || event;
    var k = e.keyCode || e.which;
    var targ = e.target || e.srcElement;
    var tamanho = targ.value.length;

    if (tamanho < 19) {
        targ.value = targ.value.replace('.', '');
        targ.value = targ.value.replace('.', '');
        targ.value = targ.value.replace('.', '');
        targ.value = targ.value.replace('-', '');
        for (i = targ.value.length; i < 15; i++) {
            targ.value = '0' + targ.value;
        }

        var s1 = targ.value.substr(0, 3);
        var s2 = targ.value.substr(3, 4);
        var s3 = targ.value.substr(7, 1);
        var s4 = targ.value.substr(8, 6);
        var s5 = targ.value.substr(14, 1);

        targ.value = s1 + '.' + s2 + '.' + s3 + '.' + s4 + '-' + s5;
    }
}

function mascaraPEPrimeiro(e) {
    // Identifica evento, código da tecla pressionada e controle que disparou evento
    var e = e || event;
    var k = e.keyCode || e.which;
    var targ = e.target || e.srcElement;
    var tamanho = targ.value.length;


    // Verifica se o tamanho máximo já foi atingido
    if ((k != 8) && (tamanho == 18))
        return false;

    // Se tamanho máximo ainda não foi atingido, verifica se tecla pressionada é número
    if (((k >= 48) && (k <= 57)) || (k == 8)) {
        // Se tecla pressionada for número e valor permitido para a posição, insere a ponto/traço caso necessário
        if (((tamanho == 5) || (tamanho == 8)) && (k != 8))
            targ.value += '.';
        else if ((tamanho == 3) && (k != 8))
            targ.value += '/';
        else if ((tamanho == 16) && (k != 8))
            targ.value += '-';
        return true;
    }
    else
        return false;
}

function completaPEPrimeiro(e) {
    // Identifica evento, código da tecla pressionada e controle que disparou evento
    var e = e || event;
    var k = e.keyCode || e.which;
    var targ = e.target || e.srcElement;
    var tamanho = targ.value.length;

    if (tamanho < 18) {
        targ.value = targ.value.replace('.', '');
        targ.value = targ.value.replace('.', '');
        targ.value = targ.value.replace('/', '');
        targ.value = targ.value.replace('-', '');
        for (i = targ.value.length; i < 14; i++) {
            targ.value = '0' + targ.value;
        }

        var s1 = targ.value.substr(0, 3);
        var s2 = targ.value.substr(3, 1);
        var s3 = targ.value.substr(4, 2);
        var s4 = targ.value.substr(6, 7);
        var s5 = targ.value.substr(13, 1);

        targ.value = s1 + '/' + s2 + '.' + s3 + '.' + s4 + '-' + s5;
    }
}

function mascaraCNJ(e) {
    // Identifica evento, código da tecla pressionada e controle que disparou evento
    var e = e || event;
    var k = e.keyCode || e.which;
    var targ = e.target || e.srcElement;
    var tamanho = targ.value.length;

    // Verifica se o tamanho máximo já foi atingido
    if ((k != 8) && (tamanho == 25))
        return false;

    // Se tamanho máximo ainda não foi atingido, verifica se tecla pressionada é número
    if (((k >= 48) && (k <= 57)) || (k == 8)) {
        // Se tecla pressionada for número e valor permitido para a posição, insere a ponto/traço caso necessário
        if (((tamanho == 10) || (tamanho == 15) || (tamanho == 17) || (tamanho == 20)) && (k != 8))
            targ.value += '.';
        else if ((tamanho == 7) && (k != 8))
            targ.value += '-';
        return true;
    }
    else
        return false;
}

function completaCNJ(e) {
    // Identifica evento, código da tecla pressionada e controle que disparou evento
    var e = e || event;
    var k = e.keyCode || e.which;
    var targ = e.target || e.srcElement;
    var tamanho = targ.value.length;

    if (tamanho < 24) {
        targ.value = targ.value.replace('-', '');
        targ.value = targ.value.replace('.', '');
        targ.value = targ.value.replace('.', '');
        targ.value = targ.value.replace('.', '');
        for (i = targ.value.length; i < 20; i++) {
            targ.value = '0' + targ.value;
        }

        var s1 = targ.value.substr(0, 7);
        var s2 = targ.value.substr(7, 2);
        var s3 = targ.value.substr(9, 4);
        var s4 = targ.value.substr(13, 1);
        var s5 = targ.value.substr(14, 2);
        var s6 = targ.value.substr(16, 4);

        targ.value = s1 + '-' + s2 + '.' + s3 + '.' + s4 + '.' + s5 + '.' + s6;
    }
}

//Nº Proceso do 2º Grau do TJE
function mascaraSegundoGrau(e) {
    // Identifica evento, código da tecla pressionada e controle que disparou evento
    var e = e || event;
    var k = e.keyCode || e.which;
    var targ = e.target || e.srcElement;
    var tamanho = targ.value.length;

    // Verifica se o tamanho máximo já foi atingido
    if ((k != 8) && (tamanho == 15))
        return false;

    // Se tamanho máximo ainda não foi atingido, verifica se tecla pressionada é número
    if (((k >= 48) && (k <= 57)) || (k == 8)) {
        // Se tecla pressionada for número e valor permitido para a posição, insere a ponto/traço caso necessário
        if (((tamanho == 4) || (tamanho == 6)) && (k != 8))
            targ.value += '.';
        else if ((tamanho == 13) && (k != 8))
            targ.value += '-';
        return true;
    }
    else
        return false;
}

function completaSegundoGrau(e) {
    // Identifica evento, código da tecla pressionada e controle que disparou evento
    var e = e || event;
    var k = e.keyCode || e.which;
    var targ = e.target || e.srcElement;
    var tamanho = targ.value.length;

    if (tamanho < 15) {
        targ.value = targ.value.replace('-', '');
        targ.value = targ.value.replace('.', '');
        targ.value = targ.value.replace('.', '');
        for (i = targ.value.length; i < 13; i++) {
            targ.value = '0' + targ.value;
        }

        var s1 = targ.value.substr(0, 4);
        var s2 = targ.value.substr(4, 1);
        var s3 = targ.value.substr(5, 6);
        var s4 = targ.value.substr(11, 1);

        targ.value = s1 + '.' + s2 + '.' + s3 + '-' + s4;
    }
}

function mascaraPEVec(e) {
    // Identifica evento, código da tecla pressionada e controle que disparou evento
    var e = e || event;
    var k = e.keyCode || e.which;
    var targ = e.target || e.srcElement;
    var tamanho = targ.value.length;


    // Verifica se o tamanho máximo já foi atingido
    if ((k != 8) && (tamanho == 10))
        return false;

    // Se tamanho máximo ainda não foi atingido, verifica se tecla pressionada é número
    if (((k >= 48) && (k <= 57)) || (k == 8)) {
        // Se tecla pressionada for número e valor permitido para a posição, insere a ponto/traço caso necessário
        if ((tamanho == 1) && (k != 8))
            targ.value = '-' + targ.value;
        else {
            targ.value = targ.value.replace('-', '');
            targ.value = targ.value.substr(0, tamanho - 1) + '-' + targ.value.substr(tamanho - 1, 1);
        }
        return true;
    }
    else
        return false;
}

function completaPEVec(e) {
    // Identifica evento, código da tecla pressionada e controle que disparou evento
    var e = e || event;
    var k = e.keyCode || e.which;
    var targ = e.target || e.srcElement;
    var tamanho = targ.value.length;

    if (tamanho < 18) {
        targ.value = targ.value.replace('.', '');
        targ.value = targ.value.replace('.', '');
        targ.value = targ.value.replace('/', '');
        targ.value = targ.value.replace('-', '');
        for (i = targ.value.length; i < 14; i++) {
            targ.value = '0' + targ.value;
        }

        var s1 = targ.value.substr(0, 3);
        var s2 = targ.value.substr(3, 1);
        var s3 = targ.value.substr(4, 2);
        var s4 = targ.value.substr(6, 7);
        var s5 = targ.value.substr(13, 1);

        targ.value = s1 + '/' + s2 + '.' + s3 + '.' + s4 + '-' + s5;
    }
}


//No. Tombo- Flagrante-Criminal
function mascaraAPF(e) {
    // Identifica evento, código da tecla pressionada e controle que disparou evento
    var e = e || event;
    var k = e.keyCode || e.which;
    var targ = e.target || e.srcElement;
    var tamanho = targ.value.length;

    // Verifica se o tamanho máximo já foi atingido
    if ((k != 8) && (tamanho == 17))
        return false;

    // Se tamanho máximo ainda não foi atingido, verifica se tecla pressionada é número
    if (((k >= 48) && (k <= 57)) || (k == 8)) {
        // Se tecla pressionada for número e valor permitido para a posição, insere a ponto/traço caso necessário
        if ((tamanho == 3) && (k != 8))
            targ.value += '/';
        else if ((tamanho == 8) && (k != 8))
            targ.value += '.';
        else if ((tamanho == 15) && (k != 8))
            targ.value += '-';
        return true;
    }
    else
        return false;
}

function completaAPF(e) {
    // Identifica evento, código da tecla pressionada e controle que disparou evento
    var e = e || event;
    var k = e.keyCode || e.which;
    var targ = e.target || e.srcElement;
    var tamanho = targ.value.length;

    if (tamanho < 17) {
        targ.value = targ.value.replace('/', '');
        targ.value = targ.value.replace('.', '');
        targ.value = targ.value.replace('-', '');
        for (i = targ.value.length; i < 14; i++) {
            targ.value = '0' + targ.value;
        }

        var s1 = targ.value.substr(0, 3);
        var s2 = targ.value.substr(3, 4);
        var s3 = targ.value.substr(7, 6);
        var s4 = targ.value.substr(13, 1);

        targ.value = s1 + '/' + s2 + '.' + s3 + '-' + s4;
    }
}

//Processo Interno Defensoria 
function mascaraPI(e) {
    // Identifica evento, código da tecla pressionada e controle que disparou evento
    var e = e || event;
    var k = e.keyCode || e.which;
    var targ = e.target || e.srcElement;
    var tamanho = targ.value.length;

    // Verifica se o tamanho máximo já foi atingido
    if ((k != 9) && (tamanho == 14))
        return false;

    // Se tamanho máximo ainda não foi atingido, verifica se tecla pressionada é número
    if (tamanho == 0 || tamanho == 1)
        return true;
    else if (((k >= 48) && (k <= 57)) || (k == 9)) {
        // Se tecla pressionada for número e valor permitido para a posição, insere a ponto/traço caso necessário
        if ((tamanho == 9) && (k != 9))
            targ.value += '/';
        return true;
    }
    else
        return false;
}

function completaPI(e) {
    // Identifica evento, código da tecla pressionada e controle que disparou evento
    var e = e || event;
    var k = e.keyCode || e.which;
    var targ = e.target || e.srcElement;
    var tamanho = targ.value.length;

    if (tamanho < 14) {
        targ.value = targ.value.replace('/', '');
        for (i = targ.value.length; i < 13; i++) {
            targ.value = '0' + targ.value;
        }

        var s1 = targ.value.substr(0, 9);
        var s2 = targ.value.substr(9, 4);

        targ.value = s1 + '/' + s2;
    }
}

function mascaraPADAC(e) {
    // Identifica evento, código da tecla pressionada e controle que disparou evento
    var e = e || event;
    var k = e.keyCode || e.which;
    var targ = e.target || e.srcElement;
    var tamanho = targ.value.length;


    // Verifica se o tamanho máximo já foi atingido
    if ((k != 9) && (tamanho == 8))
        return false;

    // Se tamanho máximo ainda não foi atingido, verifica se tecla pressionada é número
    if (tamanho == 0)
        return true;
    else if (((k >= 48) && (k <= 57)) || (k == 9)) {
        // Se tecla pressionada for número e valor permitido para a posição, insere a ponto/traço caso necessário
        if ((tamanho == 3) && (k != 9))
            targ.value += '/';
        return true;
    }
    else
        return false;
}

function completaPADAC(e) {
    // Identifica evento, código da tecla pressionada e controle que disparou evento
    var e = e || event;
    var k = e.keyCode || e.which;
    var targ = e.target || e.srcElement;
    var tamanho = targ.value.length;

    if (tamanho < 8) {
        targ.value = targ.value.replace('/', '');
        for (i = targ.value.length; i < 7; i++) {
            targ.value = '0' + targ.value;
        }

        var s1 = targ.value.substr(0, 3);
        var s2 = targ.value.substr(3, 4);

        targ.value = s1 + '/' + s2;
    }
}


function mascaraCNPJ(e) {
    // Identifica evento, código da tecla pressionada e controle que disparou evento
    var e = e || event;
    var k = e.keyCode || e.which;
    var targ = e.target || e.srcElement;
    var tamanho = targ.value.length;

    // Verifica se o tamanho máximo já foi atingido

    //       alert(k);

    // Libera uso de teclas de movimentação de cursor (setas,home,end,tab,enter,del)
    if ((k == 9) || (k == 13) || (k == 35) || (k == 36) || (k == 37) || (k == 39) || (k == 46))
        return true;

    if ((k != 8) && (tamanho == 18))
        return false;

    // Se tamanho máximo ainda não foi atingido, verifica se tecla pressionada é número
    if (((k >= 48) && (k <= 57)) || (k == 8)) {
        // Se tecla pressionada for número e valor permitido para a posição, insere a ponto/traço caso necessário
        if (((tamanho == 2) || (tamanho == 6)) && (k != 8))
            targ.value += '.';
        else if ((tamanho == 10) && (k != 8))
            targ.value += '/';
        else if ((tamanho == 15) && (k != 8))
            targ.value += '-';
        return true;
    }
    else
        return false;
}

function completaCNPJ(e) {
    // Identifica evento, código da tecla pressionada e controle que disparou evento
    var e = e || event;
    var k = e.keyCode || e.which;
    var targ = e.target || e.srcElement;
    var tamanho = targ.value.length;

    if ((tamanho <= 18) && (targ.value != '')) {
        targ.value = targ.value.replace('.', '');
        targ.value = targ.value.replace('.', '');
        targ.value = targ.value.replace('/', '');
        targ.value = targ.value.replace('-', '');
        for (i = targ.value.length; i < 14; i++) {
            targ.value = '0' + targ.value;
        }

        var s1 = targ.value.substr(0, 2);
        var s2 = targ.value.substr(2, 3);
        var s3 = targ.value.substr(5, 3);
        var s4 = targ.value.substr(8, 4);
        var s5 = targ.value.substr(12, 2);

        targ.value = s1 + '.' + s2 + '.' + s3 + '/' + s4 + '-' + s5;
    }
}


function mascaraInteiro(e) {
    // Identifica evento, código da tecla pressionada e controle que disparou evento
    var e = e || event;
    var k = e.keyCode || e.which;
    var targ = e.target || e.srcElement;
    var tamanho = targ.value.length;

    // Se tamanho máximo ainda não foi atingido, verifica se tecla pressionada é número
    return verificaCaractereInteiro(k);
}

function verificaCaractereInteiro(k) {
    return (((k >= 48) && (k <= 57)) || (k == 8));
}



function mascaraCEP(e) {
    // Identifica evento, código da tecla pressionada e controle que disparou evento
    var e = e || event;
    var k = e.keyCode || e.which;
    var targ = e.target || e.srcElement;
    var tamanho = targ.value.length;

    // Verifica se o tamanho máximo já foi atingido
    if ((k != 8) && (tamanho == 9))
        return false;

    // Se tamanho máximo ainda não foi atingido, verifica se tecla pressionada é número
    if (((k >= 48) && (k <= 57)) || (k == 8)) {
        // Se tecla pressionada for número e valor permitido para a posição, insere a ponto/traço caso necessário
        if ((tamanho == 5) && (k != 8))
            targ.value += '-';
        return true;
    }
    else
        return false;
}

function completaCEP(e) {
    // Identifica evento, código da tecla pressionada e controle que disparou evento
    var e = e || event;
    var k = e.keyCode || e.which;
    var targ = e.target || e.srcElement;
    var tamanho = targ.value.length;

    if (tamanho < 9) {
        targ.value = targ.value.replace('.', '');
        targ.value = targ.value.replace('.', '');
        targ.value = targ.value.replace('-', '');
        for (i = targ.value.length; i < 11; i++) {
            targ.value = '0' + targ.value;
        }

        var s1 = targ.value.substr(0, 5);
        var s2 = targ.value.substr(5, 3);

        targ.value = s1 + '-' + s2;
    }
}

function MovimentaCursor(textarea, pos) {
    if (pos <= 0)
        return; //se a posição for 0 não reposiciona

    if (typeof (document.selection) != 'undefined') {
        //IE
        var oRange = textarea.createTextRange();
        var LENGTH = 1;
        var STARTINDEX = pos;

        oRange.moveStart("character", -textarea.value.length);
        oRange.moveEnd("character", -textarea.value.length);
        oRange.moveStart("character", pos);
        //oRange.moveEnd("character", pos);
        oRange.select();
        textarea.focus();
    }
    if (typeof (textarea.selectionStart) != 'undefined') {
        //FireFox
        textarea.selectionStart = pos;
        textarea.selectionEnd = pos;
    }
}


function PosicaoCursor(textarea) {
    var pos = 0;
    if (typeof (document.selection) != 'undefined') {
        //IE
        var range = document.selection.createRange();
        var i = 0;
        for (i = textarea.value.length; i > 0; i--) {
            if (range.moveStart('character', 1) == 0)
                break;
        }
        pos = i;
    }
    if (typeof (textarea.selectionStart) != 'undefined') {
        //FireFox
        pos = textarea.selectionStart;
    }

    if (pos == textarea.value.length)
        return 0; //retorna 0 quando não precisa posicionar o elemento
    else
        return pos; //posição do cursor
}

function filtraNumeros(campo) {
    var s = "";
    var cp = "";
    vr = campo;
    tam = vr.length;
    for (i = 0; i < tam; i++) {
        if (vr.substring(i, i + 1) == "0" ||
            vr.substring(i, i + 1) == "1" ||
            vr.substring(i, i + 1) == "2" ||
            vr.substring(i, i + 1) == "3" ||
            vr.substring(i, i + 1) == "4" ||
            vr.substring(i, i + 1) == "5" ||
            vr.substring(i, i + 1) == "6" ||
            vr.substring(i, i + 1) == "7" ||
            vr.substring(i, i + 1) == "8" ||
            vr.substring(i, i + 1) == "9") {
            s = s + vr.substring(i, i + 1);
        }
    }
    return s;
    //return campo.value.replace("/", "").replace("-", "").replace(".", "").replace(",", "")
}

// limpa todos caracteres que não são letras
function filtraCaracteres(campo) {
   var vr = campo;
    for (i = 0; i < tam; i++) {
        //Caracter
        if (vr.charCodeAt(i) != 32 && vr.charCodeAt(i) != 94 && (vr.charCodeAt(i) < 65 ||
        (vr.charCodeAt(i) > 90 && vr.charCodeAt(i) < 96) ||
            vr.charCodeAt(i) > 122) && vr.charCodeAt(i) < 192) {
            vr = vr.replace(vr.substr(i, 1), "");
        }
    }
    return vr;
}

function filtraCampo(campo) {
    var s = "";
    var cp = "";
   var vr = campo.value;
   var tam = vr.length;
    for (i = 0; i < tam; i++) {
        if (vr.substring(i, i + 1) != "/"
            && vr.substring(i, i + 1) != "-"
            && vr.substring(i, i + 1) != "."
            && vr.substring(i, i + 1) != "("
            && vr.substring(i, i + 1) != ")"
            && vr.substring(i, i + 1) != ":"
            && vr.substring(i, i + 1) != ",") {
            s = s + vr.substring(i, i + 1);
        }
    }
    return s;
    //return campo.value.replace("/", "").replace("-", "").replace(".", "").replace(",", "")
}


function teclaValida(tecla) {
    if (tecla == 8 //backspace
    //Esta evitando o post, quando são pressionadas estas teclas.
    //Foi comentado pois, se for utilizado o evento texchange, é necessario o post.
        || tecla == 9 //TAB
        || tecla == 27 //ESC
        || tecla == 16 //Shif TAB 
        || tecla == 45 //insert
        || tecla == 46 //delete
        || tecla == 35 //home
        || tecla == 36 //end
        || tecla == 37 //esquerda
        || tecla == 38 //cima
        || tecla == 39 //direita
        || tecla == 40)//baixo
        return false;
    else
        return true;
}

//Recupera o código da tecla que foi pressionado
function getKeyCode(evt) {
    var code;
    if (typeof (evt.keyCode) == 'number')
        code = evt.keyCode;
    else if (typeof (evt.which) == 'number')
        code = evt.which;
    else if (typeof (evt.charCode) == 'number')
        code = evt.charCode;
    else
        return 0;

    return code;
}


// Formata hora no padrao HH:MM
function formataHora(campo, evt) {
    //HH:mm
    var xPos = PosicaoCursor(campo);
    evt = getEvent(evt);
    var tecla = getKeyCode(evt);
    if (!teclaValida(tecla))
        return;

    var vr = campo.value = filtraNumeros(filtraCampo(campo));

    if (tam == 2)
        campo.value = vr.substr(0, 2) + ':';
    if (tam > 2 && tam < 5)
        campo.value = vr.substr(0, 2) + ':' + vr.substr(2);
    //    if(xPos == 2)
    //        xPos = xPos + 1;
    MovimentaCursor(campo, xPos);
}

 