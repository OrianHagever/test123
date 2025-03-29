using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace miw
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Button btn1, btn2;
        int pairsRevealed; int pairsUnrevealed; int moves = 0;
        int count =0 ;
        List<Button> buttons = new List<Button>();
        List<String> imageNames_All = new List<String>()
        {"image1.jpg","image2.jpg","image3.jpg","image4.jpg","image5.jpg"};
        Random rnd = new Random();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Build()
        {
            if (this.myGrid == null)
            {
                return;
            }
            this.myGrid.Children.Clear();
            buttons.Clear();
            this.myGrid.Children.Add(buildGrid);

            pairsUnrevealed = this.count;
            moves = 0; pairsRevealed = 0;
            btn1 = null; btn2 = null;
            for (int i = 0; i < this.count * 2; i++)
            {
                Grid grid = new Grid();
                grid.Margin = new Thickness(10);

                //var uri = new Uri("pack://application:,,,/Images/" + "Images/embape.jpg");
                //var bitmap = new BitmapImage(uri);
                //grid.Background = new ImageBrush(bitmap);

                Button btn = new Button();
                btn.Click += BtnClick;

                grid.Children.Add(btn);
                this.myGrid.Children.Add(grid);

                buttons.Add(btn);

            }
            List<String> imageNames = new List<String>(imageNames_All);
            int imgCount = 0;
            while (imageNames.Count > 0 && imgCount<this.count) 
            { 
                int img = rnd.Next(imageNames.Count);
                var uri = new Uri("pack://application:,,,/images/" + imageNames[img]);
                var bitmap = new BitmapImage(uri);
                for (int i = 0; i < 2; i++)
                {
                    int inx = findFreeBtn();
                    // images[inx].Source = bitmap;
                    Grid grid = buttons[inx].Parent as Grid;
                    grid.Background = new ImageBrush(bitmap);
                    buttons[inx].Tag = imageNames[img];
                }
                imgCount++;
                imageNames.RemoveAt(img);

            }
            //foreach (UIElement ctrl in this.myGrid.Children)
            //{
            //    if (ctrl is Button) this.buttons.Add(ctrl as Button);

            //    if (ctrl is Image) this.images.Add(ctrl as Image);
            //}

            //while (imageNames.Count > 0)
            //{
            //    int img = rnd.Next(imageNames.Count);
            //    var uri = new Uri("pack://application:,,,/Images/" + imageNames[img]);
            //    var bitmap = new BitmapImage(uri);
            //    for (int i = 0; i < 2; i++)
            //    {
            //        int inx = findFreeBtn();
            //        images[inx].Source = bitmap;
            //        buttons[inx].Tag = imageNames[img];

            //    }
            //    imageNames.RemoveAt(img);
            //}
        }
        
        private int findFreeBtn()
        {
            int inx = -1;
            int x;
            while (inx < 0)
            {
                x = rnd.Next(buttons.Count);
                if (buttons[x].Tag == null)
                {
                    inx = x;
                }
            }
            return inx;
        }

        private void BuildBtn_Click(object sender, RoutedEventArgs e)
        {
            this.count = int.Parse(this.sizeTxt.Text);
            if(this.count>5 || this.count < 2)
            {
                MessageBox.Show("please pick another number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                this.Build();
            }
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            moves++; // new move

            btn.Visibility = Visibility.Collapsed; // show picture
            if (btn1 == null) //if first move in game
            {
                btn1 = btn;
            }
            else
            {
                if(btn2 == null)//if second card
                {
                    btn2 = btn;
                    if(pairsUnrevealed == pairsRevealed + 1)//check end
                    {
                        MessageBox.Show($"good job, you won in {moves} moves");
                    }
                }
                else
                {
                    if (btn1.Tag.Equals(btn2.Tag))
                    {
                        pairsRevealed++;
                    }
                    else//hide 2 cards
                    {
                        btn1.Visibility = Visibility.Visible;
                        btn2.Visibility = Visibility.Visible;
                    }
                    btn1 = btn;
                    btn2 = null;
                }
            }
        }
    }
}
