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
    new << addVec(this[0]);
    this.each{
        {first>0}.if{
            new << this
        }
    };
    new
};
$squares = makeBoard('y',20,6);
printOnBoard($squares, [1,25,2]);
$squares = printOnBoard(squares, [#['color', 'r', 'sq', [[0,1], [2,4]]], #['color', 'b', 'sq', [[2,5], [15,3]]]]);
'squares'.dumpl;
$w = #[
    'w', 830,
    'h', 470,
    'time', 50,
    'ontime', {
    },
    'keypress', {
        squares=['rgb', 'ggb']
    },
    'squares', &&squares
    ].window;
w.show

