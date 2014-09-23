($a, $b) = (1,3);
a.::printl();
b.::printl();

$c = 5;
$d = 6;
(a,b,c,d) = (b,c,d,a);

a.::printl();
(a,b,c,d).::printl();

a <-> c;
(a,b,c,d)
