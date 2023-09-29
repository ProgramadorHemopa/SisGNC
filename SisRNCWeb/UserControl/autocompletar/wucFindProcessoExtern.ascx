<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucFindProcessoExtern.ascx.cs"
    Inherits="wucFindProcessoExtern" %>
    <input id="txtwucFindProcessoExternoAuto"/><asp:HiddenField ID="hidPes_wucFindProcessoExterno_Search" runat="server" />

<script type="text/javascript">

    var newID2 = null;

    // function pageLoad() {

    if (jQuery("input[id$='hidPes_wucFindProcessoExterno_Search']").length) {
        var re = new RegExp("hidPes_wucFindProcessoExterno_Search", "ig");
        jQuery("input[id$='hidPes_wucFindProcessoExterno_Search']").each(function() {
            newID2 = jQuery(this).attr("id").replace(re, "");
        });
    }

    //}

    var PE_acOptions = {
        minChars: 3,
        max: 100,
        dataType: 'json', // this parameter is currently unused
        extraParams: {
            format: 'json' // pass the required context to the Zend Controller
        },
        parse: function(data) {
            var parsed = [];

            data = data.items;

            for (var i = 0; i < data.length; i++) {
                parsed[parsed.length] = {
                     data: data[i],
                    value: data[i].d0,
                   result: (data[i].d1)
                };
            }

            return parsed;
        },
        formatItem: function(item) {
            return item.d1;
        }
    };

    jQuery(document).ready(function($) {
    $('#txtwucFindProcessoExternoAuto')
        .autocomplete('../../UserControl/autocompletar/AutoCompleteProcessoExtern.ashx', PE_acOptions)
        .attr('d0','d1')
        .result(function(e, data) {
    $('#' + newID2 + 'hidPes_wucFindProcessoExterno_Search').val(data.d1);
        });
    });

</script>

