rem ############################################
rem # GUI
rem ############################################

echo "killing NMX Designer.exe"
taskkill /IM "NMX Designer.exe" /F

echo "killing NMX Operator.exe"
taskkill /IM "NMX Operator.exe" /F

echo "killing Service Plan Editor.exe"
taskkill /IM "Service Plan Editor.exe" /F


echo "killing NMXDaemons.exe"
taskkill /IM "NMXDaemons.exe" /F

echo "killing Harmonic.GUI.DomainManagerUI.exe"
taskkill /IM "Harmonic.GUI.DomainManagerUI.exe" /F

echo "killing Harmonic.GUI.DistAlarmUI.exe"
taskkill /IM "Harmonic.GUI.DistAlarmUI.exe" /F

rem ############################################
rem # ORS
rem ############################################

echo "killing HLORManagerProcess.exe"
taskkill /IM "HLORManagerProcess.exe" /F

rem ############################################
rem # Element Manager
rem ############################################

echo "killing AsiaPlatformElmManagerModule
taskkill /IM "AsiaPlatformElmManagerModule.exe" /F

echo "killing AfricaDeviceSimulator.exe"
taskkill /IM "AfricaDeviceSimulator.exe" /F

echo "killing AfricaElementManager.exe"
taskkill /IM "AfricaElementManager.exe" /F          

echo "killing DCTElmManagerModule.exe"
taskkill /IM "DCTElmManagerModule.exe" /F

echo "killing EuropaElementManagerModule.exe"
taskkill /IM "EuropaElementManagerModule.exe" /F

echo "killing HmEuropaDaemonModule.exe"
taskkill /IM "HmEuropaDaemonModule.exe" /F

echo "killing ModulatorElmManagerModule.exe"
taskkill /IM "ModulatorElmManagerModule.exe" /F

echo "killing NewtecModulatorElmManagerModule.exe"
taskkill /IM "NewtecModulatorElmManagerModule.exe" /F

echo "killing NsgStatManager.exe"
taskkill /IM "NsgStatManager.exe" /F               

echo "killing SwitchElmManagerModule.exe"
taskkill /IM "SwitchElmManagerModule.exe" /F

echo "killing third party.exe"
taskkill /IM "ThirdPartyElementManagerModule.exe" /F

echo "killing Asia.exe"
taskkill /IM "ASIAPlatformElmManagerModule.exe" /F

echo "killing DMSElmManagerModule.exe"
taskkill /IM "DMSElmManagerModule.exe" /F

echo "killing EllipseElmManagerModule.exe"
taskkill /IM "EllipseElmManagerModule.exe" /F

echo "killing ProviewElmManagerModule.exe"
taskkill /IM "ProviewElmManagerModule.exe" /F

echo "killing XMSElementManagerModule.exe"
taskkill /IM "XMSElementManagerModule.exe" /F

rem ############################################
rem # Automation Server
rem ############################################

echo "killing XMLSAPIClient.exe"
taskkill /IM "XMLSAPIClient.exe" /F

echo "killing BinarySapiUI.exe"
taskkill /IM "BinarySapiUI.exe" /F

echo "killing XmlSapiServer.exe"
taskkill /IM "XmlSapiServer.exe" /F

echo "killing AutomationServerAdmin.exe"
taskkill /IM "AutomationServerAdmin.exe" /F
pskill .exe

echo "killing HLAutomationServer.exe"
taskkill /IM "HLAuto~1.exe" /F
taskkill /IM "HLAutomationServer.exe" /F

rem ############################################
rem # Alarm Manager
rem ############################################

echo "killing DvAlarmUI.exe"
taskkill /IM "DvAlarmUI.exe" /F

echo "killing DistAlarmUI.exe"
taskkill /IM "DistAlarmUI.exe" /F

echo "killing DvAlarmManagerModule.exe"
taskkill /IM "DvAlarmManagerModule.exe" /F

echo "killing TFTP Server"
taskkill /IM "BGTFTP.exe" /F

echo "killing BOOTP Server"
taskkill /IM "BGBootp.exe" /F

rem ############################################
rem # Domain Manager
rem ############################################

echo "killing HLDomainManagerProcess.exe"
taskkill /IM "HLDomainManagerProcess.exe" /F

rem ############################################
rem # Services
rem ############################################

net stop HLDCGDaemonProcess /y
taskkill /IM "HLDCGDaemonProcess.exe" /F

net stop HLDMSaemonProcess /y
taskkill /IM "HLDMSDaemonProcess.exe" /F

net stop HLDomainAgentProcess /y
taskkill /IM "HLDomainAgentProcess.exe" /F

net stop HLSvrEventService /y
taskkill /IM "HLSvrEventService.exe" /F

net stop HarmonicNmxGateway
taskkill /IM "HmNmxGatewayWinSvc.exe" /F

net stop NMXValidatorService
taskkill /IM "NMXValidatorService.exe" /F

net stop "NmxNg Server Controller"
taskkill /IM "NmxNgServerController.exe" /F

net stop HarmonicRemoteDebugLog
taskkill /IM "HmDebugLogWinSvc.exe" /F

net stop XServer
taskkill /IM "XServer.exe" /F

net stop snmp /y
net stop snmptrap /y
net stop nddsagent /y

exit