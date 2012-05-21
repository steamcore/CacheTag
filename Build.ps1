
param(
	$Target = "Build",
	$Configuration = "Release"
)

& Msbuild.exe `
	/p:"Configuration=$Configuration" `
	/p:"BuildTargetDir=..\..\Build" `
	/t:$Target `
	"Source\CacheTag.Essentials.sln"

if ($Target -eq 'Clean') {
	rmdir -Recurse "Build"
}
