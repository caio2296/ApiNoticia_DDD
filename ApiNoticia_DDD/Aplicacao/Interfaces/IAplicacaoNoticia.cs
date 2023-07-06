using Aplicacao.Interfaces.Genericos;
using Entidades.Entidades;
using Entidades.Entidades.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IAplicacaoNoticia: IGenericoAplicacao<Noticia>
    {
        Task AdicionarNoticia(Noticia noticia);
        Task AtualizarNoticia(Noticia noticia);
        Task<List<Noticia>> ListarNoticiasAtivas();
        Task<List<NoticiaViewModel>> ListarNoticiasCustomizadas();
    }
}
