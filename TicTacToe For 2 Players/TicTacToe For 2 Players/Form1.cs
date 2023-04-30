using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe_For_2_Players
{
    public partial class Form1 : Form
    {

        bool turn = true; //when is true => X turn, when is false => O turn
        bool againstComputer = false;
        int turnCount = 0;

        //static String player1, player2;
        public Form1()
        {
            InitializeComponent();
        }

        //public static void setPlayersNames(String n1, String n2)
        //{
        //    player1 = n1;
        //    player2 = n2;
        //}

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("By Martin", "Tic Tac Toe About");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonClick(object sender, EventArgs e)
        {

            if ((p1.Text == "Player 1") || (p2.Text == "Player 2"))
            {
                MessageBox.Show("You must specify the players' names before you can start!");
            }
            else
            {
                Button b = (Button)sender;
                if (turn)
                {
                    b.Text = "X";
                    b.BackColor = Color.Cyan;
                }
                else
                {
                    b.Text = "O";
                    b.BackColor = Color.DarkSalmon;
                }

                turn = !turn;
                b.Enabled = false;
                turnCount++;

                //label2.Focus();
                CheckForWinner();
            }

            //check to see if playing against computer and if it's O's turn
            if ((!turn) && (againstComputer))
            {
                ComputerMakeMove();
            }
            
        }

        private void ComputerMakeMove()
        {
            //priority 1: get tic tac toe
            //priority 2: block x 
            //priority 3: go for corner space
            //priority 4: pick open space

            Button move = null;

            //look for tic tac toe opportunities
            move = LookForWinOrBlock("O"); //look for win

            if (move == null) 
            {
                move = LookForWinOrBlock("X"); //look for block
                if (move == null)
                {
                    move = LookForCorner();
                    if (move == null)
                    {
                        move = LookForOpenSpace();
                    } //end if
                }//end if
            } //end if

            if (move != null)
            {
                move.PerformClick();
            }
            
        }

        private Button LookForOpenSpace()
        {
            Console.WriteLine("Looking for open space");
            Button b = null;
            foreach (Control c in Controls)
            {
                b = c as Button;
                if (b != null)
                {
                    if (b.Text == "")
                        return b;
                }//end if
            }//end if

            return null;
        }

        private Button LookForCorner()
        {
            Console.WriteLine("Looking for corner");
            if (button1.Text == "O")
            {
                if (button3.Text == "")
                    return button3;
                if (button9.Text == "")
                    return button9;
                if (button7.Text == "")
                    return button7;
            }

            if (button3.Text == "O")
            {
                if (button1.Text == "")
                    return button1;
                if (button9.Text == "")
                    return button9;
                if (button7.Text == "")
                    return button7;
            }

            if (button9.Text == "O")
            {
                if (button1.Text == "")
                    return button3;
                if (button3.Text == "")
                    return button3;
                if (button7.Text == "")
                    return button7;
            }

            if (button7.Text == "O")
            {
                if (button1.Text == "")
                    return button3;
                if (button3.Text == "")
                    return button3;
                if (button9.Text == "")
                    return button9;
            }

            if (button1.Text == "")
                return button1;
            if (button3.Text == "")
                return button3;
            if (button7.Text == "")
                return button7;
            if (button9.Text == "")
                return button9;

            return null;
        }

        private Button LookForWinOrBlock(string mark)
        {
            Console.WriteLine("Looking for win or block:  " + mark);
            //HORIZONTAL TESTS
            if ((button1.Text == mark) && (button2.Text == mark) && (button3.Text == ""))
                return button3;
            if ((button2.Text == mark) && (button3.Text == mark) && (button1.Text == ""))
                return button1;
            if ((button1.Text == mark) && (button3.Text == mark) && (button2.Text == ""))
                return button2;

            if ((button4.Text == mark) && (button5.Text == mark) && (button6.Text == ""))
                return button6;
            if ((button5.Text == mark) && (button6.Text == mark) && (button4.Text == ""))
                return button4;
            if ((button4.Text == mark) && (button6.Text == mark) && (button5.Text == ""))
                return button5;

            if ((button7.Text == mark) && (button8.Text == mark) && (button9.Text == ""))
                return button9;
            if ((button8.Text == mark) && (button9.Text == mark) && (button7.Text == ""))
                return button7;
            if ((button7.Text == mark) && (button9.Text == mark) && (button8.Text == ""))
                return button8;

            //VERTICAL TESTS
            if ((button1.Text == mark) && (button4.Text == mark) && (button7.Text == ""))
                return button7;
            if ((button4.Text == mark) && (button7.Text == mark) && (button1.Text == ""))
                return button1;
            if ((button1.Text == mark) && (button7.Text == mark) && (button4.Text == ""))
                return button4;

            if ((button2.Text == mark) && (button5.Text == mark) && (button8.Text == ""))
                return button8;
            if ((button5.Text == mark) && (button8.Text == mark) && (button2.Text == ""))
                return button2;
            if ((button2.Text == mark) && (button8.Text == mark) && (button5.Text == ""))
                return button5;

            if ((button3.Text == mark) && (button6.Text == mark) && (button9.Text == ""))
                return button9;
            if ((button6.Text == mark) && (button9.Text == mark) && (button3.Text == ""))
                return button3;
            if ((button3.Text == mark) && (button9.Text == mark) && (button6.Text == ""))
                return button6;

            //DIAGONAL TESTS
            if ((button1.Text == mark) && (button5.Text == mark) && (button9.Text == ""))
                return button9;
            if ((button5.Text == mark) && (button9.Text == mark) && (button1.Text == ""))
                return button1;
            if ((button1.Text == mark) && (button9.Text == mark) && (button5.Text == ""))
                return button5;

            if ((button3.Text == mark) && (button5.Text == mark) && (button7.Text == ""))
                return button7;
            if ((button5.Text == mark) && (button7.Text == mark) && (button3.Text == ""))
                return button3;
            if ((button3.Text == mark) && (button7.Text == mark) && (button5.Text == ""))
                return button5;

            return null;
        }

        private void CheckForWinner()
        {

            bool winner = false;

            // horizontal checks
            if ((button1.Text == button2.Text) && (button2.Text == button3.Text) && (!button1.Enabled))
            {
                winner = true;
            }
            if ((button4.Text == button5.Text) && (button5.Text == button6.Text) && (!button4.Enabled))
            {
                winner = true;
            }
            else if ((button7.Text == button8.Text) && (button8.Text == button9.Text) && (!button7.Enabled))
            {
                winner = true;
            }

            // vertical checks
            else if ((button1.Text == button4.Text) && (button4.Text == button7.Text) && (!button1.Enabled))
            {
                winner = true;
            }
            else if ((button2.Text == button5.Text) && (button5.Text == button8.Text) && (!button2.Enabled))
            {
                winner = true;
            }
            else if ((button3.Text == button6.Text) && (button6.Text == button9.Text) && (!button3.Enabled))
            {
                winner = true;
            }

            // diagonal checks
            else if ((button1.Text == button5.Text) && (button5.Text == button9.Text) && (!button1.Enabled))
            {
                winner = true;
            }
            else if ((button3.Text == button5.Text) && (button5.Text == button7.Text) && (!button7.Enabled))
            {
                winner = true;
            }

            if (winner)
            {
                string player = "";

                if (turn)
                {
                    player = p2.Text;
                    OWinCount.Text = (int.Parse(OWinCount.Text) + 1).ToString();
                }
                else
                {
                    player = p1.Text;
                    XWinCount.Text = (int.Parse(XWinCount.Text) + 1).ToString();
                }
                MessageBox.Show("Winner is " + player, "Yay!");
                winner = false;
                turnCount = 0;
                disableButtons();
                RestartGame();
            }
            else
            {
                if (turnCount == 9)
                {
                    DrawCount.Text= (int.Parse(DrawCount.Text) + 1).ToString();
                    MessageBox.Show("Draw!", "Bummer!");
                    turnCount = 0;
                    disableButtons();
                    RestartGame();
                }
            }
            
        } //end CheckForWinner

        private void disableButtons()
        {
                //foreach (Control c in Controls)
                //{
                //    c.Enabled = false;
                //}

            button1.Enabled= false;
            button2.Enabled= false;
            button3.Enabled= false;
            button4.Enabled= false;
            button5.Enabled= false;
            button6.Enabled= false;
            button7.Enabled= false;
            button8.Enabled= false;
            button9.Enabled= false;
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            turn = true;
            turnCount = 0;

                foreach (Control c in Controls)
                {
                    try
                    {
                        Button b = (Button)c;
                        b.Enabled = true;
                        b.BackColor= Color.White;
                        b.Text = "";
                    } // end try
                    catch { }
                } //end foreach 
        }

        private void resetWinCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XWinCount.Text = "";
            OWinCount.Text = "";
            DrawCount.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Form2 f2 = new Form2();
            //f2.ShowDialog();
            //p1.Text = p1.Text + ":";
            //p2.Text = p2.Text + ":";
        }

        private void p2_TextChanged(object sender, EventArgs e)
        {
            if (p2.Text.ToUpper() == "COMPUTER")
            {
                againstComputer = true;
            }
            else
            {
                againstComputer = false;
            }
        }

        private void Restart_Click(object sender, EventArgs e)
        {
            RestartGame();
        }

        private void RestartGame()
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;
            button1.Text = "";
            button2.Text = "";
            button3.Text = "";
            button4.Text = "";
            button5.Text = "";
            button6.Text = "";
            button7.Text = "";
            button8.Text = "";
            button9.Text = "";
            button1.BackColor = DefaultBackColor;
            button2.BackColor = DefaultBackColor;
            button3.BackColor = DefaultBackColor;
            button4.BackColor = DefaultBackColor;
            button5.BackColor = DefaultBackColor;
            button6.BackColor = DefaultBackColor;
            button7.BackColor = DefaultBackColor;
            button8.BackColor = DefaultBackColor;
            button9.BackColor = DefaultBackColor;
        }
    }
}
