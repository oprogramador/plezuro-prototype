$fac = func({
	if( get(args,0) <= 0, {args}, {get(args,0) * get(args,0)-1} )
});
applyF(fac,[-3])
