using System;
using SamDataBase.Model; // namespace where we find SamEntities.cs
namespace Opus.DataBaseEnvironment
{
	public sealed class DataAccess
	{
		private static readonly DataAccess instance = new DataAccess();
		public static DataAccess Instance { get { return instance; } }

		// Auto generated repositories
		private CargoRepository cargoRepository;
		private CategoriaRepository categoriaRepository;
		private EventoRepository eventoRepository;
		private ItemRepository itenRepository;
		private PendenciaRepository pendenciaRepository;
		private ResultadoVotacoesRepository resultadoVotacoesRepository;
		private TagRepository tagRepository;
		private UsuarioRepository usuarioRepository;

		public CargoRepository CargoRepository()
		{
			return cargoRepository;
		}

		public CategoriaRepository CategoriaRepository()
		{
			return categoriaRepository;
		}

		public EventoRepository EventoRepository()
		{
			return eventoRepository;
		}

		public ItemRepository ItemRepository()
		{
			return itenRepository;
		}

		public PendenciaRepository PendenciaRepository()
		{
			return pendenciaRepository;
		}

		public ResultadoVotacoesRepository ResultadoVotacoRepository()
		{
			return resultadoVotacoesRepository;
		}

		public TagRepository TagRepository()
		{
			return tagRepository;
		}

		public UsuarioRepository UsuarioRepository()
		{
			return usuarioRepository;
		}

		public DataAccess()
		{
			var context = new SamEntities();
			cargoRepository = new CargoRepository(context);
			categoriaRepository = new CategoriaRepository(context);
			eventoRepository = new EventoRepository(context);
			itenRepository = new ItemRepository(context);
			pendenciaRepository = new PendenciaRepository(context);
			resultadoVotacoesRepository = new ResultadoVotacoesRepository(context);
			tagRepository = new TagRepository(context);
			usuarioRepository = new UsuarioRepository(context);
		}

	}
}