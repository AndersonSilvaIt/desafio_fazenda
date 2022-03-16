using GA.Fazenda.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GA.Fazenda.Business.Interfaces.Repositorios
{
	public interface IRepository<TEntity> : IDisposable where TEntity : EntidadeBase
	{
		Task Adicionar(TEntity entity);
		Task<TEntity> ObterPorId(int id);
		Task<IEnumerable<TEntity>> ObterTodos();
		Task Atualizar(TEntity entity);
		Task Remover(int id);
		Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
		Task<int> SaveChanges();
	}
}
