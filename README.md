# msbuild-provider

A powershell provider for MSBuild projects

This is a very early spike.  Use at your own risk.

# Example

```powershell
import-module codeowls.powershell.msbuild.dll
new-psdrive -name p -psprovider MSBuild -root c:\path\to\myproject.csproj
cd p:
dir
```
