./copy.sh
arg=$1
if [ "$#" = 0 ]; then
    arg=all
fi
if [ $arg = 'c' -o $arg = 'all' ]; then
    gmcs -pkg:dotnet -out:plezuro `find src/csharp_src/Mondo/Compiler -name '*.cs'` &&
    chmod 775 plezuro &&
    sudo cp plezuro /usr/bin/plezuro
fi
if [ $arg = 'i' -o $arg = 'all' ]; then
    gmcs -pkg:dotnet -out:plezuro_studio `find src/csharp_src/Mondo/IDE -name '*.cs'` &&
    chmod 775 plezuro_studio &&
    sudo cp plezuro_studio /usr/bin/plezuro_studio
fi
