using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions; 

namespace SistemaVenta.DAL.Repositorios.Contrato
{
    public interface IGenericRepository<Tmodel>where Tmodel : class
    {
        //escribiremos todo los metodo que vamos a trabajar
        //con nuestros modelos apirest
        //contrato para interactuar con todo nuestros modelos
        Task<Tmodel> Obtener(Expression<Func<Tmodel, bool>> filtro);
        Task<Tmodel> Crear(Tmodel modelo);
        Task<bool> Editar(Tmodel modelo);
        Task<bool> Eliminar(Tmodel modelo);
        Task<IQueryable<Tmodel>> Consultar(Expression<Func<Tmodel, bool>> filtro = null);


    }
}
