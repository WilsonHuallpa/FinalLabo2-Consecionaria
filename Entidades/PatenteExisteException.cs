using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class PatenteExisteException: Exception
    {
        public PatenteExisteException(string mensaje ):base(mensaje)
        {
        }
    }
}
