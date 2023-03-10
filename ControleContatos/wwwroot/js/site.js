// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    getDatatable('#table-contatos');
    getDatatable('#table-usuarios');
    getDatatable('#table-produtos');
    getDatatable('#table-fotos-produtos');
    

    $('.btn-total-contatos').click(function () {
        var usuarioId = $(this).attr('usuario-id');

        $.ajax({
            type: 'GET',
            url: '/Usuario/ListarContatosPorUsuarioId/' + usuarioId,
            success: function (result) {
                $("#listaContatosUsuario").html(result);
                getDatatable('#table-contatos-usuario');
            }
        });
    });

    $('.btn-modal-imagem').click(function () {
        var produtoId = $(this).attr('produto-id');       

        $.ajax({
            type: 'GET',
            url: '/Produto/ListarFotosProdutosPorId/' + produtoId,
            success: function (result) {
                $("#listaFotosProduto").html(result);
                getDatatable('#table-fotos-produtos');
                document.getElementById(produtoId);
            }
        });
    });

    $('.btn-adicionar-imagem').click(function () {
        var produtoId = $(this).attr('produto-id');           
        //event.preventDefault() faz com que a tela nao seja atualizada totalmente
        // “<%=request.getSession().getAttribute('sessaoProduto')%>”

        var fotoProduto = {
            produtoId: "<%=request.getSession().getAttribute('sessaoProduto') %>",
            descricao: document.getElementById("descricao").value,
            imagemUrl: document.getElementById("imagemUrl").value
        }

        event.preventDefault();

        $.ajax({
            type: 'POST',
            url: '/Produto/AdicionarFoto/',
            data: fotoProduto,
            success: function (result) {
                $("#listaFotosProduto").html(result);
                getDatatable('#table-fotos-produtos');
            }
        });
    });
})

function getDatatable(id) {
    $(id).DataTable({
        "ordering": true,
        "paging": true,
        "searching": true,
        "oLanguage": {
            "sEmptyTable": "Nenhum registro encontrado na tabela",
            "sInfo": "Mostrar _START_ at&eacute; _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrar 0 at&eacute; 0 de 0 Registros",
            "sInfoFiltered": "(Filtrar de _MAX_ total registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Mostrar _MENU_ registros por pagina",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Pesquisar",
            "oPaginate": {
                "sNext": "Proximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "Ultimo"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        }
    });
}


$('.close-alert').click(function () {
    $(".alert").hide('hide');
});

$('.btn-close-modal').click(function () {
    $(".modalContatosUsuario").hidden('hidden');
});

$(document).ready(function () {
    setTimeout(function () {
        $(".alert").fadeOut("slow", function () {
            $(this).alert('close');
        });
    }, 5000);
});

function getSessionProdutoId() {
    var name = '<%=session.getAttribute("produtoId")%>';
    alert(name);
};