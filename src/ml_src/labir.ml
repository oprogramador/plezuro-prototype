$maze = {
        ($x,$y) = args;
        $n = x*y-1; 
        ('n='+n).printl;
        {n<0}.if({
                "illegal maze dimensions".printl
        }).else({
                'aaa'.printl;
                $horiz = [([])]*(x+1);
                $verti = [([])]*(y+1); 
                $here = [(rand*x).floor, (rand*y).floor];
                $path = [here];
                $unvisited = [0]*(x+2);
                0..(x+2).each({
                        $j := args;
                        (unvisited[j] = []);
                        ('unv='+unvisited).printl;
                        0..(y+1).each({
                                $k = args;
                                unvisited[j] << (j>0 & j<x+1 & k>0 & (j!=(here[0]+1) | k!=(here[1]+1)))
                        });
                        ('unvisited='+unvisited).printl;
                });
                //{0<n}.while({
                        $potential = [[here[0]+1, here[1]], [here[0],here[1]+1],
                                [here[0]-1, here[1]], [here[0],here[1]-1]];
                        ('pot='+potential).printl;
                //})
        })
};
maze(vals)
