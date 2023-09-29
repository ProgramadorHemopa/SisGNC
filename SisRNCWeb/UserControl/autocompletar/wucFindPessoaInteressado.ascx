﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucFindPessoaInteressado.ascx.cs"
    Inherits="wucFindPessoaInteressado" %>
<div class="controls">
    <input type="text" name="required" id="txtPessoaInteressado" />
    <asp:HiddenField ID="hidPes_Nome_Interessado" runat="server" />
</div>
<script type="text/javascript">

    var newID1 = null;

    $(document).ready(function () {
        pageLoadPessoa();
    });

    $(document).ready(function () {
        PageLoadPesquisa();
    });

    function pageLoadPessoa() {

        if (jQuery("input[id$='hidPes_Nome_Interessado']").length) {
            var re = new RegExp("hidPes_Nome_Interessado", "ig");
            jQuery("input[id$='hidPes_Nome_Interessado']").each(function () {
                newID1 = jQuery(this).attr("id").replace(re, "");
            });
        }

    }

    var States_acOptions = {
        minChars: 1,
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
                    value: data[i].d2,
                    value: data[i].d0,
                    result: (data[i].d1)
                };
            }

            return parsed;
        },
        formatItem: function (item) {
            return item.d0 + ' ' + item.d1 + ' ' + item.d2;
        }
    };

    function PageLoadPesquisa() {
        jQuery(document).ready(function ($) {
            $('#txtPessoaInteressado')
        .autocomplete('../../UserControl/autocompletar/AutoCompletePessoa.ashx', States_acOptions)
        .attr('d0', 'd1', 'd2')
        .result(function (e, data) {
            $('#' + newID1 + 'hidPes_Nome_Interessado').val(data.d1);
        });
        });
    }
</script>
