using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Types;
using Sigam.CAR.LINQED;

namespace Portal.PortalRL
{
    /// <summary>
    /// Gerenciamento de interessados
    /// </summary>
    public static class RLBD
    {
        private static SqlConnection GetConn() { return new SqlConnection(ConfigurationManager.ConnectionStrings["StringDeConexao"].ConnectionString); }




        #region Recomendação Cadastro
        /// <summary>
        /// Atualiza Cadastro
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static int RLAtualiza(RLAtualizaDTO item)
        {
            using (var sqlconn = GetConn())
            using (var sqlCmd = new SqlCommand("[PORTALRL-DEV-001].dbo.RLAtualiza", sqlconn))
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddRange(new SqlParameter[]{
					new SqlParameter("@idRecomendacao", item.idRecomendacao ?? 0),
					new SqlParameter("@nomRecomendacao", item.nomRecomendacao),
                    new SqlParameter("@idUsuario", item.idUsuario),
                    new SqlParameter("@numCar", item.numCAR),
                    new SqlParameter("@idMunicipioFito", item.idMunicipioFito),
                    new SqlParameter("@idCombCarroChefe", item.idCombCarroChefe),
                    new SqlParameter("@idFinalidade", item.idFinalidade),
                    new SqlParameter("@datProjeto", item.datProjeto)
				});
                sqlconn.Open();
                return Util.DecodeFromDB<int>(sqlCmd.ExecuteScalar());
            }
        }
        #endregion
    }
}