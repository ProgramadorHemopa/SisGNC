using System;
using System.Data;

using HMP.WebInterface.SisRNCWeb.Www.DataAccess;
using HMP.DataObjects.SisRNCWeb.QueryDictionaries;
using HMP.DataObjects.SisRNCWeb;

/// <summary>
/// Summary description for EssentialFunctions
/// </summary>
namespace APB.Mercury.WebInterface.SCPWeb.Www.Essential
{
    public static class EssentialFunctions
    {
        public static string LoadLabelMessages(string Path, string WebForm, string lblInfo)
        {
            string Retorno = "";
            DataSet lista = new DataSet();
            DataRow[] dr;
            lista.ReadXml(Path + @"Xml/Labels.xml");
            dr = lista.Tables[0].Select("webform='" + WebForm + "' and label='" + lblInfo + "'");
            if (dr.Length > 0)
            {
                Retorno = dr[0]["value"].ToString();
            }

            return Retorno;
        }

        public static string LoadDOC_NUMERO(string pNumero)
        {
            try
            {
                string lReturn = "";

                lReturn = pNumero.Substring(0, 4) + "/" + pNumero.Substring(4, 5) + "-" + APB.Framework.Math.Module11.Execute(pNumero);

                return lReturn;
            }
            catch(Exception e)
            {
                return pNumero;
            }
        }

        public static string LoadStatus(string Data)
        {
            string lReturn = "";
            Data = Data.Trim();
            if (Data == "I")
                lReturn = "Inativo";
            else if (Data == "A")
                lReturn = "Ativo";

            return lReturn;
        }

        public static string LoadStatusFormularios(string Data)
        {
            string lReturn = "";
            Data = Data.Trim();
            if (Data == "I")
                lReturn = "Inativo";
            else if (Data == "A")
                lReturn = "Gerado";
            else if (Data == "R")
                lReturn = "Rascunho";
            else if (Data == "C")
                lReturn = "Cancelado";
            else if (Data == "S")
                lReturn = "Gerado";
            else if (Data == "N")
                lReturn = "Excluído";

            return lReturn;
        }

        public static string LoadStatusTriagem(string Data)
        {
            string lReturn = "";
            Data = Data.Trim();
            if (Data == "I")
                lReturn = "Inativo";
            else if (Data == "A")
                lReturn = "Triagem";
            else if (Data == "S")
                lReturn = "Atendimento Secretaria";
            else if (Data == "D")
                lReturn = "Atendimento Defensor";
            else if (Data == "U")
                lReturn = "Ausente";
            else if (Data == "FI") //OFFLINE
                lReturn = "Inativo";
            else if (Data == "FA") //OFFLINE
                lReturn = "Triagem";
            else if (Data == "FS") //OFFLINE
                lReturn = "Atendimento Secretaria";
            else if (Data == "FD") //OFFLINE
                lReturn = "Atendimento Defensor";
            else if (Data == "FU") //OFFLINE
                lReturn = "Ausente";



            return lReturn;
        }

        public static string LoadStatusAGAT(string Data)
        {
            string lReturn = "";
            Data = Data.Trim();
            if (Data == "I")
                lReturn = "Inativo";
            else if (Data == "A")
                lReturn = "Agendado";
            else if (Data == "T")
                lReturn = "Triagem";
            else if (Data == "U")
                lReturn = "Ausente";
            else if (Data == "F")
                lReturn = "Faltou";
            else if (Data == "FI") //OFFLINE
                lReturn = "Inativo";
            else if (Data == "FA") //OFFLINE
                lReturn = "Agendado";
            else if (Data == "FT") //OFFLINE
                lReturn = "Triagem";
            else if (Data == "FU") //OFFLINE
                lReturn = "Ausente";


            return lReturn;
        }

 
        public static string LoadCodition(string Data)
        {
            string lReturn = "";

            if (Data == "S")
                lReturn = "Sim";
            else if (Data == "N")
                lReturn = "Não";

            return lReturn;
        }

        public static string LoadLogico(string Data)
        {
            string lReturn = "";

            if (Data == "0")
                lReturn = "Sim";
            else if (Data == "1")
                lReturn = "Não";

            return lReturn;
        }

        public static string LoadOutrosLocaisVisitas(string Data)
        {
            string lReturn = "";

            if (Data == "")
                lReturn = "";
            else if (Data == "1")
                lReturn = "A Pedido";
            else if (Data == "2")
                lReturn = "Documentos";
            else if (Data == "3")
                lReturn = "Expansão";
            else if (Data == "4")
                lReturn = "Itinerância";
            else if (Data == "5")
                lReturn = "Mutirão";
            else if (Data == "6")
                lReturn = "Rebelião";

            return lReturn;
        }

        public static string LoadPrazoPADAC(DateTime pData)
        {
            string lReturn = "";
            double lPrazo = double.Parse(System.Configuration.ConfigurationManager.AppSettings["PrazoPADAC"]);
            
            try
            {
                lReturn = pData.AddDays(lPrazo).ToShortDateString();
            }
            catch (Exception e)
            {
                return lReturn;
            }

            return lReturn;
        }

      
    }
}
