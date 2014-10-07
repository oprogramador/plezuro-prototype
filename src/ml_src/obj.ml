$Person = 'Person'.::newClass([Object], ::dic(
        'init',{
                ($ob, $val) = args;
                ob.::pools() << ('age': (val*2))
        },
        '+',{
                ($ob, $val) = args;
                (ob.::pools()['age'] = (ob.::pools()['age'] + val))
        }
  ), Lang);

$Dog = 'Dog'.::newClass([Person], ::dic(
        'init',{
               ($ob, $age, $race) = args;
                Person.::methods()['init'](ob, age);
                ob.::pools() << ('race': race);
        }
  ), Lang);

$p = Person.::new();
p.::init(13);
p.::pools()['age']++;
p+50;
$d = Dog.::new();
d.::init(13,'akbash');
d+4;
p.::pools(), d.::pools()
