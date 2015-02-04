using System;
using System.ComponentModel.DataAnnotations;

namespace Portal.PortalRL
{
	/// <summary>
	/// Classe CAR (Cadastro do CAR)
	/// </summary>
	public class RLAtualizaDTO
	{
        public int? idRecomendacao { get; set; }
        public string nomRecomendacao { get; set; }
        public long? numCAR { get; set; }
        public int? idUsuario { get; set; }
        public int? idMunicipioFito { get; set; }
        public int? idCombCarroChefe { get; set; }
        public int? idFinalidade { get; set; }
		public DateTime? datProjeto { get; set; }
	}
}
