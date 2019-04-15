$(function () {//Cria uma tabela com o retorno da chamada do metodo do controller, não se faz necessário o uso de libs para criar a tabela neste caso.
    function gerarGrid(updata) {
        $("#tbDta tbody tr").remove();
        let valorTotal = 0;
        $.each($.parseJSON(updata), function (i, item) {
            valorTotal = valorTotal + (item.Preco * item.Quantidade);
            var $tr = $('<tr>').append(
                $('<td>').text(item.Comprador),
                $('<td>').text(item.Descricao),
                $('<td>').text(item.Preco.toFixed(2)),
                $('<td>').text(item.Quantidade),
                $('<td>').text(item.Endereco),
                $('<td>').text(item.Fornecedor)
            ).appendTo('#tbDta');
        });

        total(valorTotal);
    }
    $("#formulario").submit(function (e) {//Modo mais rapido de processar e recuperar as informações para esta situação
        var formData = new FormData(this);
        e.preventDefault();
        formData.append('file', $('#edtFile')[0].files[0]);//Força o navegador a pegar o aquivo do formulario
        $.ajax({
            url: window.location.href.indexOf("Home") !== -1 ? 'FileUpload' : '/Home/FileUpload',//a url em cenário de debug pode sofrer alteraçãoes. 
            type: 'POST',
            data: formData,
            success: function (data) {
                gerarGrid(data);
            },
            cache: false,
            contentType: false,
            processData: false,
            xhr: function () { // Custom XMLHttpRequest
                var myXhr = $.ajaxSettings.xhr();
                if (myXhr.upload) { // Avalia se tem suporte a propriedade upload
                    myXhr.upload.addEventListener('progress', function () {
                        /* faz alguma coisa durante o progresso do upload */
                    }, false);
                }
                return myXhr;
            }
        });
    });

    //Apenas para colocar o nome do arquivo abaixo do btn
    const uploadButton = document.querySelector('.browse-btn');
    const fileInfo = document.querySelector('.file-info');
    const realInput = document.getElementById('edtFile');
    uploadButton.addEventListener('click', () => {
        realInput.click();
    });
    realInput.addEventListener('change', () => {
        const name = realInput.value.split(/\\|\//).pop();
        const truncated = name.length > 20
            ? name.substr(name.length - 20)
            : name;

        fileInfo.innerHTML = truncated;
    });


    function total(value) {
        $("#lblTotal").text(parseFloat(value).toFixed(2) + " Reais.");
    }



});


