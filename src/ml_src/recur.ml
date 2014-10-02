$fac = {
        (args>1).::if({ args*fac(args-1) }, {1})
};
fac(100)
