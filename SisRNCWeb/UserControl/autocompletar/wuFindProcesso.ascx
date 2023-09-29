<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wuFindProcesso.ascx.cs"
    Inherits="UserControl_autocompletar_wuFindProcesso" %>
<div class="controls">
    <input type="text" name="required" id="txtProcesso" />
    <asp:HiddenField ID="hidProcesso_Search" runat="server" />
</div>
<script type="text/javascript">

    var newID3 = null;

    $(document).ready(function () {
        pageLoadProcesso();
    });

    $(document).ready(function () {
        PageLoadPesquisaProcesso();
    });


    function pageLoadProcesso() {

        if (jQuery("input[id$='hidProcesso_Search']").length) {
            var re = new RegExp("hidProcesso_Search", "ig");
            jQuery("input[id$='hidProcesso_Search']").each(function () {
                newID3 = jQuery(this).attr("id").replace(re, "");
            });
        }

    }

    var States_acOptions = {
        minChars: 6,
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

    function PageLoadPesquisaProcesso() {
        jQuery(document).ready(function ($) {
            $('#txtProcesso')
        .autocomplete('../../UserControl/autocompletar/AutoCompletarProcesso.ashx', States_acOptions)
        .attr('d0', 'd1', 'd2')
        .result(function (e, data) {
            $('#' + newID3 + 'hidProcesso_Search').val(data.d1);
        });
        });
    }
</script>
