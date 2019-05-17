using HLDOMAINMANAGERPROCESSLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.IO;

namespace AnsibleActivateCatalog
{
    class Program
    {
        static void Main(string[] args)
        {
            string localServer = "127.0.0.1";
            string sqlUserName = string.Empty;
            string sqlPassword = string.Empty;
            string databaseLocation = @"C:\DatabaseBkup\";
            string sErrorInfo = string.Empty;
            string catalogName = string.Empty;
            string catalogInfoXML = string.Empty;
            string catalogUpdatedName = string.Empty;
            string smsCatalogUpdatedName = string.Empty;
            int lErrorCode = 0;
            string hostIP = "127.0.0.1";
            StringBuilder logFile = new StringBuilder();
            try
            {
                if (args != null && args.Count() == 4)
                {
                    catalogName = args[0].StartsWith("/") ? args[0].Replace(@"/", string.Empty) : args[0];
                    sqlUserName = args[1].StartsWith("/") ? args[1].Replace(@"/", string.Empty) : args[1];
                    sqlPassword = args[2].StartsWith("/") ? args[2].Replace(@"/", string.Empty) : args[2];
                    hostIP = args[3].StartsWith("/") ? args[3].Replace(@"/", string.Empty) : args[3];
                    //Create new NMX Catalog.
                    catalogName = catalogName.Replace(".hzp", string.Empty);
                    catalogUpdatedName = Regex.Replace(catalogName, @"[^a-zA-Z0-9]", string.Empty);     //Replace all Alphanumerics characters.
                    if (Regex.IsMatch(catalogName, @"^\d"))
                    {
                        catalogUpdatedName = "A" + catalogUpdatedName;
                    }
                    smsCatalogUpdatedName = catalogUpdatedName + "_SMS";
                    if (!string.IsNullOrWhiteSpace(catalogName) && !string.IsNullOrWhiteSpace(catalogUpdatedName) && !string.IsNullOrWhiteSpace(smsCatalogUpdatedName))
                    {
                        //Create XML to pass as catalog information.
                        catalogInfoXML = @"<DatabaseList FileName=""" + catalogName + @".hzp""><Item><Name>" + catalogUpdatedName + "</Name><Info><Type>NMX</Type></Info></Item><Item><Name>" + catalogUpdatedName + "</Name><Info><Type>AUT</Type></Info></Item> <Item><Name>" + catalogUpdatedName + "_SMS</Name><Info><Type>SMS</Type></Info></Item></DatabaseList>";
                    }

                    logFile.AppendLine("Catalog names fetch without issue. " + DateTime.Now);
                    if (!string.IsNullOrWhiteSpace(catalogUpdatedName) && !string.IsNullOrWhiteSpace(smsCatalogUpdatedName) && !string.IsNullOrWhiteSpace(sqlUserName) && !string.IsNullOrWhiteSpace(sqlPassword))
                    {
                        CHLDomainManagerClass dm = new CHLDomainManagerClass();
                        if (dm != null)
                        {
                            logFile.AppendLine("DM object created without any issue. " + DateTime.Now);
                            string DSNString = "<Properties><DM_DSNSTRING>Provider=SQLOLEDB;Data Source=" + localServer + ";Initial Catalog=" + catalogUpdatedName + "</DM_DSNSTRING></Properties>";
                            string SMSDSNString = "<Properties><DM_SMSDSNSTRING>Provider=SQLOLEDB;Data Source=" + localServer + ";Initial Catalog=" + smsCatalogUpdatedName + "</DM_SMSDSNSTRING></Properties>";
                            //Check if Server is stop then only change the catalog.
                            if (dm.IsSystemReady() == 0)
                            {
                                logFile.AppendLine("Server was stopped. Creating NMX catalog. " + DateTime.Now);
                                if (dm.CreateCatalog_New(localServer, sqlUserName, sqlPassword, catalogUpdatedName, 0, "", "", ref sErrorInfo))
                                {
                                    logFile.AppendLine("Creating SMS catalog. " + DateTime.Now);
                                    //Create new SMS Catalog.
                                    if (dm.CreateCatalog_New(localServer, sqlUserName, sqlPassword, smsCatalogUpdatedName, 1, "", "", ref sErrorInfo))
                                    {
                                        logFile.AppendLine("Restoring Catalog. " + DateTime.Now);
                                        //Restore Catalog
                                        if (dm.RestoreCatalogs_New(localServer, sqlUserName, sqlPassword, catalogInfoXML, "", databaseLocation, false, ref sErrorInfo))
                                        {
                                            logFile.AppendLine("Make Catalog Active; Setting DSN String. " + DateTime.Now);
                                            //Make Active
                                            if (dm.SetDomainProperties(DSNString, "", "", ref sErrorInfo))
                                            {
                                                logFile.AppendLine("Setting SMS DSN String. " + DateTime.Now);
                                                if (dm.SetDomainProperties(SMSDSNString, "", "", ref sErrorInfo))
                                                {
                                                    logFile.AppendLine("Setting Machine info. " + DateTime.Now);
                                                    //Set the IP address of provided machine
                                                    if (dm.SetMachineInfo("1", "Local PC", hostIP, "", "", "", "", ref sErrorInfo))
                                                    {
                                                        logFile.AppendLine("Load from DB. " + DateTime.Now);
                                                        //Load From DB
                                                        if (dm.LoadFromDB("", "", out lErrorCode, ref sErrorInfo))
                                                        {
                                                            logFile.AppendLine("Start the server. " + DateTime.Now);
                                                            //start the server
                                                            dm.StartDomain("", ref lErrorCode, ref sErrorInfo);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (!string.IsNullOrWhiteSpace(sErrorInfo))
                                {
                                    logFile.AppendLine("Error File Content: " + sErrorInfo);
                                }
                            }
                        }
                    }
                }
                File.WriteAllText(@"c:\Temp\AnsibleLogs\ActivateCatalogLog.txt", logFile.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Source);
            }
            finally
            {
                logFile = null;
            }
        }
    }
}
