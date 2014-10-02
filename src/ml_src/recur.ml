$facr = {
        (args>1).::if({ args*facr(args-1) }, {1})
};
$faci = {
        $i = args;
        $ret = 1;
        {i>1}.::while({ ret=ret*i; i-- });
        ret
};
{facr(120)}.::time(),
{faci(120)}.::time()
