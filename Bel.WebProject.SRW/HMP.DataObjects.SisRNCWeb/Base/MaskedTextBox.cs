using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;


namespace APB.Mercury.DataBridge.SCPWeb
{
    public enum MaskedTextBoxType { CPF, Data, Hora, CEP, Moeda, Inteiro, CNPJ, TamanhoMaximo, ProcessoExterno, ProcessoInterno, ProcessoCNJ, APF, ProcessoSegundoGrau, ProcessoVEC, NumeroPADAC }
    /// <summary>
    /// Summary description for MaskedTextBox.
    /// </summary>
    [DefaultProperty("Text"),
        ToolboxData("<{0}:MaskedTextBox runat=server></{0}:MaskedTextBox>")]
    public class MaskedTextBox : TextBox
    {
        private MaskedTextBoxType _maskType;

        [Bindable(true),
            Category("Appearance"),
            DefaultValue("")]
        public MaskedTextBoxType MaskType
        {
            get
            {
                return _maskType;
            }

            set
            {
                _maskType = value;
            }
        }

        public override string Text
        {
            set
            {
                if (_maskType == MaskedTextBoxType.Data)
                {
                    if (value != DateTime.MinValue.ToString("d"))
                        base.Text = value;
                }
                else
                    base.Text = value;
            }
            get { return base.Text; }
        }

        /// <summary> 
        /// Render this control to the output parameter specified.
        /// </summary>
        /// <param name="output"> The HTML writer to write out to </param>
        protected override void Render(HtmlTextWriter output)
        {
            switch (_maskType)
            {
                /* 
                 * [TO-DO]
                 * Falta fazer verificações em CEP, Moeda e CNPJ para
                 * evitar a inserção de caracteres inválidos através do "Copiar & Colar". */
                case MaskedTextBoxType.Data:
                    output.AddAttribute("onkeypress", "javascript:return mascaraData(event);");
                    output.AddAttribute("onblur", "javascript:var bErro=false;if(this.value.length==0){bErro=false;}else if (this.value.length == 9){for(i=0;i<9;i++){if(((i==2)||(i==4))&&(this.value.charCodeAt(i)!=47)){bErro=true;break;}else{bErro=false;}}}else if (this.value.length == 8){for(i=0;i<8;i++){if(((i==1)||(i==3))&&(this.value.charCodeAt(i)!=47)){bErro=true;break;}else{bErro=false;}}}else if(this.value.length!=10){bErro=true;}else{for(i=0;i<10;i++){if(((i==2)||(i==5))&&(this.value.charCodeAt(i)!=47)){bErro=true;break;}else{bErro=!verificaCaractereData(this.value.charCodeAt(i), i, this.value.substr(0,i+1));}}}if(bErro){alert(\"Formato de data inválido! Favor preencher um valor no formato correto.\");this.value = \"\"}");
                    break;
                case MaskedTextBoxType.Hora:
                    output.AddAttribute("onkeypress", "javascript:return mascaraHora(event);");
                    break;
                case MaskedTextBoxType.CPF:
                    output.AddAttribute("onkeypress", "javascript:return mascaraCPF(event);");
                    output.AddAttribute("onblur", "javascript:var bErro=false;if(this.value.length>14)bErro=true;else{for(i=0;i<this.value.length;i++){if((((i==3)||(i==7))&&(this.value.charCodeAt(i)!=46))||((i==11)&&(this.value.charCodeAt(i)!=45))||(((i!=3)&&(i!=7)&&(i!=11))&&(!verificaCaractereInteiro(this.value.charCodeAt(i))))){bErro=true;break;}}}if(bErro){}else completaCPF(event);");
                    break;
                case MaskedTextBoxType.ProcessoExterno:
                    output.AddAttribute("onkeypress", "javascript:return mascaraPEPrimeiro(event);");
                    output.AddAttribute("onblur", "javascript:var bErro=false;if(this.value.length>18)bErro=true;else{for(i=0;i<this.value.length;i++){if((((i==5)||(i==8))&&(this.value.charCodeAt(i)!=46))||((i==3)&&(this.value.charCodeAt(i)!=47))((i==16)&&(this.value.charCodeAt(i)!=45))||(((i!=3)&&(i!=5)&&(i!=8)&&(i!=16))&&(!verificaCaractereInteiro(this.value.charCodeAt(i))))){bErro=true;break;}}}if(bErro){}else completaPEPrimeiro(event);");
                    break;
                case MaskedTextBoxType.ProcessoCNJ:
                    output.AddAttribute("onkeypress", "javascript:return mascaraCNJ(event);");
                    output.AddAttribute("onblur", "javascript:var bErro=false;if(this.value.length>24)bErro=true;else{for(i=0;i<this.value.length;i++){if((((i==10)||(i==15)||(i==19))&&(this.value.charCodeAt(i)!=46))||((i==7)&&(this.value.charCodeAt(i)!=45))||(((i!=10)&&(i!=15)&&(i!=19)&&(i!=7))&&(!verificaCaractereInteiro(this.value.charCodeAt(i))))){bErro=true;break;}}}if(bErro){}else completaCNJ(event);");
                    break;
                case MaskedTextBoxType.ProcessoSegundoGrau:
                    output.AddAttribute("onkeypress", "javascript:return mascaraSegundoGrau(event);");
                    output.AddAttribute("onblur", "javascript:var bErro=false;if(this.value.length>15)bErro=true;else{for(i=0;i<this.value.length;i++){if((((i==4)||(i==6))&&(this.value.charCodeAt(i)!=46))||((i==13)&&(this.value.charCodeAt(i)!=45))||(((i!=4)&&(i!=6)&&(i!=13))&&(!verificaCaractereInteiro(this.value.charCodeAt(i))))){bErro=true;break;}}}if(bErro){}else completaSegundoGrau(event);");
                    break;
                case MaskedTextBoxType.ProcessoInterno:
                    output.AddAttribute("onkeypress", "javascript:return mascaraPI(event);");
                    output.AddAttribute("onblur", "javascript:var bErro=false;if(this.value.length>14)bErro=true;else{for(i=0;i<this.value.length;i++){if(((i==9)&&(this.value.charCodeAt(i)!=46))||(((i!=9))&&(!verificaCaractereInteiro(this.value.charCodeAt(i))))){bErro=true;break;}}}if(bErro){}else completaPI(event);");
                    break;
                case MaskedTextBoxType.NumeroPADAC:
                    output.AddAttribute("onkeypress", "javascript:return mascaraPADAC(event);");
                    output.AddAttribute("onblur", "javascript:var bErro=false;if(this.value.length>8)bErro=true;else{for(i=0;i<this.value.length;i++){if(((i==3)&&(this.value.charCodeAt(i)!=46))||(((i!=3))&&(!verificaCaractereInteiro(this.value.charCodeAt(i))))){bErro=true;break;}}}if(bErro){}else completaPADAC(event);");
                    break;
                case MaskedTextBoxType.APF:
                    output.AddAttribute("onkeypress", "javascript:return mascaraAPF(event);");
                    output.AddAttribute("onblur", "javascript:var bErro=false;if(this.value.length>17)bErro=true;else{for(i=0;i<this.value.length;i++){if(((i==8)&&(this.value.charCodeAt(i)!=46))||(((i==3)||(i==15))&&(this.value.charCodeAt(i)!=45))||((i!=3)&&(i!=8)&&(i!=15))&&(!verificaCaractereInteiro(this.value.charCodeAt(i)))){bErro=true;break;}}}if(bErro){}else completaAPF(event);");
                    break;
                case MaskedTextBoxType.CEP:
                    output.AddAttribute("onkeypress", "javascript:return mascaraCEP(event);");
                    output.AddAttribute("onblur", "javascript:var bErro=false;if(this.value.length>9)bErro=true;else{for(i=0;i<this.value.length;i++){if(((i==5)&&(this.value.charCodeAt(i)!=46))||((i!=5)&&(!verificaCaractereInteiro(this.value.charCodeAt(i))))){bErro=true;break;}}}if(bErro){}else completaCEP(event);");
                    break;
                case MaskedTextBoxType.Moeda:
                    output.AddAttribute("onkeypress", "javascript:return mascaraMoeda(event);");
                    break;
                case MaskedTextBoxType.Inteiro:
                    output.AddAttribute("onkeypress", "javascript:return mascaraInteiro(event);");
                    output.AddAttribute("onblur", "javascript:for(i=0;i<this.value.length;i++){if(!verificaCaractereInteiro(this.value.charCodeAt(i))){alert(\"O Valor Informado não é um número inteiro válido!\");this.value=\"\";break;}};");
                    break;
                case MaskedTextBoxType.CNPJ:
                    output.AddAttribute("onkeypress", "return mascaraCNPJ(event);");
                    output.AddAttribute("onblur", "completaCNPJ(event)");
                    break;
                case MaskedTextBoxType.ProcessoVEC:
                    output.AddAttribute("onkeypress", "javascript:return mascaraPEVec(event);");
                    output.AddAttribute("onblur", "javascript:var bErro=false;if(this.value.length>18)bErro=true;else{for(i=0;i<this.value.length;i++){if((((i==5)||(i==8))&&(this.value.charCodeAt(i)!=46))||((i==3)&&(this.value.charCodeAt(i)!=47))((i==16)&&(this.value.charCodeAt(i)!=45))||(((i!=3)&&(i!=5)&&(i!=8)&&(i!=16))&&(!verificaCaractereInteiro(this.value.charCodeAt(i))))){bErro=true;break;}}}if(bErro){}");
                    break;
                case MaskedTextBoxType.TamanhoMaximo:
                    if (this.TextMode == TextBoxMode.MultiLine)
                    {
                        string sId = "__CARACTERESRESTANTES_" + this.ClientID;
                        output.AddAttribute("onkeydown", "javascript:function noNumbers(e){var e=e||event;var k=e.keyCode||e.which;var targ=e.target||e.srcElement;if(k==8||k==46)document.getElementById(\"" + sId + "\").innerHTML = (" + this.MaxLength + "-targ.value.length+(targ.value.length==0?0:1)).toString();} noNumbers(event);");
                        output.AddAttribute("onkeypress", "javascript:if (this.value.length >= " + this.MaxLength + "){return false;}; document.getElementById(\"" + sId + "\").innerHTML = (" + this.MaxLength + "-this.value.length-1).toString();");
                        output.AddAttribute("onblur", "javascript:if (this.value.length >= " + this.MaxLength + "){alert(\"AVISO: O tamanho máximo de " + this.MaxLength + " caracteres permitido para este campo foi atingido. Os caracteres excedentes serão truncados automaticamente!\");this.value = this.value.substr(0, " + this.MaxLength + ");} document.getElementById(\"" + sId + "\").innerHTML = (" + this.MaxLength + "-this.value.length).toString();");
                        output.Write("<span style=\"font-family: Courier New; font-size: 8pt; color: #999999;\">Caracteres restantes:</span>&nbsp;<span id=\"" + sId + "\" style=\"font-family: Courier New; font-size: 8pt; color: #999999;\">" + (this.MaxLength - this.Text.Length).ToString() + "</span>");
                        output.WriteBreak();
                    }
                    else if (this.TextMode == TextBoxMode.SingleLine)
                    {
                        string sId = "__CARACTERESRESTANTES_" + this.ClientID;
                        output.AddAttribute("onkeypress", "javascript:if (this.value.length >= " + this.MaxLength + "){return false;}; document.getElementById(\"" + sId + "\").innerHTML = (" + this.MaxLength + "-this.value.length-1).toString();");
                        output.AddAttribute("onblur", "javascript:if (this.value.length >= " + this.MaxLength + "){alert(\"AVISO: O tamanho máximo de " + this.MaxLength + " caracteres permitido para este campo foi atingido. Os caracteres excedentes serão truncados automaticamente!\");this.value = this.value.substr(0, " + this.MaxLength + ");} document.getElementById(\"" + sId + "\").innerHTML = (" + this.MaxLength + "-this.value.length).toString();");
                        output.Write("<span style=\"font-family: Courier New; font-size: 8pt; color: #999999;\">Caracteres restantes:</span>&nbsp;<span id=\"" + sId + "\" style=\"font-family: Courier New; font-size: 8pt; color: #999999;\">" + (this.MaxLength - this.Text.Length).ToString() + "</span>");
                        output.WriteBreak();
                    }
                    break;
            }
            base.Render(output);
        }
    }
}
