using Application.Interfaces;
using Application.Interfaces.ComandaMercaderia;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase
{
    public class ComandaMercaderiaService : IComandaMercaderiaService
    {
        private readonly IComandaMercaderiaCommand _command;

        public ComandaMercaderiaService(IComandaMercaderiaCommand command)
        {
            _command = command;
        }
        public async Task<bool> RegisterComandaMercaderia(ComandaMercaderia comandaMercaderia)
        {
            Task<bool> flag=_command.InsertComandaMercaderia(comandaMercaderia);
            if (await flag)
            {
                return true;
            }
            else { return false; }
        }
    }
}
