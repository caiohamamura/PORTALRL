<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RLConsulta.ascx.cs" Inherits="Portal.PortalRL.RLConsulta" %>
<%@ Register TagPrefix="uc1" TagName="TituloModulo" Src="../TituloModulo.ascx" %>
<style>
	.CabecalhoGridResultados
	{
		line-height: 25px;
	}
	.GridResultados
	{
		text-align: center;
		margin-top: 5px;
	}
</style>
<uc1:TituloModulo ID="TituloModulo" runat="server" />
<div class="TabelaBorda" style="margin:0px;padding:3px">
    <div class="TituloItem">
		<asp:Literal ID="litDica" runat="server"></asp:Literal>
	</div>
    <hr class="Modulo" />
	<asp:LinkButton ID="cmdAdiciona" runat="server" 
		ToolTip="Cadastrar Nova Recomendação" CssClass="BotaoCmd"
		CausesValidation="False" OnClick="Adicionar"><img src="../imagens/adicionar.gif" border="0"> Cadastrar Nova Recomendação</asp:LinkButton>
		&nbsp;<asp:LinkButton ID="cmdExcel" runat="server" ToolTip="Exporta para o Excel" CssClass="BotaoCmd"
		CausesValidation="False" OnClick="Excel"><img src="../imagens/excel.gif" border="0">Excel</asp:LinkButton>
	&nbsp;<asp:LinkButton runat="server" CausesValidation="False" CssClass="BotaoCmd"
				ToolTip="Ajuda ao Usu&#225;rio" ID="cmdHelp"><img src="../imagens/help.gif" 
				border="0">Ajuda</asp:LinkButton>
	<asp:GridView ID="gvConsulta" runat="server" AutoGenerateColumns="False" CssClass="Modulo GridResultados"
		DataKeyNames="idRecomendacao, podeConsultar, podeAlterar" EmptyDataText="Nenhuma Recomendação Cadastrada"
		Width="100%" OnSelectedIndexChanging="Edita" ShowFooter="True" OnRowDeleting="Extrato"
		OnRowDataBound="LinhaGrid" AllowPaging="True" 
		onpageindexchanging="Paginacao" PageSize="15">
		<FooterStyle CssClass="ModuloRodape" />
		<EmptyDataRowStyle CssClass="Modulo" />
		<Columns>
			<asp:TemplateField ShowHeader="False">
				<ItemTemplate>
					<asp:LinkButton ID="cmdEdita" runat="server" CausesValidation="False" CommandName="Select"
						Text="&lt;img src='./imagens/editar.gif' border='0' alt='Editar'/&gt;Alterar"></asp:LinkButton>
				</ItemTemplate>
				<HeaderStyle HorizontalAlign="Center" />
				<ItemStyle HorizontalAlign="Center" Wrap="False" />
			</asp:TemplateField>
			<asp:BoundField DataField="idRecomendacao" HeaderText="Protocolo">
				<HeaderStyle HorizontalAlign="Center" />
				<ItemStyle HorizontalAlign="Center" />
			</asp:BoundField>
            <asp:BoundField DataField="nomRecomendacao" HeaderText="Nome da Recomendação">
				<HeaderStyle HorizontalAlign="Center" />
				<ItemStyle HorizontalAlign="Center" />
			</asp:BoundField>
			<asp:BoundField DataField="datAbertura" HeaderText="Data de Abertura" DataFormatString="{0:d}"/>
			<asp:BoundField DataField="numCar" HeaderText="Número do Car">
				<HeaderStyle HorizontalAlign="Left" />
				<ItemStyle HorizontalAlign="Left" />
			</asp:BoundField>
            <asp:BoundField DataField="datProjeto" HeaderText="Data do Projeto" DataFormatString="{0:d}"/>
			<asp:TemplateField ShowHeader="False">
				<ItemTemplate>
					<asp:LinkButton ID="cmdResumo" runat="server" CausesValidation="False" CommandName="Delete"
						Text="&lt;img src='./imagens/relatorio.gif' alt='Resumo' border='0'/&gt;Resumo"></asp:LinkButton>
				</ItemTemplate>
				<HeaderStyle HorizontalAlign="Center" />
				<ItemStyle HorizontalAlign="Center" Wrap="False" />
			</asp:TemplateField>
		</Columns>
		<RowStyle CssClass="ModuloItem" />
		<EditRowStyle CssClass="ModuloEdita" />
		<SelectedRowStyle CssClass="ModuloSeleciona" />
		<HeaderStyle CssClass="ModuloCabecalho" />
		<AlternatingRowStyle CssClass="ModuloAlternado" />
	</asp:GridView>
</div>