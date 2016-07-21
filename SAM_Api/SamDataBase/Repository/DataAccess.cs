using System;
using SamDataBase.Model; // namespace where we find SamEntities.cs
namespace Opus.DataBaseEnvironment
{
	public sealed class DataAccess
	{
		private static readonly DataAccess instance = new DataAccess();
		public static DataAccess Instance { get { return instance; } }

		public CargoRepository GetCargoRepository()
		{
            return new CargoRepository(new SamEntities());
		}

        public PromocaoRepository GetPromocaoRepository()
        {
            return new PromocaoRepository(new SamEntities());
        }

        public CategoriaRepository GetCategoriaRepository()
		{
            return new CategoriaRepository(new SamEntities());
		}

		public EventoRepository GetEventoRepository()
		{
            return new EventoRepository(new SamEntities());
		}

		public ItemRepository GetItemRepository()
		{
            return new ItemRepository(new SamEntities());
		}

		public PendenciaRepository GetPendenciaRepository()
		{
            return new PendenciaRepository(new SamEntities());
		}

		public ResultadoVotacoesRepository GetResultadoVotacoRepository()
		{
            return new ResultadoVotacoesRepository(new SamEntities());
		}

		public TagRepository GetTagRepository()
		{
            return new TagRepository(new SamEntities());
		}

		public UsuarioRepository GetUsuarioRepository()
		{
            return new UsuarioRepository(new SamEntities());
		}

		private DataAccess()
		{
			
		}

	}
}