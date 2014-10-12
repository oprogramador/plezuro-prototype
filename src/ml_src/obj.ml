$Person = 'Person'.::newClass([Object], ::dic(
        'init',{
                @this << ('age': (vals*2))
        },
        '+',{
                (@this['age'] += vals)
        },
        "str",{
                "I'm "+(@this['age'])+' years old.'
        }
  ), Lang);

$Dog = 'Dog'.::newClass([Person], ::dic(
        'init',{
               ($age, $race) = vals;
                @(@Lang['Person'])['init'](this, age);
                @this << ('race': race);
        }
  ), Lang);

$p = Person(14);
@p['age']++;
p+50;
$d = Dog(13,'Akbash');
d+3;
(''+d).::printl();
@p,@d
