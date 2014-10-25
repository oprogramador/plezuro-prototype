$i=2;
$n=0;
$a=[];
{n<first}.while({
	$k := 2;
    	$ispr = true;
    	{k*k<=i}.while({
                {i%k==0}.if({ispr=false});
        	k++
    	});
        {ispr}.if({a<<i.clone; n++});
	i++
});
a
