$facr = {
        {args>1}.if({ args*facr(args-1) }).else({ 1 }).res
};
$faci = {
        $i = args;
        $ret = 1;
        {i>1}.while({ ret=ret*i; i-- });
        ret
};
facr(120),faci(120)
