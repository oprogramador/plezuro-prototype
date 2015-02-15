./copy.sh
if [ $1 = 'c' -o $1 = '' ]; then
    gmcs -pkg:dotnet -out:plezuro `find src/csharp_src/Mondo/Compiler -name '*.cs'` &&
    chmod 775 plezuro &&
    sudo cp plezuro /usr/bin/plezuro
fi
if [ $1 = 'i' -o $1 = '' ]; then
    gmcs -pkg:dotnet -out:plezuro_studio `find src/csharp_src/Mondo/IDE -name '*.cs'` &&
    chmod 775 plezuro_studio &&
    sudo cp plezuro_studio /usr/bin/plezuro_studio
fi
