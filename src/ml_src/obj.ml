Lang
<< 'Person'.::newClass($[Object], #[
        'init',{
                @this << ('age': (vals*2))
        },
        '+',{
                (@this['age'] += vals)
        },
        "get-age",{
                @this['age']
        },
        "set-age",{
                @this['age'] = vals
        },
        "str",{
                "I'm "+(@this['age'])+' years old.'
        },
        "destroy",{
                ::printl('person destroy');
        }
]);
$Per = $[@Lang['Person']];
Lang << 'Dog'.::newClass( Per, #[
        'init',{
               ($age, $race) = vals;
                @(@Lang['Person'])['init'](this, age);
                @this << ('race': race);
        }
]);

$p = (@Lang['Person'](14));
@p['age']++;
p+50;
$d = (@Lang['Dog'](13,'Akbash'));
d+3;
(''+d).::printl();
//((@Lang['Person']).::set('age'))(d,100);
@p,@d
