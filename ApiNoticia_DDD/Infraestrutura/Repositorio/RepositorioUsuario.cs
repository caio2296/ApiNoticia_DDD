﻿using Dominio.Interfaces;
using Entidades.Entidades;
using Entidades.Enums;
using Infraestrutura.Configuracoes;
using Infraestrutura.Repositorio.Generico;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorio
{
    public class RepositorioUsuario : RepositorioGenerico<ApplicationUser>, IUsuario
    {
        private readonly DbContextOptions<Contexto> _OptionsBuilder;
        public RepositorioUsuario()
        {
            _OptionsBuilder = new DbContextOptions<Contexto>();
        }
        public async Task<bool> AdicionarUsuario(string email, string senha, int idade, string celular)
        {
            try
            {
                using (var data = new Contexto(_OptionsBuilder))
                {
                    await data.ApplicationUsers.AddAsync(new ApplicationUser
                    {
                        Email = email,
                        PasswordHash = senha,
                        Idade = idade,
                        Celular = celular,
                        Tipo = TipoUsuario.Comum
                    });
                    await data.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public async Task<bool> ExisteUsuario(string email, string senha)
        {
            try
            {
                using (var data = new Contexto(_OptionsBuilder))
                {
                    await data.ApplicationUsers
                        .Where(u => u.Email.Equals(email) && u.PasswordHash.Equals(senha))
                        .AsNoTracking()
                        .AnyAsync();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<string> RetornaIdUsuario(string email)
        {
            try
            {
                using (var data = new Contexto(_OptionsBuilder))
                {
                    var usuario =await data.ApplicationUsers
                        .Where(u => u.Email.Equals(email))
                        .AsNoTracking()
                        .FirstOrDefaultAsync();
                    
                    return usuario.Id;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
