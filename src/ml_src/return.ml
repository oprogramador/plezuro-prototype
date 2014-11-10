$f = {
    $ret = thisfun;
    {args>0}.if{
        ret := 'yes';
        'ret'.dumpl
    };
    45
};
f(2)
