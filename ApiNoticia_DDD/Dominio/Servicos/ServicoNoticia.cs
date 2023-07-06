using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Entidades.Entidades;
using Entidades.Entidades.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Servicos
{
    public class ServicoNoticia : IServicoNoticia
    {
        private readonly INoticia _iNoticia;
        public ServicoNoticia(INoticia noticia)
        {
            _iNoticia = noticia;
        }
        public async Task AdicionarNoticia(Noticia noticia)
        {
            var validarTitulo = noticia.ValidarPropriedadeString(noticia.Titulo, "Titulo");
            var validarInformacao = noticia.ValidarPropriedadeString(noticia.Informacao,"Informacao");
            if (validarTitulo&&validarInformacao)
            {
                noticia.DataAlteracao = DateTime.Now;
                noticia.DataCadastro = DateTime.Now;
                noticia.Ativo = true;
                await _iNoticia.Adicionar(noticia);
            }
        }

        public async Task AtualizarNoticia(Noticia noticia)
        {
            var validarTitulo = noticia.ValidarPropriedadeString(noticia.Titulo,"Titulo");
            var validarInformacao = noticia.ValidarPropriedadeString(noticia.Informacao, "Informacao");
            if (validarTitulo&&validarInformacao)
            {
                noticia.DataAlteracao = DateTime.Now;
                noticia.DataCadastro = DateTime.Now;
                noticia.Ativo = true;
                await _iNoticia.Atualizar(noticia);
            }
        }

        public async Task<List<NoticiaViewModel>> ListarNoticiaCustomizada()
        {
            var listarNoticiasCustomizada = await _iNoticia.ListarNoticiasCustomizadas();

            var retorno = (
                from noticia in listarNoticiasCustomizada
                select new NoticiaViewModel
                {
                    Id = noticia.Id,
                    Titulo = noticia.Titulo,
                    Informacao = noticia.Informacao,
                    DataCadastro =
                     string.Concat(noticia.DataCadastro.Day, "/", noticia.DataCadastro.Month, "/", noticia.DataCadastro.Year),
                    Usuario = SeparaEmail(noticia.ApplicationUser.Email)
                }).ToList();
            return retorno;
        }

        public async Task<List<Noticia>> ListarNoticiasAtivas()
        {
            return await _iNoticia.ListarNoticias(n=> n.Ativo);
        }

        private string SeparaEmail(string Email)
        {
            var stringEmail = Email.Split("@");
            return stringEmail[0].ToString();
        }
    }
}
