using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Helpers
{
    public static class ErrorMessages
    {
        public const string ERRO_GENERICO = "Ocorreu um erro ao processar a requisição, favor contate o responsável da área !";

        //User Erro
        public const string ERROR_FIRST_NAME = "Nome é Obrigatorio !";
        public const string ERROR_LAST_NAME = "Sobre Nome Obrigatorio !";
        public const string ERROR_SOCIAL_NUMBER = "Obrigatorio numero CPF !";
        public const string ERROR_CELLPHONE = "Numero de Celular Obrigatorio !";
        public const string ERROR_EMAIL_NULL = "Obrigatorio colocar Email !";
        public const string ERROR_EMAIL = "Formato Email incorreto !";
        public const string ERROR_PASSWORD = "Obrigatorio colocar uma senha !";
        public const string ERROR_LENGTH_PASSWORD = "Senha deve ter no minimo 8 caracteres !";
        public const string CPF_CNPJ_CADASTRADA = "Usuario já cadastrado !";
        public const string USUARIO_ERRO = "Usuario ou senha invalido !";
        public const string TOKEN_ERRO = "Token Invalido";
    }
}
