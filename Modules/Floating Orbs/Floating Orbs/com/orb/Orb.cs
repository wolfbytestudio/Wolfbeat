using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Floating_Orbs.com.orb
{
    /// <summary>
    /// This class contains the blueprints and logics
    /// behind an orb.
    /// Author - Zack Davidson -
    ///         <Zackdavidson2014@outlook.com>
    ///         <wolfbytestudio@gmail.com>
    /// </summary>
    public class Orb
    {

        /// <summary>
        /// The amount of active orbs
        /// used to give a new orb an index
        /// </summary>
        public static int count = 0;

        /// <summary>
        /// The orbs index
        /// </summary>
        public int index;

        /// <summary>
        /// The orb as a rectangle
        /// </summary>
        private Rectangle orb;

        /// <summary>
        /// The grid which will contain the orb
        /// </summary>
        private Grid owner;

        /// <summary>
        /// The orbs handler
        /// </summary>
        private OrbHandler handler;

        /// <summary>
        /// The orbs X starting position
        /// </summary>
        private double startPosX;

        /// <summary>
        /// The orbs Y starting position
        /// </summary>
        private double startPosY;

        /// <summary>
        /// The orbs target X destination
        /// </summary>
        private double endPointX;

        /// <summary>
        /// The orbs target Y destination
        /// </summary>
        private double endPointY;

        /// <summary>
        /// The final size of the orb
        /// </summary>
        private double size;

        /// <summary>
        /// The orbs current X position
        /// </summary>
        private double currentPositionX;

        /// <summary>
        /// The orbs current Y position
        /// </summary>
        private double currentPositionY;

        /// <summary>
        /// The life time of the orb
        /// will be decreased
        /// </summary>
        private double lifetime;

        /// <summary>
        /// The duration the orb will be active
        /// </summary>
        private double startLifetime;

        /// <summary>
        /// Checks if the orb is being 'born'
        /// Starts small and increases in size and opacity over
        /// a duration of time
        /// </summary>
        private bool birth = true;

        /// <summary>
        /// Checks if the rob should start being destroyed
        /// If the orb starts getting destroyed it should fade out and shrink
        /// </summary>
        private bool startDestroy = false;

        /// <summary>
        /// Constructor for Orb
        /// </summary>
        /// <param name="owner">The grid owner</param>
        /// <param name="handler">The orb's handler</param>
        /// <param name="startPosX">The start x position</param>
        /// <param name="startPosY">The start y position</param>
        /// <param name="endPointX">The target x destination</param>
        /// <param name="endPointY">The target y destination</param>
        /// <param name="size">The size of the orb</param>
        /// <param name="lifetime">The durartion that the orb will be active</param>
        public Orb(
            Grid owner, OrbHandler handler, double startPosX,
            double startPosY, double endPointX, double endPointY,
            double size, double lifetime)
        {
            index = ++count;
            this.startPosX = startPosX;
            this.startPosY = startPosY;
            this.endPointX = endPointX;
            this.endPointY = endPointY;
            this.size = size;
            startLifetime = lifetime * 1000;
            this.lifetime = lifetime * 1000;
            this.owner = owner;
            this.handler = handler;

            orb = new Rectangle();
            orb.Width = size / 4;
            orb.Height = size / 4;
            orb.Fill = new SolidColorBrush(Color.FromRgb(250, 250, 250));
            orb.RadiusX = size;
            orb.RadiusY = size;
            orb.Opacity = 0.1;
            orb.HorizontalAlignment = HorizontalAlignment.Left;
            orb.VerticalAlignment = VerticalAlignment.Top;
            orb.Margin = new Thickness(startPosX, startPosY, 0, 0);
            owner.Children.Add(orb);
        }

        /// <summary>
        /// Updates the orb
        /// </summary>
        public void update()
        {
            int orbCounter = 0;
            owner.Dispatcher.BeginInvoke(new Action(() =>
                {
                    
                    if(birth)
                    {
                        orb.Width += size / 40;
                        orb.Height += size / 40;
                        orb.Opacity += 0.019;
                        if (orb.Width >= size)
                        {
                            birth = false;
                        }
                    }
                    if(startDestroy)
                    {
                        orbCounter++;
                        if(orbCounter >= 10)
                        {
                            orbCounter = 0;
                            if (orb.Width - (size / 9) <= 0)
                            {
                                orb.Width = 0;
                                orb.Height = 0;
                                remove(index);
                                destroy();
                                return;
                            }

                            orb.Width -= size / 9;
                            orb.Height -= size / 9;
                            orb.Margin = new Thickness(
                                currentPositionX + size / 9,
                                currentPositionY + size / 9, 0, 0);

                            
                        }
                        if(orb.Opacity == 0)
                        {
                            orb.Width = 0;
                            orb.Height = 0;
                            remove(index);
                            destroy();
                            return;
                        }
                        orb.Opacity -= 0.009;

                    }

                    double newPosX;
                    double newPosY;
                    currentPositionX = orb.Margin.Left;
                    currentPositionY = orb.Margin.Top;

                    newPosX = currentPositionX + (endPointX - currentPositionX) * (1 / (startLifetime / 20));
                    newPosY = currentPositionY + (endPointY - currentPositionY) * (1 / (startLifetime / 20));



                    lifetime -= 10;
                    if (lifetime <= 1000)
                    {
                        startDestroy = true;
                    }

                    if(lifetime <= 0)
                    {
                        orb.Width = 0;
                        orb.Height = 0;
                        remove(index);
                        destroy();
                        return;
                    }

                    orb.Margin = new Thickness(newPosX, newPosY, 0, 0);

                }));
        }

        /// <summary>
        /// Removes the orb from the orb list
        /// </summary>
        /// <param name="index"></param>
        public void remove(int index)
        {
            handler.remove(index);
        }

        /// <summary>
        /// Destroys the orb and removes it from the grid
        /// </summary>
        public void destroy()
        {
            owner.Dispatcher.BeginInvoke(new Action(() =>
            {
                owner.Children.Remove(orb);
                remove(index);
            }));
       }


    }//End of class
}
