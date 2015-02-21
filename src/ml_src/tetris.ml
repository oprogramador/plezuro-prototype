($makeBoard, $printOnBoard, $addVec, $directions) = 'gamelib.ml'.load();

$moveTetromino = {
    $dir = first==empty ? 's' : first;
    'dir'.dumpl;
    this.map{addVec(this,directions[dir])}
};

$tetrominos = [
    [[0,0], [0,1], [0,2], [0,3]],
    [[0,0], [0,1], [1,1], [1,0]],
    [[0,0], [1,0], [2,0], [1,1]],
    [[0,0], [1,0], [2,0], [2,1]],
    [[0,0], [1,0], [2,0], [0,1]],
    [[1,0], [2,0], [0,1], [1,1]],
    [[1,0], [0,0], [0,1], [2,1]]
];

$W = 20;
$H = 32;
$squares = makeBoard('y',W,H);
$tetromino = tetrominos.rand;
'tetromino'.dumpl;
$direction = 'a';
$info = '';

$w = #[
    'w', 470,
    'h', 600,
    'time', 500,
    'ontime', {
        squares = makeBoard('y',W,H);
        tetromino = moveTetromino(tetromino);
        'tetromino'.dumpl;
        squares = printOnBoard(squares, [ #['color', 'b', 'sq', tetromino]]);
        info = 'result: '
    },
    'keypress', {
        $newdir = this;
        {'ad'.contains(newdir)}.if{
            direction = newdir
        };
        tetromino = moveTetromino(tetromino, direction);
    },
    'squares', &&squares,
    'info', [
        #[
            'text', &&info,
            'color', 'b',
            'size', 16
        ]
    ]
    ].window;
w.show
