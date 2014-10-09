$Person = 'Person'.::newClass([Object], ::dic(
        'init',{
                @this << ('age': (vals*2))
        },
        '+',{
                (@this['age'] += vals)
        }
  ), Lang);

$Dog = 'Dog'.::newClass([Person], ::dic(
        'init',{
               ($age, $race) = vals;
                @Person['init'](this, age);
                @this << ('race': race);
        }
  ), Lang);

$p = Person.::new();
p.::init(13);
@p['age']++;
p+50;
$d = Dog.::new();
d.::init(13,'akbash');
d+4;
@p,@d
