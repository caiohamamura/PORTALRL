using System;
using System.Data.SqlClient;
using System.Web.UI;
using AjaxControlToolkit;

namespace Portal.PortalRL
{
	/// <summary>
	/// Classe dos Administração do Cadastro do CAR
	/// </summary>
	public partial class RLAdm : PaginaGenerica, Portal.PortalRL.IFonteIdRL
	{
		protected override string GetUrlDeTransferencia() { return "/PortalRL/RLAdm.aspx"; }

		public TabContainer GetTabPanel { get { return TabNavegacao; } }

		public class PCRLAdm : PCPaginaComModulo
		{
			public int? IdRecomendacao { get; set; }
			public PCRLAdm(int? idRecomendacao, int idModulo, Page paginaOrigem)
				: base(idModulo, typeof(RLAdm), paginaOrigem)
			{
				IdRecomendacao = idRecomendacao;
			}
		}

		public RLAdm() { }
		public RLAdm(int? idRecomendacao, int idModulo, Page paginaOrigem) : base(new PCRLAdm(idRecomendacao, idModulo, paginaOrigem)) { }

		#region Propriedades
		protected new PCRLAdm ParamsEntrada
		{
			get { return (PCRLAdm)base.ParamsEntrada; }
		}
		public int? IdRecomendacao 
		{
			get { return ParamsEntrada.IdRecomendacao; }
			private set { ParamsEntrada.IdRecomendacao = value; }
		}
        //public string codSituacao
        //{
        //    get { return (string)ViewState["codSituacao"]; }
        //    set { ViewState["codSituacao"] = value; }
        //}
		#endregion

		#region Inicialização
		/// <summary>
		/// Carga da página
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, EventArgs e)
		{
			if (ParamsEntrada == null && IsCallback)
				//TODO: no caso de callback ver se não é possivel registrar um javascript que faz a transferencia no caso de erro.
				throw new Exception("Sua sessão expirou volte para o inicio e refaça seu login!");                        

			ConfiguraDependenciasDosFilhos();

			if (IsPostBack || IsCallback) return;

			ConfiguraPermissoes();
		}

		/// <summary>
		/// Após Carga da Página
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_LoadComplete(object sender, EventArgs e)
		{
			if (IsPostBack || IsCallback) return;
			Inicializa();
		}

		/// <summary>
		/// Configuração dos Controles Filho
		/// </summary>
		private void ConfiguraDependenciasDosFilhos() 
		{
            rlCadastro.Configura(() => IdRecomendacao);
			
		}

		/// <summary>
		/// Verificação das Permissões de Acesso
		/// </summary>
		private void ConfiguraPermissoes()
		{
			PodeAlterar = (PodeAlterar || FlaAdm);
			PodeIncluir = (PodeIncluir || FlaAdm);
			PodeExcluir = (PodeExcluir || FlaAdm);
			PodeConsultar = (PodeConsultar || FlaAdm);
		}

		/// <summary>
		/// Coloca esta pagina e seus controles no estado de edição do IdRecomendacao informado 
		/// </summary>
		/// Um IdRecomendacao valido ou null para ir para o estado de inclusao.</param>
		private void Inicializa()
		{
			ConfiguraPermissoesEspecificas(IdRecomendacao);

			//daqui para frente o podeconsultar não deve mudar mais;
			if (!PodeConsultar)
				new MsgPortal("Acesso Não Autorizado", "Desculpe, mas você não possui autorização para consultar este Cadastro.", "erro1.gif", null, null, null, this);

			MudaParaEstadoInicial();

			if (!IdRecomendacao.HasValue) return;
			
			Popula(IdRecomendacao.Value);

			// Alteração Permitida só nestas Situações
            //if (!"CADASTRO,ALTERACAO,REVISAO".Contains(codSituacao))
            //{
            //    PodeAlterar = false;
            //    PodeIncluir = false;
            //    PodeExcluir = false;
            //}

            // Se já estiver inscrito, a aba Finalizar passa a ser Alterar
            //if (codSituacao == "INSCRITO")
            //    TBFinaliza.HeaderText = "Alterar";
            //else
            //    TBFinaliza.HeaderText = "Finalizar";

            //TBFinaliza.Visible = !("ANALISE,REVISADO,CANCELADO,VALIDO,INVALIDO".Contains(codSituacao));

			//os controles abaixo dependem da existencia do idRecomendação para estarem visiveis e inicializados
			VisualizaAbas(true);
            rlCadastro.Configura(IdModulo, PodeConsultar, PodeAlterar, PodeIncluir, PodeExcluir, false);
            rlCadastro.Inicializa();
		}

		/// <summary>
		/// Configura Permissões do Usuário
		/// </summary>
		/// <param name="IdCar"></param>
		private void ConfiguraPermissoesEspecificas(int? idRecomendacao)
		{
			// Se é uma nova recomendação pode tudo, pois caracteriza um usuario incluindo um novo registro
			if (!idRecomendacao.HasValue)
			{
				PodeConsultar = true;
				PodeIncluir = true;
				PodeAlterar = true;
				PodeExcluir = true;
				return;
			}
			//se for o adm não preciso mais mexer nas permissões. retornar
			if (FlaAdm)
			{
				PodeConsultar = true;
				PodeIncluir = true;
				PodeAlterar = true;
				PodeExcluir = true;
				return; 
			}

			// Se é um car existente e o usuario logado não é um adm
			using (var sqlDR = Util.LeDataReaderUmRegistro("SELECT podeConsultar,podeIncluir,podeAlterar,podeExcluir FROM CARPESSOA with (nolock) WHERE idCAR = @idCAR AND idPessoa = @idPessoa", new SqlParameter("@idPessoa", IdUsuario), new SqlParameter("@idCAR", idRecomendacao)))
			{
				if (sqlDR.HasRows)  //o usuario logado não tem permissoes extras para este car, vale o que já esta definido. retornar.
				{
					sqlDR.Read();
					//TODO: fazendo isso eu perco o estado original dos pode<xxxx> o melhor seria usar pode<xxxx> especificos e sempre compor com o original.
					PodeConsultar = (PodeConsultar || Util.DecodeFromDB<bool>(sqlDR["podeConsultar"]));
					PodeIncluir = (PodeIncluir || Util.DecodeFromDB<bool>(sqlDR["podeIncluir"]));
					PodeAlterar = (PodeAlterar || Util.DecodeFromDB<bool>(sqlDR["podeAlterar"]));
					PodeExcluir = (PodeExcluir || Util.DecodeFromDB<bool>(sqlDR["podeExcluir"]));
				}
			}
		}

		/// <summary>
		/// Coloca o controle em um estado de uma nova edição, limpando os valores texto, escondendo as abas que dependem do cadastro principal.
		/// </summary>
		private void MudaParaEstadoInicial()
		{
			//configura estado inicial das abas
			VisualizaAbas(false);
			TabNavegacao.ActiveTabIndex = 0;

			//defaults
			lblUsuario.Text = ParametrosPortal.Usuario.nomPessoa;
			lblTitulo.Text = "NOVA RECOMENDAÇÃO PARA RESERVA LEGAL";
			lblID.Text = "";
		}

		/// <summary>
		/// Após a Atualização do Cadastro
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void rlCadastro_OnCadastroAtualizado(int idRecomendacao)
		{
			IdRecomendacao = idRecomendacao;
			MudaParaEstadoInicial();
			Inicializa();
		}

		/// <summary>
		/// Cabeçalho da Recomendacao
		/// </summary>
		private void Popula(int idRecomendacao)
		{
            if (idRecomendacao == 0) { return; }
            using (var dr = Util.LeDataReaderUmRegistro("SELECT nomRecomendacao FROM [PORTALRL-DEV-001].dbo.RLRECOMENDACAO WHERE idRecomendacao = @idCAR", new SqlParameter("@idCAR", idRecomendacao)))
			{
				dr.Read();
				lblTitulo.Text = Util.PopTxt(dr["nomRecomendacao"]);
				lblID.Text = Util.PopTxt(idRecomendacao);
				// codSituacao = Util.PopTxt(dr["codSituacao"]); //Vamos fazer?
				// lblSituacao.Text = Util.PopTxt(dr["nomSituacao"]).ToUpper();
			}
		}

		/// <summary>
		/// Exibição das Abas
		/// </summary>
		/// <param name="visivel"></param>
		private void VisualizaAbas(bool visivel)
		{
			TBCadastro.Visible = true;
            //TBFinaliza.Visible = (codSituacao != "ANALISE" && visivel);
			cmdAnterior.Visible =
			cmdProximo.Visible = visivel;
		}
		#endregion

		/// <summary>
		/// Retorna para a Consulta
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Retornar(object sender, EventArgs e)
		{
			Voltar();
		}

		private void Voltar()
		{
			if (IdRecomendacao.HasValue)
				new Default(Page);
			else
				new Default(13352, Page);
		}

		/// <summary>
		/// Navega para a Aba Anterior
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Anterior(object sender, EventArgs e)
		{		
			if (TabNavegacao.ActiveTabIndex == 0)
				return;
			
			TabNavegacao.ActiveTabIndex = TabNavegacao.ActiveTabIndex - 1;
			doActiveTabChanged();
		}

		/// <summary>
		/// Aba Seguida
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void AbaSeg(object sender, EventArgs e)
		{
			TabNavegacao.ActiveTabIndex = 2; // TabNavegacao.ActiveTabIndex + 1;
			doActiveTabChanged();
		}

		protected void AbaAnt(object sender, EventArgs e)
		{
			TabNavegacao.ActiveTabIndex = 0; //TabNavegacao.ActiveTabIndex - 1;
			doActiveTabChanged();
		}

		/// <summary>
		/// Navega para a Próxima Aba
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Proximo(object sender, EventArgs e)
		{
				
			if (TabNavegacao.ActiveTabIndex == TabNavegacao.Tabs.Count)
				return;

					TabNavegacao.ActiveTabIndex = TabNavegacao.ActiveTabIndex + 1;
			doActiveTabChanged();
			}

		/// <summary>
		/// Finalização da Recomendacao
		/// </summary>
		protected void FinalizaCAR(object sender, EventArgs e)
		{
			Inicializa();
		}

		/// <summary>
		/// Alteração do CAR
		/// </summary>
		protected void AlteraCAR(object sender, EventArgs e)
		{
			Inicializa();
		}

		protected void doActiveTabChanged()
		{
			//TabNavegacao.ActiveTab = sender as TabPanel;
		}
	}
}
