'hello'.printl;
/*
class...
*/
$makeBoard = {
    [[this]*first]*second
};
$printOnBoard = {
    $board = this;
    first.each{
        $color = this['color'];
        $sq = this['sq'];
        sq.each{
            board[this[1]][this[0]] = color
        }
    };
    board
};
$addVec = {
    [this[0]+first[0], this[1]+first[1]]
};
$moveSnike = {
    $directions = #[
        'w', [0, -1],
        'a', [-1, 0],
        's', [0, 1],
        'd', [1, 0]
    ];
    $new = [];
    $di = first;
    new << addVec(this[0], directions[first]);
    new += this;
    new >> $pop;
    'new'.dumpl;
    new
};
$snike = [[4,0], [3,0], [2,0], [1,0], [0,0]];
$squares = makeBoard('y',40,22);
$squares = printOnBoard(squares, [#['color', 'r', 'sq', [[0,1], [2,4]]], #['color', 'b', 'sq', $snike]]);
'squares'.dumpl;
$direction = 'd';
$w = #[
    'w', 830,
    'h', 470,
    'time', 500,
    'ontime', {
        squares = makeBoard('y',40,22);
        snike = moveSnike(snike, direction);
        squares = printOnBoard(squares, [#['color', 'b', 'sq', snike]])
    },
    'keypress', {
        {'wasd'.contains(this)}.if{
            direction = this
        }
    },
    'squares', &&squares
    ].window;
w.show

