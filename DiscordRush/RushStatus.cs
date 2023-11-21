using Reptile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordRush
{
    public class RushStatus
    {
        public string details;
        public string state;

        public int currentPartySize;
        public int maxPartySize;

        public long elapsedTime;

        public long startTime;
        public long endTime;

        public string smallImage;
        public string smallText;

        public string largeImage;
        public string largeText;

        public bool isSlopNetworked;
        public Stage currentStage;
    }
}
