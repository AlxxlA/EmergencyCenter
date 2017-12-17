using System;

namespace EmergencyCenter.Core.Engine
{
    public class CommandCenter
    {
        /// <summary>
        /// Execute given command
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public Report ExecuteCommand(Command command)
        {
            return new Report();
        }

        /// <summary>
        /// Call update method of all units
        /// </summary>
        public void UpdateUnits()
        {

        }
    }
}
