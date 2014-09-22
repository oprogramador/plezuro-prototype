$x = 2;
::if( x<0, {x++}, {x--});

$a = x>0 ? 'yes' : 'no';
$b = (x>0).::if({'yes'}, {'no'});
$c = ::if(x>0, {'yes'}, {'no'});

a,b,c //it prints ("yes","yes","yes")
