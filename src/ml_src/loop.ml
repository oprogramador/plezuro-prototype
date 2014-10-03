$i = 0;
::while({i<20},{
        i.::printl();
        i++
});

[1,2,3,4,5].::each({ args.::printl() });

::range(30,70,6.5).::each({ args.::printl() })
