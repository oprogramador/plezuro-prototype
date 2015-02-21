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
$directions = #[
    'w', [0, -1],
    'a', [-1, 0],
    's', [0, 1],
    'd', [1, 0]
];
[makeBoard, printOnBoard, addVec, directions]
