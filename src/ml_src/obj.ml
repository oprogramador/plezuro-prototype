$Person = 'Person'.::newClass([Object], ::dic(
        'init',{
                ($ob, $val) = args;
                ob.::pools() << ('one': (val*2))
        },
        '+',{
                ($ob, $val) = args;
                (ob.::pools()['one'] = (ob.::pools()['one'] + val))
        }
  ), Lang);
$p = Person.::new();
p.::init(13);
p.::pools()['one']++;
p+50;
p.::pools()
