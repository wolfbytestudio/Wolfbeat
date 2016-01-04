using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Floating_Orbs.com.orb
{

    /// <summary>
    /// This class contains a lot of methods for
    /// handling, removing, adding orbs etc.
    /// 
    /// Running the timer will handle orbs on a seperate thread. All 
    /// orbs are handled on this thread
    /// 
    /// behind an orb.
    /// Author - Zack Davidson -
    ///         <Zackdavidson2014@outlook.com>
    ///         <wolfbytestudio@gmail.com>
    /// </summary>
    public class OrbHandler
    {

        /// <summary>
        /// Contains a list of all the orbs
        /// </summary>
        private List<Orb> orbs;

        /// <summary>
        /// Then grid to place orbs
        /// </summary>
        private Grid grid;

        /// <summary>
        /// The maximum amount of orbs that can be placed
        /// </summary>
        private byte size;

        /// <summary>
        /// Timer to start the orbs
        /// </summary>
        private Timer timer;

        /// <summary>
        /// Random object to randomise each orb
        /// </summary>
        private Random random;

        /// <summary>
        /// Checks if the timer has been stopped
        /// </summary>
        private bool stopped;

        /// <summary>
        /// Constructor for orbs
        /// </summary>
        public OrbHandler(Grid grid, byte size)
        {
            this.grid = grid;
            this.size = size;
            random = new Random();
            orbs = new List<Orb>();
        }

        /// <summary>
        /// Starts the orb spawning
        /// </summary>
        public void start()
        {
            stopped = false;
            timer = new Timer(tick, null, 0, 1);
        }

        /// <summary>
        /// Stops the timer
        /// </summary>
        public void stop()
        {
            stopped = true;
            timer = null;
        }

        /// <summary>
        /// Executes every millesecond
        /// This is run on a sepereate thread from the main UI
        /// </summary>
        /// <param name="state"></param>
        private void tick(object state)
        {
            if (stopped)
            {
                return;
            }
                
            grid.Parent.Dispatcher.Invoke(() =>
            {
                if (amount() <= size)
                {
                    spawnRandomOrb();
                }
                updateOrbs();
            });
        }

        /// <summary>
        /// Submits a new orb
        /// </summary>
        /// <param name="orb">The orb to add</param>
        public void submit(Orb orb)
        {
            orbs.Add(orb);
        }

        /// <summary>
        /// Removes an orb from the list
        /// </summary>
        /// <param name="index">The index to remove</param>
        public void remove(int index)
        {
            for(int i = 0; i < orbs.Count; i++)
            {
                if(orbs[i].index == index)
                {
                    orbs[i].destroy();
                    orbs.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Destroys all current orbs
        /// </summary>
        public void clear()
        {
            int amount = orbs.Count();
            Orb.count = 0;
            for (int i = 0; i < amount; i++)
            {
                orbs[i].destroy();
                orbs[i] = null;
            }

            orbs.Clear();

        }

        /// <summary>
        /// Gets the total amount of orbs.
        /// </summary>
        /// <returns>the total amount of orbs</returns>
        public int amount()
        {
            return orbs.Count;
        }

        /// <summary>
        /// Updates all orbs
        /// </summary>
        public void updateOrbs()
        {
            foreach(Orb o in orbs)
            {
                o.update();
            }
        }
        
        /// <summary>
        /// Gets a random double between a minimum value and maximum
        /// </summary>
        /// <param name="minimum">the lowest the double can be</param>
        /// <param name="maximum">the highest the double can be</param>
        /// <returns>The new random double</returns>
        private double getRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        /// <summary>
        /// Spawns a random orb
        /// </summary>
        private void spawnRandomOrb()
        {
            double posX = random.Next((int) grid.ActualWidth);
            double posY = 100 + grid.ActualHeight - random.Next((int)grid.ActualHeight / 2);
            double newPosX = random.Next((int)grid.ActualWidth);
            double newPosY = 20 - random.Next(100);
            double lifetime = getRandomNumber(5, 15);
            double speed = getRandomNumber(1, 20);
            double size = 1 + random.Next(5);

            submit(new Orb(grid, this,
                posX, posY, newPosX, newPosY, size, lifetime));

        }

    }
}
