<%@ Control Language="c#" Inherits="Portal.PortalRL.RLCadastro" CodeBehind="RLCadastro.ascx.cs"
	AutoEventWireup="True" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI" Assembly="eWorld.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<style type="text/css">
	.style1
	{
		color: #008040;
	}
</style>
<table id="TabCadastro" cellspacing="3" cellpadding="3" style="margin-left: auto;
	margin-right: auto; width: 100%; border: none 0" runat="server">
	<tr>
		<td class="TituloItem" colspan="2">
			<asp:Literal ID="litDica" runat="server"></asp:Literal>
			&nbsp;<asp:LinkButton runat="server" CausesValidation="False" CssClass="BotaoCmd"
				ToolTip="Ajuda ao Usu&#225;rio" ID="cmdHelp"><img src="../imagens/help.gif" 
				border="0">Ajuda</asp:LinkButton>
			<hr class="Modulo" />
		</td>
	</tr>
	<tr>
		<td class="TituloItem" width="20%" align="right">
			Atualização:
		</td>
		<td class="TituloItem" width="80%">
			<asp:Label ID="lbldatAtualiza" runat="server" CssClass="TituloItem"></asp:Label>
		</td>
	</tr>
    <tr>
		<td class="TituloItem" width="20%" align="right">
			Nome da Recomendação:
		</td>
		<td class="TituloItem" width="80%">
			<asp:TextBox ID="txtNomRecomendacao" runat="server" CssClass="Modulo" Width="130px" Text="Nome da recomendação"
											EnableViewState="false"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td class="TituloItem" width="20%" align="right">
			Número do CAR:
		</td>
		<td class="TituloItem" width="80%">
			<asp:TextBox ID="txtNumCar" runat="server" CssClass="Modulo" Width="130px" Text=""
											EnableViewState="false"></asp:TextBox>
		</td>
	</tr>
	<%--<tr>
		<td class="TituloItem" width="20%" align="right">
			Coordenadas:</td>
		<td class="TituloItem" width="  80%">
			Latitude:
			<ew:NumericBox ID="numLatitude" runat="server" CssClass="Modulo" 
				DecimalPlaces="8" DecimalSign=","
				GroupingSeparator="." PlacesBeforeDecimal="2" TextAlign="Right" 
				Width="120px" ToolTip="Latitude em números decimais"></ew:NumericBox>
			&nbsp;<asp:RangeValidator ID="rvLat" ControlToValidate="numLatitude" runat="server" 
				Display="Dynamic" ErrorMessage="deve pertencer ao intervalo -90 à 90. " MaximumValue="90" 
				MinimumValue="-90" SetFocusOnError="True" Type="Double" ValidationGroup="CarCadastro" />
			Longitude:
			<ew:NumericBox ID="numLongitude" runat="server" CssClass="Modulo" 
				DecimalPlaces="8" DecimalSign=","
				GroupingSeparator="." PlacesBeforeDecimal="3" TextAlign="Right" 
				Width="120px" ToolTip="Longitude em números decimais"></ew:NumericBox>
			<asp:RangeValidator ID="rvLong" runat="server" ControlToValidate="numLongitude" 
				Display="Dynamic" ErrorMessage="deve pertencer ao intervalo -180 à 180." 
				MaximumValue="180" MinimumValue="-180" SetFocusOnError="True" Type="Double" ValidationGroup="CarCadastro" />
				&nbsp;&nbsp;<asp:HyperLink ID="hypUTM" runat="server" ToolTip="Conversor de UTM em Lat/Long e vice-versa"><img border="0" src="../imagens/wizard.gif"></img>Conversor UTM</asp:HyperLink>
	&nbsp;<span class="style1">(opcional)</span>&nbsp;<asp:LinkButton runat="server" CausesValidation="False" CssClass="BotaoCmd"
				ToolTip="Ajuda ao Usu&#225;rio" ID="hlpCoordenada"><img src="../imagens/info.gif" 
				border="0"></asp:LinkButton>
	</td>--%>
	<tr>
		<td colspan="2">
			<div align="center">
				<asp:Label ID="lblMensagem" runat="server" ForeColor="Red" CssClass="Modulo"></asp:Label>
			</div>
		</td>
	</tr>
	<tr>
		<td class="Modulo" colspan="2" width="  80%" align="center">
			<hr class="Modulo" width="100%" size="1">
			<div align="center">
				<asp:LinkButton ID="cmdAtualiza" runat="server" CssClass="BotaoCmd" BorderStyle="none"
					Text="&lt;img src=&quot;../imagens/atualizar.gif&quot; border='0'&gt;Salvar"
					OnClick="Atualiza" ToolTip="Grava as alterações" ValidationGroup="CarCadastro"></asp:LinkButton>
				&nbsp;<asp:LinkButton ID="cmdFinaliza" runat="server" CssClass="BotaoCmd" ToolTip="Grava e retorna para a Consulta"
					BorderStyle="none" Text="&lt;img src=&quot;../imagens/retornar.gif&quot; border='0'&gt;Retornar"
					OnClick="Retorna" ValidationGroup="CarCadastro" Visible="False"></asp:LinkButton>
				&nbsp;<asp:LinkButton ID="cmdExclui" runat="server" CssClass="BotaoCmd" CausesValidation="False"
					BorderStyle="none" Text="&lt;img src=&quot;../imagens/excluir.gif&quot; border='0'&gt;Excluir"
					OnClick="Exclui" ToolTip="Exclui este cadastro" Visible="False"></asp:LinkButton>
			</div>
		</td>
	</tr>
</table>
