mkdir -p ../res
if [ ! -f ../res/world.db ]; then
	cp world.db ../res/world.db
fi
gmcs -pkg:dotnet -out:../calc.exe csharp_src/*.cs csharp_src/*/*.cs csharp_src/*/*/*.cs
