// a variable initialization
$x = 12.3;
// an incrementation
x++;
// a conditional expression
{x<15}.if({
    'x'.dumpl; // dumping a variable to console, very useful when debugging
    x *= 3
}).elsif({x==0},{
    'x'.dumpl;
    x--;
});
'x'.dumpl;

//a loop, printing to console
'\na loop'.printl;
{x<1000}.while({
    'x'.dumpl;
    x *= 2
});
