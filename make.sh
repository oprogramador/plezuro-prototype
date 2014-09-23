#mkdir -p ../res
#if [ ! -f ../res/world.db ]; then
#	cp world.db ../res/world.db
#fi
gmcs -pkg:dotnet -out:calc.exe `find src -name '*.cs'`
