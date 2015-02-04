using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;
using Portal.Componentes.AnexoHelper;
using Sigam.Controles.Repositorio.Classes;

namespace Portal.PortalRL
{
	/// <summary>
	/// Classe para Cadastro de Empreendimentos
	/// </summary>
	public partial class RLCadastro : ControlePadrao
	{
		#region Eventos
		public delegate void CadastroAtualizado(int idItem);
		public event CadastroAtualizado OnCadastroAtualizado;
		#endregion

		#region propriedades
		private Func<int?> _getIdRecomendacao;
		public int? idRecomendacao { get { return _getIdRecomendacao == null ? null : _getIdRecomendacao(); } }
		#endregion

		#region Inicialização
		public void Configura(Func<int?> getIdRecomendacao)
		{
			_getIdRecomendacao = getIdRecomendacao;
		}
		/// <summary>
		/// Carga da Página
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Page.IsPostBack) return;
			ConfiguraPropriedadesInvariantes();
		}

		/// <summary>
		/// Configura Invariantes
		/// </summary>
		private void ConfiguraPropriedadesInvariantes()
		{
			// Helps
			cmdHelp.BotaoHelp("RL/Cadastro");

			cmdExclui.BotaoConfirma("excluir.gif", "Excluir", "Você confirma a Exclusão desta recomendação?", "Exclui esta recomendação e todos os dados associados.");
			// Dica
			try
			{
				litDica.Text = Util.LeEscalar("SELECT desItem FROM vCarTabela WHERE codClasse=@codClasse AND codItem=@codItem", new SqlParameter("@codItem", "PROPRIEDADE"), new SqlParameter("@codClasse", "DICAFUNCAO")).ToString();
			}
			catch
			{
				litDica.Text = "Aqui você deve informar os dados da sua recomendação.";
			}
            						
		}
		#endregion

		#region Atualização do RL
		/// <summary>
		/// Limpa dados
		/// </summary>
		protected void MudaParaEstadoInicial()
		{
			lbldatAtualiza.Text = DateTime.Today.ToShortDateString();

			//campos texto
			txtNumCar.Text =
			lblMensagem.Text = String.Empty;

		}

		/// <summary>
		/// Atualiza
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Atualiza(Object sender, EventArgs e)
		{   
            Salva();
		}

		/// <summary>
		/// Finaliza a Edição
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Finaliza(object sender, EventArgs e)
		{
			Salva();
			new Default(Page);
		}

		/// <summary>
		/// Grava as alterações do RL
		/// </summary>
		protected void Salva()
		{
			Page.Validate("RLCadastro");
			if (!Page.IsValid) return;

			var IdRecomendacao = RLBD.RLAtualiza(new RLAtualizaDTO
			{
                idRecomendacao = idRecomendacao,
                nomRecomendacao = txtNomRecomendacao.Text,
                idUsuario = idUsuario,
                numCAR = Int32.Parse(txtNumCar.Text),
                idMunicipioFito = 1, //REVISAR TODO
                idCombCarroChefe = null,
                idFinalidade = null,
                datProjeto = null
			});
            
			if (OnCadastroAtualizado != null)
				OnCadastroAtualizado(IdRecomendacao);

			lblMensagem.Visible = true;
			lblMensagem.Text = "Cadastro da atualizado com sucesso!";
		}

		/// <summary>
		/// Exclui
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Exclui(Object sender, EventArgs e)
		{
			Page.Alerta("Por enquanto este cadastro não pode ser excluído!");
		}

		/// <summary>
		/// Retorna para a Consulta
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Retorna(object sender, EventArgs e)
		{
			new Default(Page);
		}


		internal void Inicializa()
		{
			MudaParaEstadoInicial(); // limpa antes de ler
			if (!idRecomendacao.HasValue) return;
			cmdAtualiza.Visible = podeAlterar;
			LeDadosRL();
		}

		/// <summary>
		/// Lê os Dados do RL
		/// </summary>
		private void LeDadosRL()
		{
            using (var sqlDR = Util.LeDataReader(@"SELECT nomRecomendacao, idCar, datAbertura  FROM [PORTALRL-DEV-001].dbo.RLRECOMENDACAO WHERE idRecomendacao = @idRecomendacao", 
												new SqlParameter("@idRecomendacao", idRecomendacao.Value)))
			{
				sqlDR.Read();
                txtNomRecomendacao.Text = Util.DecodeFromDB<string>(sqlDR["nomRecomendacao"], "");
				lbldatAtualiza.Text = Util.PopTxtData(sqlDR["datAbertura"]);
                txtNumCar.Text = Util.DecodeFromDB<string>(sqlDR["idCar"], "");
			}
		}
		#endregion
		
	}
}
