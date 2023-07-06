using Aplicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Entidades.Entidades;
using Entidades.Entidades.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Aplicacoes
{
    public class AplicacaoNoticia: IAplicacaoNoticia
    {
        INoticia _INoticia;
        IServicoNoticia _iservicoNoticia;
        public AplicacaoNoticia(INoticia inoticia, IServicoNoticia iserviconoticia)
        {
            _INoticia = inoticia;
            _iservicoNoticia = iserviconoticia;
        }

        public async Task Adicionar(Noticia Object)
        {
             await _INoticia.Adicionar(Object);
        }

        public async Task AdicionarNoticia(Noticia noticia)
        {
            await _iservicoNoticia.AdicionarNoticia(noticia);
        }

        public async Task Atualizar(Noticia Object)
        {
            await _INoticia.Atualizar(Object);
        }

        public async Task AtualizarNoticia(Noticia noticia)
        {
            await _iservicoNoticia.AtualizarNoticia(noticia);
        }

        public async Task<Noticia> BuscarPorId(int Id)
        {
            return await _INoticia.BuscarPorId(Id);
        }

        public async Task Excluir(Noticia Object)
        {
            await _INoticia.Excluir(Object);
        }

        public async Task<List<Noticia>> Listar()
        {
            return await _INoticia.Listar();
        }

        public async Task<List<Noticia>> ListarNoticiasAtivas()
        {
            return await _iservicoNoticia.ListarNoticiasAtivas();
        }

        public async Task<List<NoticiaViewModel>> ListarNoticiasCustomizadas()
        {
            return await _iservicoNoticia.ListarNoticiaCustomizada();
        }
    }
}
