using System;
using SamDataBase.Model; // namespace where we find SamEntities.cs
namespace Opus.DataBaseEnvironment
{
	public sealed class DataAccess
	{
		private static readonly DataAccess instance = new DataAccess();
		public static DataAccess Instance { get { return instance; } }

		// Auto generated repositories
		//private CargoRepository cargoRepository;
		//private CategoriaRepository categoriaRepository;
		//private EventoRepository eventoRepository;
		//private ItemRepository itenRepository;
		//private PendenciaRepository pendenciaRepository;
		//private ResultadoVotacoesRepository resultadoVotacoesRepository;
		//private TagRepository tagRepository;
		//private UsuarioRepository usuarioRepository;
        //private PromocaoRepository promocaoRepository;

		public CargoRepository CargoRepository()
		{
            return new CargoRepository(new SamEntities());
            //return cargoRepository;
		}

        public PromocaoRepository PromocaoRepository()
        {
            return new PromocaoRepository(new SamEntities());
            //return promocaoRepository;
        }

        public CategoriaRepository CategoriaRepository()
		{
            return new CategoriaRepository(new SamEntities());
            //return categoriaRepository;
		}

		public EventoRepository EventoRepository()
		{
            return new EventoRepository(new SamEntities());
			//return eventoRepository;
		}

		public ItemRepository ItemRepository()
		{
            return new ItemRepository(new SamEntities());
			//return itenRepository;
		}

		public PendenciaRepository PendenciaRepository()
		{
            return new PendenciaRepository(new SamEntities());
			//return pendenciaRepository;
		}

		public ResultadoVotacoesRepository ResultadoVotacoRepository()
		{
            return new ResultadoVotacoesRepository(new SamEntities());
			//return resultadoVotacoesRepository;
		}

		public TagRepository TagRepository()
		{
            return new TagRepository(new SamEntities());
			//return tagRepository;
		}

		public UsuarioRepository UsuarioRepository()
		{
            return new UsuarioRepository(new SamEntities());
			//return usuarioRepository;
		}

		public DataAccess()
		{
			//var context = new SamEntities();
            //promocaoRepository = new PromocaoRepository(new SamEntities());
            //cargoRepository = new CargoRepository(new SamEntities());
			//categoriaRepository = new CategoriaRepository(new SamEntities());
			//eventoRepository = new EventoRepository(new SamEntities());
			//itenRepository = new ItemRepository(new SamEntities());
			//pendenciaRepository = new PendenciaRepository(new SamEntities());
			//resultadoVotacoesRepository = new ResultadoVotacoesRepository(new SamEntities());
			//tagRepository = new TagRepository(new SamEntities());
			//usuarioRepository = new UsuarioRepository(new SamEntities());
		}

	}
}