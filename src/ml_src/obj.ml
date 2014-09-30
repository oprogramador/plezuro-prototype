$Person = 'Person'.::newClass([Object], ::dic('init',{
  ($ob, $val) = args;
  ob.::pools() << ('one': (val*2))
}), Lang);
$p = Person.::new();
p.::init(13);
p.::pools()['one']++;
p.::pools()
