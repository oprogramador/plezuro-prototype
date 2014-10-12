Lang
<< 'Person'.::newClass([Object], ::dic(
        'init',{
                @this << ('age': (vals*2))
        },
        '+',{
                (@this['age'] += vals)
        },
        "str",{
                "I'm "+(@this['age'])+' years old.'
        },
        "destroy",{
                ::printl('person destroy');
        }
));
$Per = [@Lang['Person']];
Lang << 'Dog'.::newClass( Per, ::dic(
        'init',{
               ($age, $race) = vals;
                @(@Lang['Person'])['init'](this, age);
                @this << ('race': race);
        }
));

$p = (@Lang['Person'](14));
@p['age']++;
p+50;
$d = (@Lang['Dog'](13,'Akbash'));
d+3;
(''+d).::printl();
@p,@d
