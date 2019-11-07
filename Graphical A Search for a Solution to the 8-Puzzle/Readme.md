# Graphical A* Search for a Solution to the 8-Puzzle
## Requires
- Visual Studio 2008
## License
- MIT
## Technologies
- C#
- Visual Studio 2008
- Visual C#
## Topics
- Graphics
- Windows Forms
## Updated
- 03/17/2015
## Description

<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<h1>Introduction</h1>
<p><em>A* search is an informed (heuristic) search strategy. It is well known among practitioners of the computer science discipline known as artificial intelligence. The strategy minimizes the total estimated solution cost. A* search is complete and optimal
 for a given admissable heuristic function. The 8-puzzle or 15-puzzle are well known problems whose optimal solutions are NP-hard.
<br>
</em></p>
<h1><span>Building the Sample</span></h1>
<p><em>This project should build easily using Microsoft Visual Studio 2008 and perhaps later editions.<br>
</em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><em>In this version of A* search the Manhattan distance also known as the city block distance heuristic is utilized. Generally, I have found that an optimal solution to a randomly generated 8-puzzle initial state is found in about 30 moves or less. The 8-puzzle
 consists of 8 tiles with labels 1-8 and a blank tile that I number as tile 0. The solution of the puzzle from a randomized initial state is to get the nine tiles in the order 12345678 going around the perimeter of the square with the 0 tile in the middle.
 Larger variants of the puzzle exist with probably the most popular being the 15-puzzle.<img id="134988" src="134988-astar%201.jpg" alt="" width="820" height="904"><img id="134989" src="134989-astar%202.jpg" alt="" width="820" height="904"><img id="134991" src="134991-astar%203.jpg" alt="" width="820" height="904"><br>
</em></p>
<p><em>You can include <em><strong>code snippets,&nbsp;</strong></em><strong>images</strong>,
<strong>videos</strong>. &nbsp;&nbsp;</em></p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">using System;

namespace AStarPuzzle8
{
    class PuzzleASCS
    {
        int g, nodesExpanded;
        int[,] board;
        DateTime date = DateTime.Now;
        Random random;
        public static int MaxMoves = 256;

        public PuzzleASCS() {
		    bool found;
		    int digit, i, j, k;
		    int[] placed = new int[9];
		
		    random = new Random((int) date.Ticks);
		    for (i = 0; i &lt; 9; i&#43;&#43;)
			    placed[i] = 0;
		    board = new int[3, 3];
		    g = nodesExpanded = 0;
		    for (i = 0; i &lt; 3; i&#43;&#43;)
			    for (j = 0; j &lt; 3; j&#43;&#43;)
				    board[i, j] = 0;
		    for (i = 0; i &lt; 9; i&#43;&#43;) {
			    found = false;
			    do {
				    digit = random.Next(9);
				    found = placed[digit] == 0;
				    if (found)
					    placed[digit] = 1;
			    } while (!found);
			    do {
				    j = random.Next(3);
				    k = random.Next(3);
				    found = board[j, k] == 0;
				    if (found)
					    board[j, k] = digit;
			    } while (!found);
		    }
	    }

        int getNodesExpanded()
        {
            return nodesExpanded;
        }

        int expand(int[,] square, ref int[,,] tempSquare)
        {
            int b = -1, col = -1, i, j, k, row = -1;

            for (i = 0; i &lt; 4; i&#43;&#43;)
                for (j = 0; j &lt; 3; j&#43;&#43;)
                    for (k = 0; k &lt; 3; k&#43;&#43;)
                        tempSquare[i, j, k] = square[j, k];
            for (i = 0; i &lt; 3; i&#43;&#43;)
            {
                for (j = 0; j &lt; 3; j&#43;&#43;)
                {
                    if (square[i, j] == 0)
                    {
                        row = i;
                        col = j;
                        break;
                    }
                }
            }
            if (row == 0 &amp;&amp; col == 0)
            {
                tempSquare[0 , 0 , 0] = tempSquare[0 , 0 , 1];
                tempSquare[0 , 0 , 1] = 0;
                tempSquare[1 , 0 , 0] = tempSquare[1 , 1 , 0];
                tempSquare[1 , 1 , 0] = 0;
                b = 2;
            }
            else if (row == 0 &amp;&amp; col == 1)
            {
                tempSquare[0 , 0 , 1] = tempSquare[0 , 0 , 0];
                tempSquare[0 , 0 , 0] = 0;
                tempSquare[1 , 0 , 1] = tempSquare[1 , 1 , 1];
                tempSquare[1 , 1 , 1] = 0;
                tempSquare[2 , 0 , 1] = tempSquare[2 , 0 , 2];
                tempSquare[2 , 0 , 2] = 0;
                b = 3;
            }
            else if (row == 0 &amp;&amp; col == 2)
            {
                tempSquare[0 , 0 , 2] = tempSquare[0 , 0 , 1];
                tempSquare[0 , 0 , 1] = 0;
                tempSquare[1 , 0 , 2] = tempSquare[1 , 1 , 2];
                tempSquare[1 , 1 , 2] = 0;
                b = 2;
            }
            else if (row == 1 &amp;&amp; col == 0)
            {
                tempSquare[0 , 1 , 0] = tempSquare[0 , 0 , 0];
                tempSquare[0 , 0 , 0] = 0;
                tempSquare[1 , 1 , 0] = tempSquare[1 , 1 , 1];
                tempSquare[1 , 1 , 1] = 0;
                tempSquare[2 , 1 , 0] = tempSquare[2 , 2 , 0];
                tempSquare[2 , 2 , 0] = 0;
                b = 3;
            }
            else if (row == 1 &amp;&amp; col == 1)
            {
                tempSquare[0 , 1 , 1] = tempSquare[0 , 1 , 0];
                tempSquare[0 , 1 , 0] = 0;
                tempSquare[1 , 1 , 1] = tempSquare[1 , 0 , 1];
                tempSquare[1 , 0 , 1] = 0;
                tempSquare[2 , 1 , 1] = tempSquare[2 , 1 , 2];
                tempSquare[2 , 1 , 2] = 0;
                tempSquare[3 , 1 , 1] = tempSquare[3 , 2 , 1];
                tempSquare[3 , 2 , 1] = 0;
                b = 4;
            }
            else if (row == 1 &amp;&amp; col == 2)
            {
                tempSquare[0 , 1 , 2] = tempSquare[0 , 0 , 2];
                tempSquare[0 , 0 , 2] = 0;
                tempSquare[1 , 1 , 2] = tempSquare[1 , 1 , 1];
                tempSquare[1 , 1 , 1] = 0;
                tempSquare[2 , 1 , 2] = tempSquare[2 , 2 , 2];
                tempSquare[2 , 2 , 2] = 0;
                b = 3;
            }
            else if (row == 2 &amp;&amp; col == 0)
            {
                tempSquare[0 , 2 , 0] = tempSquare[0 , 1 , 0];
                tempSquare[0 , 1 , 0] = 0;
                tempSquare[1 , 2 , 0] = tempSquare[1 , 2 , 1];
                tempSquare[1 , 2 , 1] = 0;
                b = 2;
            }
            else if (row == 2 &amp;&amp; col == 1)
            {
                tempSquare[0 , 2 , 1] = tempSquare[0 , 2 , 0];
                tempSquare[0 , 2 , 0] = 0;
                tempSquare[1 , 2 , 1] = tempSquare[1 , 1 , 1];
                tempSquare[1 , 1 , 1] = 0;
                tempSquare[2 , 2 , 1] = tempSquare[2 , 2 , 2];
                tempSquare[2 , 2 , 2] = 0;
                b = 3;
            }
            else if (row == 2 &amp;&amp; col == 2)
            {
                tempSquare[0 , 2 , 2] = tempSquare[0 , 2 , 1];
                tempSquare[0 , 2 , 1] = 0;
                tempSquare[1 , 2 , 2] = tempSquare[1 , 1 , 2];
                tempSquare[1 , 1 , 2] = 0;
                b = 2;
            }
            return b;
        }

        int heuristic(int[ , ] square)
        {
            return ManhattanDistance(square);
        }

        bool move()
        {
            int b, count, i, j, k, min;
            int[] f = new int[4], index = new int[4];
            int[ ,  , ] tempSquare = new int[4, 3, 3];

            if (board[0 , 0] == 1 &amp;&amp; board[0 , 1] == 2 &amp;&amp; board[0 , 2] == 3 &amp;&amp;
                board[1 , 0] == 8 &amp;&amp; board[1 , 1] == 0 &amp;&amp; board[1 , 2] == 4 &amp;&amp;
                board[2 , 0] == 7 &amp;&amp; board[2 , 1] == 6 &amp;&amp; board[2 , 2] == 5)
                return true;
            b = expand(board, ref tempSquare);
            for (i = 0; i &lt; b; i&#43;&#43;)
            {
                int[,] ts = new int[3, 3];

                for (j = 0; j &lt; 3; j&#43;&#43;)
                    for (k = 0; k &lt; 3; k&#43;&#43;)
                        ts[j, k] = tempSquare[i, j, k];

                f[i] = g &#43; heuristic(ts);

                for (j = 0; j &lt; 3; j&#43;&#43;)
                    for (k = 0; k &lt; 3; k&#43;&#43;)
                        board[j , k] = tempSquare[i , j , k];
            }
            // find the node of minimum f
            min = f[0];
            for (i = 1; i &lt; b; i&#43;&#43;)
                if (f[i] &lt; min)
                    min = f[i];
            for (count = i = 0; i &lt; b; i&#43;&#43;)
                if (f[i] == min)
                    index[count&#43;&#43;] = i;
            i = index[random.Next(count)];
            // increment the cost of the path thus far
            g&#43;&#43;;
            nodesExpanded &#43;= b;
            for (j = 0; j &lt; 3; j&#43;&#43;)
                for (k = 0; k &lt; 3; k&#43;&#43;)
                    board[j, k] = tempSquare[i, j, k];
            return false;
        }

        int outOfPlace(int[ , ] square)
        {
            int i, j, oop = 0;
            int[ , ] goal = new int[3, 3];

            goal[0 , 0] = 1;
            goal[0 , 1] = 2;
            goal[0 , 2] = 3;
            goal[1 , 0] = 8;
            goal[1 , 1] = 0;
            goal[1 , 2] = 4;
            goal[2 , 0] = 7;
            goal[2 , 1] = 6;
            goal[2 , 2] = 5;
            for (i = 0; i &lt; 3; i&#43;&#43;)
                for (j = 0; j &lt; 3; j&#43;&#43;)
                    if (square[i , j] != goal[i , j])
                        oop&#43;&#43;;
            return oop;
        }

        int ManhattanDistance(int[ , ] square)
        {
            // city block or Manhatten distance heuristic
            int md = 0;

            if (square[0, 0] == 1)
                md &#43;= 0;
            else if (square[0, 0] == 2)
                md &#43;= 1;
            else if (square[0, 0] == 3)
                md &#43;= 2;
            else if (square[0, 0] == 4)
                md &#43;= 3;
            else if (square[0, 0] == 5)
                md &#43;= 4;
            else if (square[0, 0] == 6)
                md &#43;= 3;
            else if (square[0, 0] == 7)
                md &#43;= 2;
            else if (square[0, 0] == 8)
                md &#43;= 1;
            if (square[0, 1] == 1)
                md &#43;= 1;
            else if (square[0, 1] == 2)
                md &#43;= 0;
            else if (square[0, 1] == 3)
                md &#43;= 1;
            else if (square[0, 1] == 4)
                md &#43;= 2;
            else if (square[0, 1] == 5)
                md &#43;= 3;
            else if (square[0, 1] == 6)
                md &#43;= 2;
            else if (square[0, 1] == 7)
                md &#43;= 3;
            else if (square[0, 1] == 8)
                md &#43;= 2;
            if (square[0, 2] == 1)
                md &#43;= 2;
            else if (square[0, 2] == 2)
                md &#43;= 1;
            else if (square[0, 2] == 3)
                md &#43;= 0;
            else if (square[0, 2] == 4)
                md &#43;= 1;
            else if (square[0, 2] == 5)
                md &#43;= 2;
            else if (square[0, 2] == 6)
                md &#43;= 3;
            else if (square[0, 2] == 7)
                md &#43;= 4;
            else if (square[0, 2] == 8)
                md &#43;= 3;
            if (square[1, 0] == 0)
                md &#43;= 1;
            else if (square[1, 0] == 1)
                md &#43;= 1;
            else if (square[1, 0] == 2)
                md &#43;= 2;
            else if (square[1, 0] == 3)
                md &#43;= 3;
            else if (square[1, 0] == 4)
                md &#43;= 2;
            else if (square[1, 0] == 5)
                md &#43;= 3;
            else if (square[1, 0] == 6)
                md &#43;= 2;
            else if (square[1, 0] == 7)
                md &#43;= 1;
            else if (square[1, 0] == 8)
                md &#43;= 0;
            if (square[1 , 1] == 1)
                md &#43;= 2;
            else if (square[1 , 1] == 2)
                md &#43;= 1;
            else if (square[1 , 1] == 3)
                md &#43;= 2;
            else if (square[1 , 1] == 4)
                md &#43;= 1;
            else if (square[1 , 1] == 5)
                md &#43;= 2;
            else if (square[1 , 1] == 6)
                md &#43;= 1;
            else if (square[1 , 1] == 7)
                md &#43;= 2;
            else if (square[1 , 1] == 8)
                md &#43;= 1;
            if (square[1 , 2] == 1)
                md &#43;= 3;
            else if (square[1 , 2] == 2)
                md &#43;= 2;
            else if (square[1 , 2] == 3)
                md &#43;= 1;
            else if (square[1 , 2] == 4)
                md &#43;= 0;
            else if (square[1 , 2] == 5)
                md &#43;= 1;
            else if (square[1 , 2] == 6)
                md &#43;= 2;
            else if (square[1 , 2] == 7)
                md &#43;= 3;
            else if (square[1 , 2] == 8)
                md &#43;= 2;
            if (square[2 , 0] == 1)
                md &#43;= 2;
            else if (square[2 , 0] == 2)
                md &#43;= 3;
            else if (square[2 , 0] == 3)
                md &#43;= 4;
            else if (square[2 , 0] == 4)
                md &#43;= 3;
            else if (square[2 , 0] == 5)
                md &#43;= 2;
            else if (square[2 , 0] == 6)
                md &#43;= 1;
            else if (square[2 , 0] == 7)
                md &#43;= 0;
            else if (square[2 , 0] == 8)
                md &#43;= 1;
            if (square[2, 1] == 1)
                md &#43;= 3;
            else if (square[2, 1] == 2)
                md &#43;= 2;
            else if (square[2, 1] == 3)
                md &#43;= 3;
            else if (square[2, 1] == 4)
                md &#43;= 2;
            else if (square[2, 1] == 5)
                md &#43;= 1;
            else if (square[2, 1] == 6)
                md &#43;= 0;
            else if (square[2, 1] == 7)
                md &#43;= 1;
            else if (square[2, 1] == 8)
                md &#43;= 2;
            if (square[2, 2] == 1)
                md &#43;= 4;
            else if (square[2, 2] == 2)
                md &#43;= 3;
            else if (square[2, 2] == 3)
                md &#43;= 2;
            else if (square[2, 2] == 4)
                md &#43;= 1;
            else if (square[2, 2] == 5)
                md &#43;= 0;
            else if (square[2, 2] == 6)
                md &#43;= 1;
            else if (square[2, 2] == 7)
                md &#43;= 2;
            else if (square[2, 2] == 8)
                md &#43;= 3;
            return md;
        }

        public int solve(ref char[ , ] solution)
        {
            bool found;
            int i, j, k, m = 0;

            do
            {
                for (i = k = 0; i &lt; 3; i&#43;&#43;)
                    for (j = 0; j &lt; 3; j&#43;&#43;)
                        solution[m , k&#43;&#43;] = (char)(board[i , j] &#43; '0');
                found = move();
                m&#43;&#43;;
            } while (!found &amp;&amp; m &lt; MaxMoves);
            for (i = k = 0; i &lt; 3; i&#43;&#43;)
                for (j = 0; j &lt; 3; j&#43;&#43;)
                    solution[m , k&#43;&#43;] = (char)(board[i , j] &#43; '0');
            return m;
        }
    }
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">using</span>&nbsp;System;&nbsp;
&nbsp;
<span class="cs__keyword">namespace</span>&nbsp;AStarPuzzle8&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">class</span>&nbsp;PuzzleASCS&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;g,&nbsp;nodesExpanded;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>[,]&nbsp;board;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DateTime&nbsp;date&nbsp;=&nbsp;DateTime.Now;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Random&nbsp;random;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;MaxMoves&nbsp;=&nbsp;<span class="cs__number">256</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;PuzzleASCS()&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">bool</span>&nbsp;found;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;digit,&nbsp;i,&nbsp;j,&nbsp;k;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>[]&nbsp;placed&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;<span class="cs__keyword">int</span>[<span class="cs__number">9</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;random&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Random((<span class="cs__keyword">int</span>)&nbsp;date.Ticks);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(i&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;i&nbsp;&lt;&nbsp;<span class="cs__number">9</span>;&nbsp;i&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;placed[i]&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;board&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;<span class="cs__keyword">int</span>[<span class="cs__number">3</span>,&nbsp;<span class="cs__number">3</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;g&nbsp;=&nbsp;nodesExpanded&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(i&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;i&nbsp;&lt;&nbsp;<span class="cs__number">3</span>;&nbsp;i&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(j&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;j&nbsp;&lt;&nbsp;<span class="cs__number">3</span>;&nbsp;j&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;board[i,&nbsp;j]&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(i&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;i&nbsp;&lt;&nbsp;<span class="cs__number">9</span>;&nbsp;i&#43;&#43;)&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;found&nbsp;=&nbsp;<span class="cs__keyword">false</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">do</span>&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;digit&nbsp;=&nbsp;random.Next(<span class="cs__number">9</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;found&nbsp;=&nbsp;placed[digit]&nbsp;==&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(found)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;placed[digit]&nbsp;=&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;<span class="cs__keyword">while</span>&nbsp;(!found);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">do</span>&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;j&nbsp;=&nbsp;random.Next(<span class="cs__number">3</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;k&nbsp;=&nbsp;random.Next(<span class="cs__number">3</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;found&nbsp;=&nbsp;board[j,&nbsp;k]&nbsp;==&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(found)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;board[j,&nbsp;k]&nbsp;=&nbsp;digit;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;<span class="cs__keyword">while</span>&nbsp;(!found);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;getNodesExpanded()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;nodesExpanded;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;expand(<span class="cs__keyword">int</span>[,]&nbsp;square,&nbsp;<span class="cs__keyword">ref</span>&nbsp;<span class="cs__keyword">int</span>[,,]&nbsp;tempSquare)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;b&nbsp;=&nbsp;-<span class="cs__number">1</span>,&nbsp;col&nbsp;=&nbsp;-<span class="cs__number">1</span>,&nbsp;i,&nbsp;j,&nbsp;k,&nbsp;row&nbsp;=&nbsp;-<span class="cs__number">1</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(i&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;i&nbsp;&lt;&nbsp;<span class="cs__number">4</span>;&nbsp;i&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(j&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;j&nbsp;&lt;&nbsp;<span class="cs__number">3</span>;&nbsp;j&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(k&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;k&nbsp;&lt;&nbsp;<span class="cs__number">3</span>;&nbsp;k&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[i,&nbsp;j,&nbsp;k]&nbsp;=&nbsp;square[j,&nbsp;k];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(i&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;i&nbsp;&lt;&nbsp;<span class="cs__number">3</span>;&nbsp;i&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(j&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;j&nbsp;&lt;&nbsp;<span class="cs__number">3</span>;&nbsp;j&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[i,&nbsp;j]&nbsp;==&nbsp;<span class="cs__number">0</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;row&nbsp;=&nbsp;i;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;col&nbsp;=&nbsp;j;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(row&nbsp;==&nbsp;<span class="cs__number">0</span>&nbsp;&amp;&amp;&nbsp;col&nbsp;==&nbsp;<span class="cs__number">0</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">0</span>]&nbsp;=&nbsp;tempSquare[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">1</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">0</span>]&nbsp;=&nbsp;tempSquare[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">0</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">0</span>]&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;b&nbsp;=&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(row&nbsp;==&nbsp;<span class="cs__number">0</span>&nbsp;&amp;&amp;&nbsp;col&nbsp;==&nbsp;<span class="cs__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;=&nbsp;tempSquare[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">0</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">0</span>]&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;=&nbsp;tempSquare[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;=&nbsp;tempSquare[<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">2</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">2</span>]&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;b&nbsp;=&nbsp;<span class="cs__number">3</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(row&nbsp;==&nbsp;<span class="cs__number">0</span>&nbsp;&amp;&amp;&nbsp;col&nbsp;==&nbsp;<span class="cs__number">2</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">2</span>]&nbsp;=&nbsp;tempSquare[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">1</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">2</span>]&nbsp;=&nbsp;tempSquare[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">2</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">2</span>]&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;b&nbsp;=&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(row&nbsp;==&nbsp;<span class="cs__number">1</span>&nbsp;&amp;&amp;&nbsp;col&nbsp;==&nbsp;<span class="cs__number">0</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">0</span>]&nbsp;=&nbsp;tempSquare[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">0</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">0</span>]&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">0</span>]&nbsp;=&nbsp;tempSquare[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">0</span>]&nbsp;=&nbsp;tempSquare[<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">0</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">0</span>]&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;b&nbsp;=&nbsp;<span class="cs__number">3</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(row&nbsp;==&nbsp;<span class="cs__number">1</span>&nbsp;&amp;&amp;&nbsp;col&nbsp;==&nbsp;<span class="cs__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;=&nbsp;tempSquare[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">0</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">0</span>]&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;=&nbsp;tempSquare[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">1</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;=&nbsp;tempSquare[<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">2</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">2</span>]&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">3</span>&nbsp;,&nbsp;<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;=&nbsp;tempSquare[<span class="cs__number">3</span>&nbsp;,&nbsp;<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">1</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">3</span>&nbsp;,&nbsp;<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;b&nbsp;=&nbsp;<span class="cs__number">4</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(row&nbsp;==&nbsp;<span class="cs__number">1</span>&nbsp;&amp;&amp;&nbsp;col&nbsp;==&nbsp;<span class="cs__number">2</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">2</span>]&nbsp;=&nbsp;tempSquare[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">2</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">2</span>]&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">2</span>]&nbsp;=&nbsp;tempSquare[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">2</span>]&nbsp;=&nbsp;tempSquare[<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">2</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">2</span>]&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;b&nbsp;=&nbsp;<span class="cs__number">3</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(row&nbsp;==&nbsp;<span class="cs__number">2</span>&nbsp;&amp;&amp;&nbsp;col&nbsp;==&nbsp;<span class="cs__number">0</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">0</span>]&nbsp;=&nbsp;tempSquare[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">0</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">0</span>]&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">0</span>]&nbsp;=&nbsp;tempSquare[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">1</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;b&nbsp;=&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(row&nbsp;==&nbsp;<span class="cs__number">2</span>&nbsp;&amp;&amp;&nbsp;col&nbsp;==&nbsp;<span class="cs__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;=&nbsp;tempSquare[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">0</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">0</span>]&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;=&nbsp;tempSquare[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;=&nbsp;tempSquare[<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">2</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">2</span>]&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;b&nbsp;=&nbsp;<span class="cs__number">3</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(row&nbsp;==&nbsp;<span class="cs__number">2</span>&nbsp;&amp;&amp;&nbsp;col&nbsp;==&nbsp;<span class="cs__number">2</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">2</span>]&nbsp;=&nbsp;tempSquare[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">1</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">2</span>]&nbsp;=&nbsp;tempSquare[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">2</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tempSquare[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">2</span>]&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;b&nbsp;=&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;b;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;heuristic(<span class="cs__keyword">int</span>[&nbsp;,&nbsp;]&nbsp;square)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;ManhattanDistance(square);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">bool</span>&nbsp;move()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;b,&nbsp;count,&nbsp;i,&nbsp;j,&nbsp;k,&nbsp;min;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>[]&nbsp;f&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;<span class="cs__keyword">int</span>[<span class="cs__number">4</span>],&nbsp;index&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;<span class="cs__keyword">int</span>[<span class="cs__number">4</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>[&nbsp;,&nbsp;&nbsp;,&nbsp;]&nbsp;tempSquare&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;<span class="cs__keyword">int</span>[<span class="cs__number">4</span>,&nbsp;<span class="cs__number">3</span>,&nbsp;<span class="cs__number">3</span>];&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(board[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">0</span>]&nbsp;==&nbsp;<span class="cs__number">1</span>&nbsp;&amp;&amp;&nbsp;board[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;==&nbsp;<span class="cs__number">2</span>&nbsp;&amp;&amp;&nbsp;board[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">2</span>]&nbsp;==&nbsp;<span class="cs__number">3</span>&nbsp;&amp;&amp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;board[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">0</span>]&nbsp;==&nbsp;<span class="cs__number">8</span>&nbsp;&amp;&amp;&nbsp;board[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;==&nbsp;<span class="cs__number">0</span>&nbsp;&amp;&amp;&nbsp;board[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">2</span>]&nbsp;==&nbsp;<span class="cs__number">4</span>&nbsp;&amp;&amp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;board[<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">0</span>]&nbsp;==&nbsp;<span class="cs__number">7</span>&nbsp;&amp;&amp;&nbsp;board[<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;==&nbsp;<span class="cs__number">6</span>&nbsp;&amp;&amp;&nbsp;board[<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">2</span>]&nbsp;==&nbsp;<span class="cs__number">5</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">true</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;b&nbsp;=&nbsp;expand(board,&nbsp;<span class="cs__keyword">ref</span>&nbsp;tempSquare);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(i&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;i&nbsp;&lt;&nbsp;b;&nbsp;i&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>[,]&nbsp;ts&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;<span class="cs__keyword">int</span>[<span class="cs__number">3</span>,&nbsp;<span class="cs__number">3</span>];&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(j&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;j&nbsp;&lt;&nbsp;<span class="cs__number">3</span>;&nbsp;j&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(k&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;k&nbsp;&lt;&nbsp;<span class="cs__number">3</span>;&nbsp;k&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ts[j,&nbsp;k]&nbsp;=&nbsp;tempSquare[i,&nbsp;j,&nbsp;k];&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;f[i]&nbsp;=&nbsp;g&nbsp;&#43;&nbsp;heuristic(ts);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(j&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;j&nbsp;&lt;&nbsp;<span class="cs__number">3</span>;&nbsp;j&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(k&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;k&nbsp;&lt;&nbsp;<span class="cs__number">3</span>;&nbsp;k&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;board[j&nbsp;,&nbsp;k]&nbsp;=&nbsp;tempSquare[i&nbsp;,&nbsp;j&nbsp;,&nbsp;k];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;find&nbsp;the&nbsp;node&nbsp;of&nbsp;minimum&nbsp;f</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;min&nbsp;=&nbsp;f[<span class="cs__number">0</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(i&nbsp;=&nbsp;<span class="cs__number">1</span>;&nbsp;i&nbsp;&lt;&nbsp;b;&nbsp;i&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(f[i]&nbsp;&lt;&nbsp;min)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;min&nbsp;=&nbsp;f[i];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(count&nbsp;=&nbsp;i&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;i&nbsp;&lt;&nbsp;b;&nbsp;i&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(f[i]&nbsp;==&nbsp;min)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;index[count&#43;&#43;]&nbsp;=&nbsp;i;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;i&nbsp;=&nbsp;index[random.Next(count)];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;increment&nbsp;the&nbsp;cost&nbsp;of&nbsp;the&nbsp;path&nbsp;thus&nbsp;far</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;g&#43;&#43;;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;nodesExpanded&nbsp;&#43;=&nbsp;b;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(j&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;j&nbsp;&lt;&nbsp;<span class="cs__number">3</span>;&nbsp;j&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(k&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;k&nbsp;&lt;&nbsp;<span class="cs__number">3</span>;&nbsp;k&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;board[j,&nbsp;k]&nbsp;=&nbsp;tempSquare[i,&nbsp;j,&nbsp;k];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">false</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;outOfPlace(<span class="cs__keyword">int</span>[&nbsp;,&nbsp;]&nbsp;square)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;i,&nbsp;j,&nbsp;oop&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>[&nbsp;,&nbsp;]&nbsp;goal&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;<span class="cs__keyword">int</span>[<span class="cs__number">3</span>,&nbsp;<span class="cs__number">3</span>];&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;goal[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">0</span>]&nbsp;=&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;goal[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;=&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;goal[<span class="cs__number">0</span>&nbsp;,&nbsp;<span class="cs__number">2</span>]&nbsp;=&nbsp;<span class="cs__number">3</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;goal[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">0</span>]&nbsp;=&nbsp;<span class="cs__number">8</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;goal[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;goal[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">2</span>]&nbsp;=&nbsp;<span class="cs__number">4</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;goal[<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">0</span>]&nbsp;=&nbsp;<span class="cs__number">7</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;goal[<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;=&nbsp;<span class="cs__number">6</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;goal[<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">2</span>]&nbsp;=&nbsp;<span class="cs__number">5</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(i&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;i&nbsp;&lt;&nbsp;<span class="cs__number">3</span>;&nbsp;i&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(j&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;j&nbsp;&lt;&nbsp;<span class="cs__number">3</span>;&nbsp;j&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[i&nbsp;,&nbsp;j]&nbsp;!=&nbsp;goal[i&nbsp;,&nbsp;j])&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;oop&#43;&#43;;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;oop;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;ManhattanDistance(<span class="cs__keyword">int</span>[&nbsp;,&nbsp;]&nbsp;square)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;city&nbsp;block&nbsp;or&nbsp;Manhatten&nbsp;distance&nbsp;heuristic</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;md&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>]&nbsp;==&nbsp;<span class="cs__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>]&nbsp;==&nbsp;<span class="cs__number">2</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>]&nbsp;==&nbsp;<span class="cs__number">3</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>]&nbsp;==&nbsp;<span class="cs__number">4</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">3</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>]&nbsp;==&nbsp;<span class="cs__number">5</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">4</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>]&nbsp;==&nbsp;<span class="cs__number">6</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">3</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>]&nbsp;==&nbsp;<span class="cs__number">7</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0</span>]&nbsp;==&nbsp;<span class="cs__number">8</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">0</span>,&nbsp;<span class="cs__number">1</span>]&nbsp;==&nbsp;<span class="cs__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">0</span>,&nbsp;<span class="cs__number">1</span>]&nbsp;==&nbsp;<span class="cs__number">2</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">0</span>,&nbsp;<span class="cs__number">1</span>]&nbsp;==&nbsp;<span class="cs__number">3</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">0</span>,&nbsp;<span class="cs__number">1</span>]&nbsp;==&nbsp;<span class="cs__number">4</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">0</span>,&nbsp;<span class="cs__number">1</span>]&nbsp;==&nbsp;<span class="cs__number">5</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">3</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">0</span>,&nbsp;<span class="cs__number">1</span>]&nbsp;==&nbsp;<span class="cs__number">6</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">0</span>,&nbsp;<span class="cs__number">1</span>]&nbsp;==&nbsp;<span class="cs__number">7</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">3</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">0</span>,&nbsp;<span class="cs__number">1</span>]&nbsp;==&nbsp;<span class="cs__number">8</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">0</span>,&nbsp;<span class="cs__number">2</span>]&nbsp;==&nbsp;<span class="cs__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">0</span>,&nbsp;<span class="cs__number">2</span>]&nbsp;==&nbsp;<span class="cs__number">2</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">0</span>,&nbsp;<span class="cs__number">2</span>]&nbsp;==&nbsp;<span class="cs__number">3</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">0</span>,&nbsp;<span class="cs__number">2</span>]&nbsp;==&nbsp;<span class="cs__number">4</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">0</span>,&nbsp;<span class="cs__number">2</span>]&nbsp;==&nbsp;<span class="cs__number">5</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">0</span>,&nbsp;<span class="cs__number">2</span>]&nbsp;==&nbsp;<span class="cs__number">6</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">3</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">0</span>,&nbsp;<span class="cs__number">2</span>]&nbsp;==&nbsp;<span class="cs__number">7</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">4</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">0</span>,&nbsp;<span class="cs__number">2</span>]&nbsp;==&nbsp;<span class="cs__number">8</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">3</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">1</span>,&nbsp;<span class="cs__number">0</span>]&nbsp;==&nbsp;<span class="cs__number">0</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">1</span>,&nbsp;<span class="cs__number">0</span>]&nbsp;==&nbsp;<span class="cs__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">1</span>,&nbsp;<span class="cs__number">0</span>]&nbsp;==&nbsp;<span class="cs__number">2</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">1</span>,&nbsp;<span class="cs__number">0</span>]&nbsp;==&nbsp;<span class="cs__number">3</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">3</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">1</span>,&nbsp;<span class="cs__number">0</span>]&nbsp;==&nbsp;<span class="cs__number">4</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">1</span>,&nbsp;<span class="cs__number">0</span>]&nbsp;==&nbsp;<span class="cs__number">5</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">3</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">1</span>,&nbsp;<span class="cs__number">0</span>]&nbsp;==&nbsp;<span class="cs__number">6</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">1</span>,&nbsp;<span class="cs__number">0</span>]&nbsp;==&nbsp;<span class="cs__number">7</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">1</span>,&nbsp;<span class="cs__number">0</span>]&nbsp;==&nbsp;<span class="cs__number">8</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;==&nbsp;<span class="cs__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;==&nbsp;<span class="cs__number">2</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;==&nbsp;<span class="cs__number">3</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;==&nbsp;<span class="cs__number">4</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;==&nbsp;<span class="cs__number">5</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;==&nbsp;<span class="cs__number">6</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;==&nbsp;<span class="cs__number">7</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">1</span>]&nbsp;==&nbsp;<span class="cs__number">8</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">2</span>]&nbsp;==&nbsp;<span class="cs__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">3</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">2</span>]&nbsp;==&nbsp;<span class="cs__number">2</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">2</span>]&nbsp;==&nbsp;<span class="cs__number">3</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">2</span>]&nbsp;==&nbsp;<span class="cs__number">4</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">2</span>]&nbsp;==&nbsp;<span class="cs__number">5</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">2</span>]&nbsp;==&nbsp;<span class="cs__number">6</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">2</span>]&nbsp;==&nbsp;<span class="cs__number">7</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">3</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">1</span>&nbsp;,&nbsp;<span class="cs__number">2</span>]&nbsp;==&nbsp;<span class="cs__number">8</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">0</span>]&nbsp;==&nbsp;<span class="cs__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">0</span>]&nbsp;==&nbsp;<span class="cs__number">2</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">3</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">0</span>]&nbsp;==&nbsp;<span class="cs__number">3</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">4</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">0</span>]&nbsp;==&nbsp;<span class="cs__number">4</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">3</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">0</span>]&nbsp;==&nbsp;<span class="cs__number">5</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">0</span>]&nbsp;==&nbsp;<span class="cs__number">6</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">0</span>]&nbsp;==&nbsp;<span class="cs__number">7</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">2</span>&nbsp;,&nbsp;<span class="cs__number">0</span>]&nbsp;==&nbsp;<span class="cs__number">8</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">2</span>,&nbsp;<span class="cs__number">1</span>]&nbsp;==&nbsp;<span class="cs__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">3</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">2</span>,&nbsp;<span class="cs__number">1</span>]&nbsp;==&nbsp;<span class="cs__number">2</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">2</span>,&nbsp;<span class="cs__number">1</span>]&nbsp;==&nbsp;<span class="cs__number">3</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">3</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">2</span>,&nbsp;<span class="cs__number">1</span>]&nbsp;==&nbsp;<span class="cs__number">4</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">2</span>,&nbsp;<span class="cs__number">1</span>]&nbsp;==&nbsp;<span class="cs__number">5</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">2</span>,&nbsp;<span class="cs__number">1</span>]&nbsp;==&nbsp;<span class="cs__number">6</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">2</span>,&nbsp;<span class="cs__number">1</span>]&nbsp;==&nbsp;<span class="cs__number">7</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">2</span>,&nbsp;<span class="cs__number">1</span>]&nbsp;==&nbsp;<span class="cs__number">8</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">2</span>,&nbsp;<span class="cs__number">2</span>]&nbsp;==&nbsp;<span class="cs__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">4</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">2</span>,&nbsp;<span class="cs__number">2</span>]&nbsp;==&nbsp;<span class="cs__number">2</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">3</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">2</span>,&nbsp;<span class="cs__number">2</span>]&nbsp;==&nbsp;<span class="cs__number">3</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">2</span>,&nbsp;<span class="cs__number">2</span>]&nbsp;==&nbsp;<span class="cs__number">4</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">2</span>,&nbsp;<span class="cs__number">2</span>]&nbsp;==&nbsp;<span class="cs__number">5</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">2</span>,&nbsp;<span class="cs__number">2</span>]&nbsp;==&nbsp;<span class="cs__number">6</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">2</span>,&nbsp;<span class="cs__number">2</span>]&nbsp;==&nbsp;<span class="cs__number">7</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(square[<span class="cs__number">2</span>,&nbsp;<span class="cs__number">2</span>]&nbsp;==&nbsp;<span class="cs__number">8</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;md&nbsp;&#43;=&nbsp;<span class="cs__number">3</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;md;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;solve(<span class="cs__keyword">ref</span>&nbsp;<span class="cs__keyword">char</span>[&nbsp;,&nbsp;]&nbsp;solution)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">bool</span>&nbsp;found;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;i,&nbsp;j,&nbsp;k,&nbsp;m&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">do</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(i&nbsp;=&nbsp;k&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;i&nbsp;&lt;&nbsp;<span class="cs__number">3</span>;&nbsp;i&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(j&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;j&nbsp;&lt;&nbsp;<span class="cs__number">3</span>;&nbsp;j&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;solution[m&nbsp;,&nbsp;k&#43;&#43;]&nbsp;=&nbsp;(<span class="cs__keyword">char</span>)(board[i&nbsp;,&nbsp;j]&nbsp;&#43;&nbsp;<span class="cs__string">'0'</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;found&nbsp;=&nbsp;move();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m&#43;&#43;;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;<span class="cs__keyword">while</span>&nbsp;(!found&nbsp;&amp;&amp;&nbsp;m&nbsp;&lt;&nbsp;MaxMoves);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(i&nbsp;=&nbsp;k&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;i&nbsp;&lt;&nbsp;<span class="cs__number">3</span>;&nbsp;i&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(j&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;j&nbsp;&lt;&nbsp;<span class="cs__number">3</span>;&nbsp;j&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;solution[m&nbsp;,&nbsp;k&#43;&#43;]&nbsp;=&nbsp;(<span class="cs__keyword">char</span>)(board[i&nbsp;,&nbsp;j]&nbsp;&#43;&nbsp;<span class="cs__string">'0'</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;m;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em>AStarPuzzle8Form.cs - the main and only form for this application.</em> </li><li><em><em>Program.cs - Visual Studo generated program class that creates the main form.</em></em>
</li><li><em>PuzzleASCS.cs - the 8-puzzle solving class</em> </li></ul>
<h1>More Information</h1>
<p><em>For more information on the A* search and the 8-puzzle, see &quot;Artificial Intelligence A Modern Approach Second Edition&quot; by Stuart Russell and Peter Norvig pages 105-110 or search the Internet using the keywords: A* search and 8-puzzle.<br>
</em></p>