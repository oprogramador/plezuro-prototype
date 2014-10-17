$f = {
        ($x,$y,$z) = args;
        ::printl ('args='+args);
        ::printl ('x='+x);
        ::printl ('y='+y);
        ::printl ('z='+z);
        x+y*z};

f.::applyF([2,3,4]).::printl();
f(4,5,6).::printl();
f.::time().::printl(); //executing time in milliseconds; x,y,z are undefined here
{f(4,5,6)}.::time().::printl(); //executed time; x,y,z are defined
