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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Threading;
using System.Runtime.Remoting.Messaging;

namespace pr13.Pages
{
    /// <summary>
    /// Логика взаимодействия для Mainpage.xaml
    /// </summary>
    public partial class Mainpage : Page
    {

        public Mainpage()
        {
            InitializeComponent();

            DoubleAnimation WA = new DoubleAnimation(); // создание объекта для настройки анимации
            WA.From = 250; // начальное значение свойства
            WA.To = 400; // конечное значение свойства
            WA.Duration = TimeSpan.FromSeconds(2); // продолжительность анимации (в секундах)
            WA.RepeatBehavior = RepeatBehavior.Forever; // бесконечность анимации
            WA.AutoReverse = true; // воспроизведение временной шкалы в обратном порядке
            key.BeginAnimation(WidthProperty, WA); // «навешивание» анимации на свойство ширины кнопки


            DoubleAnimation bt = new DoubleAnimation(); // создание объекта для настройки анимации
            bt.From = 80; // начальное значение свойства
            bt.To = 200; // конечное значение свойства
            bt.Duration = TimeSpan.FromSeconds(3); // продолжительность анимации (в секундах)
            bt.RepeatBehavior = RepeatBehavior.Forever; // бесконечность анимации
            bt.AutoReverse = true; // воспроизведение временной шкалы в обратном порядке
            btn1.BeginAnimation(WidthProperty, bt); // «навешивание» анимации на свойство ширины кнопки


            DoubleAnimation FSAB = new DoubleAnimation(); // Цвет
            ColorAnimation BAB = new ColorAnimation(); 
            ColorConverter CC = new ColorConverter();
            Color Cstart = (Color)CC.ConvertFrom("green");
            btn1.Background = new SolidColorBrush(Cstart);
            BAB.From = Cstart;
            BAB.To = (Color)CC.ConvertFrom("#6fe01e");
            BAB.Duration = TimeSpan.FromSeconds(2);
            BAB.RepeatBehavior = RepeatBehavior.Forever;
            FSAB.AutoReverse = true;
            btn1.Background.BeginAnimation(SolidColorBrush.ColorProperty, BAB);


            DoubleAnimation FSATB = new DoubleAnimation(); // текст
            FSATB.From = 22;
            FSATB.To = 30;
            FSATB.Duration = TimeSpan.FromSeconds(1);
            FSATB.RepeatBehavior = RepeatBehavior.Forever;
            FSATB.AutoReverse = true;
            text.BeginAnimation(FontSizeProperty, FSATB);



            
            var switchOffAnimation = new DoubleAnimation
            {
                To = 0,
                Duration = TimeSpan.Zero
            };

            var switchOnAnimation = new DoubleAnimation
            {
                To = 1,
                Duration = TimeSpan.Zero,
                BeginTime = TimeSpan.FromSeconds(0.5)
            };

            var blinkStoryboard = new Storyboard
            {
                Duration = TimeSpan.FromSeconds(1),
                RepeatBehavior = RepeatBehavior.Forever
            };

            Storyboard.SetTarget(switchOffAnimation, logo);
            Storyboard.SetTargetProperty(switchOffAnimation, new PropertyPath(Canvas.OpacityProperty));
            blinkStoryboard.Children.Add(switchOffAnimation);

            Storyboard.SetTarget(switchOnAnimation, logo);
            Storyboard.SetTargetProperty(switchOnAnimation, new PropertyPath(Canvas.OpacityProperty));
            blinkStoryboard.Children.Add(switchOnAnimation);

            logo.BeginStoryboard(blinkStoryboard);

            
            var thread = new Thread(this.AnimateBalloon);
            thread.Start();

        }

        public void AnimateBalloon()
        {
            var rnd = new Random();
            var direction = -1;

            while (true)
            {
                var percent = rnd.Next(0, 100);
                direction *= -1; // Direction changes every iteration
                var rotation = (int)((percent / 100d) * 10 * direction); // Max 45 degree rotation
                var duration = (int)(750 * (percent / 100d)); // Max 750ms rotation

                advertisement.Dispatcher.BeginInvoke(
                    (Action)(() =>
                    {
                        var da = new DoubleAnimation
                        {
                            To = rotation,
                            Duration = new Duration(TimeSpan.FromMilliseconds(duration)),
                            AutoReverse = true 
                        };

                        var rt = new RotateTransform();
                        advertisement.RenderTransform = rt; 
                        rt.BeginAnimation(RotateTransform.AngleProperty, da);
                    }));

                Thread.Sleep(duration * 2);
            }

        }
    }

}
