$is_solved = {
    =>$board;
    board.eachi($row,{
        row.eachi($cell,{
            {cell==0}.if
