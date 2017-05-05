using System;
using Autenticacao.Infra.Data.Context;
using Autenticacao.Infra.Data.Interfaces;

namespace Autenticacao.Infra.Data.UoW
{
	public class UnitOfWork :IUnitOfWork
	{
		private readonly AutenticacaoContext _db;
		private bool _disposed;


		public UnitOfWork(AutenticacaoContext db)
		{
			_db = db;
		}

		public void BeginTransaction()
		{
			_disposed = false;
		}

		public void Commit()
		{
			_db.SaveChanges();
		}


		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					_db.Dispose();
				}
			}
			_disposed = true;
		}


		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

	}
}