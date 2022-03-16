using GA.Fazenda.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GA.Fazenda.Business.Interfaces.Repositorios
{
	public interface IRepository<TEntity> : IDisposable where TEntity : EntidadeBase
	{
		void Adicionar(TEntity entity);
		Task<TEntity> ObterPorId(int id);
		Task<IEnumerable<TEntity>> ObterTodos();
		void Atualizar(TEntity entity);
		void Remover(int id);
		Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
	}
}
