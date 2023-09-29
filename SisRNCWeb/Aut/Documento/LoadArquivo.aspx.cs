using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.IO;
using System.Text;

using APB.Mercury.Exceptions;
using HMP.DataObjects.SisRNCWeb;
using HMP.DataObjects.SisRNCWeb.QueryDictionaries;
using HMP.WebInterface.SisRNCWeb.Www.DataAccess;

using Winnovative.WnvHtmlConvert;
using Winnovative.WnvHtmlConvert.PdfDocument;

public partial class Aut_Documento_LoadArquivo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Request["ANXOCR_ID"] != null)
            {
                LoadAnexoOcorrencia(Request["ANXOCR_ID"].DBToDecimal());
            }

            if (Request["ANXACS_ID"] != null)
            {
                LoadAnexoAcao(Request["ANXACS_ID"].DBToDecimal());
            }

            if (Request["ANCE_ID"] != null)
            {
                LoadAnexoCausaEfeito(Request["ANCE_ID"].DBToDecimal());
            }

        }
        catch (WebManagerException em)
        {
            em.TratarExcecao(true);
        }
        catch (Exception err)
        {
            (new UnknownException(err)).TratarExcecao(true);
        }
    }


    private void LoadAnexoOcorrencia(decimal pANXOCR_ID)
    {

        try
        {
            DataTable lTableImagem = new DataTable();

            lTableImagem = NC_AnexoOcorrenciaDo.GetArquivoAnexoOcorrenciaById(pANXOCR_ID, LocalInstance.ConnectionInfo);


            if (lTableImagem.Rows.Count > 0)
            {
                int ArraySize = new int();

                byte[] documento = new byte[0];
                

                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                documento = (byte[])lTableImagem.Rows[0][NC_AnexoOcorrenciaQD._ANXOCR_ARQUIVO.Name];


                ArraySize = documento.GetUpperBound(0);
                string tpArquivo = "";
                string[] lAnexoDescricao;

                lAnexoDescricao = lTableImagem.Rows[0][NC_AnexoOcorrenciaQD._ANXOCR_DESCRICAO.Name].ToString().Split('.');

                tpArquivo = lAnexoDescricao[lAnexoDescricao.Length - 1];

                Response.Clear();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                Response.AppendHeader("Content-Disposition", "attachment; filename = " + lTableImagem.Rows[0][NC_AnexoOcorrenciaQD._ANXOCR_DESCRICAO.Name].ToString().ToLower() + "; size=" + documento.Length.ToString());

                if (tpArquivo.ToLower() == "pdf")
                    Response.ContentType = "application/pdf";
                else if (tpArquivo.ToLower() == "doc")
                    Response.ContentType = "application/msword";
                else if (tpArquivo.ToLower() == "docx")
                    Response.ContentType = "application/ms-word";
                else if (tpArquivo.ToLower() == "xls" || tpArquivo.ToLower() == "xlsx")
                    Response.ContentType = "application/vnd.ms-excel";
                else if (tpArquivo.ToLower() == "txt")
                    Response.ContentType = "application/msword";
                else if (tpArquivo.ToLower() == "jpg")
                    Response.ContentType = "image/jpeg";

                Response.BinaryWrite(documento);
                Response.Flush();
                Response.End();
            }
        }
        catch
        {

        }
    }


    private void LoadAnexoAcao(decimal pANXACS_ID)
    {

        try
        {
            DataTable lTableImagem = new DataTable();

            lTableImagem = NC_AnexoAcoesDo.GetArquivoAnexoAcoesById(pANXACS_ID, LocalInstance.ConnectionInfo);


            if (lTableImagem.Rows.Count > 0)
            {
                int ArraySize = new int();

                byte[] documento = new byte[0];


                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                documento = (byte[])lTableImagem.Rows[0][NC_AnexoAcoesQD._ANXACS_ARQUIVO.Name];


                ArraySize = documento.GetUpperBound(0);
                string tpArquivo = "";
                string[] lAnexoDescricao;

                lAnexoDescricao = lTableImagem.Rows[0][NC_AnexoAcoesQD._ANXACS_DESCRICAO.Name].ToString().Split('.');

                tpArquivo = lAnexoDescricao[lAnexoDescricao.Length - 1];

                Response.Clear();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                Response.AppendHeader("Content-Disposition", "attachment; filename = " + lTableImagem.Rows[0][NC_AnexoAcoesQD._ANXACS_DESCRICAO.Name].ToString().ToLower() + "; size=" + documento.Length.ToString());

                if (tpArquivo.ToLower() == "pdf")
                    Response.ContentType = "application/pdf";
                else if (tpArquivo.ToLower() == "doc")
                    Response.ContentType = "application/msword";
                else if (tpArquivo.ToLower() == "docx")
                    Response.ContentType = "application/ms-word";
                else if (tpArquivo.ToLower() == "xls")
                    Response.ContentType = "application/vnd.ms-excel";
                else if (tpArquivo.ToLower() == "txt")
                    Response.ContentType = "application/msword";
                else if (tpArquivo.ToLower() == "jpg")
                    Response.ContentType = "image/jpeg";

                Response.BinaryWrite(documento);
                Response.Flush();
                Response.End();
            }
        }
        catch
        {

        }
    }


    private void LoadAnexoCausaEfeito(decimal pANCE_ID)
    {

        try
        {
            DataTable lTableImagem = new DataTable();

            lTableImagem = NC_AnaliseCausaEfeitoDo.GetNC_AnaliseCausaEfeitoById(pANCE_ID, LocalInstance.ConnectionInfo);


            if (lTableImagem.Rows.Count > 0)
            {
                int ArraySize = new int();

                byte[] documento = new byte[0];


                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                documento = (byte[])lTableImagem.Rows[0][NC_AnaliseCausaEfeitoQD._ANCE_ARQUIVO.Name];


                ArraySize = documento.GetUpperBound(0);
                string tpArquivo = "";
                string[] lAnexoDescricao;

                lAnexoDescricao = lTableImagem.Rows[0][NC_AnaliseCausaEfeitoQD._ANCE_ARQUIVODESCRICAO.Name].ToString().Split('.');

                tpArquivo = lAnexoDescricao[lAnexoDescricao.Length - 1];

                Response.Clear();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                Response.AppendHeader("Content-Disposition", "attachment; filename = " + lTableImagem.Rows[0][NC_AnaliseCausaEfeitoQD._ANCE_ARQUIVODESCRICAO.Name].ToString().ToLower() + "; size=" + documento.Length.ToString());

                if (tpArquivo.ToLower() == "pdf")
                    Response.ContentType = "application/pdf";
                else if (tpArquivo.ToLower() == "doc")
                    Response.ContentType = "application/msword";
                else if (tpArquivo.ToLower() == "docx")
                    Response.ContentType = "application/ms-word";
                else if (tpArquivo.ToLower() == "xls")
                    Response.ContentType = "application/vnd.ms-excel";
                else if (tpArquivo.ToLower() == "txt")
                    Response.ContentType = "application/msword";
                else if (tpArquivo.ToLower() == "jpg")
                    Response.ContentType = "image/jpeg";

                Response.BinaryWrite(documento);
                Response.Flush();
                Response.End();
            }
        }
        catch
        {

        }
    }


}
