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

