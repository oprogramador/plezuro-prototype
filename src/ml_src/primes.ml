$i=2;
$n=0;
::while({n<1000},
{
	$k = 2;
    	$ispr = true;
    	::while({k*k<=i},
    	{
        	::if(i%k==0, {ispr=false}, {0});
        	k=k+1
    	});
	::if(ispr, {n=n+1}, {0});
	i=i+1
});
i-1
