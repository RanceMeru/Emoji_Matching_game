using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Headfirst_part_1
{
   
    using System.Windows.Threading;

    public partial class MainWindow : Window
    {
    // this will be creating a timer object that we can use later along with our matches in our matching game.
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondElapsed;
        int matchesFound;
        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {//the timer will stop when we have found 8 matches which is all the possible matches acailable.
            tenthsOfSecondElapsed++;
            timeTextBlock.Text = (tenthsOfSecondElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";

            }


           // throw new NotImplementedException();
        }

        private void SetUpGame()
        {
           List<string> animalEmoji = new List<string>()
            {
                "🐙","🐙",
                "🐡","🐡",
                "🐘","🐘",
                "🐳","🐳",
                "🐪","🐪",
                "🦕","🦕",
                "🦘","🦘",
                "🦔","🦔",
            };
            //will make a random configuration

            Random random = new Random();
            {
                foreach (TextBlock textblock in mainGrid.Children.OfType<TextBlock>())
                   
                {
                    if (textblock.Name != "timeTextBlock")

                    {
                        textblock.Visibility = Visibility.Visible;
                        int index = random.Next(animalEmoji.Count);
                        string nextEmoji = animalEmoji[index];
                        textblock.Text = nextEmoji;
                        animalEmoji.RemoveAt(index);
                    }
                }
            }
            //will start the timer only when we begin 
            timer.Start();
            tenthsOfSecondElapsed = 0;
            matchesFound = 0;
        }
        TextBlock lastTextBLockClicked;
        bool findingMatch = false;
        private void TextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBLockClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextBLockClicked.Text)
            {
                matchesFound++;    //increases the matches found by one everytime the player finds a match
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextBLockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void timeTextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (matchesFound == 8)    //this if will say that when all 8 matches are found 
            {
                SetUpGame();     // it will reset the game
            }


                
        }
    }
}
