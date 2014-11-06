$Car = 'Car'.newClass($[], #[
        'init', {
                @this << ('price' : vals)
        },
        'str',  {
                'a car which costs '+(@this['price'])+'€'
        },
        'price', {
                @this['price'] = vals
        }
]);

$c = Car(7000);
$[c,''+c];
c.price 900;
@c
