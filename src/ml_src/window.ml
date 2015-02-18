'hello'.printl;
$w = #[
    'w', 830,
    'h', 470,
    'keypress', {(args*3).printl},
    'squares', ['gg','rb']
    ].window;
w.squares(['bbgr', 'ggbb', 'rrgr']);
w.show
