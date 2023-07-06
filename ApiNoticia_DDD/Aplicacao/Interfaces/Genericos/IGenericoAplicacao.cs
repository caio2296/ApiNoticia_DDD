﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces.Genericos
{
    public interface IGenericoAplicacao<T> where T : class
    {
        Task Adicionar(T Object);
        Task Atualizar(T Object);
        Task Excluir(T Object);
        Task<T> BuscarPorId(int Id);
        Task<List<T>> Listar();
    }
}
