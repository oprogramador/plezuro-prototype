$f = {
        ($x,$y,$z) = args;
        ::printl ('args='+args);
        ::printl ('x='+x);
        ::printl ('y='+y);
        ::printl ('z='+z);
        x+y*z};

f.::applyF([2,3,4]).::printl();
f.::time().::printl(); //executing time in milliseconds; x,y,z are undefined here
{f.::applyF([2,3,4])}.::time().::printl() //executed time; x,y,z are defined
