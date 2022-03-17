using GA.Fazenda.Business.Interfaces.Repositorios;
using GA.Fazenda.Business.Models;
using GA.Fazenda.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GA.Fazenda.Data.Repository
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : EntidadeBase, new()
	{
		protected readonly FazendaContexto Db;
		protected readonly DbSet<TEntity> DbSet;
		public BaseRepository(FazendaContexto db)
		{
			Db = db;
			DbSet = db.Set<TEntity>();
		}

		public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
		{
			return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
		}

		public virtual async Task<TEntity> ObterPorId(int id)
		{
			return await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
		}

		public virtual async Task<IEnumerable<TEntity>> ObterTodos()
		{
			return await DbSet.AsNoTracking().ToListAsync();
		}

		public virtual void Adicionar(TEntity entity)
		{
			DbSet.Add(entity);
		}

		public virtual void Atualizar(TEntity entity)
		{
			DbSet.Update(entity);
		}

		public virtual void Remover(int id)
		{
			var entity = new TEntity { Id = id };
			DbSet.Remove(entity);
		}

		public void Dispose()
		{
			Db?.Dispose();
		}
	}
}
