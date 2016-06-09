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
		private ItenRepository itenRepository;
		private PendenciaRepository pendenciaRepository;
		private ResultadoVotacoRepository resultadovotacoRepository;
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

		public ItenRepository ItenRepository()
		{
			return itenRepository;
		}

		public PendenciaRepository PendenciaRepository()
		{
			return pendenciaRepository;
		}

		public ResultadoVotacoRepository ResultadoVotacoRepository()
		{
			return resultadovotacoRepository;
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
			itenRepository = new ItenRepository(context);
			pendenciaRepository = new PendenciaRepository(context);
			resultadovotacoRepository = new ResultadoVotacoRepository(context);
			tagRepository = new TagRepository(context);
			usuarioRepository = new UsuarioRepository(context);
		}

	}
}