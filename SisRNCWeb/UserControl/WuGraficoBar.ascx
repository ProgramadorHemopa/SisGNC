<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WuGraficoBar.ascx.cs" Inherits="UserControl_WuGraficoBar" %>
<script type="text/javascript">
    function ShowGrafico(message) {
        // Use Morris.Bar
        Morris.Bar({
            element: 'graph',
            data: [
              { x: 'Ademar.Silva', y: 3, z: 2, a: 3 },
              { x: 'Aline.Coelho', y: 2, z: null, a: 1 },
              { x: 'Aline.Lima', y: 0, z: 2, a: 4 },
              { x: 'Raquel.Ribeiro', y: 2, z: 4, a: 3 }
            ],
            xkey: 'x',
            ykeys: ['y', 'z', 'a'],
            labels: ['Y', 'Z', 'A']
        }).on('click', function (i, row) {
            console.log(i, row);
        });
    };
</script>
