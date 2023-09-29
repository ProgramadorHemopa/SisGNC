<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PesquisarPessoaId.ascx.cs"
    Inherits="UserControl_autocompletar_PesquisarPessoaId" %>
<div class="controls">
    <input type="text" name="required" id="txtPessoaAutosid" />
    <asp:HiddenField ID="hidPes_Id_Search" runat="server" />
</div>
<script type="text/javascript">

    var newID2 = null;

    $(document).ready(function () {
        pageLoadPessoas();
    });

    $(document).ready(function () {
        PageLoadPesquisas();
    });

    jQuery(document).ready(function () {
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(PageLoadPesquisas);
        PageLoadPesquisas();
    });


    jQuery(document).ready(function () {
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(pageLoadPessoas);
        pageLoadPessoas();
    });

    function pageLoadPessoas() {

        if (jQuery("input[id$='hidPes_Id_Search']").length) {
            var re = new RegExp("hidPes_Id_Search", "ig");
            jQuery("input[id$='hidPes_Id_Search']").each(function () {
                newID2 = jQuery(this).attr("id").replace(re, "");
            });
        }

    }


    var States_acOptions = {
        minChars: 0,
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
                    value: data[i].d0,
                    result: (data[i].d0)
                };
            }

            return parsed;
        },
        formatItem: function (item) {
            return item.d0 + ' ' + item.d0 + ' ' + item.d0;
        }
    };

    function PageLoadPesquisas() {
        jQuery(document).ready(function ($) {
            $('#txtPessoaAutosid')
        .autocomplete('../../UserControl/autocompletar/PesquisaPessoaId.ashx', States_acOptions)
        .attr('d0', 'd0', 'd0')
        .result(function (e, data) {
            $('#' + newID2 + 'hidPes_Id_Search').val(data.d0);
        });
        });
    }
</script>
