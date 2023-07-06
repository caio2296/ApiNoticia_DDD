using Entidades.Entidades;
using Entidades.Entidades.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoNoticia
    {
        Task AdicionarNoticia(Noticia noticia);
        Task AtualizarNoticia(Noticia noticia);
        Task<List<Noticia>> ListarNoticiasAtivas();
        Task<List<NoticiaViewModel>> ListarNoticiaCustomizada();
    }
}
