# StudioShell solution module for Empty
# 

$menuItems = @(
    # list any menu items your module adds here
    #
    # e.g.:
    # new-item dte:/commandbars/help -name 'about my module' -value { 'Module Empty' | out-outputpane; invoke-item dte:/windows/output; }	
	new-item dte:/commandbars/solution/add -name 'New Sample P2F Provider' -value { new-providerProject 'SampleProvider' }
);

# this function is called automatically when your solution is unloaded
function unregister-Empty
{
    # remove any added menu items;
    $menuItems | remove-item;
}

function new-providerProject( $name )
{
	# create the project
	new-item "dte:\solution\projects\$name" -type classlibrary -language csharp
	
	
	# add necessary references
	new-item "dte:\solution\projects\$name\references" -type assembly -name System.Management.Automation
	
	install-package "P2F" -project $name;	
	#new-item "dte:\solution\projects\$name\references" -type project -name CodeOwls.PowerShell.Paths
	#new-item "dte:\solution\projects\$name\references" -type project -name CodeOwls.PowerShell.Provider

	$project = get-item "dte:\solution\projects\$name"
	$project.configurationmanager | foreach {
		$_.properties.item('startprogram').value = "c:\windows\system32\windowspowershell\v1.0\powershell.exe"
		$_.properties.item('startarguments').value = '-noexit -command "ls *.dll | ipmo"'
		$_.properties.item('startaction').value = 1
	}
    $dte.solution.projects.item("StartupProject").value = $project.name;
}
