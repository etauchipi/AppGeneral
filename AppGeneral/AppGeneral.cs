using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace AppGeneral
{
    public class AppGeneral
    {

		private readonly string[] Blacklist = new string[] { "%252f", "%c0%af","%c1%1c","%ef%bc%8f","%e2%88%95","%252E%252E%252F", "%e2%81%84","%cc%b8","%cc%b7",".%u2216",".%255c","%2e%2e%5c",".%2f","%2e%2e/","%2e%2e%2f","<", ">", "?", ":", "~","..", "./","'", "--", ";--", ";", "|", "/*", "*/", "=", "[", "%", "@@", "@", "char", "nchar", "varchar", "nvarchar", "alter", "begin", "cast", "create", "cursor", "declare", "delete", "drop", "end", "exec", "execute", "fetch", "insert", "kill", "open", "select", "sys", "sysobjects", "syscolumns", "table", "update", "grant", "shutdown", "information_schema", "exists", "command", "load_file", "privileges", "group_concat", "db_name", "object_schema_name", "schema_id", "schema_name", "connectionproperty", "context_info", "host_id", "$partition", "current_user", "session_user", "suser_id", "suser_name", "user_name", "original_login", "permissions", "database_principal_id", "pwdencrypt", "pwdcompare", "db_name", "serverproperty", "scope_identity", "databasepropertyex", "applock_test"};


		public string CadenaFecha(string Cadena, FormatoFecha nFormato, Boolean Normalizada)
        {
            // Recibe una cadena que contiene una fecha con el formato "DD/MM/YYYY"
            // Y devuelve una cadena formateada de acuerdo a:

            // 0: DD/MM/YYYY
            // 1: DD-MM-YYYY
            // 2: MM/DD/YYYY
            // 3: MM-DD-YYYY
            // 4: YYYY/MM/DD
            // 5: YYYY-MM-DD
            // 6: YYYY/DD/MM
            // 7: YYYY-DD-MM
            // 8: iso YYYYMMDD

            string Retorno;
            string dia;
            string mes;
            string ano;
            string pos1;
            string pos2;
            string pos3;
            string separador;

            Retorno = string.Empty;
            pos1 = string.Empty;
            pos2 = string.Empty;
            pos3 = string.Empty;
            separador = string.Empty;

            dia = Cadena.Substring(0, 2);
            mes = Cadena.Substring(3, 2);
            ano = Cadena.Substring(6, 4);

            switch (nFormato)
            {
                case FormatoFecha.Slash_DD_MM_YYYY:
                    separador = "/";
                    pos1 = dia;
                    pos2 = mes;
                    pos3 = ano;
                    break;
                case FormatoFecha.Guion_DD_MM_YYYY:
                    separador = "-";
                    pos1 = dia;
                    pos2 = mes;
                    pos3 = ano;
                    break;
                case FormatoFecha.Slash_MM_DD_YYYY:
                    separador = "/";
                    pos1 = mes;
                    pos2 = dia;
                    pos3 = ano;
                    break;
                case FormatoFecha.Guion_MM_DD_YYYY:
                    separador = "-";
                    pos1 = mes;
                    pos2 = dia;
                    pos3 = ano;
                    break;
                case FormatoFecha.Slash_YYYY_MM_DD:
                    separador = "/";
                    pos1 = ano;
                    pos2 = mes;
                    pos3 = dia;
                    break;
                case FormatoFecha.Guion_YYYY_MM_DD:
                    separador = "-";
                    pos1 = ano;
                    pos2 = mes;
                    pos3 = dia;
                    break;
                case FormatoFecha.Slash_YYYY_DD_MM:
                    separador = "/";
                    pos1 = ano;
                    pos2 = dia;
                    pos3 = mes;
                    break;
                case FormatoFecha.Guion_YYYY_DD_MM:
                    separador = "-";
                    pos1 = ano;
                    pos2 = dia;
                    pos3 = mes;
                    break;
                case FormatoFecha.iso_YYYYMMDD:
                    separador = "";
                    pos1 = ano;
                    pos2 = mes;
                    pos3 = dia;
                    break;
                default:
                    separador = "";
                    pos1 = ano;
                    pos2 = mes;
                    pos3 = dia;
                    break;
            }

            Retorno = pos1 + separador + pos2 + separador + pos3;

            return Retorno;

        }

        public string CadenaFecha(DateTime Fecha, FormatoFecha nFormato, Boolean Normalizada)
        {
            //Recibe una fecha
            //Y devuelve una cadena formateada de acuerdo a:

            //0: DD/MM/YYYY
            //1: DD-MM-YYYY
            //2: MM/DD/YYYY
            //3: MM-DD-YYYY
            //4: YYYY/MM/DD
            //5: YYYY-MM-DD
            //6: YYYY/DD/MM
            //7: YYYY-DD-MM
            //8: iso YYYYMMDD

            string Retorno;
            string dia;
            string mes;
            string ano;
            string cadena;

            Retorno = string.Empty;
            cadena = string.Empty;
            dia = Fecha.Day.ToString("00");
            mes = Fecha.Month.ToString("00");
            ano = Fecha.Year.ToString("####");
            cadena = dia + "/" + mes + "/" + ano;

            Retorno = CadenaFecha(cadena, nFormato, Normalizada);

            return Retorno;

        }

		public bool CadenaValida(string Cadena)
		{
			bool Retorno;

			Retorno = true;

			Retorno = (Cadena.Trim() == CleanString(Cadena.Trim()));

			return Retorno;

		}

        public string CleanString(string Cadena)
            {
                
                string Retorno;
                string cad;
                int nMax;

                Retorno = Cadena;
                cad = string.Empty;
                
                nMax = Blacklist.GetUpperBound(0);

                for (int i = 0; i <= nMax; i++)
                {
                    cad = Blacklist[i];
                    Retorno =  Retorno.Replace(cad, "");
                }

				return Retorno;
            }

		public string CleanStringforXML(string Cadena)
		{

			string Retorno;
			string cad;

			Retorno = Cadena;
			cad = string.Empty;

			cad = Cadena;
			cad = cad.Replace("&", "&amp;") ;
			cad = cad.Replace("<", "&lt;");
			cad = cad.Replace(">", "&gt;");
			cad = cad.Replace("'", "&apos;");
			cad = cad.Replace("\"", "&quot;");

			Retorno = cad;

			return Retorno;
		}

		public string Cadena_br_crlf(string Cadena)
            {

            string Retorno;

            Retorno = string.Empty;
            Retorno = Cadena.Replace("</br>", Environment.NewLine);
            return Retorno;

            }

        public string Cadena_crlf_br(string Cadena)
            {

                string Retorno;

                Retorno = string.Empty;
                Retorno = Cadena.Replace(Environment.NewLine, "</br>");
                Retorno = Cadena.Replace("\r\n", "</br>");
                Retorno = Cadena.Replace("\r", "</br>");
                return Retorno;

            }

		public string Cadena_To_Base64(string Cadena)
			{

				string Retorno;
				byte[] byt  = System.Text.Encoding.UTF8.GetBytes(Cadena.Trim());

				Retorno = string.Empty;

				//convierte el array a una cadena base 64
				Retorno = Convert.ToBase64String(byt);

				return Retorno;

			}

		public string Cadena_From_Base64(string Cadena)
			{

				string Retorno;
				byte[] byt = Convert.FromBase64String(Cadena);

				Retorno = string.Empty;

				//convierte el array a una cadena UTF8
				Retorno = System.Text.Encoding.UTF8.GetString(byt);

				return Retorno;

			}

        public bool eMailVerifica(string eMail)
            {

				bool Retorno;
				string pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

				Retorno = true;

				try
				{
					Retorno = Regex.IsMatch(eMail, pattern);
				}
				catch (Exception)
				{
					Retorno = false;
				}

				return Retorno;
        
            }

		public void RegistreEventoError(Exception ex , [Optional] string App)
			{

				System.Diagnostics.EventLog Log;

				try
				{
					Log = new System.Diagnostics.EventLog();

					if ((App != "Application") && (App != string.Empty))
						Log.Source = App;
					else
						Log.Source = "Application";

					Log.WriteEntry(string.Format(
						"App Origen: " + App + Environment.NewLine +
						"Mensaje: {0}" + Environment.NewLine +
						"Fuente: {1}" + Environment.NewLine +
						"Targetsite: {2}" + Environment.NewLine +
						"StackTrace: {3}" , Environment.NewLine,
						ex.Message , ex.Source , ex.TargetSite.Name , ex.StackTrace),
						System.Diagnostics.EventLogEntryType.Error);
										
				}
				catch (Exception)
				{
					//throw;
				}
			}

		public void RegistreEvento(string sEvento, TipoEvento nTipo, string sApp)
			{

				System.Diagnostics.EventLog Log;
				string sCadena;
				string dFecha;

				dFecha = " - Fecha: " + DateTime.Now.ToShortDateString();
				sCadena = sApp.Trim() + " - Evento: ";

				switch (nTipo)
				{
					case TipoEvento.Ev_General:
						sCadena += "General - ";
						break;
					case TipoEvento.Ev_Login:
						sCadena += "Login - ";
						break;
					case TipoEvento.Ev_Logout:
						sCadena += "Logout - ";
						break;
					case TipoEvento.Ev_Modifica:
						sCadena += "Modificación - ";
						break;
					case TipoEvento.Ev_Elimina:
						sCadena += "Eliminación - ";
						break;
					case TipoEvento.Ev_Adiciona:
						sCadena += "Adición - ";
						break;
					case TipoEvento.Ev_Crear:
						sCadena += "Creación - ";
						break;
					case TipoEvento.Ev_IntentoLogin:
						sCadena += "Fallo intentando Login - ";
						break;
					default:
						break;
				}

				sCadena += sEvento + dFecha;

				try
				{
                Log = new System.Diagnostics.EventLog
                {
                    Source = sApp
                };
                Log.WriteEntry(sCadena);
				}
				catch (Exception ex)
				{
					RegistreEventoError(ex, sApp);
				}
			}

		public void Delete_Files_Date(string sPath, DateTime Fecha, bool Todos)
			{

				DirectoryInfo DirectorioInfo;
				IOrderedEnumerable<FileInfo> FilesList;
				List<FileInformacion> Retorno;
				FileInformacion nFile;

				DirectorioInfo = new DirectoryInfo(sPath);
				Retorno = new List<FileInformacion>();
				nFile = new FileInformacion();

				//Opción por defecto
				FilesList = DirectorioInfo.GetFiles("*.*").OrderBy(p => p.LastWriteTime);

				foreach (FileInfo fi in FilesList)
				{
					nFile = new FileInformacion();

					nFile = Carga_FileInfo(fi);

					if (Todos)
						fi.Delete();
					else
						if (((nFile.Fec_UEscritura).Day != (Fecha.Day)) || ((nFile.Fec_UEscritura).Month != (Fecha.Month))) 
							fi.Delete();
				}

			}

		public List<FileInformacion> Get_Files_List(String sPath, FileOrden Opcion, bool OrdenInverso, bool BuscarEnSubdirectorios, [Optional] String Wildcard)
			{

				List<FileInformacion> Retorno;
				DirectoryInfo DirectorioInfo;
				IOrderedEnumerable<FileInfo> FilesList;
				FileInformacion nFile;
				SearchOption NivelBusqueda;

				DirectorioInfo = new DirectoryInfo(sPath);
				Retorno = new List<FileInformacion>();
				nFile = new FileInformacion();

				if (Wildcard == string.Empty)
					Wildcard = "*.*";

				if (BuscarEnSubdirectorios)
					NivelBusqueda = SearchOption.AllDirectories;
				else
					NivelBusqueda = SearchOption.TopDirectoryOnly;

				switch (Opcion)
				{
					case FileOrden.Nombre:
						if (OrdenInverso)
							FilesList = DirectorioInfo.GetFiles(Wildcard, NivelBusqueda).OrderBy(p => p.Name);
						else
							FilesList = DirectorioInfo.GetFiles(Wildcard, NivelBusqueda).OrderByDescending(p => p.Name);
					 break;
					case FileOrden.Extension:
						if (OrdenInverso)
							FilesList = DirectorioInfo.GetFiles(Wildcard, NivelBusqueda).OrderBy(p => p.Extension);
						else
							FilesList = DirectorioInfo.GetFiles(Wildcard, NivelBusqueda).OrderByDescending(p => p.Extension);
					 break;
					case FileOrden.Fec_Creacion:
						if (OrdenInverso)
							FilesList = DirectorioInfo.GetFiles(Wildcard, NivelBusqueda).OrderBy(p => p.CreationTime);
						else
							FilesList = DirectorioInfo.GetFiles(Wildcard, NivelBusqueda).OrderByDescending(p => p.CreationTime);
					 break;
					case FileOrden.Ult_Acceso:
						if (OrdenInverso)
							FilesList = DirectorioInfo.GetFiles(Wildcard, NivelBusqueda).OrderBy(p => p.LastAccessTime);
						else
							FilesList = DirectorioInfo.GetFiles(Wildcard, NivelBusqueda).OrderByDescending(p => p.LastAccessTime);
					 break;
					case FileOrden.Ult_Escritura:
						if (OrdenInverso)
							FilesList = DirectorioInfo.GetFiles(Wildcard, NivelBusqueda).OrderBy(p => p.LastWriteTime);
						else
							FilesList = DirectorioInfo.GetFiles(Wildcard, NivelBusqueda).OrderByDescending(p => p.LastWriteTime);
					 break;
					case FileOrden.Tamano:
						if (OrdenInverso)
							FilesList = DirectorioInfo.GetFiles(Wildcard, NivelBusqueda).OrderBy(p => p.Length);
						else
							FilesList = DirectorioInfo.GetFiles(Wildcard, NivelBusqueda).OrderByDescending(p => p.Length);
					 break;
					default:
						if (OrdenInverso)
							FilesList = DirectorioInfo.GetFiles(Wildcard, NivelBusqueda).OrderBy(p => p.Name);
						else
							FilesList = DirectorioInfo.GetFiles(Wildcard, NivelBusqueda).OrderByDescending(p => p.Name);
					 break;
				}

				foreach (FileInfo fi in FilesList)
				{
					nFile = new FileInformacion();
					nFile = Carga_FileInfo(fi);
					Retorno.Add(nFile);
				}

				return Retorno;

			}

		private FileInformacion Carga_FileInfo(FileInfo fi)
			{

				FileInformacion Retorno;

            Retorno = new FileInformacion
            {
                Extension = fi.Extension,
                Fec_Creacion = fi.CreationTime,
                Fec_UAcceso = fi.LastAccessTime,
                Fec_UEscritura = fi.LastWriteTime,
                Nombre = fi.Name,
                Tamano = fi.Length,
                SoloLectura = fi.IsReadOnly
            };

            return Retorno;

			}

		public class FileInformacion
			{
				public string Nombre { get; set; }
				public string Extension { get; set; }
				public DateTime Fec_Creacion { get; set; }
				public DateTime Fec_UAcceso { get; set; }
				public DateTime Fec_UEscritura { get; set; }
				public long Tamano { get; set; }
				public bool SoloLectura { get; set; }
			}

        public enum FileOrden
        {
            Nombre = 0x1,
            Extension = 0x2,
            Fec_Creacion = 0x3,
            Ult_Acceso = 0x4,
            Ult_Escritura = 0x5,
            Tamano = 0x6,
        }

        public enum TipoEvento
        {
            // nTipo: 0=General, 1=Login, 2=Logout, 3=Modifica, 4=Elimina, 5=Adiciona, 6=Crear, 7 Intento Login
            Ev_General = 0x0,
            Ev_Login = 0x1,
            Ev_Logout = 0x2,
            Ev_Modifica = 0x3,
            Ev_Elimina = 0x4,
            Ev_Adiciona = 0x5,
            Ev_Crear = 0x6,
            Ev_IntentoLogin = 0x7
        }

        public enum FormatoFecha
        {
            // 0: DD/MM/YYYY
            // 1: DD-MM-YYYY
            // 2: MM/DD/YYYY
            // 3: MM-DD-YYYY
            // 4: YYYY/MM/DD
            // 5: YYYY-MM-DD
            // 6: YYYY/DD/MM
            // 7: YYYY-DD-MM
            // 8: iso YYYYMMDD

            Slash_DD_MM_YYYY = 0x0,
            Guion_DD_MM_YYYY = 0x1,
            Slash_MM_DD_YYYY = 0x2,
            Guion_MM_DD_YYYY = 0x3,
            Slash_YYYY_MM_DD = 0x4,
            Guion_YYYY_MM_DD = 0x5,
            Slash_YYYY_DD_MM = 0x6,
            Guion_YYYY_DD_MM = 0x7,
            iso_YYYYMMDD = 0x8
        }

    }

}



