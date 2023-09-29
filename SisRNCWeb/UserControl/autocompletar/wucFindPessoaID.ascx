<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucFindPessoaID.ascx.cs"
    Inherits="wucFindPessoaID" %>
<div class="controls">
    <input type="text" name="required" id="txtPessoaAuto" />
    <asp:HiddenField ID="hidPes_Nome_Search" runat="server" />
</div>
<script type="text/javascript">

    var newID1 = null;

    $(document).ready(function () {
        pageLoadPessoa();
    });

    $(document).ready(function () {
        PageLoadPesquisa();
    });

    jQuery(document).ready(function () {
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(PageLoadPesquisa);
        PageLoadPesquisa();
    });

    jQuery(document).ready(function () {
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(pageLoadPessoa);
        pageLoadPessoa();
    });


    function pageLoadPessoa() {

        if (jQuery("input[id$='hidPes_Nome_Search']").length) {
            var re = new RegExp("hidPes_Nome_Search", "ig");
            jQuery("input[id$='hidPes_Nome_Search']").each(function () {
                newID1 = jQuery(this).attr("id").replace(re, "");
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

    function PageLoadPesquisa() {
        jQuery(document).ready(function ($) {
            $('#txtPessoaAuto')
        .autocomplete('../../UserControl/autocompletar/AutoCompletePessoa.ashx', States_acOptions)
        .attr('d0', 'd1', 'd2')
        .result(function (e, data) {
            $('#' + newID1 + 'hidPes_Nome_Search').val(data.d0);
        });
        });
    }
</script>
