using Aplicacao.Interfaces;
using Entidades.Entidades;
using Entidades.Entidades.ViewModels;
using Entidades.Notificacoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NoticiaController : ControllerBase
    {
        private readonly IAplicacaoNoticia _IAplicacaoNoticia;

        public NoticiaController(IAplicacaoNoticia aplicacaoNoticia)
        {
            _IAplicacaoNoticia = aplicacaoNoticia;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/ListarNoticiasCustomizadas")]
        public async Task<List<NoticiaViewModel>> ListarNoticiasCustomizadas()
        {
            return await _IAplicacaoNoticia.ListarNoticiasCustomizadas();
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/ListarNoticias")]
        public async Task<List<Noticia>> ListarNoticias()
        {
            return await _IAplicacaoNoticia.ListarNoticiasAtivas();
        }
        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/AdicionarNoticia")]
        public async Task<List<Notifica>> AdicionarNoticia(NoticiaModel noticia)
        {
            var noticiaNova = new Noticia();
            noticiaNova.Titulo = noticia.Titulo;
            noticiaNova.Informacao = noticia.Informacao;
            noticiaNova.UserId = await RetornarIdUsuarioLogado();
            await _IAplicacaoNoticia.AdicionarNoticia(noticiaNova);
            return noticiaNova.Notificacoes;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/AtualizaNoticia")]
        public async Task<List<Notifica>> AtualizaNoticia(NoticiaModel noticia)
        {
            var noticiaNova = await _IAplicacaoNoticia.BuscarPorId(noticia.IdNoticia);
            noticiaNova.Titulo = noticia.Titulo;
            noticiaNova.Informacao = noticia.Informacao;
            noticiaNova.UserId = await RetornarIdUsuarioLogado();
            await _IAplicacaoNoticia.AtualizarNoticia(noticiaNova);

            return noticiaNova.Notificacoes;
        }
        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/ExcluirNoticia")]
        public async Task<List<Notifica>> ExcluirNoticia(NoticiaModel noticia)
        {
            var noticiaExcluir = await _IAplicacaoNoticia.BuscarPorId(noticia.IdNoticia);
            await _IAplicacaoNoticia.Excluir(noticiaExcluir);
            return noticiaExcluir.Notificacoes;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/BuscarPorId")]
        public async Task<Noticia> BuscarPorId(NoticiaModel noticia)
        {
            return await _IAplicacaoNoticia.BuscarPorId(noticia.IdNoticia);
        }

        private async Task<string> RetornarIdUsuarioLogado()
        {
            if (User != null)
            {
                var idUsuario = User.FindFirst("idUsuario");
                return idUsuario.Value;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
