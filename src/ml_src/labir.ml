$maze = {
	=>($y,$x);
	$n := x*y-1; 
	$horiz;
	$verti;
	{n<0}.if{
		"illegal maze dimensions".printl
	}.else{
		$max = [x,y].max+1;
		horiz := [[false]*max]*(x+1);
		verti := [[false]*max]*(x+1); 
		$here := [(rand*x).floor, (rand*y).floor];
		$path := [here];
		$unvisited = [0]*(x+2);
		0..(x+2).eachi($j,{
			unvisited[j] := [];
			0..(y+1).eachi($k,{
				unvisited[j] << (j>0 & j<x+1 & k>0 & (j!=(here[0]+1) | k!=(here[1]+1)))
			});
		});
		{0<n}.while{
			$potential := [[here[0]+1, here[1]], [here[0],here[1]+1],
				[here[0]-1, here[1]], [here[0],here[1]-1]];
			$neighbors := [];
			0..4.eachi($j,{
				{unvisited[potential[j][0]+1][potential[j][1]+1]}.if({
					neighbors << potential[j]
				})
			});
			{neighbors.len>0}.if{
				n--;
				$next := neighbors[(rand*neighbors.len).floor];
				unvisited[next[0]+1][next[1]+1] := false;
				{next[0] == here[0]}.if{
					horiz[next[0]][(next[1]+here[1]-1)/2] := true
				}.else{
					verti[(next[0]+here[0]-1)/2][next[1]] := true
				};
				path << (here := next);
			}.else{
				path >> here;
			};
		}
	};
	#['x',x, 'y',y, 'horiz',horiz, 'verti',verti]
};

$display = {
	=>$m;
	$text = '';
	0..(m['x']*2+1).eachi($j,{
		$line = [];
		{0==j%2}.if{
			0..(m['y']*4+1).eachi($k,{
				{0==k%4}.if{
					line << '*'
				}.else{
					{j>0 & m['verti'][j/2-1][(k/4).floor]}.if{
						line << ' '
					}.else{
						line << '-'
					}
				}
			})
		}.else{
			0..(m['y']*4+1).eachi($k,{
				{0==k%4}.if{
					{k>0 & m['horiz'][(j-1)/2][k/4-1]}.if{
						line << ' '
					}.else({
						line << '|'
					}
				}.else{
					line << ' '
				}
			}
		};
		{0==j}.if{
			line[1] = line[3] = line[2] = ' '
		};
		{m['x']*2-1==j}.if{
			line[4*m['y']] = ' '
		};
		text += (line.join+'\n')
	});
	text
};

display(maze(vals)).printl;
