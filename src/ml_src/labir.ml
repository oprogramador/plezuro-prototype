$maze = {
        ($x,$y) = args;
        $n := x*y-1; 
        ('n='+n).printl;
        $horiz;
        $verti;
        {n<0}.if({
                "illegal maze dimensions".printl
        }).else({
                'aaa'.printl;
                horiz := [[false]*(y+1)]*(x+1);
                verti := [[false]*(x+1)]*(y+1); 
                $here := [(rand*x).floor, (rand*y).floor];
                $path := [here];
                $unvisited = [0]*(x+2);
                0..(x+2).each({
                        $j := args;
                        ('j='+j).printl;
                        ('#unv='+unvisited).printl;
                        unvisited[j] := [];
                        ('unv='+unvisited).printl;
                        0..(y+1).each({
                                $k := args;
                                unvisited[j] << (j>0 & j<x+1 & k>0 & (j!=(here[0]+1) | k!=(here[1]+1)))
                        });
                        ('unvisited='+unvisited).printl;
                });
                {0<n}.while({
                        $potential := [[here[0]+1, here[1]], [here[0],here[1]+1],
                                [here[0]-1, here[1]], [here[0],here[1]-1]];
                        $neighbors := [];
                        ('pot='+potential).printl;
                        0..4.each({
                                $j := args;
                                {unvisited[potential[j][0]+1][potential[j][1]+1]}.if({
                                        neighbors << potential[j]
                                })
                        });
                        ('pot='+potential).printl;
                        ('nei='+neighbors).printl;
                        {neighbors.len>0}.if({
                                ('n='+n).printl;
                                n--;
                                $next := neighbors[(rand*neighbors.len).floor];
                                unvisited[next[0]+1][next[1]+1] := false;
                                {next[0] == here[0]}.if({
                                        horiz[next[0]][(next[1]+here[1]-1)/2] := true
                                }).else({
                                        verti[(next[0]+here[0]-1)/2][next[1]] := true
                                });
                                'here'.dumpl;
                                'next'.dumpl;
                                'horiz'.dumpl;
                                'verti'.dumpl;
                                path << (here := next);
                        }).else({
                                path >> here;
                        });
                })
        });
        #['x',x, 'y',y, 'horiz',horiz, 'verti',verti]
};
maze(vals)
