using System;
using System.Collections.Generic;
using EmergencyCenter.Units;

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
            return  null;
        }

        /// <summary>
        /// Call update method of all units
        /// </summary>
        public IEnumerable<Report> UpdateUnits()
        {
            return null;
        }
    }
}
