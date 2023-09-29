<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wuFindPessoaCpf.ascx.cs"
    Inherits="UserControl_autocompletar_wuFindPessoaCpf" %>
<div class="controls">
    <input type="text" name="required" id="txtCPFBusca" />
    <asp:HiddenField ID="hidPes_CPF_Search" runat="server" />
</div>
<script type="text/javascript">

    var newID2 = null;


    $(document).ready(function () {
        pageLoadPessoaCPF();
    });

    $(document).ready(function () {
        PageLoadPesquisaCPF();
    });


    function pageLoadPessoaCPF() {

        if (jQuery("input[id$='hidPes_CPF_Search']").length) {
            var re = new RegExp("hidPes_CPF_Search", "ig");
            jQuery("input[id$='hidPes_CPF_Search']").each(function () {
                newID2 = jQuery(this).attr("id").replace(re, "");
            });
        }

    }

    var States_acOptions = {
        minChars: 3,
        max: 100,
        dataType: 'json', // this parameter is currently unused
        extraParams: {
            format: 'json' // pass the required context to the Zend Controller
        },
        parse: function (data) {
            var parsed = [];

            data = data.items;

            for (var i = 0; i < data.length; i++) {
                parsed[parsed.length] = {
                    data: data[i],
                    value: data[i].d0,
                    value: data[i].d2,
                    result: (data[i].d1)
                };
            }

            return parsed;
        },
        formatItem: function (item) {
            return item.d0 + ' ' + item.d1 + ' ' + item.d2;
        }
    };

    function PageLoadPesquisaCPF() {
        jQuery(document).ready(function ($) {
            $('#txtCPFBusca')
        .autocomplete('../../UserControl/autocompletar/AutoCompletarCpf.ashx', States_acOptions)
        .attr('d0', 'd1', 'd2')
        .result(function (e, data) {
            $('#' + newID2 + 'hidPes_CPF_Search').val(data.d0);
        });
        });
    }
</script>
