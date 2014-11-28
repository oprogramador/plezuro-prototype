#mkdir -p ../res
#if [ ! -f ../res/world.db ]; then
#	cp world.db ../res/world.db
#fi
gmcs -pkg:dotnet -out:plezuro.exe `find src -name '*.cs'` &&
chmod 775 plezuro.exe &&
sudo cp plezuro.exe /usr/bin/plezuro
