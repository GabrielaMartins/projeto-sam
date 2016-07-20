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
        private PromocaoRepository promocaoRepository;

		public CargoRepository CargoRepository()
		{
			return cargoRepository;
		}

        public PromocaoRepository PromocaoRepository()
        {
            return promocaoRepository;
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
            promocaoRepository = new PromocaoRepository(new SamEntities());
            cargoRepository = new CargoRepository(new SamEntities());
			categoriaRepository = new CategoriaRepository(new SamEntities());
			eventoRepository = new EventoRepository(new SamEntities());
			itenRepository = new ItemRepository(new SamEntities());
			pendenciaRepository = new PendenciaRepository(new SamEntities());
			resultadoVotacoesRepository = new ResultadoVotacoesRepository(new SamEntities());
			tagRepository = new TagRepository(new SamEntities());
			usuarioRepository = new UsuarioRepository(new SamEntities());
		}

	}
}