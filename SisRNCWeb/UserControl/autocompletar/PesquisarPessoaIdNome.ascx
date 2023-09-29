<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PesquisarPessoaIdNome.ascx.cs"
    Inherits="UserControl_autocompletar_PesquisarPessoaIdNome" %>
<div class="controls">
    <input type="text" name="required" id="txtPessoaAutosid2" />
    <asp:HiddenField ID="hidPes_Id2_Search" runat="server" />
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

        if (jQuery("input[id$='hidPes_Id2_Search']").length) {
            var re = new RegExp("hidPes_Id2_Search", "ig");
            jQuery("input[id$='hidPes_Id2_Search']").each(function () {
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

    function PageLoadPesquisas() {
        jQuery(document).ready(function ($) {
            $('#txtPessoaAutosid2')
        .autocomplete('../../UserControl/autocompletar/AutoCompletePessoa.ashx', States_acOptions)
         .attr('d0', 'd1', 'd2')
        .result(function (e, data) {
            $('#' + newID2 + 'hidPes_Id2_Search').val(data.d0);
        });
        });
    }
</script>
