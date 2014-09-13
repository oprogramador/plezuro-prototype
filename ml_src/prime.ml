$i=2;
$n=0;
$a=[];
::while({n<100},
{
	$k = 2;
    	$ispr = true;
    	::while({k*k<=i},
    	{
        	::if(i%k==0, {ispr=false}, {0});
        	k=k+1
    	});
	::if(ispr, {a<<i; n=n+1}, {0});
	i=i+1
});
a
