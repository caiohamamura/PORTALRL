using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Portal.PortalRL
{
    public partial class RLConsulta :  ModuloGenerico, IModuloDeConfiguracaoAutomatica
    {
        #region IModuloDeConfiguracaoAutomatica
        public string NomModuloDefinicao
        {
            get { return "PortalRL - Consulta"; }
        }

        public string DesModuloDefinicao
        {
            get { return "Consulta às recomendações do usuário"; }
        }

        public string NomCaminhoModulo
        {
            get { return "PortalRL/RLConsulta.ascx"; }
        }

        public string NomCaminhoHelp
        {
            get { return ""; }
        }

        public string NomCaminhoAdm
        {
            get { return ""; }
        }

        public string CodCategoria
        {
            get { return "PORTALRL"; }
        }

        public string NomTabela
        {
            get { return "RLRECOMENDACAO"; }
        }

        public string IdTabela
        {
            get { return "idRecomendacao"; }
        }

        public string DesTabela
        {
            get { return ""; }
        }

        public string ProcCopia
        {
            get { return ""; }
        }

        public string TitConsulta
        {
            get { return "Consulta"; }
        }

        public string TitInclui
        {
            get { return "Inclui"; }
        }

        public string TitAltera
        {
            get { return "Altera"; }
        }

        public string TitExclui
        {
            get { return "Exclui"; }
        }

        public void InstalarBd()
        {
            return;
        }
        #endregion

        #region Propriedades
        private string sqlCMD
        {
            get { return (string)ViewState["sqlCMD"]; }
            set { ViewState["sqlCMD"] = value; }
        }
        #endregion

        #region Funções de Inicialização
        /// <summary>
        /// Carga da Página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IdUsuario <= 0)
            {
                Visible = false;
                return;
            }

            if (IsPostBack) return;

            cmdAdiciona.BotaoImagem("adicionar.gif", "Cadastrar Nova Recomendação", "Cadastro de Nova Recomendação");
            cmdExcel.BotaoImagem("excel.gif", "Exportar para Excel", "Exportar em formato de Planilha Excel");
            cmdHelp.BotaoHelp("PortalRL/RLConsulta.ascx");

            litDica.Text = (string)Util.LeEscalar("SELECT desItem FROM vCarTabela WHERE codClasse=@codClasse AND codItem=@codItem", new SqlParameter("@codItem", "CONSULTACADASTRO"), new SqlParameter("@codClasse", "DICAFUNCAO"))
                ?? "Aqui podem ser consultadas as recomendações já cadastradas no PortalRL.";

            if (FlaAdm)
                sqlCMD = "SELECT DISTINCT idRecomendacao,nomRecomendacao,datAbertura,idCar as numCar,datProjeto,1 AS podeConsultar,1 AS podeAlterar FROM [PORTALRL-DEV-001].dbo.RLRECOMENDACAO ORDER BY idRecomendacao";
            else
                sqlCMD = "SELECT DISTINCT idCAR,codSituacao,nomSituacao,numCAR,nomPropriedade,nomMunicipio,datCadastro,podeConsultar,podeAlterar FROM vCARUsuario WHERE idPessoa=" + IdUsuario.ToString() + " ORDER BY idCAR ";

            if (IdUsuario > 0)
                cmdAdiciona.Visible = true;

            if (PopulaGrid() == 0)
                goAdicionar();
        }
        #endregion

        /// <summary>
        /// Popula o Grid
        /// </summary>
        private int PopulaGrid()
        {
            DataTable sqlDT;
            sqlDT = Util.LeDataTable(sqlCMD, "RLRECOMENDACAO", "StringDeConexao");

            if (sqlDT.Rows.Count > 0)
            {
                gvConsulta.DataSource = sqlDT;
                gvConsulta.DataBind();
            }

            //return sqlDT.Rows.Count;
            return gvConsulta.Rows.Count;
        }

        /// <summary>
        /// Armazena a Altura do Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AlturaGrid(object sender, EventArgs e)
        {
            //divConsulta.Style["height"] = ddlAltura.SelectedValue = Util.AlturaSet("CARUSUARIO", IdUsuario, ddlAltura.SelectedValue);
        }

        /// <summary>
        /// Monta a Linha do Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinhaGrid(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            // Botão de Edição (só visível para Administradores ou Usuário com Acesso ao CAR)
            var flaConsulta = Util.PopBool(gvConsulta.DataKeys[e.Row.RowIndex]["podeConsultar"]);
            var codSituacao = Util.PopTxt(gvConsulta.DataKeys[e.Row.RowIndex]["codSituacao"]);
            var btnEdita = (LinkButton)e.Row.FindControl("cmdEdita");
            if ("CADASTRO,ALTERACAO,REVISAO".Contains(codSituacao))
            {
                btnEdita.Text = "<img src='" + ResolveUrl("~/imagens/editar.gif") + "' border='0'/>Alterar";
                btnEdita.Visible = flaConsulta;
            }
            else
            {
                btnEdita.Text = "<img src='" + ResolveUrl("~/imagens/procurar.gif") + "' border='0'/>Consultar";
                btnEdita.Visible = flaConsulta;
            }
            var btnResumo = (LinkButton)e.Row.FindControl("cmdResumo");
            btnResumo.Text = "<img src='" + ResolveUrl("~/imagens/relatorio.gif") + "' border='0'/>Resumo";
            btnResumo.Visible = flaConsulta;
        }

        /// <summary>
        /// Nova Recomendação
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Adicionar(object sender, EventArgs e)
        {
            goAdicionar();
        }

        private void goAdicionar()
        {
            new RLAdm(null, IdModulo, Page);
        }
        /// <summary>
        /// Edição do CAR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Edita(object sender, GridViewSelectEventArgs e)
        {
            new RLAdm((int)gvConsulta.DataKeys[e.NewSelectedIndex]["idRecomendacao"], IdModulo, Page);
        }
        /// <summary>
        /// Emissão do Extrato
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Extrato(object sender, GridViewDeleteEventArgs e)
        {
            var idCAR = Util.PopInt(gvConsulta.DataKeys[e.RowIndex]["idCAR"]);
            //new CARExtrato(idCAR, IdModulo, Page, CARExtrato.CARExtratoTransferMode.PopUp);
        }
        /// <summary>
        /// Exporta para o Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Excel(object sender, EventArgs e)
        {
            // Variveis necessarias para a criacao do documento
            new ArquivoExcel(sqlCMD, Page);
        }

        /// <summary>
        /// Paginação
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Paginacao(object sender, GridViewPageEventArgs e)
        {
            gvConsulta.PageIndex = e.NewPageIndex;
            PopulaGrid();
        }
    }
}