using System;
using System.IO;

class Minesweeper{
    private int size;
    private int bombsPct;
    private int bombsCt;
    private int[,] board;
    private string[,] displayBoard;


    public Minesweeper(int boardSize, string gameDifficulty){
        size = boardSize;
        board = new int[boardSize, boardSize];
        displayBoard = new string[boardSize, boardSize];
        
        if(gameDifficulty=="hard"){
            bombsPct = 20;
        }
        else if(gameDifficulty=="medium"){
            bombsPct = 10;
        }
        else if(gameDifficulty=="easy"){
            bombsPct = 5;
        }
        initBoard();
    }


    private void initBoard(){

        var random = new Random();

        for(int i = 0; i<size; i++){
            for(int j = 0; j<size; j++){
                if(random.Next(100)<bombsPct){
                    board[i, j] = -1;
                    bombsCt += 1;
                }
            }
        }

        for(int i = 0; i<size; i++){
            for(int j = 0; j<size; j++){
                if(board[i, j]!=-1){
                    board[i, j] = countAdjBombs(i, j);
                }
            }
        }

        for(int i = 0; i<size; i++){
            for(int j = 0; j<size; j++){
                displayBoard[i, j] = "\u25A1";
            }
        }

    }


    private int countAdjBombs(int x, int y){
        int adjBombs = 0;

        try{
            if(board[x-1, y-1]==-1){
                adjBombs += 1;
            }
        }catch(Exception e){}
        try{
            if(board[x+1, y-1]==-1){
                adjBombs += 1;
            }
        }catch(Exception e){}
        try{
            if(board[x-1, y+1]==-1){
                adjBombs += 1;
            }
        }catch(Exception e){}
        try{
            if(board[x+1, y+1]==-1){
                adjBombs += 1;
            }
        }catch(Exception e){}
        try{
            if(board[x, y-1]==-1){
                adjBombs += 1;
            }
        }catch(Exception e){}
        try{
            if(board[x-1, y]==-1){
                adjBombs += 1;
            }
        }catch(Exception e){}
        try{
            if(board[x+1, y]==-1){
                adjBombs += 1;
            }
        }catch(Exception e){}
        try{
            if(board[x, y+1]==-1){
                adjBombs += 1;
            }
        }catch(Exception e){}


        return adjBombs;
    }

    
    private void uncoverBlanks(int x, int y){
        if(displayBoard[x, y]==" "){
            return;
        }
        displayBoard[x, y] = " ";

        try{
            if(board[x-1, y]>0){
                displayBoard[x-1, y] = board[x-1, y].ToString();
            }
            if(board[x-1, y]==0){
                uncoverBlanks(x-1, y);
            }
        }catch(Exception e){}
        try{
            if(board[x, y-1]>0){
                displayBoard[x, y-1] = board[x, y-1].ToString();
            }
            if(board[x, y-1]==0){
                uncoverBlanks(x, y-1);
            }
        }catch(Exception e){}
        try{
            if(board[x+1, y]>0){
                displayBoard[x+1, y] = board[x+1, y].ToString();
            }
            if(board[x+1, y]==0){
                uncoverBlanks(x+1, y);
            }
        }catch(Exception e){}
        try{
            if(board[x, y+1]>0){
                displayBoard[x, y+1] = board[x, y+1].ToString();
            }
            if(board[x, y+1]==0){
                uncoverBlanks(x, y+1);
            }
        }catch(Exception e){}

        try{
           if(board[x+1, y+1]>0){
                displayBoard[x+1, y+1] = board[x+1, y+1].ToString();
            } 
        }catch(Exception e){}
        try{
           if(board[x-1, y+1]>0){
                displayBoard[x-1, y+1] = board[x-1, y+1].ToString();
            } 
        }catch(Exception e){}
        try{
           if(board[x+1, y-1]>0){
                displayBoard[x+1, y-1] = board[x+1, y-1].ToString();
            } 
        }catch(Exception e){}
        try{
           if(board[x-1, y-1]>0){
                displayBoard[x-1, y-1] = board[x-1, y-1].ToString();
            } 
        }catch(Exception e){}
    }


    private int countMinedSquares(){
        int mined = size*size;

        for(int i=0; i<size; i++){
            for(int j=0; j<size; j++){
                if(displayBoard[i, j]=="\u25A3" | displayBoard[i, j]=="\u25A1"){
                    mined--;
                }
            }
        }

        return mined;
    }


    private void mineSquare(int x, int y){
        if(board[x, y]<0){
            printExposedBoard();
            Console.WriteLine("You Lose!");
            Environment.Exit(0);
        }
        if(board[x, y]==0){
            uncoverBlanks(x, y);
        }
        if(board[x, y]>0){
            displayBoard[x, y] = board[x, y].ToString();
        }


        int minedSquares = countMinedSquares();
        if(minedSquares==(size*size)-bombsCt){
            printExposedBoard();
            Console.WriteLine("You won!");
            Environment.Exit(0);
        }

    }


    private void markSquare(int x, int y){
        displayBoard[x, y] = "\u25A3";
    }


    private void printExposedBoard(){

        Console.WriteLine();

        for(int i = 0; i<size; i++){
            for(int j = 0; j<size; j++){
                if(board[i, j]<0){
                    Console.Write("*");
                }
                if(board[i, j]==0){
                    Console.Write(" ");
                }
                if(board[i, j]>0){
                    Console.Write(board[i, j].ToString());
                }
                Console.Write(" ");
            }
            Console.WriteLine();
        }
        Console.WriteLine("\n");
    }


    private void printBoard(){
        Console.WriteLine();

        Console.Write("    ");
        for(int i = 0; i<size; i++){
            if(i>=10){
                Console.Write((i-(i%10)).ToString()[0]);
            }
            else{
                Console.Write(" ");
            }
            Console.Write(" ");
        }
        Console.WriteLine();
        Console.Write("    ");
        for(int i = 0; i<size; i++){
            Console.Write(i%10 + " ");
        }
        Console.WriteLine("\n");

        for(int i = 0; i<size; i++){

            Console.Write(i);
            if(i>=10){
                Console.Write("  ");
            }
            else{
                Console.Write("   ");
            }

            for(int j = 0; j<size; j++){
                Console.Write(displayBoard[i, j] + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }



    public void gameLoop(){
        while(true){
            printBoard();
            string input = Console.ReadLine();
            string[] cmd = input.Split(" ");
            if(cmd[0]=="mine"){
                mineSquare(int.Parse(cmd[1]), int.Parse(cmd[2]));
            }
            if(cmd[0]=="mark"){
                markSquare(int.Parse(cmd[1]), int.Parse(cmd[2]));
            }
        }
    }


    static void Main(String[] args){
        var MS = new Minesweeper(int.Parse(args[0]), args[1]);
        MS.gameLoop();
    }
}
