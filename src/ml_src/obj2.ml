$Car = 'Car'.newClass($[], #[
        'init', {
                @this << ('price' : vals)
        },
        'str',  {
                'a car which costs '+(@this['price'])+'€'
        }
]);

$c = Car(7000);
$[c,''+c]
