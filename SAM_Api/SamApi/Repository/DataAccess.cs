using System;
using SamApi.App_Data; // namespace where we find SAMEntities.cs
namespace Opus.DataBaseEnvironment
{
	public sealed class DataAccess
	{
		private static readonly DataAccess instance = new DataAccess();
		public static DataAccess Instance { get { return instance; } }

		// Auto generated repositories
		private CARGORepository cargoRepository;
		private CATEGORIARepository categoriaRepository;
		private EVENTORepository eventoRepository;
		private ITENRepository itenRepository;
		private PENDENCIARepository pendenciaRepository;
		private RESULTADOS_VOTACOESRepository resultados_votacoesRepository;
		private TAGRepository tagRepository;
		private USUARIORepository usuarioRepository;

		public CARGORepository CARGORepository()
		{
			return cargoRepository;
		}

		public CATEGORIARepository CATEGORIARepository()
		{
			return categoriaRepository;
		}

		public EVENTORepository EVENTORepository()
		{
			return eventoRepository;
		}

		public ITENRepository ITENRepository()
		{
			return itenRepository;
		}

		public PENDENCIARepository PENDENCIARepository()
		{
			return pendenciaRepository;
		}

		public RESULTADOS_VOTACOESRepository RESULTADOS_VOTACOESRepository()
		{
			return resultados_votacoesRepository;
		}

		public TAGRepository TAGRepository()
		{
			return tagRepository;
		}

		public USUARIORepository USUARIORepository()
		{
			return usuarioRepository;
		}

		public DataAccess()
		{
			var context = new SAMEntities();
			cargoRepository = new CARGORepository(context);
			categoriaRepository = new CATEGORIARepository(context);
			eventoRepository = new EVENTORepository(context);
			itenRepository = new ITENRepository(context);
			pendenciaRepository = new PENDENCIARepository(context);
			resultados_votacoesRepository = new RESULTADOS_VOTACOESRepository(context);
			tagRepository = new TAGRepository(context);
			usuarioRepository = new USUARIORepository(context);
		}

	}
}