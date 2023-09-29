<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wuFindPessoaRG.ascx.cs"
    Inherits="UserControl_autocompletar_wuFindPessoaRG" %>
<div class="controls">
    <input type="text" name="required" id="txtRGBusca" />
    <asp:HiddenField ID="hidPes_RG_Search" runat="server" />
</div>
<script type="text/javascript">

    var newID4 = null;

    $(document).ready(function () {
        pageLoadPessoaRG();
    });

    $(document).ready(function () {
        PageLoadPesquisaRG();
    });


    function pageLoadPessoaRG() {

        if (jQuery("input[id$='hidPes_RG_Search']").length) {
            var re = new RegExp("hidPes_RG_Search", "ig");
            jQuery("input[id$='hidPes_RG_Search']").each(function () {
                newID4 = jQuery(this).attr("id").replace(re, "");
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

    function PageLoadPesquisaRG() {

        jQuery(document).ready(function ($) {
            $('#txtRGBusca')
        .autocomplete('../../UserControl/autocompletar/AutoCompletarRG.ashx', States_acOptions)
        .attr('d0', 'd1', 'd2')
        .result(function (e, data) {
            $('#' + newID4 + 'hidPes_RG_Search').val(data.d0);
        });
        });
    }
</script>
