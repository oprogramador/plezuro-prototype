./copy.sh
if [ $1 = 'c' -o $1 = '' ]; then
    gmcs -pkg:dotnet -out:plezuro.exe `find src/csharp_src/Mondo/Compiler -name '*.cs'` &&
    chmod 775 plezuro.exe &&
    sudo cp plezuro.exe /usr/bin/plezuro
fi
if [ $1 = 'i' -o $1 = '' ]; then
    gmcs -pkg:dotnet -out:plezuro_studio.exe `find src/csharp_src/Mondo/IDE -name '*.cs'` &&
    chmod 775 plezuro_studio.exe &&
    sudo cp plezuro_studio.exe /usr/bin/plezuro_studio
fi
