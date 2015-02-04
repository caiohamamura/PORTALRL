<%@ Page Language="c#" CodeBehind="RLAdm.aspx.cs" AutoEventWireup="True" Inherits="Portal.PortalRL.RLAdm"
    SmartNavigation="False" MasterPageFile="~/MasterPages/Sigam3.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="RLCadastro.ascx" TagName="RLCadastro" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="conteudo" EnableViewState="true" runat="server">
    <style type="text/css">
        .TabPanelPostbackBtn {
            display: none;
        }
    </style>
    <table id="cabecalho" cellpadding="1" cellspacing="1" class="TabelaFicha TabFixNoPadding" style="width: 99%; margin-left: auto; margin-right: auto">
        <tr>
            <td rowspan="3" style="text-align: center; width: 155px">
                <asp:Image ID="imgLogo" runat="server" ImageUrl="../imagens/logo/rlLogo.jpg" AlternateText="Logo PortalRL"
                    Width="150px" />
            </td>
            <td class="TituloItem">Nome:
				<asp:Label ID="lblTitulo" runat="server" CssClass="TituloDado">[nomRecomendacao]</asp:Label>
            </td>
            <td width="150px">
                <asp:LinkButton runat="server" CausesValidation="False" CssClass="BotaoCmd" ToolTip="Ajuda ao Usu&#225;rio"
                    ID="cmdHelp"><img src="../imagens/help.gif" border="0"> Ajuda</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td class="TituloItem" colspan="2">Número da Recomendação:
				<asp:Label ID="lblID" runat="server" CssClass="TituloDado"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="TituloItem">Usuário:
				<asp:Label ID="lblUsuario" runat="server" CssClass="TituloDado">[Usuário]</asp:Label>
            </td>
            <td style="white-space: nowrap" width="150px">
                <asp:Label ID="lblSituacao" runat="server" CssClass="TituloDado" Text="Em Cadastramento"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <ajaxToolkit:TabContainer runat="server" ID="TabNavegacao" Width="100%" ActiveTabIndex="0"
                    BorderStyle="None" Visible="true" Enabled="true" ScrollBars="Auto">
                    <ajaxToolkit:TabPanel runat="server" ID="TBCadastro">
                        <HeaderTemplate>
                            <span title="Dados da Recomendação">Recomendação</span>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <uc1:RLCadastro ID="rlCadastro" runat="server" OnOnCadastroAtualizado="rlCadastro_OnCadastroAtualizado"></uc1:RLCadastro>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="server" ID="TBEscolhaEspecies" HeaderText="Escolha das Espécies">
                        <ContentTemplate>        
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel runat="server" ID="TBFinaliza" HeaderText="Finalizar">
                        <ContentTemplate>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center" height="40px" valign="middle">
                <asp:LinkButton ID="cmdAnterior" runat="server" CssClass="BotaoCmd" ToolTip="Retorna para o Anterior"
                    OnClick="Anterior" CausesValidation="False"><img 
					src="../imagens/setas/anterior.gif" border='0'> Anterior</asp:LinkButton>
                &nbsp;
				<asp:LinkButton ID="cmdRetorna" runat="server" CssClass="BotaoCmd" ToolTip="Sair do Cadastro"
                    OnClick="Retornar" CausesValidation="False"><img 
				src="../imagens/fechar.gif" border='0'> Sair</asp:LinkButton>
                &nbsp;
				<asp:LinkButton ID="cmdProximo" runat="server" CssClass="BotaoCmd" ToolTip="Avança para o Próximo"
                    OnClick="Proximo" CausesValidation="False">Próximo <img 
					src="../imagens/setas/proxima.gif" border='0'></asp:LinkButton>
            </td>
        </tr>
    </table>
</asp:Content>
