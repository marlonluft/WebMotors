webMotorsApp.controller('anuncioController', function ($scope, $http, toastr) {

    const EnumTela = {
        CONSULTA: 0,
        EDICAO: 1,
    }

    $scope.Editar = (anuncio) => {
        IncluirEditar(anuncio);
    }

    $scope.Remover = (anuncio) => {

        $http({
            method: 'DELETE',
            url: "Anuncio/" + anuncio.id,
            headers: {
                'Content-Type': "application/json; charset=utf-8"
            }
        }).then(function (response) {
            if (ValidarRetorno(response, toastr)) {

                var data = response.data;
                var idRemovido = angular.copy(data);

                $scope.Dados.ListaAnuncios = $scope.Dados.ListaAnuncios.filter((anuncio) => {
                    return anuncio.id != idRemovido
                });
            }
        }, function (response) {
            ValidarRetorno(response, toastr);
        });

    }

    $scope.Incluir = () => {
        IncluirEditar(null);
    }

    $scope.Gravar = (anuncio) => {

        toastr.clear();

        if (anuncio.id > 0) {
            $http({
                method: 'PUT',
                url: "Anuncio",
                data: JSON.stringify(anuncio),
                headers: {
                    'Content-Type': "application/json; charset=utf-8"
                }
            }).then(function (response) {
                if (ValidarRetorno(response, toastr)) {

                    $scope.Dados.ListaAnuncios = $scope.Dados.ListaAnuncios.map((anuncio) => {

                        if (anuncio.id == $scope.Dados.Edicao.id) {
                            anuncio = angular.copy($scope.Dados.Edicao);
                        }

                        return anuncio;
                    });

                    $scope.MostrarConsulta();
                }
            }, function (response) {
                ValidarRetorno(response, toastr);
            });
        }
        else {
            $http({
                method: 'POST',
                url: "Anuncio",
                data: JSON.stringify(anuncio),
                headers: {
                    'Content-Type': "application/json; charset=utf-8"
                }
            }).then(function (response) {
                if (ValidarRetorno(response, toastr)) {

                    var data = response.data;

                    anuncio.id = angular.copy(data);
                    $scope.Dados.ListaAnuncios.push(anuncio);

                    $scope.MostrarConsulta();

                }
            }, function (response) {
                ValidarRetorno(response, toastr);
            });
        }
    }

    $scope.MostrarEdicao = () => {
        IncluirEditar(null)
    }

    function IncluirEditar(anuncio) {
        if (anuncio != null) {

            $scope.Dados.Edicao = angular.copy(anuncio);

            $scope.ConsultarModelos($scope.Dados.Edicao.marca, () => {
                $scope.ConsultarVersao($scope.Dados.Edicao.modelo);
            });
        }

        $scope.Dados.Tela = EnumTela.EDICAO;
    }

    $scope.MostrarConsulta = () => {
        LimparEdicao();
        $scope.Dados.Tela = EnumTela.CONSULTA;
    }

    function Consultar() {

        $http({
            method: 'GET',
            url: "Anuncio",
            headers: {
                'Content-Type': "application/json; charset=utf-8"
            }
        }).then(function (response) {
            if (ValidarRetorno(response, toastr)) {

                var data = response.data;
                $scope.Dados.ListaAnuncios = angular.copy(data);

            }
        }, function (response) {
            ValidarRetorno(response, toastr);
        });

    };

    function ConsultarMarcas() {

        $http({
            method: 'GET',
            url: "http://desafioonline.webmotors.com.br/api/OnlineChallenge/Make",
            headers: {
                'Content-Type': "application/json; charset=utf-8"
            }
        }).then(function (response) {
            if (ValidarRetorno(response, toastr)) {

                var data = response.data;
                $scope.Dados.Marcas = angular.copy(data);

            }
        }, function (response) {
            ValidarRetorno(response, toastr);
        });

    }

    $scope.ConsultarModelos = (marcaNome, callback) => {

        toastr.clear();

        if (marcaNome) {

            var marcaId = $scope.Dados.Marcas.filter((marca) => marca.Name === marcaNome)[0].ID;

            $http({
                method: 'GET',
                url: "http://desafioonline.webmotors.com.br/api/OnlineChallenge/Model",
                params: {
                    MakeID: marcaId
                },
                headers: {
                    'Content-Type': "application/json; charset=utf-8"
                }
            }).then(function (response) {
                if (ValidarRetorno(response, toastr)) {

                    var data = response.data;
                    $scope.Dados.Modelos = angular.copy(data);

                    if (callback != null) {
                        callback();
                    }
                    else {
                        LimparEdicaoVersao();
                    }                    
                }
            }, function (response) {
                ValidarRetorno(response, toastr);
                LimparEdicaoModelo();
                LimparEdicaoVersao();
            });
        }
        else {
            LimparEdicaoModelo();

            if (callback == null) {
                LimparEdicaoVersao();
            }            
        }
    }

    $scope.ConsultarVersao = (modeloNome) => {

        toastr.clear();

        if (modeloNome) {

            var modeloId = $scope.Dados.Modelos.filter((modelo) => modelo.Name === modeloNome)[0].ID;

            $http({
                method: 'GET',
                url: "http://desafioonline.webmotors.com.br/api/OnlineChallenge/Version",
                params: {
                    ModelID: modeloId
                },
                headers: {
                    'Content-Type': "application/json; charset=utf-8"
                }
            }).then(function (response) {
                if (ValidarRetorno(response, toastr)) {

                    var data = response.data;
                    $scope.Dados.Versoes = angular.copy(data);

                }
            }, function (response) {
                ValidarRetorno(response, toastr);
            });
        }
    }

    function Limpar() {
        $scope.Dados = {
            ListaAnuncios: [],
            Tela: EnumTela.CONSULTA,
            Edicao: null,
            Marcas: [],
            Modelos: [],
            Versoes: [],
        };

        LimparEdicao();
    }

    function LimparEdicao() {
        $scope.Dados.Edicao = {
            id: 0,
            marca: '',
            modelo: '',
            versao: '',
            ano: 0,
            quilometragem: 0,
            observacao: '',
        };

        LimparEdicaoModelo();
        LimparEdicaoVersao();
    }

    function LimparEdicaoModelo() {

        $scope.Dados.Edicao.modelo = '';
        $scope.Dados.Modelos = [];
    }

    function LimparEdicaoVersao() {

        $scope.Dados.Edicao.versao = '';
        $scope.Dados.Versoes = [];
    }

    Limpar();
    Consultar();
    ConsultarMarcas();
});