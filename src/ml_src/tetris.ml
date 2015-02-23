($makeBoard, $printOnBoard, $addVec, $directions) = 'gamelib.ml'.load();

$moveTetromino = {
    $dir = first==empty ? 's' : first;
    'dir'.dumpl;
    this.map{addVec(this,directions[dir])}
};

$replace = {
    [this[1], -this[0]]
};

$moveAll = {
    $vec = first;
    this.map({addVec(this, vec)})
};

$invVec = {
    this.map{-this}
};

'moveAll([[0,1], [2,4], [5,6]], [2,2])'.dumpl;
'addVec([3,3], [1,2])'.dumpl;
'invVec([2,3])'.dumpl;

$rotateTetromino = {
    $head = this[0];
    //addVec(this.map{replace(this.map(addVec(this, head.map(-this))))}, head)
    moveAll(this.map{replace(addVec(this, invVec(head)))}, head)
};

$tetrominos = [
    [[0,0], [0,1], [0,2], [0,3]],
    [[0,0], [0,1], [1,1], [1,0]],
    [[0,0], [1,0], [2,0], [1,1]],
    [[0,0], [1,0], [2,0], [2,1]],
    [[0,0], [1,0], [2,0], [0,1]],
    [[1,0], [2,0], [0,1], [1,1]],
    [[1,0], [0,0], [1,1], [2,1]]
];

$generateTetromino = {
    #['color', ['b', 'r', 'g'].rand, 'sq', tetrominos.rand]
};

$W = 20;
$H = 32;
$squares = makeBoard('y',W,H);
$tetromino := generateTetromino();
'tetromino'.dumpl;
$direction = 'a';
$info = '';

$w = #[
    'w', 470,
    'h', 600,
    'time', 500,
    'ontime', {
        squares = makeBoard('y',W,H);
        tetromino['sq'] = moveTetromino(tetromino['sq']);
        'tetromino'.dumpl;
        squares = printOnBoard(squares, [tetromino]);
        'squares'.dumpl;
        info = 'result: '
    },
    'keypress', {
        $newdir = this;
        {'ad'.contains(newdir)}.if{
            direction = newdir
        }.else{
            direction = ''     
        };
        {direction!=''}.if{
            tetromino['sq'] = moveTetromino(tetromino['sq'], direction)
        };
        {newdir=='k'}.if{
            tetromino['sq'] = rotateTetromino(tetromino['sq'])
        }
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
